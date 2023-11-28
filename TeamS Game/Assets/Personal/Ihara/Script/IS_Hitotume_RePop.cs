/**
 * @file  IS_Hitotume_RePop.cs
 * @brief  Hitotumeの「復活」クラス
 * @author IharaShota
 * @date   2023/11/24
 * @Update 2023/11/24 作成
 **/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IS_Hitotume_RePop : NK_HitotumeStrategy
{
    private HitotumeProto Hitotume; // 自身を格納するためのコンポーネント

    private void Start()
    {
        Hitotume = this.gameObject.GetComponent<HitotumeProto>();
    }

    public override void UpdateState()
    {
        // 「復活 → 待ち」
        if(Hitotume.GetSetGround.m_bStand)
        {
            Hitotume.GetSetEnemyState = EnemyState.Idle;
            return;
        }
    }

    public override void UpdateStrategy()
    {
    }
}
