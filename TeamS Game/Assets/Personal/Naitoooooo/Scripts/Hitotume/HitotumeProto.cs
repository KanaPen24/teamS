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
        if(ObjPlayer.instance.GetSetPos.x > GetSetPos.x)
        {
            GetSetDir = ObjDir.RIGHT;
        }
        else
        {
            GetSetDir = ObjDir.LEFT;
        }

        m_HitotumeStrategy[(int)m_EnemyState].UpdateState();
        m_HitotumeStrategy[(int)m_EnemyState].UpdateStrategy();
        CheckDivision();
        //m_vSpeed.x = 3.0f;
    }
}