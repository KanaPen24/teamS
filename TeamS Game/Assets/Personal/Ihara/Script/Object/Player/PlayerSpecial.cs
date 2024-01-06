/**
 * @file   PlayerIdle.cs
 * @brief  Player�́u�K�E�v�N���X
 * @author IharaShota
 * @date   2023/11/30
 * @Update 2023/11/30 �쐬
 **/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpecial : PlayerStrategy
{
    [SerializeField] private Vector3 m_vAtkArea;
    [SerializeField] private float m_fLength;
    private int atknum = -1;

    public override void UpdateState()
    {
        if (ObjPlayer.instance.Anim.GetAnimNormalizeTime(PlayerAnimState.Special, 1f))
        {
            // �K�E �� �҂�
            if (ObjPlayer.instance.GetSetGround.m_bStand)
            {
                ObjPlayer.instance.m_PlayerState = PlayerState.Idle;
                EndState();
                return;
            }
        }
    }

    public override void UpdatePlayer()
    {
        // �J�ڍŏ��̏���
        if (m_bStartFlg) StartState();

        // �U�����͍��W�X�V
        if (atknum != -1)
        {
            if (ObjPlayer.instance.GetSetDir == ObjDir.RIGHT)
                ON_HitManager.instance.SetCenter(atknum, ObjPlayer.instance.GetSetPos + new Vector3(m_fLength, 0f, 0f));
            else if (ObjPlayer.instance.GetSetDir == ObjDir.LEFT)
                ON_HitManager.instance.SetCenter(atknum, ObjPlayer.instance.GetSetPos - new Vector3(m_fLength, 0f, 0f));
        }

        // ���x�͌���(��)
        ObjPlayer.instance.GetSetSpeed *= new Vector2(0.8f, 1f);
    }

    public override void StartState()
    {
        // �A�j���\�V�����ύX
        ObjPlayer.instance.Anim.ChangeAnim(PlayerAnimState.Special);

        // �U���̓����蔻�萶��(�����ɂ���Đ����ʒu���ς��)
        if (ObjPlayer.instance.GetSetDir == ObjDir.RIGHT)
        {
            atknum = ON_HitManager.instance.GenerateHit(ObjPlayer.instance.GetSetPos + new Vector3(m_fLength, 0f, 0f),
            m_vAtkArea, true, HitType.SPECIAL, ObjPlayer.instance.GetSetObjID);

            ObjPlayer.instance.GetSetSpeed += new Vector2(5f, 0f);
        }
        else if (ObjPlayer.instance.GetSetDir == ObjDir.LEFT)
        {
            atknum = ON_HitManager.instance.GenerateHit(ObjPlayer.instance.GetSetPos - new Vector3(m_fLength, 0f, 0f),
            m_vAtkArea, true, HitType.SPECIAL, ObjPlayer.instance.GetSetObjID);

            ObjPlayer.instance.GetSetSpeed += new Vector2(-5f, 0f);
        }

        // �U��SE���Đ�
        AudioManager.instance.PlaySE(SEType.SE_PlayerAtk);

        // �J�ڍŏ��̃t���O��off
        m_bStartFlg = false;
    }

    public override void EndState()
    {
        // �U���̓����蔻��폜
        ON_HitManager.instance.DeleteHit(atknum);

        // �U���̓����蔻��ID��-1�ɂ���
        atknum = -1;

        // �J�ڍŏ��̃t���O��ON�ɂ��Ă���
        m_bStartFlg = true;
    }
}
