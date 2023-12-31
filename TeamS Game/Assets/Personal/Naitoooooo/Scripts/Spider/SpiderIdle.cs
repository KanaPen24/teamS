using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderIdle : EnemyStrategy
{
    [SerializeField] private Spider m_Spider;
    [SerializeField] private float m_Reng;
    public override void UpdateState()
    {
        //待ち→落下
        if (!m_Spider.GetSetGround.m_bStand)
        {
            m_Spider.GetSetEnemyState = EnemyState.Drop;
            return;
        }

        //待ち→移動
        if(m_Spider.GetSetPos.x>ObjPlayer.instance.GetSetPos.x-m_Reng&&
            m_Spider.GetSetPos.x<ObjPlayer.instance.GetSetPos.x+m_Reng)
        {
            m_Spider.GetSetEnemyState = EnemyState.Walk;
            return;
        }
    }

    public override void UpdateStrategy()
    {
        m_Spider.GetSetSpeed = new Vector2(0.0f, 0.0f);
    }
}
