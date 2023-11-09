/**
 * @file   PlayerIdle.cs
 * @brief  Playerの「待ち」クラス
 * @author IharaShota
 * @date   2023/10/27
 * @Update 2023/10/27 作成
 **/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdle : PlayerStrategy
{
    public override void UpdateState()
    {
        // 待ち → 落下
        if (!ObjPlayer.instance.GetSetGround.m_bStand)
        {
            ObjPlayer.instance.m_PlayerState = PlayerState.Drop;
            ObjPlayer.m_bDropFlg = true;
            return;
        }
        // 待ち → 移動
        if (IS_XBoxInput.LStick_H > 0.2f || IS_XBoxInput.LStick_H < -0.2f)
        {
            ObjPlayer.instance.m_PlayerState = PlayerState.Walk;
            ObjPlayer.m_bWalkFlg = true;
            return;
        }
        // 待ち → 跳躍
        if (Input.GetKeyDown(IS_XBoxInput.A))
        {
            ObjPlayer.instance.m_PlayerState = PlayerState.Jump;
            ObjPlayer.instance.GetSetSpeed = new Vector2(ObjPlayer.instance.GetSetSpeed.x, 0.7f);
            ObjPlayer.instance.GetSetGround.m_bStand = false;
            ObjPlayer.m_bJumpFlg = true;
            return;
        }
        // 移動 → 攻撃
        if (Input.GetKeyDown(IS_XBoxInput.B))
        {
            ObjPlayer.instance.m_PlayerState = PlayerState.Atk;
            ObjPlayer.m_bAtkFlg = true;
            return;
        }
    }

    public override void UpdatePlayer()
    {
        // 速度は0に一定
        ObjPlayer.instance.GetSetSpeed = new Vector2(0f, 0f);
    }
}
