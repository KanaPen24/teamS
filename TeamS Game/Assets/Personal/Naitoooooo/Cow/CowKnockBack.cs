using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CowKnockBack : EnemyStrategy
{
    [SerializeField] private Cow m_cow;
    private bool endFlg = false;
    // Start is called before the first frame update
    public override void UpdateState()
    {
        if(endFlg)
        {
            m_cow.GetSetEnemyState = EnemyState.Idle;
            endFlg = false;
            return;
        }
    }

    public override void UpdateStrategy()
    {
        if(m_cow.m_Ground.m_bStand)
        {
            //å∏êäèàóù
            m_cow.GetSetSpeed =
                new Vector2(m_cow.GetSetSpeed.x * m_cow.GetSetKnockBack.m_fDamping,
                            m_cow.GetSetSpeed.y * m_cow.GetSetKnockBack.m_fDamping);
            if (m_cow.GetSetSpeed.y<=0.1f)
            {
                m_cow.GetSetSpeed = Vector2.zero;
                endFlg = true;
            }
            m_cow.m_Ground.m_bStand = false;
        }
    }
}
