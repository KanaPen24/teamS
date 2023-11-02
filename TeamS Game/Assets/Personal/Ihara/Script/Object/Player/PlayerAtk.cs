/**
 * @file   PlayerIdle.cs
 * @brief  Player‚ÌuUŒ‚vƒNƒ‰ƒX
 * @author IharaShota
 * @date   2023/10/27
 * @Update 2023/10/27 ì¬
 **/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAtk : PlayerStrategy
{
    public float m_fInterval;
    public float m_fTime;
    private int atknum;

    private void Start()
    {
        m_fTime = m_fInterval;
    }

    public override void UpdateState()
    {
        if (m_fTime <= 0)
        {
            // UŒ‚ŠÔ‚ğ0‚É‚·‚é
            m_fTime = 0;

            // UŒ‚ ¨ ‘Ò‚¿
            if (ObjPlayer.instance.GetSetGround.m_bStand)
            {
                ObjPlayer.instance.m_PlayerState = PlayerState.Idle;
                ON_HitManager.instance.DeleteHit(atknum);
                return;
            }
        }
    }

    public override void UpdatePlayer()
    {
        if(ObjPlayer.m_bAtkFlg)
        {
            if (GameManager.IsDebug())
                Debug.Log("HitGenerate");

            // UŒ‚‚Ì“–‚½‚è”»’è¶¬(Œü‚«‚É‚æ‚Á‚Ä¶¬ˆÊ’u‚ª•Ï‚í‚é)
            if(ObjPlayer.instance.GetSetDir == ObjDir.RIGHT)
            {
                atknum = ON_HitManager.instance.GenerateHit(ObjPlayer.instance.GetSetPos + new Vector3(1f, 0f, 0f),
                ObjPlayer.instance.GetSetScale / 2f, true, HitType.ATTACK, ObjPlayer.instance.GetSetObjID);
            }
            else if(ObjPlayer.instance.GetSetDir == ObjDir.LEFT)
            {
                atknum = ON_HitManager.instance.GenerateHit(ObjPlayer.instance.GetSetPos - new Vector3(1f, 0f, 0f),
                ObjPlayer.instance.GetSetScale / 2f, true, HitType.ATTACK, ObjPlayer.instance.GetSetObjID);
            }

            m_fTime = m_fInterval;
            ObjPlayer.m_bAtkFlg = false;
        }
        else m_fTime -= Time.deltaTime;

        // ‘¬“x‚ÍŒ¸‘¬(‰¼)
        ObjPlayer.instance.GetSetSpeed *= new Vector2(0.7f, 1f);
    }
}
