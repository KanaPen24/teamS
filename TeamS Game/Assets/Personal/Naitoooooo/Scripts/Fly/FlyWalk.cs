using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyWalk : FlyStrategy
{
    [SerializeField] private FlyEnemy m_Fly;
    [SerializeField] private float m_AtkReng;
    [SerializeField] private float m_MoveSpeed;
    private ObjDir oldDir;
    [SerializeField] private float m_ReMoveTime;
    [SerializeField] private float m_SpeedHolder;


    public override void UpdateState()
    {
        //ˆÚ“®¨UŒ‚
        if (m_Fly.GetSetPos.x > ObjPlayer.instance.GetSetPos.x - m_AtkReng &&
            m_Fly.GetSetPos.x < ObjPlayer.instance.GetSetPos.x + m_AtkReng)
        {
            m_Fly.GetSetEnemyState = EnemyState.Drop;
        }
    }

    public override void UpdateStrategy()
    {
        //if(oldDir!=m_Fly.GetSetDir)
        //{
        //    m_MoveSpeed = 0;
        //    Invoke("ReMove",m_ReMoveTime);
        //}
        if(m_Fly.GetSetDir==ObjDir.RIGHT)
        {
            m_Fly.GetSetSpeed = new Vector2(m_MoveSpeed, 0.0f);
        }
        else
        {
            m_Fly.GetSetSpeed = new Vector2(-m_MoveSpeed, 0.0f);
        }
        //oldDir = m_Fly.GetSetDir;
    }

    //private void ReMove()
    //{
    //    m_MoveSpeed = m_SpeedHolder;
    //}
}
