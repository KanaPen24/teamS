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
    public ParticleSystem landingEffect;
    public override void UpdateState()
    {
        // 落下 → 待ち
        if (ObjPlayer.instance.GetSetGround.m_bStand)
        {
            ObjPlayer.instance.m_PlayerState = PlayerState.Idle;
            AudioManager.instance.PlaySE(SEType.SE_PlayerLanding);
            landingEffect.Play();
            landingEffect.transform.position = ObjPlayer.instance.GetSetPos + Vector3.down * 1.5f;
            return;
        }
    }

    public override void UpdatePlayer()
    {
        if (ObjPlayer.m_bDropFlg)
        {
            ObjPlayer.m_bDropFlg = false;
        }

        // 速度はスティックの方向け具合で決まる
        if (IS_XBoxInput.LStick_H > 0.2f || IS_XBoxInput.LStick_H < -0.2f)
        {
            ObjPlayer.instance.GetSetSpeed
            += new Vector2(IS_XBoxInput.LStick_H * ObjPlayer.instance.GetSetAccel / 2f, 0f);
        }
        else ObjPlayer.instance.GetSetSpeed *= new Vector2(0.8f, 1f);
    }
}
