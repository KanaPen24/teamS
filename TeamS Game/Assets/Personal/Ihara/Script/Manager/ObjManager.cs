using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjManager : MonoBehaviour
{
    public List<ObjBase> Objs;

    private void Start()
    {
        if(ON_HitManager.instance == null)
        {
            ON_HitManager.instance = new ON_HitManager();
            ON_HitManager.instance.Init();
        }

        for (int i = 0; i < Objs.Count; ++i)
        {
            Objs[i].GetSetObjID = i;
            Objs[i].GenerateHit();
        }

    }

    private void FixedUpdate()
    {
        // --- オブジェクトの更新処理 ---
        for(int i = 0; i < Objs.Count; ++i)
        {
            Objs[i].UpdateObj();

            // オブジェクトが移動した座標に当たり判定を移動させる
            ON_HitManager.instance.SetCenter(Objs[i].GetSetHitID, Objs[i].transform.position);
            //ON_HitManager.instance.DebugHitID(Objs[i].GetSetHitID);
        }
        // ------------------------------


        // --- 当たり判定処理 ---
        // ここに記述予定
        ON_HitManager.instance.UpdateHit();
        for(int i = 0; i < ON_HitManager.instance.GetHitCnt(); ++i)
        {
            if(ON_HitManager.instance.GetData(i).state == HitState.GRUOND)
            {
                // 相手を調べる
                Debug.Log("あたったよ");
            }
        }

        // ----------------------
        

        // --- 最終的な処理をここで行う ---
        //

        // --------------------------------

        // --- デバッグ表示 ---
        if(GameManager.IsDebug())
        {
            for (int i = 0; i < Objs.Count; ++i)
            {
                //Objs[i].UpdateDebug();
            }
        }
        // ---------------------

    }
}
