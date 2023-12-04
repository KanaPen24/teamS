using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CowWalk : EnemyStrategy
{
    [SerializeField] private Cow m_cow;
    [SerializeField] private float m_MoveSpeed;
    [SerializeField] private float m_MaxSpeed;
    [SerializeField] private float m_addSpeed;

    public override void UpdateState()
    {

    }
    public override void UpdateStrategy()
    {
        if(m_cow.GetSetDir==ObjDir.RIGHT)
        {
            if(m_MoveSpeed<m_MaxSpeed)
            {
                m_MoveSpeed += m_addSpeed;
            }
        }
        else
        {
            if(m_MoveSpeed>-m_MaxSpeed)
            {
                m_MoveSpeed -= m_addSpeed;
            }
        }
        m_cow.GetSetSpeed = new Vector2(m_MoveSpeed, 0f);
    }

    public void ReSetMoveSpeed()
    {
        m_MoveSpeed = 0;
    }
}
