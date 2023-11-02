/**
 * @file   PlayerIdle.cs
 * @brief  Playerの「移動」クラス
 * @author IharaShota
 * @date   2023/10/27
 * @Update 2023/10/27 作成
 **/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWalk : PlayerStrategy
{
    public override void UpdateState()
    {
        // 移動 → 落下
        if (!ObjPlayer.instance.GetSetGround.m_bStand)
        {
            ObjPlayer.instance.m_PlayerState = PlayerState.Drop;
            return;
        }
        // 移動 → 待ち
        if ((IS_XBoxInput.LStick_H <= 0.2f && IS_XBoxInput.LStick_H >= -0.2f) &&
            (ObjPlayer.instance.GetSetSpeed.x > -0.1f && ObjPlayer.instance.GetSetSpeed.x < 0.1f))
        {
            ObjPlayer.instance.m_PlayerState = PlayerState.Idle;
            return;
        }
        // 移動 → 跳躍
        if (Input.GetKeyDown(IS_XBoxInput.A))
        {
            ObjPlayer.instance.m_PlayerState = PlayerState.Jump;
            ObjPlayer.instance.GetSetSpeed = new Vector2(ObjPlayer.instance.GetSetSpeed.x, 0.7f);
            ObjPlayer.instance.GetSetGround.m_bStand = false;
            return;
        }
        // 移動 → 攻撃
        if (Input.GetKey(IS_XBoxInput.B))
        {
            ObjPlayer.instance.m_PlayerState = PlayerState.Atk;
            ObjPlayer.m_bAtkFlg = true;
            return;
        }
    }

    public override void UpdatePlayer()
    {
        // 速度はスティックの方向け具合で決まる
        // 
        if (IS_XBoxInput.LStick_H > 0.2f || IS_XBoxInput.LStick_H < -0.2f)
        {
            ObjPlayer.instance.GetSetSpeed
            += new Vector2(IS_XBoxInput.LStick_H * ObjPlayer.instance.GetSetAccel, 0f);
        }
        else ObjPlayer.instance.GetSetSpeed *= new Vector2(0.8f, 1f);
    }
}
