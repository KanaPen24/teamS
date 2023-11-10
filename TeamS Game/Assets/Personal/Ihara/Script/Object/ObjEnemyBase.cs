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
    Jump,       //����
    KnockBack,  //�m�b�N�o�b�N
    Atk,        //�U��
    Death       //���S
}

//�C���X�y�N�^�[�Ńm�b�N�o�b�N�p�̕ϐ��������悤��
[System.Serializable]
public class KnockBack
{
    [HideInInspector]
    public Vector2 m_vSpeed;     // ���݂̑��x
    public Vector2 m_vInitSpeed; //�������x
    public float m_fDamping;     //������
}


public class ObjEnemyBase : ObjBase
{
    [SerializeField] protected KnockBack knockBack;
    [SerializeField] protected EnemyState m_EnemyState;
    public override void UpdateObj()
    {
    }

    public override void UpdateDebug()
    {
        //Debug.Log("EnemyBase");
    }
    public override void DamageAttack()
    {
        //�U�������������m�b�N�o�b�N����
        //KnockBackObj();
    }

    public override void KnockBackObj(ObjDir dir)
    {
        //�m�b�N�o�b�N���Ȃ�
        if (m_EnemyState == EnemyState.KnockBack)
        {
            //�A���̏ꍇ
            //GetSetSpeed = knockBack.m_fSpeed * 0.2f;
            if (dir == ObjDir.RIGHT)
                GetSetSpeed += knockBack.m_vInitSpeed;
            else if (dir == ObjDir.LEFT)
                GetSetSpeed += new Vector2(-knockBack.m_vInitSpeed.x, knockBack.m_vInitSpeed.y);
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
        // �I�u�W�F�N�g�̃^�C�v���uFIELD�v��������
        // �n�ʂɗ����Ă����Ԃɂ��� �� �������x��0�ŏI��
        if (m_EnemyState == EnemyState.KnockBack && m_Ground.m_bStand) 
        {
            //��������
            GetSetSpeed = 
                new Vector2(knockBack.m_vSpeed.x * knockBack.m_fDamping, knockBack.m_vSpeed.y * knockBack.m_fDamping);
            knockBack.m_vSpeed = GetSetSpeed;
            //�e�ނ̂�0.1f�ȉ��ɂȂ�����
            if (GetSetSpeed.y <= 0.1f)
            {   
                GetSetSpeed = Vector2.zero;     //�~�߂�
                //if(�̗͂��[���Ȃ�)
                m_EnemyState = EnemyState.Idle;
            }
            m_Ground.m_bStand = false;
            return;
        }

        // �����ݗ����Ă���n�ʂ𗣂ꂽ��c
        if (GetSetPos.x + GetSetScale.x / 2f < m_Ground.m_vCenter.x - (m_Ground.m_vSize.x / 2f) ||
            GetSetPos.x - GetSetScale.x / 2f > m_Ground.m_vCenter.x + (m_Ground.m_vSize.x / 2f))
            m_Ground.m_bStand = false;

        // �n�ʂɂ��Ă��Ȃ�������A������
        if (!GetSetGround.m_bStand)
        {
            if (GameManager.IsDebug())
                Debug.Log("�n�ʂ��痣�ꂽ ObjID: " + m_nObjID);

            m_vSpeed.y -= 0.05f;
        }        
    }

    public EnemyState GetSetEnemyState
    {
         get { return m_EnemyState; }
         set { m_EnemyState = value; } 
    }
}
