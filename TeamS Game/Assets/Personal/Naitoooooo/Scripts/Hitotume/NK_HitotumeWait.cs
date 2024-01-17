using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NK_HitotumeWait : NK_HitotumeStrategy
{
    //カウント用
    private float c;
    [SerializeField] private HitotumeProto m_HitotumeProto;
    [SerializeField] private float m_Reng;      //走り始めるtiming

    public override void UpdateState()
    {
        //待ち→落下
        if(!m_HitotumeProto.GetSetGround.m_bStand)
        {
            m_HitotumeProto.GetSetEnemyState = EnemyState.Drop;
            return;
        }
        //待ち→移動
        if (m_HitotumeProto.GetSetPos.x > ObjPlayer.instance.GetSetPos.x - m_Reng &&
            m_HitotumeProto.GetSetPos.x < ObjPlayer.instance.GetSetPos.x + m_Reng)
        {
            m_HitotumeProto.GetSetEnemyState = EnemyState.Walk;
            return;
        }
    }
    public override void UpdateStrategy()
    {
        m_HitotumeProto.GetSetSpeed = Vector2.zero;
    }
}
