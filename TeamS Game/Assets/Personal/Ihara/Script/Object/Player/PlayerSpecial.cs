/**
 * @file   PlayerSpecial.cs
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
    [SerializeField] private Vector3 m_vSpecialArea;
    [SerializeField] private float m_fLength;
    private int specialNum;

    public override void UpdateState()
    {
        //if (m_fTime <= 0 && !ObjPlayer.m_bAtkFlg)
        if (ObjPlayer.instance.Anim.GetAnimNormalizeTime(PlayerAnimState.Atk, 1f) && !ObjPlayer.m_bAtkFlg)
        {
            // 必殺 → 待ち
            if (ObjPlayer.instance.GetSetGround.m_bStand)
            {
                ObjPlayer.instance.m_PlayerState = PlayerState.Idle;
                ON_HitManager.instance.DeleteHit(specialNum);
                return;
            }
        }
    }

    public override void UpdatePlayer()
    {
        // アニメ―ション
        ObjPlayer.instance.Anim.ChangeAnim(PlayerAnimState.Atk);

        if (ObjPlayer.m_bSpecialFlg)
        {
            // 攻撃の当たり判定生成(向きによって生成位置が変わる)
            if (ObjPlayer.instance.GetSetDir == ObjDir.RIGHT)
            {
                specialNum = ON_HitManager.instance.GenerateHit(ObjPlayer.instance.GetSetPos + new Vector3(m_fLength, 0f, 0f),
                m_vSpecialArea, true, HitType.ATTACK, ObjPlayer.instance.GetSetObjID);

                ObjPlayer.instance.GetSetSpeed += new Vector2(5f, 0f);
            }
            else if (ObjPlayer.instance.GetSetDir == ObjDir.LEFT)
            {
                specialNum = ON_HitManager.instance.GenerateHit(ObjPlayer.instance.GetSetPos - new Vector3(m_fLength, 0f, 0f),
                m_vSpecialArea, true, HitType.ATTACK, ObjPlayer.instance.GetSetObjID);

                ObjPlayer.instance.GetSetSpeed += new Vector2(-5f, 0f);
            }

            AudioManager.instance.PlaySE(SEType.SE_PlayerAtk);
            ObjPlayer.m_bSpecialFlg = false;
        }
        // 速度は減速(仮)
        ObjPlayer.instance.GetSetSpeed *= new Vector2(0f, 0f);
    }
}
