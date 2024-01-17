/**
 * @file   HitotumeProto.cs
 * @brief  ��ڂ̃N���X
 * @author NaitoKoki
 * @date   2023/11/07
 * @Update 2023/11/07 �쐬
 **/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitotumeProto : ObjEnemyBase
{
    [SerializeField] private List<NK_HitotumeStrategy> m_HitotumeStrategy;
    private float m_localScalex;
    [SerializeField] private Animator m_Anim;
    [SerializeField] private GameObject m_buttobiSmog;
    private EnemyState OldEnemyState;
    [SerializeField] private float m_buttobiTime;

    private void Start()
    {
        m_localScalex = this.transform.localScale.x;
    }

    public override void UpdateObj()
    {
        // ��ʊO�ɂ��鎞�͍X�V���Ȃ��悤�ɂ���
        // ���m�b�N�o�b�N���͗�O
        //if(Mathf.Abs(ObjPlayer.instance.GetSetPos.x - GetSetPos.x) >= 12f &&
        //    GetSetEnemyState != EnemyState.KnockBack)
        //{
        //    m_vSpeed.x = 0f;
        //    return;
        //}
        if(ObjPlayer.instance.GetSetPos.x > GetSetPos.x)
        {
            GetSetDir = ObjDir.RIGHT;
            this.transform.localScale =
                  new Vector3(-m_localScalex, this.transform.localScale.y, this.transform.localScale.z);
        }
        else
        {
            GetSetDir = ObjDir.LEFT;
            this.transform.localScale =
                  new Vector3(m_localScalex, this.transform.localScale.y, this.transform.localScale.z);
        }
        if(GetSetEnemyState!=EnemyState.KnockBack)
        {
            m_Anim.SetBool("KnockFlag", false);
        }
        else
        {
            m_Anim.SetBool("KnockFlag", true);
            if(OldEnemyState!=EnemyState.KnockBack)
            {
                ButtobiStart();
                //AudioManager.instance.PlaySE(SEType.SE_EFly);
                AudioManager.instance.PlaySE(SEType.SE_PEAtkDamage);
            }
        }

        m_HitotumeStrategy[(int)m_EnemyState].UpdateState();
        m_HitotumeStrategy[(int)m_EnemyState].UpdateStrategy();
        CheckDivision();
        OldEnemyState = GetSetEnemyState;
        //m_vSpeed.x = 3.0f;
    }

    private void ButtobiStart()
    {
        m_buttobiSmog.SetActive(true);
        Invoke("ButtobiEnd", m_buttobiTime);
    }

    private void ButtobiEnd()
    {
        m_buttobiSmog.SetActive(false);
    }
}