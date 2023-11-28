using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyKnockBack : FlyStrategy
{
    [SerializeField] private FlyEnemy m_Fly;
    private bool endFlg;
    private bool KnockFlag=true;

    public override void UpdateState()
    {
        if(endFlg)
        {
            m_Fly.GetSetEnemyState = EnemyState.Atk;
            endFlg = false;
            m_Fly.GetSetKnockBack.m_fGravity = 0.0f;
            m_Fly.m_Ground.m_bStand = false;
            return;
        }
    }

    public override void UpdateStrategy()
    {
        m_Fly.GetSetKnockBack.m_fGravity = 0.02f;
        if (m_Fly.m_Ground.m_bStand)
        {
            //å∏êäèàóù
            m_Fly.GetSetSpeed =
                new Vector2(m_Fly.GetSetKnockBack.m_vSpeed.x * m_Fly.GetSetKnockBack.m_fDamping,
                            m_Fly.GetSetKnockBack.m_vSpeed.y * m_Fly.GetSetKnockBack.m_fDamping);
            m_Fly.GetSetKnockBack.m_vSpeed = m_Fly.GetSetSpeed;
            //íeÇﬁÇÃÇ™0.1fà»â∫Ç…Ç»Ç¡ÇΩÇÁ
            if (m_Fly.GetSetSpeed.y <= 0.1f)
            {
                m_Fly.GetSetSpeed = Vector2.zero;     //é~ÇﬂÇÈ
                endFlg = true;
            }
            m_Fly.m_Ground.m_bStand = false;
        }
    }
}

