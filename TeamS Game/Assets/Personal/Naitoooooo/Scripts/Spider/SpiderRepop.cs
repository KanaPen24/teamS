using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderRepop : EnemyStrategy
{
    [SerializeField] private Spider m_Spider;
    public override void UpdateState()
    {
        if(m_Spider.GetSetGround.m_bStand)
        {
            m_Spider.GetSetEnemyState = EnemyState.Idle;
            return;
        }
    }

    public override void UpdateStrategy()
    {
    }
}
