using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NK_HitotumeDrop : NK_HitotumeStrategy
{
    [SerializeField] private HitotumeProto m_HitotumeProto;

    public override void UpdateState()
    {
        //óéâ∫Å®ë“Çø
        if (m_HitotumeProto.GetSetGround.m_bStand)
        {
            m_HitotumeProto.GetSetEnemyState = EnemyState.Idle;
        }
    }
    public override void UpdateStrategy()
    {

    }
}
