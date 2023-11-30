using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderAtk : EnemyStrategy
{
    [SerializeField] private Spider m_Spider;
    [SerializeField] private float m_AtkTiming;
    [SerializeField] private GameObject m_BalletPos;
    [SerializeField] private Vector3 m_BalletSize;
    [SerializeField] private float m_MissileY;
    [SerializeField] private float m_YSpeed;
    [SerializeField] private float m_XSpeed;
    private float m_YposHolder;
    private float m_XposHolder;
    private float m_NowYpos = 0;
    private float m_NowXpos = 0;
    private int atknum;
    private bool m_bAtk;
    private float m_cnt;
    public override void UpdateState()
    {
        //m_YposHolder = m_BalletPos.transform.position.y + m_NowYpos;
        //m_XposHolder = m_BalletPos.transform.position.x + m_NowXpos;
        if(m_bAtk)
        {
            //if (m_YposHolder < m_MissileY)
            //{
            //    ON_HitManager.instance.GetHit(atknum).SetCenter(m_BalletPos.transform.position + new Vector3(0.0f, m_NowYpos, 0.0f));
            //    m_NowYpos += m_YSpeed;
            //}
            //else
            //{
            //    if(ObjPlayer.instance.GetSetPos.x < m_NowXpos)
            //    {
            //        ON_HitManager.instance.GetHit(atknum).SetCenter(
            //            m_BalletPos.transform.position + new Vector3(m_NowXpos, 0.0f, 0.0f));
            //        m_NowXpos += m_XSpeed;
            //    }
            //    else
            //    {
            //        ON_HitManager.instance.GetHit(atknum).SetCenter(
            //            m_BalletPos.transform.position + new Vector3(m_NowXpos, 0.0f, 0.0f));
            //        m_NowXpos -= m_XSpeed;
            //    }
            //}
        }
    }

    public override void UpdateStrategy()
    {
        m_cnt += Time.deltaTime;
        if(m_cnt>m_AtkTiming&&!m_bAtk)
        {

        }
        m_Spider.GetSetSpeed = new Vector2(0.0f, 0.0f);
    }
}
