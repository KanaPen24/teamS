using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NK_HitotumeWait : NK_HitotumeStrategy
{
    //�J�E���g�p
    private float c;
    [SerializeField] private HitotumeProto m_HitotumeProto;
    [SerializeField] private float DashTiming;      //����n�߂�timing

    public override void UpdateState()
    {
        c++;
        //�҂�������
        if(!m_HitotumeProto.GetSetGround.m_bStand)
        {
            m_HitotumeProto.GetSetEnemyState = EnemyState.Drop;
        }
        //�҂����ړ�
        if (c > DashTiming)
        {
            m_HitotumeProto.GetSetEnemyState = EnemyState.Walk;
            c = 0;
        }
    }
    public override void UpdateStrategy()
    {

    }
}
