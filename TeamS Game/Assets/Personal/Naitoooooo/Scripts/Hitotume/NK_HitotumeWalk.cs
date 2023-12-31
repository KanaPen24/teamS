using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NK_HitotumeWalk : NK_HitotumeStrategy
{
    [SerializeField] private HitotumeProto m_Hitotume;
    [SerializeField] private float t;
    [SerializeField] private float s;
    //加速用変数
    [SerializeField] private float acceleration;
    //速度倍率
    [SerializeField] private float magnification;
    //前の向きを保存
    private ObjDir oldDir;

    public override void UpdateState()
    {
        //落下→待ち
        if (!m_Hitotume.GetSetGround.m_bStand)
        {
            m_Hitotume.GetSetEnemyState = EnemyState.Drop;
        }
    }
    public override void UpdateStrategy()
    {

        if (s < 90)
        {
            s += acceleration;
        }
        if(oldDir!=m_Hitotume.GetSetDir)
        {
            s = 0;
        }
        t = s * Mathf.Deg2Rad;
        if (m_Hitotume.GetSetDir==ObjDir.RIGHT)
        {
            m_Hitotume.GetSetSpeed = new Vector2(Mathf.Sin(t) * magnification, 0.0f);
        }
        else
        {
            m_Hitotume.GetSetSpeed = new Vector2(-Mathf.Sin(t) * magnification, 0.0f);
        }
        //m_Hitotume.GetSetSpeed = new Vector2(5.0f,0.0f);
        oldDir = m_Hitotume.GetSetDir;
    }
}
