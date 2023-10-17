using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjManager : MonoBehaviour
{
    public List<ObjBase> Objs;
    private void FixedUpdate()
    {
        // --- オブジェクトの更新処理 ---
        for(int i = 0; i < Objs.Count; ++i)
        {
            Objs[i].UpdateObj();
        }
        // ------------------------------


        // --- 当たり判定処理 ---
        // ここに記述予定
        //ON_HitManager.instance.Update();

        // ----------------------
        

        // --- 最終的な処理をここで行う ---
        //

        // --------------------------------

        // --- デバッグ表示 ---
        if(GameManager.IsDebug())
        {
            for (int i = 0; i < Objs.Count; ++i)
            {
                Objs[i].UpdateDebug();
            }
        }
        // ---------------------

    }
}
