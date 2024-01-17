using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NK_HitotumeWait : NK_HitotumeStrategy
{
    //�J�E���g�p
    private float c;
    [SerializeField] private HitotumeProto m_HitotumeProto;
    [SerializeField] private float m_Reng;      //����n�߂�timing

    public override void UpdateState()
    {
        //�҂�������
        if(!m_HitotumeProto.GetSetGround.m_bStand)
        {
            m_HitotumeProto.GetSetEnemyState = EnemyState.Drop;
            return;
        }
        //�҂����ړ�
        if (m_HitotumeProto.GetSetPos.x > ObjPlayer.instance.GetSetPos.x - m_Reng &&
            m_HitotumeProto.GetSetPos.x < ObjPlayer.instance.GetSetPos.x + m_Reng)
        {
            m_HitotumeProto.GetSetEnemyState = EnemyState.Walk;
            return;
        }
    }
    public override void UpdateStrategy()
    {
        m_HitotumeProto.GetSetSpeed = Vector2.zero;
    }
}
