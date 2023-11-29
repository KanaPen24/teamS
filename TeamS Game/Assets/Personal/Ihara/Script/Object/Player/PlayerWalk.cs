/**
 * @file   PlayerIdle.cs
 * @brief  Player�́u�ړ��v�N���X
 * @author IharaShota
 * @date   2023/10/27
 * @Update 2023/10/27 �쐬
 **/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWalk : PlayerStrategy
{
    public override void UpdateState()
    {
        // �ړ� �� ����
        if (!ObjPlayer.instance.GetSetGround.m_bStand)
        {
            ObjPlayer.instance.m_PlayerState = PlayerState.Drop;
            AudioManager.instance.StopSE(SEType.SE_PlayerWalk);
            ObjPlayer.m_bDropFlg = true;
            return;
        }
        // �ړ� �� �҂�
        if ((IS_XBoxInput.LStick_H <= 0.2f && IS_XBoxInput.LStick_H >= -0.2f) &&
            (ObjPlayer.instance.GetSetSpeed.x > -0.1f && ObjPlayer.instance.GetSetSpeed.x < 0.1f))
        {
            ObjPlayer.instance.m_PlayerState = PlayerState.Idle;
            AudioManager.instance.StopSE(SEType.SE_PlayerWalk);
            return;
        }
        // �ړ� �� ����
        if (Input.GetKeyDown(IS_XBoxInput.A))
        {
            ObjPlayer.instance.m_PlayerState = PlayerState.Jump;
            AudioManager.instance.StopSE(SEType.SE_PlayerWalk);
            ObjPlayer.instance.GetSetSpeed = new Vector2(ObjPlayer.instance.GetSetSpeed.x, 0.7f);
            ObjPlayer.instance.GetSetGround.m_bStand = false;
            ObjPlayer.m_bJumpFlg = true;
            return;
        }
        // �ړ� �� �U��
        if (Input.GetKeyDown(IS_XBoxInput.X))
        {
            ObjPlayer.instance.m_PlayerState = PlayerState.Atk;
            AudioManager.instance.StopSE(SEType.SE_PlayerWalk);
            ObjPlayer.m_bAtkFlg = true;
            return;
        }
    }

    public override void UpdatePlayer()
    {
        // �A�j���\�V����
        ObjPlayer.instance.Anim.ChangeAnim(PlayerAnimState.Walk);

        if (ObjPlayer.m_bWalkFlg)
        {
            AudioManager.instance.PlaySE(SEType.SE_PlayerWalk);
            AudioManager.instance.GetSE(SEType.SE_PlayerWalk).loop = true;
            ObjPlayer.m_bWalkFlg = false;
        }

        // ���x�̓X�e�B�b�N�̕�������Ō��܂�
        if (IS_XBoxInput.LStick_H > 0.2f || IS_XBoxInput.LStick_H < -0.2f)
        {
            ObjPlayer.instance.GetSetSpeed
            += new Vector2(IS_XBoxInput.LStick_H * ObjPlayer.instance.GetSetAccel, 0f);
        }
        else ObjPlayer.instance.GetSetSpeed *= new Vector2(0.8f, 1f);
    }
}
