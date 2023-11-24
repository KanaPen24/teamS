using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyWait : FlyStrategy
{
    [SerializeField] private FlyEnemy m_Fly;
    [SerializeField] private float m_Reng;

    public override void UpdateState()
    {
        //‘Ò‚¿¨ˆÚ“®
        if(m_Fly.GetSetPos.x>ObjPlayer.instance.GetSetPos.x-m_Reng&&
            m_Fly.GetSetPos.x < ObjPlayer.instance.GetSetPos.x + m_Reng)
        {
            m_Fly.GetSetEnemyState = EnemyState.Walk;
        }
    }

    public override void UpdateStrategy()
    {
        m_Fly.GetSetSpeed = new Vector2(0.0f, 0.0f);
    }
}
