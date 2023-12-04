using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CowIdel : EnemyStrategy
{
    [SerializeField] private Cow m_Cow;
    [SerializeField] private float m_Reng;

    public override void UpdateState()
    {
        if(!m_Cow.GetSetGround.m_bStand)
        {
            m_Cow.GetSetEnemyState = EnemyState.Drop;
            return;
        }
        if(m_Cow.GetSetPos.x>ObjPlayer.instance.GetSetPos.x - m_Reng &&
            m_Cow.GetSetPos.x < ObjPlayer.instance.GetSetPos.x + m_Reng)
        {
            m_Cow.GetSetEnemyState = EnemyState.Walk;
            return;
        }

    }

    public override void UpdateStrategy()
    {
        m_Cow.GetSetSpeed = new Vector2(0f, 0f);
    }
}
