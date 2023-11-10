/**
 * @file   HitotumeProto.cs
 * @brief  一つ目のクラス
 * @author NaitoKoki
 * @date   2023/11/07
 * @Update 2023/11/07 作成
 **/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitotumeProto : ObjEnemyBase
{
    [SerializeField] private List<NK_HitotumeStrategy> m_HitotumeStrategy;

    public override void UpdateObj()
    {
        m_HitotumeStrategy[(int)m_EnemyState].UpdateStrategy();
        //m_vSpeed.x = 3.0f;

    }

    public override void UpdateDebug()
    {
        //Debug.Log("EnemyA");
    }
}