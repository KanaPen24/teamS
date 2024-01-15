using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderKnockBack : EnemyStrategy
{
    [SerializeField] private Spider m_Spider;
    private bool endFlg = false;

    public override void UpdateState()
    {
        //—‰º¨‘Ò‚¿
        if(endFlg)
        {
            m_Spider.GetSetEnemyState = EnemyState.Idle;
            endFlg = false;
            return;
        }
    }

    public override void UpdateStrategy()
    {
        if(m_Spider.m_Ground.m_bStand)
        {
            //Œ¸Šˆ—
            m_Spider.GetSetSpeed =
                new Vector2(m_Spider.GetSetSpeed.x * m_Spider.GetSetKnockBack.m_fDamping,
                            m_Spider.GetSetSpeed.y * m_Spider.GetSetKnockBack.m_fDamping);
            if (m_Spider.GetSetSpeed.y<=0.1f)
            {
                m_Spider.GetSetSpeed = Vector2.zero;
                endFlg = true;
            }
            m_Spider.m_Ground.m_bStand = false;
        }
    }
}
