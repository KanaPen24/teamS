using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderWalk : EnemyStrategy
{
    [SerializeField] private Spider m_Spider;
    [SerializeField] private float m_AtkTiming;
    [SerializeField] private float m_MoveSpeed;
    private float m_cnt;
    public override void UpdateState()
    {
        m_cnt += Time.deltaTime;
        if(m_cnt>m_AtkTiming)
        {
            m_Spider.GetSetEnemyState = EnemyState.Atk;
            m_cnt = 0;
            return;
        }
    }

    public override void UpdateStrategy()
    {
        if(m_Spider.GetSetDir==ObjDir.RIGHT)
        {
            m_Spider.GetSetSpeed = new Vector2(m_MoveSpeed, 0.0f);
        }
        else
        {
            m_Spider.GetSetSpeed = new Vector2(-m_MoveSpeed, 0.0f);
        }
    }
}
