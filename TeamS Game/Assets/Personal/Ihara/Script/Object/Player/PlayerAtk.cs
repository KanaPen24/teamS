/**
 * @file   PlayerIdle.cs
 * @brief  PlayerฬuUvNX
 * @author IharaShota
 * @date   2023/10/27
 * @Update 2023/10/27 ์ฌ
 **/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAtk : PlayerStrategy
{
    public float m_fInterval;

    public override void UpdateInput()
    {
        m_fInterval -= Time.deltaTime;
        if (m_fInterval > 0) return;
        else m_fInterval = 0;

        // U จ าฟ
        if (ObjPlayer.instance.GetSetGround.GetSetStand)
        {
            ObjPlayer.instance.m_PlayerState = PlayerState.Idle;
            return;
        }
    }

    public override void UpdatePlayer()
    {
        if(ObjPlayer.m_bAtkFlg)
        {
            if (GameManager.IsDebug())
                Debug.Log("HitGenerate");

            ON_HitManager.instance.GenerateHit(ObjPlayer.instance.GetSetPos + new Vector3(1f, 0f, 0f),
                ObjPlayer.instance.GetSetScale / 2f, true, HitType.ATTACK, ObjPlayer.instance.GetSetObjID);

            ObjPlayer.m_bAtkFlg = false;
        }

        // ฌxอ0(ผ)
        ObjPlayer.instance.GetSetSpeed
            = new Vector2(0f,0f);
    }
}
