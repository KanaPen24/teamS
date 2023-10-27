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
    public override void UpdateInput()
    {
        // �ړ� �� ����
        if (!ObjPlayer.instance.GetSetGround.GetSetStand)
        {
            ObjPlayer.instance.m_PlayerState = PlayerState.Drop;
            return;
        }
        // �ړ� �� �҂�
        if (IS_XBoxInput.LStick_H <= 0.2f && IS_XBoxInput.LStick_H >= -0.2f)
        {
            ObjPlayer.instance.m_PlayerState = PlayerState.Idle;
            return;
        }
        // �ړ� �� ����
        if (Input.GetKeyDown(IS_XBoxInput.A))
        {
            ObjPlayer.instance.m_PlayerState = PlayerState.Jump;
            ObjPlayer.instance.GetSetSpeed = new Vector2(ObjPlayer.instance.GetSetSpeed.x, 0.7f);
            ObjPlayer.instance.GetSetGround.GetSetStand = false;
            return;
        }
    }

    public override void UpdatePlayer()
    {
        // ���x�̓X�e�B�b�N�̕�������Ō��܂�
        ObjPlayer.instance.GetSetSpeed
            = new Vector2(IS_XBoxInput.LStick_H * ObjPlayer.instance.GetSetAccel, 0f);
    }
}
