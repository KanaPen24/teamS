using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour
{
    private int Missilenum;
    private Spider m_Spider;
    [SerializeField] private float m_MissileY;
    [SerializeField] private float m_YSpeed;
    [SerializeField] private float m_XSpeed;
    [SerializeField] private float m_DownTiming;
    private float m_cnt;
    private bool m_Yposb=false;

    private void Start()
    {
        Missilenum = ON_HitManager.instance.GenerateHit(transform.position, transform.localScale, true, HitType.BULLET, m_Spider.GetSetObjID);
    }

    private void Update()
    {
        ON_HitManager.instance.GetHit(Missilenum).SetCenter(this.transform.position);
    }

    private void FixedUpdate()
    {
        if(this.transform.position.y<m_MissileY&&!m_Yposb)
        {
            //missile‚Ì‚“x‚ª‘«‚ç‚È‚¢‚Æ‚«Bmissile‚ðã‚°‚é
            transform.Translate(new Vector3(0f, m_YSpeed, 0f));
        }
        else
        {
            m_cnt += Time.deltaTime;
            if (m_DownTiming > m_cnt)
            {
                if (ObjPlayer.instance.GetSetPos.x > this.transform.position.x)
                {
                    transform.Translate(m_XSpeed, 0f, 0f);
                }
                else
                {
                    transform.Translate(-m_XSpeed, 0f, 0f);
                }
            }
            else
            {
                transform.Translate(0f, -m_YSpeed, 0);
                m_Yposb = true;
            }
        }
    }

    public void SetSpider(Spider PSpider)
    {
        m_Spider = PSpider;
    }
}
