using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyRepop : FlyStrategy
{
    [SerializeField] private FlyEnemy m_Fly;

    public override void UpdateState()
    {
        //•œŠˆ¨‘Ò‚¿
        if(m_Fly.GetSetGround.m_bStand)
        {
            m_Fly.GetSetEnemyState = EnemyState.Idle;
            return;
        }
    }

    public override void UpdateStrategy()
    {
        
    }
}
