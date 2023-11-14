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

public class ObjManager : MonoBehaviour
{
    [SerializeField] private List<ObjBase> Objs; // オブジェクトを格納する配列
    public static ObjManager instance;
    public ObjEnemyUnion unionObj;
    public ParticleSystem hitEffect;

    private int myID;
    private int otherID;

    public ObjBase GetObjs(int i)
    {
        return Objs[i];
    }

    public List<ObjBase> GetObj()
    {
        return Objs;
    }

    private void Start()
    {
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

        // 格納されているオブジェクトの数だけ回す
        for (int i = 0; i < Objs.Count; ++i)
        {
            // オブジェクトIDを設定 → 当たり判定生成
            // → オブジェクトの初期化 → オブジェクトの参照パラメーターを更新
            Objs[i].GetSetObjID = i;
            Objs[i].GenerateHit();
            Objs[i].InitObj();
            Objs[i].UpdateCheckParam();
        }

    }

    private void FixedUpdate()
    {
        if (GameManager.m_sGameState != GameState.GamePlay)
            return;

        // --- オブジェクトの更新処理 ---
        for(int i = 0; i < Objs.Count; ++i)
        {
            // --- オブジェクトが存在している場合 ---
            if (Objs[i].GetSetExist)
            {
                // オブジェクトの表示
                Objs[i].texObj.enabled = true;

                // 更新処理  → 地面判定 → 向き調整 → 移動量に速度を格納 → 速度調整 →
                // 無敵時間更新 → 座標更新
                Objs[i].UpdateObj();
                Objs[i].CheckObjGround();
                SaveObjSpeed(i);
                FixObjDir(i);
                UpdateInvincible(i);
                Objs[i].GetSetMove = Objs[i].GetSetSpeed;
                Objs[i].GetSetPos += new Vector3(Objs[i].GetSetMove.x, Objs[i].GetSetMove.y, 0f);

                // オブジェクトが移動した座標に当たり判定を移動させる
                ON_HitManager.instance.SetCenter(Objs[i].GetSetHitID, Objs[i].GetSetPos);
            }
            // --- 存在していない場合 ---
            else
            {
                // オブジェクトの非表示
                Objs[i].texObj.enabled = false;
            }

            // オブジェクトのパラメーター更新
            Objs[i].UpdateCheckParam();
        }

        // --- 当たり判定処理 ---
        CollisionUpdate();
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

                // 両方の検索が終わっていたら…
                if (myID != -1 && otherID != -1)
                {
                    // 修正を行い終了する
                    CollisionFix(i, ON_HitManager.instance.GetData(i).state);
                    break;
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
            // 地面接触の判定だったら
            if (ON_HitManager.instance.GetData(i).state == HitState.GRUOND)
            {
                // 右に当たっていたら
                if (ON_HitManager.instance.GetData(i).dir == HitDir.RIGHT)
                {
                    // 座標調整
                    Objs[myID].GetSetPos = new Vector3(Objs[otherID].GetSetPos.x, Objs[myID].GetSetPos.y, 0f) -
                                           new Vector3(Objs[otherID].GetSetScale.x / 2f +
                                                       Objs[myID].GetSetScale.x / 2f, 0f, 0f);

                    // 速度を0にする
                    Objs[myID].GetSetSpeed = new Vector3(0f, Objs[myID].GetSetSpeed.y, 0f);
                }
                // 左に当たっていたら
                if (ON_HitManager.instance.GetData(i).dir == HitDir.LEFT)
                {
                    // 座標調整
                    Objs[myID].GetSetPos = new Vector3(Objs[otherID].GetSetPos.x, Objs[myID].GetSetPos.y, 0f) +
                                           new Vector3(Objs[otherID].GetSetScale.x / 2f +
                                                       Objs[myID].GetSetScale.x / 2f, 0f, 0f);

                    // 速度を0にする
                    Objs[myID].GetSetSpeed = new Vector3(0f, Objs[myID].GetSetSpeed.y, 0f);
                }
                // 上に当たっていたら
                if (ON_HitManager.instance.GetData(i).dir == HitDir.UP)
                {
                    // 座標調整
                    Objs[myID].GetSetPos = new Vector3(Objs[myID].GetSetPos.x, Objs[otherID].GetSetPos.y, 0f) -
                                           new Vector3(0f, Objs[otherID].GetSetScale.y / 2f +
                                                       Objs[myID].GetSetScale.y / 2f, 0f);

                    // 速度を0にする
                    Objs[myID].GetSetSpeed = new Vector3(Objs[myID].GetSetSpeed.x, 0f, 0f);
                }
                // 下に当たっていたら
                if (ON_HitManager.instance.GetData(i).dir == HitDir.DOWN)
                {
                    // 座標調整
                    Objs[myID].GetSetPos = new Vector3(Objs[myID].GetSetPos.x, Objs[otherID].GetSetPos.y, 0f) +
                                           new Vector3(0f, Objs[otherID].GetSetScale.y / 2f +
                                                       Objs[myID].GetSetScale.y / 2f, 0f);

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

            //攻撃の判定だったら
            if (ON_HitManager.instance.GetData(i).state == HitState.DEFENCE)
            {
                if(!Objs[otherID].GetSetInvincible.m_bInvincible)
                {
                    // 敵にノックバック処理 → 無敵時間設定
                    Objs[otherID].KnockBackObj(Objs[myID].GetSetDir);
                    Objs[otherID].GetSetInvincible.SetInvincible(0.3f);

                    // ヒットエフェクト再生
                    hitEffect.Play();
                    hitEffect.transform.position = Objs[otherID].GetSetPos;

                    // コンボ発生
                    YK_Combo.AddCombo();
                }
            }

            // 敵同士の判定だったら
            if (ON_HitManager.instance.GetData(i).state == HitState.ENEMY)
            {
                // どちらかがノックバックの状態だったら
                if(Objs[myID].GetComponent<ObjEnemyBase>().GetSetEnemyState == EnemyState.KnockBack &&
                    Objs[otherID].GetComponent<ObjEnemyBase>().GetSetEnemyState == EnemyState.KnockBack)
                {
                    // 敵同士の存在を無くす
                    Objs[myID].GetSetExist = false;
                    Objs[otherID].GetSetExist = false;

                    Objs[myID].GetComponent<ObjEnemyBase>().GetSetEnemyState = EnemyState.Idle;
                    Objs[otherID].GetComponent<ObjEnemyBase>().GetSetEnemyState = EnemyState.Idle;

                    // 敵の当たり判定の存在を無くす
                    ON_HitManager.instance.SetActive(myID,false);
                    ON_HitManager.instance.SetActive(otherID,false);

                    // 合体した敵を生成
                    ObjEnemyUnion unionEnemy = Instantiate(unionObj,
                        Objs[myID].GetSetPos + new Vector3(0f,5f,0f),
                        Quaternion.Euler(new Vector3(0f,0f,0f)
                        ));

                    // 生成した敵の初期化
                    Objs.Add(unionEnemy);
                    unionEnemy.GetSetObjID = Objs.Count - 1;
                    unionEnemy.m_nEnemyCnt = Objs[myID].GetComponent<ObjEnemyBase>().m_nEnemyCnt +
                                             Objs[otherID].GetComponent<ObjEnemyBase>().m_nEnemyCnt;
                    unionEnemy.m_nEnemyIDs.Add(Objs[myID].GetSetObjID);
                    unionEnemy.m_nEnemyIDs.Add(Objs[otherID].GetSetObjID);
                    unionEnemy.GenerateHit();
                    unionEnemy.InitObj();
                }
            }
            // -------------------------------
        }
    }

    // --- 当たり判定による修正関数 ---
    private void CollisionFix(int dataNum, HitState hitState)
    {
        switch(hitState)
        {
            case HitState.ATTACK:
                break;
            case HitState.BALANCE:
                break;
            case HitState.BODYS:
                break;
            case HitState.DEFENCE:
                if (Objs[myID].GetComponent<ObjEnemyBase>() != null)
                    (myID, otherID) = (otherID, myID);
                break;
            case HitState.ENEMY:
                break;
            case HitState.GRUOND:
                if (Objs[myID].GetComponent<ObjField>() != null)
                {
                    (myID, otherID) = (otherID, myID);
                    if(ON_HitManager.instance.GetData(dataNum).dir == HitDir.UP)
                    {
                        ON_HitManager.instance.GetData(dataNum).dir = HitDir.DOWN;
                        break;
                    }
                    if (ON_HitManager.instance.GetData(dataNum).dir == HitDir.DOWN)
                    {
                        ON_HitManager.instance.GetData(dataNum).dir = HitDir.UP;
                        break;
                    }
                    if (ON_HitManager.instance.GetData(dataNum).dir == HitDir.RIGHT)
                    {
                        ON_HitManager.instance.GetData(dataNum).dir = HitDir.LEFT;
                        break;
                    }
                    if (ON_HitManager.instance.GetData(dataNum).dir == HitDir.LEFT)
                    {
                        ON_HitManager.instance.GetData(dataNum).dir = HitDir.RIGHT;
                        break;
                    }
                }
                break;
        }
    }
}
