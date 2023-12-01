using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderAtk : EnemyStrategy
{
    [SerializeField] private Spider m_Spider;
    [SerializeField] private float m_AtkTiming;
    [SerializeField] private GameObject m_BalletPos;
    [SerializeField] private GameObject m_Missile;
    private int atknum;
    private bool m_bAtk;
    private float m_cnt;
    public override void UpdateState()
    {
        if(m_bAtk)
        {
            m_Spider.GetSetEnemyState = EnemyState.Idle;
            m_bAtk = false;
            m_cnt = 0;
            return;
        }
    }

    public override void UpdateStrategy()
    {
        m_cnt += Time.deltaTime;
        if(m_cnt>m_AtkTiming&&!m_bAtk)
        {
            if(m_Spider.myMissile == null)
            {
                m_Spider.myMissile = Instantiate(m_Missile, m_BalletPos.transform.position, Quaternion.identity);
                m_Spider.myMissile.GetComponent<Missile>().SetSpider(m_Spider);
                m_Spider.myMissile.GetComponent<Missile>().FireMissile();
                m_bAtk = true;
            }
        }
        m_Spider.GetSetSpeed = new Vector2(0.0f, 0.0f);
    }
}
