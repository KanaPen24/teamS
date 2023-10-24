/**
 * @file   ObjManager.cs
 * @brief  オブジェクト管理クラス
 * @author IharaShota
 * @date   2023/10/13
 * @Update 2023/10/13 作成
 * @Update 2023/10/19 当たり判定取得完了
 **/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjManager : MonoBehaviour
{
    [SerializeField] private List<ObjBase> Objs; // オブジェクトを格納する配列

    private void Start()
    {
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
        // --- オブジェクトの更新処理 ---
        for(int i = 0; i < Objs.Count; ++i)
        {
            // オブジェクトの更新 → オブジェクトの参照パラメーターを更新
            Objs[i].UpdateObj();
            Objs[i].UpdateCheckParam();

            // オブジェクトが移動した座標に当たり判定を移動させる
            ON_HitManager.instance.SetCenter(Objs[i].GetSetHitID, Objs[i].GetSetPos);
            //ON_HitManager.instance.DebugHitID(Objs[i].GetSetHitID);
        }
        // ------------------------------


        // --- 当たり判定処理 ---
        // 当たり判定更新(衝突時データをリストに格納している)
        ON_HitManager.instance.UpdateHit();

        // 衝突時データ分を回す
        for(int i = 0; i < ON_HitManager.instance.GetHitCombiCnt(); ++i)
        {
            int myID = -1;
            int otherID = -1;

            // 自身と相手のオブジェクトを当たり判定IDで検索
            for (int j = 0; j < Objs.Count; ++j)
            {
                // 自身のIDを検索
                if(Objs[j].GetSetHitID == ON_HitManager.instance.GetData(i).myID)
                {
                    myID = j;
                }
                // 相手のIDを検索
                if (Objs[j].GetSetHitID == ON_HitManager.instance.GetData(i).otherID)
                {
                    otherID = j;
                }

                // 両方の検索が終わっていたら終了する
                if (myID != -1 && otherID != -1)
                {
                    if (myID > otherID)
                        (myID, otherID) = (otherID, myID);
                        break;
                }
            }

            // どちらかのIDが割り振られていないものだったらスキップする
            if (myID == -1 || otherID == -1) continue;

            // 確認用
            Debug.Log("判定: " + ON_HitManager.instance.GetData(i).state +
                " 衝突方向: " + ON_HitManager.instance.GetData(i).dir +
                " 自身: " + myID +
                " 相手: " + otherID);

            // 衝突時データが地面接触の判定だったら
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

                    Objs[myID].GetSetStand = true;
                    Objs[myID].GetSetGndLen = new Vector2(Objs[otherID].GetSetScale.x, Objs[otherID].GetSetScale.y);
                }
            }

            //攻撃判定
            if (ON_HitManager.instance.GetData(i).state == HitState.DEFENCE)
            {
                Objs[otherID].DamageAttack();
            }
        }

        // ----------------------
        

        // --- 最終的な処理をここで行う ---
        // 地面判定

        // --------------------------------

        // --- デバッグ表示 ---
        // ---------------------

    }
}
