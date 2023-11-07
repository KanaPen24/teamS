/**
 * @file   PlayerIdle.cs
 * @brief  Playerの「攻撃」クラス
 * @author IharaShota
 * @date   2023/10/27
 * @Update 2023/10/27 作成
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
        if (m_fTime <= 0 && !ObjPlayer.m_bAtkFlg)
        {
            // 攻撃時間を0にする
            m_fTime = 0;

            // 攻撃 → 待ち
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

            // 攻撃の当たり判定生成(向きによって生成位置が変わる)
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

        // 攻撃の当たり判定の座標更新
        if(ObjPlayer.instance.GetSetDir == ObjDir.RIGHT)
        ON_HitManager.instance.GetHit(atknum).SetCenter(ObjPlayer.instance.GetSetPos + new Vector3(1f, 0f, 0f));
        else if(ObjPlayer.instance.GetSetDir == ObjDir.LEFT)
            ON_HitManager.instance.GetHit(atknum).SetCenter(ObjPlayer.instance.GetSetPos - new Vector3(1f, 0f, 0f));

        // 速度は減速(仮)
        ObjPlayer.instance.GetSetSpeed *= new Vector2(0.7f, 1f);
    }
}
