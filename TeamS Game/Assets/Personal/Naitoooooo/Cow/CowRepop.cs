using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CowRepop : EnemyStrategy
{
    [SerializeField] private Cow m_cow;
    public override void UpdateState()
    {
        if(m_cow.GetSetGround.m_bStand)
        {
            m_cow.GetSetEnemyState = EnemyState.Idle;
            return;
        }
    }

    public override void UpdateStrategy()
    {

    }
}
