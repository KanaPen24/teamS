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
            EndState();
            return;
        }
        // 跳躍 → 待ち
        if (ObjPlayer.instance.GetSetGround.m_bStand)
        {
            ObjPlayer.instance.m_PlayerState = PlayerState.Idle;
            EndState();
            return;
        }
    }

    public override void UpdatePlayer()
    {
        // 遷移最初の処理
        if (m_bStartFlg) StartState();

        if (IS_XBoxInput.LStick_H > 0.2f || IS_XBoxInput.LStick_H < -0.2f)
        {
            ObjPlayer.instance.GetSetSpeed
            += new Vector2(IS_XBoxInput.LStick_H * ObjPlayer.instance.GetSetAccel / 2f, 0f);
        }
        else ObjPlayer.instance.GetSetSpeed *= new Vector2(0.8f, 1f);
    }

    public override void StartState()
    {
        // アニメ―ション変更
        ObjPlayer.instance.Anim.ChangeAnim(PlayerAnimState.Jump);

        // 跳躍SEを再生
        AudioManager.instance.PlaySE(SEType.SE_PlayerJump);

        // 跳躍エフェクト
        jumpEffect.Play();
        jumpEffect.transform.position = ObjPlayer.instance.GetSetPos + Vector3.down * 1.5f;

        // 初速度を設定
        ObjPlayer.instance.GetSetSpeed = new Vector2(ObjPlayer.instance.GetSetSpeed.x, 0.7f);
        ObjPlayer.instance.GetSetGround.m_bStand = false;

        // 遷移最初のフラグをoff
        m_bStartFlg = false;
    }

    public override void EndState()
    {
        // 遷移最初のフラグをONにしておく
        m_bStartFlg = true;
    }
}
