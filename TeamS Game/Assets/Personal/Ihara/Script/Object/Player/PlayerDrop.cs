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
            EndState();
            return;
        }
    }

    public override void UpdatePlayer()
    {
        // 遷移最初の処理
        if (m_bStartFlg) StartState();

        // 速度はスティックの傾け具合で決まる(傾けていない場合は減速していく)
        // X軸移動
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
        ObjPlayer.instance.Anim.ChangeAnim(PlayerAnimState.Drop);

        // 遷移最初のフラグをoff
        m_bStartFlg = false;
    }

    public override void EndState()
    {
        // 着地SEを再生
        AudioManager.instance.PlaySE(SEType.SE_PlayerLanding);
        
        // 着地エフェクトを再生
        landingEffect.Play();
        landingEffect.transform.position = ObjPlayer.instance.GetSetPos + Vector3.down * 1.5f;

        // 遷移最初のフラグをONにしておく
        m_bStartFlg = true;
    }
}
