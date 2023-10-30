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
    public override void UpdateInput()
    {
        // ���� �� �҂�
        if (ObjPlayer.instance.GetSetGround.GetSetStand)
        {
            ObjPlayer.instance.m_PlayerState = PlayerState.Idle;
            return;
        }
    }

    public override void UpdatePlayer()
    {
        // ���x�̓X�e�B�b�N�̕�������Ō��܂�
        ObjPlayer.instance.GetSetSpeed
            = new Vector2(IS_XBoxInput.LStick_H * ObjPlayer.instance.GetSetAccel / 2f, 
                            ObjPlayer.instance.GetSetSpeed.y);
    }
}