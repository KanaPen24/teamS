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
        // ----- 状態遷移 -----
        // 待ち → 落下
        if (!ObjPlayer.instance.GetSetGround.m_bStand)
        {
            ObjPlayer.instance.m_PlayerState = PlayerState.Drop;
            EndState();
            return;
        }
        // 待ち → 移動
        if (IS_XBoxInput.LStick_H > 0.2f || IS_XBoxInput.LStick_H < -0.2f)
        {
            ObjPlayer.instance.m_PlayerState = PlayerState.Walk;
            EndState();
            return;
        }
        // 待ち → 跳躍
        if (Input.GetKeyDown(IS_XBoxInput.A))
        {
            ObjPlayer.instance.m_PlayerState = PlayerState.Jump;
            EndState();
            return;
        }
        // 待ち → 攻撃
        if (Input.GetKeyDown(IS_XBoxInput.X))
        {
            ObjPlayer.instance.m_PlayerState = PlayerState.Atk;
            EndState();
            return;
        }
        // 待ち → 必殺
        if (Input.GetKeyDown(IS_XBoxInput.Y))
        {
            ObjPlayer.instance.m_PlayerState = PlayerState.Special;
            EndState();
            return;
        }
    }

    public override void UpdatePlayer()
    {
        // 遷移最初の処理
        if (m_bStartFlg) StartState();

        // 速度は0に一定
        ObjPlayer.instance.GetSetSpeed = new Vector2(0f, 0f);
    }

    public override void StartState()
    {
        // アニメ―ション変更
        ObjPlayer.instance.Anim.ChangeAnim(PlayerAnimState.Idle);

        // 遷移最初のフラグをoff
        m_bStartFlg = false;
    }

    public override void EndState()
    {
        // 遷移最初のフラグをONにしておく
        m_bStartFlg = true;
    }
}
