using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyJump : FlyStrategy
{
    private float m_StartPosY;
    [SerializeField] private FlyEnemy m_fly;
    [SerializeField] private float m_YSpeed;

    public void Start()
    {
        m_StartPosY = m_fly.GetSetPos.y;
    }

    public override void UpdateState()
    {
        if(m_fly.GetSetPos.y>m_StartPosY)
        {
            m_fly.GetSetEnemyState = EnemyState.Idle;
        }
    }

    public override void UpdateStrategy()
    {
        m_fly.GetSetSpeed = new Vector2(0.0f, m_YSpeed);
    }
}
