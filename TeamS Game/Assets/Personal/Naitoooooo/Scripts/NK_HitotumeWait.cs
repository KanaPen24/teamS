using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NK_HitotumeWait : NK_HitotumeStrategy
{
    //カウント用
    private float c;
    [SerializeField] private HitotumeProto m_HitotumeProto;
    [SerializeField] private float DashTiming;      //走り始めるtiming
    public override void UpdateStrategy()
    {
        c++;
        if (c > DashTiming)
        {
            m_HitotumeProto.GetSetEnemyState = EnemyState.Walk;
        }
    }
}
