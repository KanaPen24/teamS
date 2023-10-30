/**
 * @file   PlayerIdle.cs
 * @brief  Playerの「落下」クラス
 * @author IharaShota
 * @date   2023/10/27
 * @Update 2023/10/27 作成
 **/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDrop : PlayerStrategy
{
    public override void UpdateInput()
    {
        // 落下 → 待ち
        if (ObjPlayer.instance.GetSetGround.GetSetStand)
        {
            ObjPlayer.instance.m_PlayerState = PlayerState.Idle;
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
