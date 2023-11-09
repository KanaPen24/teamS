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
    public ParticleSystem jumpEffect;
    public override void UpdateState()
    {
        // 跳躍 → 落下
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
            jumpEffect.Play();
            jumpEffect.transform.position = ObjPlayer.instance.GetSetPos + Vector3.down * 1.5f;
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
