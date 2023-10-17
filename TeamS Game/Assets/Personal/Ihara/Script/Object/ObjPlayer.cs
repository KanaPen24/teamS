/**
 * @file   ObjPlayer.cs
 * @brief  Player�N���X
 * @author IharaShota
 * @date   2023/10/13
 * @Update 2023/10/13 �쐬
 **/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjPlayer : ObjBase
{
    public float m_fSpeed;   // ��{���x
    public float m_fMaxSpeed;// �ō����x
    public float m_fAccel;   // �����x
    private Vector3 m_vVel;  // ���x 

    public void Update()
    {
        // --- ���͊m�F ---
        // �ړ�
        if (IS_XBoxInput.LStick_H > 0.2f || IS_XBoxInput.LStick_H < -0.2f)
        {
            m_vVel.x += IS_XBoxInput.LStick_H * m_fSpeed * m_fAccel;
            if (m_vVel.x > m_fMaxSpeed)
            {
                m_vVel.x = m_fMaxSpeed;
            }
            if (m_vVel.x < -m_fMaxSpeed)
            {
                m_vVel.x = -m_fMaxSpeed;
            }
        }
        else
        {
            m_vVel.x *= 0.99f;
            if(m_vVel.x <= 0.01f && m_vVel.x >= 0.01f)
            {
                m_vVel.x = 0f;
            }
        }
        // ----------------
    }

    public override void UpdateObj()
    {
        this.transform.position += m_vVel;
    }

    public override void UpdateDebug()
    {
        //Debug.Log("Player");
    }
}
