/**
 * @file   PlayerAtk02.cs
 * @brief  Player�́u�U��03�v�N���X
 * @author IharaShota
 * @date   2023/10/27
 * @Update 2023/10/27 �쐬
 **/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class PlayerAtk03 : PlayerStrategy
{
    [SerializeField] private Vector3 m_vAtkArea;
    [SerializeField] private float m_fLength;
    [SerializeField] private float m_fAtkTime;
    [SerializeField] private VisualEffect atkEffect;
    private int atknum = -1;
    private float m_fTime;

    public override void UpdateState()
    {
        // �A�j���[�V�������I�������
        if (ObjPlayer.instance.Anim.GetAnimNormalizeTime(PlayerAnimState.Atk03,1f))
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

        // �U������
        if (atknum != -1)
        {
            // ���W�X�V
            if (ObjPlayer.instance.GetSetDir == ObjDir.RIGHT)
                ON_HitManager.instance.SetCenter(atknum, ObjPlayer.instance.GetSetPos + new Vector3(m_fLength, 0f, 0f));
            else if (ObjPlayer.instance.GetSetDir == ObjDir.LEFT)
                ON_HitManager.instance.SetCenter(atknum, ObjPlayer.instance.GetSetPos - new Vector3(m_fLength, 0f, 0f));

            // �U�����Ԃ��I�������
            if (m_fTime <= 0)
            {
                // �U���̓����蔻��폜
                ON_HitManager.instance.DeleteHit(atknum);

                // �U���̓����蔻��ID��-1�ɂ���
                atknum = -1;

                m_fTime = 0;
            }
            else m_fTime -= Time.deltaTime;
        }

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
        ObjPlayer.instance.Anim.ChangeAnim(PlayerAnimState.Atk02);

        // �U���̓����蔻�萶��(�����ɂ���Đ����ʒu���ς��)
        if (ObjPlayer.instance.GetSetDir == ObjDir.RIGHT)
        {
            atknum = ON_HitManager.instance.GenerateHit(ObjPlayer.instance.GetSetPos + new Vector3(m_fLength, 0f, 0f),
            m_vAtkArea, true, HitType.ATTACK, ObjPlayer.instance.GetSetObjID);

            ObjPlayer.instance.GetSetSpeed += new Vector2(5f, 0f);

            // �G�t�F�N�g�Đ�
            Invoke(nameof(StartHitEffect), 0.3f);
            atkEffect.transform.position = this.gameObject.transform.position + new Vector3(0.4f,0.5f,0.0f);
            atkEffect.transform.localScale = new Vector3(-1f, 1f, 1f);
        }
        else if (ObjPlayer.instance.GetSetDir == ObjDir.LEFT)
        {
            atknum = ON_HitManager.instance.GenerateHit(ObjPlayer.instance.GetSetPos - new Vector3(m_fLength, 0f, 0f),
            m_vAtkArea, true, HitType.ATTACK, ObjPlayer.instance.GetSetObjID);

            ObjPlayer.instance.GetSetSpeed += new Vector2(-5f, 0f);

            // �G�t�F�N�g�Đ�
            Invoke(nameof(StartHitEffect), 0.3f);
            atkEffect.transform.position = this.gameObject.transform.position + new Vector3(-0.4f, 0.5f, 0.0f); ;
            atkEffect.transform.localScale = new Vector3(1f, 1f, 1f);
        }

        // �����蔻�莞�ԃZ�b�g
        m_fTime = m_fAtkTime;

        // �U��SE���Đ�
        AudioManager.instance.PlaySE(SEType.SE_PlayerAtk);

        // �J�ڍŏ��̃t���O��off
        m_bStartFlg = false;
    }

    public override void EndState()
    {
        // �J�ڍŏ��̃t���O��ON�ɂ��Ă���
        m_bStartFlg = true;
    }

    public void StartHitEffect()
    {
        atkEffect.Play();
    }
}
