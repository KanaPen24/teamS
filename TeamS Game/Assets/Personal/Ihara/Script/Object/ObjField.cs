/**
 * @file   ObjField.cs
 * @brief  フィールドクラス
 * @author IharaShota
 * @date   2023/10/20
 * @Update 2023/10/20 作成
 **/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjField : ObjBase
{
    public bool m_bHitWall;

    // --- 更新関数 ---
    public override void UpdateObj()
    {
    }

    //  ---- 当たり判定を生成した際の処理 ----
    public override void GenerateHit()
    {
        // 生成時に当たり判定IDを取得し、初期値を設定している(中心座標,大きさ,当たり判定の種類,objのID)
        m_nHitID = ON_HitManager.instance.GenerateHit(this.gameObject.transform.position,
        this.gameObject.transform.localScale / 2,
        true, HitType.FIELD, GetSetObjID);

        Debug.Log("当たり判定生成 HitID:" + GetSetHitID);
    }
}
