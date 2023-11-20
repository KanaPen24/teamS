using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NK_HitotumeWait : NK_HitotumeStrategy
{
    //カウント用
    private float c;
    [SerializeField] private HitotumeProto m_HitotumeProto;
    [SerializeField] private float DashTiming;      //走り始めるtiming

    public override void UpdateState()
    {
        c++;
        //待ち→落下
        if(!m_HitotumeProto.GetSetGround.m_bStand)
        {
            m_HitotumeProto.GetSetEnemyState = EnemyState.Drop;
        }
        //待ち→移動
        if (c > DashTiming)
        {
            m_HitotumeProto.GetSetEnemyState = EnemyState.Walk;
            c = 0;
        }
    }
    public override void UpdateStrategy()
    {

    }
}
