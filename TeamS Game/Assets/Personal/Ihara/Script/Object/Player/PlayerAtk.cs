/**
 * @file   PlayerIdle.cs
 * @brief  Player�́u�U���v�N���X
 * @author IharaShota
 * @date   2023/10/27
 * @Update 2023/10/27 �쐬
 **/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class PlayerAtk : PlayerStrategy
{
    [SerializeField] private Vector3 m_vAtkArea;
    [SerializeField] private float m_fLength;
    public VisualEffect atkEffect;
    private int atknum = -1;

    public override void UpdateState()
    {
        if (ObjPlayer.instance.Anim.GetAnimNormalizeTime(PlayerAnimState.Atk,0.5f))
        {
            // �U�� �� �҂�
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

        //if (HitStop.instance.hitStopState == HitStopState.ON)
        if (ObjPlayer.instance.GetSetHitStopParam.m_bHitStop)
        {
            atkEffect.playRate = 0.1f;
        }
        else atkEffect.playRate = 1f;

        // ���x�͌���(��)
        ObjPlayer.instance.GetSetSpeed *= new Vector2(0.8f, 1f);
    }

    public override void StartState()
    {
        // �A�j���\�V�����ύX
        ObjPlayer.instance.Anim.ChangeAnim(PlayerAnimState.Atk);

        // �U���̓����蔻�萶��(�����ɂ���Đ����ʒu���ς��)
        if (ObjPlayer.instance.GetSetDir == ObjDir.RIGHT)
        {
            atknum = ON_HitManager.instance.GenerateHit(ObjPlayer.instance.GetSetPos + new Vector3(m_fLength, 0f, 0f),
            m_vAtkArea, true, HitType.ATTACK, ObjPlayer.instance.GetSetObjID);

            ObjPlayer.instance.GetSetSpeed += new Vector2(5f, 0f);

            // �G�t�F�N�g�Đ�
            atkEffect.Play();
            atkEffect.transform.position = this.gameObject.transform.position;
            atkEffect.transform.localScale = new Vector3(-1f, 1f, 1f);
        }
        else if (ObjPlayer.instance.GetSetDir == ObjDir.LEFT)
        {
            atknum = ON_HitManager.instance.GenerateHit(ObjPlayer.instance.GetSetPos - new Vector3(m_fLength, 0f, 0f),
            m_vAtkArea, true, HitType.ATTACK, ObjPlayer.instance.GetSetObjID);

            ObjPlayer.instance.GetSetSpeed += new Vector2(-5f, 0f);

            // �G�t�F�N�g�Đ�
            atkEffect.Play();
            atkEffect.transform.position = this.gameObject.transform.position;
            atkEffect.transform.localScale = new Vector3(1f, 1f, 1f);
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
