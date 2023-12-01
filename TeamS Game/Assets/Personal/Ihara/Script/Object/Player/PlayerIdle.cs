/**
 * @file   PlayerIdle.cs
 * @brief  Player�́u�҂��v�N���X
 * @author IharaShota
 * @date   2023/10/27
 * @Update 2023/10/27 �쐬
 **/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdle : PlayerStrategy
{
    public override void UpdateState()
    {
        // ----- ��ԑJ�� -----
        // �҂� �� ����
        if (!ObjPlayer.instance.GetSetGround.m_bStand)
        {
            ObjPlayer.instance.m_PlayerState = PlayerState.Drop;
            EndState();
            return;
        }
        // �҂� �� �ړ�
        if (IS_XBoxInput.LStick_H > 0.2f || IS_XBoxInput.LStick_H < -0.2f)
        {
            ObjPlayer.instance.m_PlayerState = PlayerState.Walk;
            EndState();
            return;
        }
        // �҂� �� ����
        if (Input.GetKeyDown(IS_XBoxInput.A))
        {
            ObjPlayer.instance.m_PlayerState = PlayerState.Jump;
            EndState();
            return;
        }
        // �҂� �� �U��
        if (Input.GetKeyDown(IS_XBoxInput.X))
        {
            ObjPlayer.instance.m_PlayerState = PlayerState.Atk;
            EndState();
            return;
        }
        // �҂� �� �K�E
        if (Input.GetKeyDown(IS_XBoxInput.Y))
        {
            ObjPlayer.instance.m_PlayerState = PlayerState.Special;
            EndState();
            return;
        }
    }

    public override void UpdatePlayer()
    {
        // �J�ڍŏ��̏���
        if (m_bStartFlg) StartState();

        // ���x��0�Ɉ��
        ObjPlayer.instance.GetSetSpeed = new Vector2(0f, 0f);
    }

    public override void StartState()
    {
        // �A�j���\�V�����ύX
        ObjPlayer.instance.Anim.ChangeAnim(PlayerAnimState.Idle);

        // �J�ڍŏ��̃t���O��off
        m_bStartFlg = false;
    }

    public override void EndState()
    {
        // �J�ڍŏ��̃t���O��ON�ɂ��Ă���
        m_bStartFlg = true;
    }
}
