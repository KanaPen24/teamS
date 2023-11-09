using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NK_HitotumeWait : NK_HitotumeStrategy
{
    private float c;
    [SerializeField] private HitotumeProto m_HitotumeProto;
    public override void UpdateStrategy()
    {
        c++;
        if (c > 30.0f)
        {
            m_HitotumeProto.GetSetEnemyState = EnemyState.Walk;
        }
    }
}
