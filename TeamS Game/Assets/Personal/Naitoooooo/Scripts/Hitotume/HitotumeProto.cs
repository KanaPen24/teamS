/**
 * @file   HitotumeProto.cs
 * @brief  ˆê‚Â–Ú‚ÌƒNƒ‰ƒX
 * @author NaitoKoki
 * @date   2023/11/07
 * @Update 2023/11/07 ì¬
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

        oldDir = GetSetDir;

        m_HitotumeStrategy[(int)m_EnemyState].UpdateState();
        m_HitotumeStrategy[(int)m_EnemyState].UpdateStrategy();
        //m_vSpeed.x = 3.0f;
    }

    public override void UpdateDebug()
    {
        //Debug.Log("EnemyA");
    }
}