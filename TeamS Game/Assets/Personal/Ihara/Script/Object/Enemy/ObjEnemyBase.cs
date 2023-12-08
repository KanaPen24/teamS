/**
 * @file   ObjEnemyBase.cs
 * @brief  �G�̊��N���X
 * @author IharaShota
 * @date   2023/10/13
 * @Update 2023/10/13 �쐬
 * @Update 2023/10/27 �m�b�N�o�b�N�̏����ǋL Kanase
 * @Update 2023/11/02 �m�b�N�o�b�N�̏����C�� Ihara
 **/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//�G�̏��
public enum EnemyState
{
    Idle,       //�ҋ@
    Walk,       //����
    Drop,       //������
    Jump,       //����
    KnockBack,  //�m�b�N�o�b�N
    Atk,        //�U��
    Death,      //���S
    RePop,      //����
}

//�C���X�y�N�^�[�Ńm�b�N�o�b�N�p�̕ϐ��������悤��
[System.Serializable]
public class KnockBack
{
    [HideInInspector]
    public Vector2 m_vSpeed;     // ���݂̑��x
    public Vector2 m_vInitSpeed; //�������x
    public float m_fDamping;     //������
    public float m_fGravity;     //�d��
}

// ���U�p�̃N���X
[System.Serializable]
public class Division
{
    [HideInInspector]
    public bool m_bDivisionTrigger;
    [HideInInspector]
    public float m_fDivisionTime;
    [HideInInspector]
    public float m_fBlinkTime;
    [HideInInspector]
    public float m_fMaxBlinkTime;
}


public class ObjEnemyBase : ObjBase
{
    [SerializeField] protected KnockBack knockBack;
    [SerializeField] protected EnemyState m_EnemyState;
    [HideInInspector]
    public Division m_division;

    public override void InitObj()
    {
        base.InitObj();
        m_division.m_bDivisionTrigger = false;
        m_division.m_fDivisionTime = 1.0f;
        m_division.m_fMaxBlinkTime = 0.1f;
        m_division.m_fBlinkTime = m_division.m_fMaxBlinkTime;
    }

    public override void UpdateObj()
    {
    }

    public override void DamageAttack()
    {
        //�U�������������m�b�N�o�b�N����
        //KnockBackObj();
    }

    public override void KnockBackObj(ObjDir dir = ObjDir.NONE)
    {
        //�m�b�N�o�b�N���Ȃ�
        if (m_EnemyState == EnemyState.KnockBack)
        {
            //�A���̏ꍇ
            GetSetSpeed = Vector2.zero; //���Z�b�g����
            if (dir == ObjDir.RIGHT)
                GetSetSpeed += new Vector2(knockBack.m_vInitSpeed.x * 0.53f, knockBack.m_vInitSpeed.y * 0.7f);
            else if (dir == ObjDir.LEFT)
                GetSetSpeed += new Vector2(-knockBack.m_vInitSpeed.x * 0.53f, knockBack.m_vInitSpeed.y * 0.7f);
            //���E�l�͉z���Ȃ�
            //if (GetSetMaxSpeed.x <= GetSetSpeed.x || GetSetMaxSpeed.y <= GetSetSpeed.y)
            //    GetSetSpeed = GetSetMaxSpeed;
            Debug.Log("�A������");
        }
        else
        {
            //�ŏ��̃m�b�N�o�b�N����
            m_EnemyState = EnemyState.KnockBack; //�m�b�N�o�b�N�ɕύX
            GetSetGround.m_bStand = false;       // �n�ʂɗ����Ă��Ȃ���Ԃɂ���

            // �����ɂ���ď����x��ݒ�
            if (dir == ObjDir.RIGHT)
                GetSetSpeed = knockBack.m_vInitSpeed;
            else if (dir == ObjDir.LEFT)
                GetSetSpeed = new Vector2(-knockBack.m_vInitSpeed.x, knockBack.m_vInitSpeed.y);
        }

        // ���݂̑��x���m�b�N�o�b�N�v�Z�p�̑��x�Ɋi�[(Ihara)
        knockBack.m_vSpeed = GetSetSpeed;
    }

    public override void CheckObjGround()
    {
        // �����ݗ����Ă���n�ʂ𗣂ꂽ��c
        if (GetSetPos.x + GetSetScale.x / 2f < m_Ground.m_vCenter.x - (m_Ground.m_vSize.x / 2f) ||
            GetSetPos.x - GetSetScale.x / 2f > m_Ground.m_vCenter.x + (m_Ground.m_vSize.x / 2f))
            m_Ground.m_bStand = false;

        // �n�ʂɂ��Ă��Ȃ�������A������
        if (!GetSetGround.m_bStand)
        {
            if (GameManager.IsDebug())
                Debug.Log("�n�ʂ��痣�ꂽ ObjID: " + m_nObjID);
            //�d�͂��g���ė��Ƃ�
            m_vSpeed.y -= knockBack.m_fGravity;
        }        
    }

    protected void CheckDivision()
    {
        if (m_division.m_bDivisionTrigger)
        {
            m_division.m_fDivisionTime -= Time.deltaTime;
            if (m_division.m_fDivisionTime <= 0f)
            {
                GetSetDestroy = true;
            }
            else UpdateDivision();
        }
    }

    private void UpdateDivision()
    {
        if (m_division.m_fBlinkTime <= 0f)
        {
            texObj.enabled = !texObj.enabled;
            m_division.m_fBlinkTime = m_division.m_fMaxBlinkTime;
        }
        else m_division.m_fBlinkTime -= Time.deltaTime;
    }

    public EnemyState GetSetEnemyState
    {
         get { return m_EnemyState; }
         set { m_EnemyState = value; } 
    }

    public KnockBack GetSetKnockBack
    {
        get { return knockBack; }
        set { knockBack = value; }
    }
}
