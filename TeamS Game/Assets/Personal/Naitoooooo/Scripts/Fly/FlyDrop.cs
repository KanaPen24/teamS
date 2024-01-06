using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyDrop : FlyStrategy
{
    [SerializeField] private FlyEnemy m_fly;
    [SerializeField] private float m_XmoveSpeed;
    [SerializeField] private float m_YmoveSpeed;
    [SerializeField] private float m_DownTiming;
    private float m_Cnt;
    [SerializeField] private float m_angle;

    public override void UpdateState()
    {
        //óéâ∫Å®è„è∏
        if(m_fly.GetSetGround.m_bStand)
        {
            m_Cnt = 0;
            m_fly.GetSetGround.m_bStand = false;
            m_fly.GetSetEnemyState = EnemyState.Atk;
        }
    }

    public override void UpdateStrategy()
    {
        m_Cnt += Time.deltaTime;
        if (m_Cnt < m_DownTiming)
        {
            if (m_fly.GetSetDir == ObjDir.LEFT)
            {
                transform.rotation = Quaternion.AngleAxis(m_angle, new Vector3(0, 0, 1));
            }
            else
            {
                transform.rotation = Quaternion.AngleAxis(-m_angle, new Vector3(0, 0, 1));
            }
            m_fly.GetSetSpeed = new Vector2(0.0f, 0.0f);
        }
        else
        {
            
            if (transform.rotation.z < 0)
            {
                m_fly.GetSetSpeed = new Vector2(m_XmoveSpeed, -m_YmoveSpeed);
            }
            else
            {
                m_fly.GetSetSpeed = new Vector2(-m_XmoveSpeed, -m_YmoveSpeed);
            }
        }
    }
}