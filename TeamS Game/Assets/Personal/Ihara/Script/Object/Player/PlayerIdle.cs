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
        // �҂� �� ����
        if (!ObjPlayer.instance.GetSetGround.m_bStand)
        {
            ObjPlayer.instance.m_PlayerState = PlayerState.Drop;
            ObjPlayer.m_bDropFlg = true;
            return;
        }
        // �҂� �� �ړ�
        if (IS_XBoxInput.LStick_H > 0.2f || IS_XBoxInput.LStick_H < -0.2f)
        {
            ObjPlayer.instance.m_PlayerState = PlayerState.Walk;
            ObjPlayer.m_bWalkFlg = true;
            return;
        }
        // �҂� �� ����
        if (Input.GetKeyDown(IS_XBoxInput.A))
        {
            ObjPlayer.instance.m_PlayerState = PlayerState.Jump;
            ObjPlayer.instance.GetSetSpeed = new Vector2(ObjPlayer.instance.GetSetSpeed.x, 0.7f);
            ObjPlayer.instance.GetSetGround.m_bStand = false;
            ObjPlayer.m_bJumpFlg = true;
            return;
        }
        // �ړ� �� �U��
        if (Input.GetKeyDown(IS_XBoxInput.B))
        {
            ObjPlayer.instance.m_PlayerState = PlayerState.Atk;
            ObjPlayer.m_bAtkFlg = true;
            return;
        }
    }

    public override void UpdatePlayer()
    {
        // ���x��0�Ɉ��
        ObjPlayer.instance.GetSetSpeed = new Vector2(0f, 0f);
    }
}
