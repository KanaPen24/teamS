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
        }
    }

    public override void UpdateStrategy()
    {
        m_cnt += Time.deltaTime;
        if(m_cnt>m_AtkTiming&&!m_bAtk)
        {
           GameObject missile = Instantiate(m_Missile, m_BalletPos.transform.position, Quaternion.identity);
            missile.GetComponent<Missile>().SetSpider(m_Spider);
            m_bAtk = true;
        }
        m_Spider.GetSetSpeed = new Vector2(0.0f, 0.0f);
    }
}
