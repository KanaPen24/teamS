/**
 * @file   PlayerIdle.cs
 * @brief  PlayerΜu΅τvNX
 * @author IharaShota
 * @date   2023/10/27
 * @Update 2023/10/27 μ¬
 **/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : PlayerStrategy
{
    public override void UpdateState()
    {
        // ΅τ ¨ Ί
        if (ObjPlayer.instance.GetSetSpeed.y < 0f)
        {
            ObjPlayer.instance.m_PlayerState = PlayerState.Drop;
            ObjPlayer.m_bDropFlg = true;
            return;
        }
    }

    public override void UpdatePlayer()
    {
        if (ObjPlayer.m_bJumpFlg)
        {
            AudioManager.instance.PlaySE(SEType.SE_PlayerJump);
            ObjPlayer.m_bJumpFlg = false;
        }

        if (IS_XBoxInput.LStick_H > 0.2f || IS_XBoxInput.LStick_H < -0.2f)
        {
            ObjPlayer.instance.GetSetSpeed
            += new Vector2(IS_XBoxInput.LStick_H * ObjPlayer.instance.GetSetAccel / 2f, 0f);
        }
        else ObjPlayer.instance.GetSetSpeed *= new Vector2(0.8f, 1f);
    }
}
