/**
 * @file   ObjManager.cs
 * @brief  オブジェクト管理クラス
 * @author IharaShota
 * @date   2023/10/13
 * @Update 2023/10/13 作成
 * @Update 2023/10/19 当たり判定取得完了
 * @Update 2023/10/24 攻撃判定取得完了(仮)
 * @Update 2023/10/26 地面判定終了
 **/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class ObjManager : MonoBehaviour
{
    [SerializeField] private List<ObjBase> Objs; // オブジェクトを格納する配列
    [SerializeField] private CinemachineImpulseSource cinema;
    public static ObjManager instance;
    public ParticleSystem hitEffect;
    public ObjEnemyUnion enemyUnionPrefab;

    private int myID;    // 自身のオブジェクトID
    private int otherID; // 相手のオブジェクトID
    private int maxID;   // 現在割り振られているオブジェクトIDの最大数

    private void Start()
    {
        // --- 初期化 ---
        if(instance == null)
        {
            instance = this;
        }
        // 当たり判定管理クラスがnullだったら…
        if(ON_HitManager.instance == null)
        {
            // インスタンスを作成して初期化する
            ON_HitManager.instance = new ON_HitManager();
            ON_HitManager.instance.Init();
        }

        // それぞれのIDを初期化
        myID = otherID = maxID = 0;

        // 格納されているオブジェクトの数だけ回す
        for (int i = 0; i < Objs.Count; ++i)
        {
            // オブジェクトIDを設定 → 当たり判定生成
            // → オブジェクトの初期化 → オブジェクトの参照パラメーターを更新
            Objs[i].GetSetObjID = SetObjID();
            Objs[i].GenerateHit();
            Objs[i].InitObj();
            Objs[i].UpdateCheckParam();
        }

    }

    private void FixedUpdate()
    {
        if (GameManager.m_sGameState != GameState.GamePlay || 
            HitStop.instance.hitStopState == HitStopState.ON)
            return;

        // --- オブジェクトの更新処理 ---
        for(int i = 0; i < Objs.Count; ++i)
        {
            // --- オブジェクトが存在している場合 ---
            if (Objs[i].GetSetExist)
            {
                // オブジェクトの表示
                if (Objs[i].texObj != null)
                {
                    if(Objs[i].GetComponent<ObjEnemyBase>() != null)
                    {
                        if(!Objs[i].GetComponent<ObjEnemyBase>().m_division.m_bDivisionTrigger)
                            Objs[i].texObj.enabled = true;
                    }
                    else Objs[i].texObj.enabled = true;
                }

                // 更新処理  → 地面判定 → 向き調整 → 移動量に速度を格納 → 速度調整 →
                // 無敵時間更新 → 座標更新
                Objs[i].UpdateObj();
                Objs[i].CheckObjGround();
                SaveObjSpeed(i);
                FixObjDir(i);
                UpdateInvincible(i);
                Objs[i].GetSetMove = Objs[i].GetSetSpeed;
                Objs[i].GetSetPos += new Vector3(Objs[i].GetSetMove.x, Objs[i].GetSetMove.y, 0f);

                // ヒットストップ更新
                Objs[i].UpdateHitStop();

                if (!Objs[i].m_HitStopParam.m_bHitStop)
                {
                    // オブジェクトが移動した座標に当たり判定を移動させる
                    ON_HitManager.instance.SetCenter(Objs[i].GetSetHitID, Objs[i].GetSetPos);
                }
            }
            // --- 存在していない場合 ---
            else
            {
                // オブジェクトの非表示
                if (Objs[i].texObj != null)
                    Objs[i].texObj.enabled = false;
            }         

            // オブジェクトのパラメーター更新
            Objs[i].UpdateCheckParam();

            // ヒットを初期化
            Objs[i].GetSetHit = false;
        }

        // --- 当たり判定処理 ---
        CollisionUpdate();

        // --- オブジェクトの削除 ---
        // ※m_bDestroyがtrueになっている場合
        // 最後尾から調べないとバグる
        int ObjMaxNum = Objs.Count - 1;
        for(int i = ObjMaxNum; i > 0; --i)
        {
            if(Objs[i].GetSetDestroy)
            {
                Objs[i].DestroyObj();
                Objs.RemoveAt(i);
            }
        }

        // --- オブジェクトの攻撃判定処理 ---
        for (int i = 0; i < Objs.Count; ++i)
        {
            // --- オブジェクトがヒットしている場合 ---
            if (Objs[i].GetSetHit)
            {
                // 無敵時間を設定
                Objs[i].GetSetInvincible.SetInvincible(1f);

                // ヒットストップ発生
                Objs[i].m_HitStopParam.SetHitStop(0.4f);

                // 敵だった場合、ノックバックのカウントダウンを始める
                if(Objs[i].GetComponent<ObjEnemyBase>() != null)
                {
                    Objs[i].GetComponent<ObjEnemyBase>().GetSetKnockBack.m_fStartTime = 0.5f;
                }
            }

            // ノックバック発生管理(余りに汚いので修正必須)
            if (Objs[i].GetComponent<ObjEnemyBase>() != null)
            {
                if (Objs[i].GetComponent<ObjEnemyBase>().GetSetKnockBack.m_fStartTime > 0)
                {
                    Objs[i].GetComponent<ObjEnemyBase>().GetSetKnockBack.m_fStartTime -= Time.deltaTime;
                    if (Objs[i].GetComponent<ObjEnemyBase>().GetSetKnockBack.m_fStartTime <= 0)
                    {
                        Objs[i].KnockBackObj(Objs[0].GetSetDir);
                        Objs[i].GetComponent<ObjEnemyBase>().GetSetKnockBack.m_fStartTime = 0;
                    }
                }
            }
        }
    }

    // --- 速度調整処理 ---
    // ※最大・最小速度を超えていたら、最大・最小速度に調整する
    private void SaveObjSpeed(int i)
    {
        if (Objs[i].GetSetSpeed.x > Objs[i].GetSetMaxSpeed.x)
        {
            Objs[i].GetSetSpeed = new Vector2(Objs[i].GetSetMaxSpeed.x, Objs[i].GetSetSpeed.y);
        }
        if (Objs[i].GetSetSpeed.x < -Objs[i].GetSetMaxSpeed.x)
        {
            Objs[i].GetSetSpeed = new Vector2(-Objs[i].GetSetMaxSpeed.x, Objs[i].GetSetSpeed.y);
        }
        if (Objs[i].GetSetSpeed.y < -Objs[i].GetSetMaxSpeed.y)
        {
            Objs[i].GetSetSpeed = new Vector2(Objs[i].GetSetSpeed.x, -Objs[i].GetSetMaxSpeed.y);
        }
    }

    // --- 向き調整関数 ---
    // ※X速度がプラスだと「Right」,マイナスだと「Left」
    //   Fieldタイプのオブジェクトは「None」
    private void FixObjDir(int i)
    {
        if(Objs[i].GetSetType == ObjType.Field)
        {
            Objs[i].GetSetDir = ObjDir.NONE;
            return;
        }

        if (Objs[i].GetSetSpeed.x > 0f)
            Objs[i].GetSetDir = ObjDir.RIGHT;
        else if(Objs[i].GetSetSpeed.x < 0f)
            Objs[i].GetSetDir = ObjDir.LEFT;
    }

    // --- 無敵時間更新 ---
    private void UpdateInvincible(int i)
    {
        if (Objs[i].GetSetInvincible.m_fTime <= 0f)
        {
            Objs[i].GetSetInvincible.m_bInvincible = false;
            Objs[i].GetSetInvincible.m_fTime = 0f;
        }
        else Objs[i].GetSetInvincible.m_fTime -= Time.deltaTime;
    }

    // --- 当たり判定反映関数 ---
    private void CollisionUpdate()
    {
        // 当たり判定更新(衝突時データをリストに格納している)
        ON_HitManager.instance.UpdateHit();

        // --- 衝突時データ分を回す ---
        for (int i = 0; i < ON_HitManager.instance.GetHitCombiCnt(); ++i)
        {
            myID = -1;
            otherID = -1;

            // --- 自身と相手のオブジェクトを当たり判定IDで検索 ---
            for (int j = 0; j < Objs.Count; ++j)
            {
                // 自身のIDを検索
                if (Objs[j].GetSetObjID == ON_HitManager.instance.GetData(i).myID)
                {
                    myID = j;
                }
                // 相手のIDを検索
                if (Objs[j].GetSetObjID == ON_HitManager.instance.GetData(i).otherID)
                {
                    otherID = j;
                }
            }
            // -----------------------------------------------------

            // どちらかのIDが割り振られていないものだったらスキップする
            if (myID == -1 || otherID == -1) continue;

            // 確認用
            if (GameManager.IsDebug())
                Debug.Log("判定: " + ON_HitManager.instance.GetData(i).state +
                " 衝突方向: " + ON_HitManager.instance.GetData(i).dir +
                " 自身: " + myID +
                " 相手: " + otherID);

            // --- 判定によってゲームに反映 ---
            // 攻撃を当てた判定だったら
            if (ON_HitManager.instance.GetData(i).state == HitState.ATTACK)
            {
                if (!Objs[otherID].GetSetInvincible.m_bInvincible)
                {
                    //Playerの場合は
                    if (Objs[myID].GetComponent<ObjPlayer>() != null)
                    {
                        // コンボ加算
                        YK_Combo.AddCombo();

                        // ヒットエフェクト再生
                        hitEffect.Play();
                        hitEffect.transform.position = Objs[otherID].GetSetPos;

                        // ヒットストップをかける
                        Objs[myID].GetSetHitStopParam.SetHitStop(0.5f);
                    }
                }
            }
            // 攻撃を受けた判定だったら
            if (ON_HitManager.instance.GetData(i).state == HitState.DEFENCE)
            {
                // 自身が無敵でなければ…
                if (!Objs[myID].GetSetInvincible.m_bInvincible)
                {
                    // 自身がプレイヤーだったら
                    if (Objs[myID].GetComponent<ObjPlayer>() != null)
                    {
                        // 攻撃を受ける
                        Objs[myID].DamageAttack();
                        Objs[myID].GetSetHit = true;
                    }
                    // 自身が敵だったら
                    if (Objs[myID].GetComponent<ObjEnemyBase>() != null)
                    {
                        // ノックバック処理
                        //Objs[myID].KnockBackObj(Objs[otherID].GetSetDir);
                        Objs[myID].GetSetHit = true;
                    }

                }
            }
            // 体同士の接触判定だったら
            if (ON_HitManager.instance.GetData(i).state == HitState.BODYS)
            {
                //// 右に当たっていたら
                //if (ON_HitManager.instance.GetData(i).dir == HitDir.RIGHT)
                //{
                //    // 座標調整
                //    Objs[myID].GetSetPos = new Vector3(Objs[otherID].GetSetPos.x, Objs[myID].GetSetPos.y, 0f) -
                //                           new Vector3(Objs[otherID].GetSetScale.x / 2f +
                //                                       Objs[myID].GetSetScale.x / 2f, 0f, 0f);

                //    // 速度を0にする
                //    Objs[myID].GetSetSpeed = new Vector3(0f, Objs[myID].GetSetSpeed.y, 0f);

                //    if(Objs[myID].GetComponent<ObjEnemyBase>() != null)
                //    {
                //        Objs[myID].GetComponent<ObjEnemyBase>().GetSetEnemyState = EnemyState.Idle;
                //    }
                //}
                //// 左に当たっていたら
                //if (ON_HitManager.instance.GetData(i).dir == HitDir.LEFT)
                //{
                //    // 座標調整
                //    Objs[myID].GetSetPos = new Vector3(Objs[otherID].GetSetPos.x, Objs[myID].GetSetPos.y, 0f) +
                //                           new Vector3(Objs[otherID].GetSetScale.x / 2f +
                //                                       Objs[myID].GetSetScale.x / 2f, 0f, 0f);

                //    // 速度を0にする
                //    Objs[myID].GetSetSpeed = new Vector3(0f, Objs[myID].GetSetSpeed.y, 0f);

                //    if (Objs[myID].GetComponent<ObjEnemyBase>() != null)
                //    {
                //        Objs[myID].GetComponent<ObjEnemyBase>().GetSetEnemyState = EnemyState.Idle;
                //    }
                //}
            }
            // 攻撃同士の接触判定だったら
            if (ON_HitManager.instance.GetData(i).state == HitState.BALANCE)
            {
            }
            // 敵同士の判定だったら
            if (ON_HitManager.instance.GetData(i).state == HitState.ENEMY)
            {
                ObjEnemyBase Enemy_1 = Objs[myID].GetComponent<ObjEnemyBase>();
                ObjEnemyBase Enemy_2 = Objs[otherID].GetComponent<ObjEnemyBase>();

                // 互いに敵が存在していたら…
                if(Enemy_1.GetSetExist && Enemy_2.GetSetExist)
                {
                    // どちらもノックバックの状態 on
                    // どちらも無敵状態ではなかった場合
                    //if ((Enemy_1.GetSetEnemyState == EnemyState.KnockBack && Enemy_2.GetSetEnemyState == EnemyState.KnockBack) &&
                    //    (!Enemy_1.GetSetInvincible.m_bInvincible && !Enemy_2.GetSetInvincible.m_bInvincible))
                    if (Enemy_1.GetSetEnemyState == EnemyState.KnockBack && Enemy_2.GetSetEnemyState == EnemyState.KnockBack)
                    {
                        // --- 敵の合成 ---
                        UnionEnemy(Enemy_1.GetSetObjID, Enemy_2.GetSetObjID);
                    }
                }
            }
            // 必殺技を当てた判定だったら
            if (ON_HitManager.instance.GetData(i).state == HitState.SPECIAL)
            {
                if (!Objs[otherID].GetSetInvincible.m_bInvincible)
                {
                    // Playerが敵に必殺技を当てた時
                    if (Objs[myID].GetComponent<ObjPlayer>() != null &&
                    Objs[otherID].GetComponent<ObjEnemyBase>() != null)
                    {
                        int EnemyNum; // 敵の数を格納する
                        // 相手が合体敵だったら
                        if (Objs[otherID].GetComponent<ObjEnemyUnion>() != null)
                        {
                            EnemyNum = Objs[otherID].GetComponent<ObjEnemyUnion>().m_nEnemyIDs.Count;
                            YK_Score.instance.FieldAddScore(EnemyNum);
                        }
                        // 単体の敵だったら
                        else YK_Score.instance.FieldAddScore(1);

                        // ヒットエフェクト再生
                        hitEffect.Play();
                        hitEffect.transform.position = Objs[otherID].GetSetPos;
                    }
                }
            }
            // 必殺技を受けた判定だったら
            if (ON_HitManager.instance.GetData(i).state == HitState.DEFSPECIAL)
            {
                // 自身が無敵でなければ…
                if (!Objs[myID].GetSetInvincible.m_bInvincible)
                {
                    // 自身が合体敵だったら
                    if (Objs[myID].GetComponent<ObjEnemyUnion>() != null)
                    {
                        // 破壊トリガーをON
                        Objs[myID].GetSetDestroy = true;
                        Objs[myID].GetComponent<ObjEnemyUnion>().DestroyTriggerChildEnemy();
                        continue;
                    }

                    // 自身が敵だったら
                    if (Objs[myID].GetComponent<ObjEnemyBase>() != null)
                    {
                        // ノックバック処理
                        ON_HitManager.instance.SetActive(Objs[myID].GetSetObjID, false);
                        Objs[myID].GetSetDestroy = true;
                        continue;
                    }
                }
            }
            // 弾を当てた判定だったら
            if (ON_HitManager.instance.GetData(i).state == HitState.BULLET)
            {
                if (Objs[myID].GetComponent<Spider>() != null)
                {
                    hitEffect.Play();
                    hitEffect.transform.position = Objs[otherID].GetSetPos;

                    Objs[myID].GetComponent<Spider>().DeleteMissile();
                }
            }
            // 弾を受けた判定だったら
            if (ON_HitManager.instance.GetData(i).state == HitState.DEFBULLET)
            {
            }
            // 弾を破壊する判定だったら
            if (ON_HitManager.instance.GetData(i).state == HitState.BULLET2DESTROY)
            {
            }
            // 地面に当たった判定だったら
            if (ON_HitManager.instance.GetData(i).state == HitState.GRUOND)
            {
                float pos;
                // 右に当たっていたら
                if (ON_HitManager.instance.GetData(i).dir == HitDir.RIGHT)
                {
                    // 座標調整(相手の左端 - 自身の縮小 / 2)
                    pos = Objs[otherID].GetSetPos.x - Mathf.Abs(Objs[otherID].GetSetScale.x / 2f) -
                          Mathf.Abs(Objs[myID].GetSetScale.x / 2f);

                    Objs[myID].GetSetPos = new Vector3(pos, Objs[myID].GetSetPos.y, 0f);

                    // 速度を0にする
                    Objs[myID].GetSetSpeed = new Vector3(0f, Objs[myID].GetSetSpeed.y, 0f);
                }
                // 左に当たっていたら
                if (ON_HitManager.instance.GetData(i).dir == HitDir.LEFT)
                {
                    // 座標調整(相手の右端 + 自身の縮小 / 2)
                    pos = Objs[otherID].GetSetPos.x + Mathf.Abs(Objs[otherID].GetSetScale.x / 2f) +
                          Mathf.Abs(Objs[myID].GetSetScale.x / 2f);

                    Objs[myID].GetSetPos = new Vector3(pos, Objs[myID].GetSetPos.y, 0f);

                    // 速度を0にする
                    Objs[myID].GetSetSpeed = new Vector3(0f, Objs[myID].GetSetSpeed.y, 0f);
                }
                // 上に当たっていたら
                if (ON_HitManager.instance.GetData(i).dir == HitDir.UP)
                {
                    // 座標調整(相手の下端 - 自身の縮小 / 2)
                    pos = Objs[otherID].GetSetPos.y - Mathf.Abs(Objs[otherID].GetSetScale.y / 2f) -
                          Mathf.Abs(Objs[myID].GetSetScale.y / 2f);

                    Objs[myID].GetSetPos = new Vector3(Objs[myID].GetSetPos.x, pos, 0f);

                    // 速度を0にする
                    Objs[myID].GetSetSpeed = new Vector3(Objs[myID].GetSetSpeed.x, 0f, 0f);
                }
                // 下に当たっていたら
                if (ON_HitManager.instance.GetData(i).dir == HitDir.DOWN)
                {
                    // 座標調整(相手の上端 + 自身の縮小 / 2)
                    pos = Objs[otherID].GetSetPos.y + Mathf.Abs(Objs[otherID].GetSetScale.y / 2f) +
                          Mathf.Abs(Objs[myID].GetSetScale.y / 2f);

                    Objs[myID].GetSetPos = new Vector3(Objs[myID].GetSetPos.x, pos, 0f);

                    // 速度を0にする
                    Objs[myID].GetSetSpeed = new Vector3(Objs[myID].GetSetSpeed.x, 0f, 0f);

                    // 地面の情報を格納
                    Objs[myID].GetSetGround.m_bStand = true;
                    Objs[myID].GetSetGround.m_vCenter = Objs[otherID].GetSetPos;
                    Objs[myID].GetSetGround.m_vSize = Objs[otherID].GetSetScale;

                    if(GameManager.IsDebug())
                        Debug.Log("地面に当たった");
                }
            }
            // 地面から見て何かに当てられた判定だったら
            if (ON_HitManager.instance.GetData(i).state == HitState.DEFGRUOND)
            {
            }

            // -------------------------------
        }
    }

    // --- 敵の合体処理 ---
    private void UnionEnemy(int id_1, int id_2)
    {
        int enemy_id_1 = -1;
        int enemy_id_2 = -1;
        // 引数のIDを持つオブジェクトIDを検索
        for (int i = 0; i < Objs.Count; ++i)
        {
            if (Objs[i].GetSetObjID == id_1)
                enemy_id_1 = i; 
            if (Objs[i].GetSetObjID == id_2)
                enemy_id_2 = i;
        }

        // 検索しても出てこなかった場合は終了
        if (enemy_id_1 == -1 || enemy_id_2 == -1)
            return;

        // 敵同士の存在,当たり判定を消す
        Objs[enemy_id_1].GetSetExist = false;
        Objs[enemy_id_2].GetSetExist = false;
        Objs[enemy_id_1].GetComponent<ObjEnemyBase>().GetSetEnemyState = EnemyState.RePop;
        Objs[enemy_id_2].GetComponent<ObjEnemyBase>().GetSetEnemyState = EnemyState.RePop;
        Objs[enemy_id_1].GetSetSpeed = new Vector2(Random.RandomRange(-0.1f, 0.1f), Random.RandomRange(0.3f, 0.5f));
        Objs[enemy_id_2].GetSetSpeed = new Vector2(Random.RandomRange(-0.1f, 0.1f), Random.RandomRange(0.3f, 0.5f));
        ON_HitManager.instance.SetActive(id_1, false);
        ON_HitManager.instance.SetActive(id_2, false);

        // 合体敵の生成 → 初期化
        ObjEnemyUnion enemyUnion = 
            Instantiate(enemyUnionPrefab, Vector3.zero,Quaternion.Euler(Vector3.zero));
        enemyUnion.GetSetObjID = SetObjID();
        enemyUnion.GenerateHit();
        enemyUnion.InitObj();

        // 座標設定
        enemyUnion.GetSetPos = Objs[enemy_id_1].GetSetPos + new Vector3(0f, 5f, 0f);

        // 合体元のオブジェクトIDを格納
        enemyUnion.m_nEnemyIDs.Add(Objs[enemy_id_1].GetSetObjID);
        enemyUnion.m_nEnemyIDs.Add(Objs[enemy_id_2].GetSetObjID);

        // 合体元のオブジェクトが「合体敵」だった場合、
        // 格納されているIDを引き継ぐ
        int id;
        if(Objs[enemy_id_1].GetComponent<ObjEnemyUnion>() != null)
        {
            for(int i = 0; i < Objs[enemy_id_1].GetComponent<ObjEnemyUnion>().m_nEnemyIDs.Count;++i)
            {
                id = Objs[enemy_id_1].GetComponent<ObjEnemyUnion>().m_nEnemyIDs[i];
                enemyUnion.m_nEnemyIDs.Add(id);
            }
        }
        if (Objs[enemy_id_2].GetComponent<ObjEnemyUnion>() != null)
        {
            for (int i = 0; i < Objs[enemy_id_2].GetComponent<ObjEnemyUnion>().m_nEnemyIDs.Count; ++i)
            {
                id = Objs[enemy_id_2].GetComponent<ObjEnemyUnion>().m_nEnemyIDs[i];
                enemyUnion.m_nEnemyIDs.Add(id);
            }
        }

        // 状態を設定
        enemyUnion.GetSetEnemyState = EnemyState.Drop;
        enemyUnion.GetSetHit = true;

        // オブジェクトのリストに追加
        Objs.Add(enemyUnion);
    }

    // オブジェクトIDを設定する
    public int SetObjID()
    {
        int selfID = maxID;
        maxID++;
        return selfID;
    }

    // オブジェクト単体を取得する
    // int i … オブジェクトのID
    public ObjBase GetObj(int id)
    {
        for(int i = 0; i < Objs.Count; ++i)
        {
            if(Objs[i].GetSetObjID == id)
            return Objs[i];
        }

        return null;
    }

    // オブジェクトのリストを取得する
    public List<ObjBase> GetObjList()
    {
        return Objs;
    }
}
