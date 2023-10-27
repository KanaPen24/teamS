/**
 * @file   PlayerIdle.cs
 * @brief  Playerの「跳躍」クラス
 * @author IharaShota
 * @date   2023/10/27
 * @Update 2023/10/27 作成
 **/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : PlayerStrategy
{
    public override void UpdateInput()
    {
        // 跳躍 → 落下
        if (ObjPlayer.instance.GetSetSpeed.y < 0f)
        {
            ObjPlayer.instance.m_PlayerState = PlayerState.Drop;
            return;
        }
    }

    public override void UpdatePlayer()
    {
        // 速度はスティックの方向け具合で決まる
        ObjPlayer.instance.GetSetSpeed
            = new Vector2(IS_XBoxInput.LStick_H * ObjPlayer.instance.GetSetAccel / 2f, 
                            ObjPlayer.instance.GetSetSpeed.y);
    }
}
