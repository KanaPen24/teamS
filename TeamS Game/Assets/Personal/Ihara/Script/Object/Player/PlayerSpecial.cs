/**
 * @file   PlayerIdle.cs
 * @brief  Playerの「必殺」クラス
 * @author IharaShota
 * @date   2023/11/30
 * @Update 2023/11/30 作成
 **/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpecial : PlayerStrategy
{
    [SerializeField] private Vector3 m_vAtkArea;
    [SerializeField] private float m_fLength;
    private int atknum = -1;

    public override void UpdateState()
    {
        if (ObjPlayer.instance.Anim.GetAnimNormalizeTime(PlayerAnimState.Special, 1f))
        {
            // 必殺 → 待ち
            if (ObjPlayer.instance.GetSetGround.m_bStand)
            {
                ObjPlayer.instance.m_PlayerState = PlayerState.Idle;
                EndState();
                return;
            }
        }
    }

    public override void UpdatePlayer()
    {
        // 遷移最初の処理
        if (m_bStartFlg) StartState();

        // 攻撃中は座標更新
        if (atknum != -1)
        {
            if (ObjPlayer.instance.GetSetDir == ObjDir.RIGHT)
                ON_HitManager.instance.SetCenter(atknum, ObjPlayer.instance.GetSetPos + new Vector3(m_fLength, 0f, 0f));
            else if (ObjPlayer.instance.GetSetDir == ObjDir.LEFT)
                ON_HitManager.instance.SetCenter(atknum, ObjPlayer.instance.GetSetPos - new Vector3(m_fLength, 0f, 0f));
        }

        // 速度は減速(仮)
        ObjPlayer.instance.GetSetSpeed *= new Vector2(0.8f, 1f);
    }

    public override void StartState()
    {
        // アニメ―ション変更
        ObjPlayer.instance.Anim.ChangeAnim(PlayerAnimState.Special);

        // 攻撃の当たり判定生成(向きによって生成位置が変わる)
        if (ObjPlayer.instance.GetSetDir == ObjDir.RIGHT)
        {
            atknum = ON_HitManager.instance.GenerateHit(ObjPlayer.instance.GetSetPos + new Vector3(m_fLength, 0f, 0f),
            m_vAtkArea, true, HitType.SPECIAL, ObjPlayer.instance.GetSetObjID);

            ObjPlayer.instance.GetSetSpeed += new Vector2(5f, 0f);
        }
        else if (ObjPlayer.instance.GetSetDir == ObjDir.LEFT)
        {
            atknum = ON_HitManager.instance.GenerateHit(ObjPlayer.instance.GetSetPos - new Vector3(m_fLength, 0f, 0f),
            m_vAtkArea, true, HitType.SPECIAL, ObjPlayer.instance.GetSetObjID);

            ObjPlayer.instance.GetSetSpeed += new Vector2(-5f, 0f);
        }

        // 攻撃SEを再生
        AudioManager.instance.PlaySE(SEType.SE_PlayerAtk);

        // 遷移最初のフラグをoff
        m_bStartFlg = false;
    }

    public override void EndState()
    {
        // 攻撃の当たり判定削除
        ON_HitManager.instance.DeleteHit(atknum);

        // 攻撃の当たり判定IDを-1にする
        atknum = -1;

        // 遷移最初のフラグをONにしておく
        m_bStartFlg = true;
    }
}
