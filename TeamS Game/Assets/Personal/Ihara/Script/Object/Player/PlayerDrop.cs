/**
 * @file   PlayerIdle.cs
 * @brief  Player�́u�����v�N���X
 * @author IharaShota
 * @date   2023/10/27
 * @Update 2023/10/27 �쐬
 **/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDrop : PlayerStrategy
{
    public ParticleSystem landingEffect;
    public override void UpdateState()
    {
        // ���� �� �҂�
        if (ObjPlayer.instance.GetSetGround.m_bStand)
        {
            ObjPlayer.instance.m_PlayerState = PlayerState.Idle;
            EndState();
            return;
        }
    }

    public override void UpdatePlayer()
    {
        // �J�ڍŏ��̏���
        if (m_bStartFlg) StartState();

        // ���x�̓X�e�B�b�N�̌X����Ō��܂�(�X���Ă��Ȃ��ꍇ�͌������Ă���)
        // X���ړ�
        if (IS_XBoxInput.LStick_H > 0.2f || IS_XBoxInput.LStick_H < -0.2f)
        {
            ObjPlayer.instance.GetSetSpeed
            += new Vector2(IS_XBoxInput.LStick_H * ObjPlayer.instance.GetSetAccel / 2f, 0f);
        }
        else ObjPlayer.instance.GetSetSpeed *= new Vector2(0.8f, 1f);
    }

    public override void StartState()
    {
        // �A�j���\�V�����ύX
        ObjPlayer.instance.Anim.ChangeAnim(PlayerAnimState.Drop);

        // �J�ڍŏ��̃t���O��off
        m_bStartFlg = false;
    }

    public override void EndState()
    {
        // ���nSE���Đ�
        AudioManager.instance.PlaySE(SEType.SE_PlayerLanding);
        
        // ���n�G�t�F�N�g���Đ�
        landingEffect.Play();
        landingEffect.transform.position = ObjPlayer.instance.GetSetPos + Vector3.down * 1.5f;

        // �J�ڍŏ��̃t���O��ON�ɂ��Ă���
        m_bStartFlg = true;
    }
}
