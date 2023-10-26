/**
 * @file   ObjPlayer.cs
 * @brief  Playerクラス
 * @author IharaShota
 * @date   2023/10/13
 * @Update 2023/10/13 作成
 **/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjPlayer : ObjBase
{
    public static bool m_bJumpFlg;
    public static bool m_bDropFlg;
    public static bool m_bAtkFlg;
    public static bool m_bDefFlg;
    public static bool m_bWalkFlg;

    public void Update()
    {
        // --- 入力確認 ---
        // 移動
        if (IS_XBoxInput.LStick_H > 0.2f || IS_XBoxInput.LStick_H < -0.2f)
        {
            m_vSpeed.x += IS_XBoxInput.LStick_H * m_fAccel;

            if (m_vSpeed.x > m_vMaxSpeed.x)
            {
                m_vSpeed.x = m_vMaxSpeed.x;
            }
            if (m_vSpeed.x < -m_vMaxSpeed.x)
            {
                m_vSpeed.x = -m_vMaxSpeed.x;
            }
        }
        else
        {
            m_vSpeed.x *= 0.99f;
            if (m_vSpeed.x <= 0.01f && m_vSpeed.x >= 0.01f)
            {
                m_vSpeed.x = 0f;
            }
        }
        // ----------------

        if (Input.GetKeyDown(IS_XBoxInput.B))
        {
            if(GameManager.IsDebug())
                Debug.Log("HitGenerate");

            ON_HitManager.instance.GenerateHit(this.gameObject.transform.position + new Vector3(1f, 0f, 0f),
                new Vector3(1f, 1f, 1f), true, HitType.ATTACK, m_nObjID);
        }

        if (Input.GetKeyDown(IS_XBoxInput.A) || Input.GetKeyDown(KeyCode.A))
        {
            if (GameManager.IsDebug())
                Debug.Log("Jump");
            m_vSpeed.y = 0.7f;
            m_Ground.GetSetStand = false;
        }
    }

    public override void UpdateObj()
    {
    }

    public override void UpdateDebug()
    {
        //Debug.Log("Player");
    }

    public override void DamageAttack()
    {

    }
}
