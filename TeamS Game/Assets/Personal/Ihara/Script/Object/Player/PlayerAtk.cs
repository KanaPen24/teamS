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
    [SerializeField] private Vector3 m_vAtkArea;
    [SerializeField] private float m_fLength;
    //[SerializeField] private float m_fInterval;
    //[SerializeField] private float m_fTime;
    private int atknum;
    private bool m_bAtk;

    //private void Start()
    //{
    //    m_fTime = m_fInterval;
    //}

    public override void UpdateState()
    {
        //if (m_fTime <= 0 && !ObjPlayer.m_bAtkFlg)
        if (ObjPlayer.instance.Anim.GetAnimNormalizeTime(PlayerAnimState.Atk,1f) && !ObjPlayer.m_bAtkFlg)
        {
            //// 攻撃時間を0にする
            //m_fTime = 0;

            // 攻撃 → 待ち
            if (ObjPlayer.instance.GetSetGround.m_bStand)
            {
                ObjPlayer.instance.m_PlayerState = PlayerState.Idle;
                ON_HitManager.instance.DeleteHit(atknum);
                m_bAtk = false;
                return;
            }

            // 攻撃の当たり判定の座標更新
            if (m_bAtk)
            {
                if (ObjPlayer.instance.GetSetDir == ObjDir.RIGHT)
                ON_HitManager.instance.SetCenter(atknum,ObjPlayer.instance.GetSetPos + new Vector3(m_fLength, 0f, 0f));
                else if (ObjPlayer.instance.GetSetDir == ObjDir.LEFT)
                    ON_HitManager.instance.SetCenter(atknum, ObjPlayer.instance.GetSetPos - new Vector3(m_fLength, 0f, 0f));
            }
        }
    }

    public override void UpdatePlayer()
    {
        // アニメ―ション
        ObjPlayer.instance.Anim.ChangeAnim(PlayerAnimState.Atk);

        if (ObjPlayer.m_bAtkFlg)
        {
            if (GameManager.IsDebug())
                Debug.Log("HitGenerate");

            // 攻撃の当たり判定生成(向きによって生成位置が変わる)
            if(ObjPlayer.instance.GetSetDir == ObjDir.RIGHT)
            {
                atknum = ON_HitManager.instance.GenerateHit(ObjPlayer.instance.GetSetPos + new Vector3(m_fLength, 0f, 0f),
                m_vAtkArea, true, HitType.ATTACK, ObjPlayer.instance.GetSetObjID);

                ObjPlayer.instance.GetSetSpeed += new Vector2(5f, 0f);
            }
            else if(ObjPlayer.instance.GetSetDir == ObjDir.LEFT)
            {
                atknum = ON_HitManager.instance.GenerateHit(ObjPlayer.instance.GetSetPos - new Vector3(m_fLength, 0f, 0f),
                m_vAtkArea, true, HitType.ATTACK, ObjPlayer.instance.GetSetObjID);

                ObjPlayer.instance.GetSetSpeed += new Vector2(-5f, 0f);
            }

            //m_fTime = m_fInterval;
            AudioManager.instance.PlaySE(SEType.SE_PlayerAtk);
            ObjPlayer.m_bAtkFlg = false;
            m_bAtk = true;
        }
        //else m_fTime -= Time.deltaTime;

        // 速度は減速(仮)
        ObjPlayer.instance.GetSetSpeed *= new Vector2(0.8f, 1f);
    }
}
