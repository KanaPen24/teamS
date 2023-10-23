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
    private void Start()
    {
    }
    public void Update()
    {
        //// --- 入力確認 ---
        //// 移動
        //if (IS_XBoxInput.LStick_H > 0.2f || IS_XBoxInput.LStick_H < -0.2f)
        //{
        //    m_vSpeed.x += IS_XBoxInput.LStick_H * m_fAccel;

        //    if (m_vSpeed.x > m_vMaxSpeed.x)
        //    {
        //        m_vSpeed.x = m_vMaxSpeed.x;
        //    }
        //    if (m_vSpeed.x < -m_vMaxSpeed.x)
        //    {
        //        m_vSpeed.x = -m_vMaxSpeed.x;
        //    }
        //}
        //else
        //{
        //    m_vSpeed.x *= 0.99f;
        //    if (m_vSpeed.x <= 0.01f && m_vSpeed.x >= 0.01f)
        //    {
        //        m_vSpeed.x = 0f;
        //    }
        //}
        //// ----------------
    }

    public override void UpdateObj()
    {
        m_vMove = m_vSpeed;
        this.transform.position += new Vector3(m_vMove.x, m_vMove.y, 0f);
    }

    public override void UpdateDebug()
    {
        //Debug.Log("Player");
    }

    public override void DamageAttack()
    {

    }
}
