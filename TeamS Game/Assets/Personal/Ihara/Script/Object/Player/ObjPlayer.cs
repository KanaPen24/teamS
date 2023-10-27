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

public enum PlayerState
{
    Idle,
    Walk,
    //Dash,
    Jump,
    Drop,
    //Atk,
    //Def,

    MaxPlayerState
}

public class ObjPlayer : ObjBase
{
    public static bool m_bJumpFlg;
    public static bool m_bDropFlg;
    public static bool m_bAtkFlg;
    public static bool m_bDefFlg;
    public static bool m_bWalkFlg;

    public static ObjPlayer instance;
    public List<PlayerStrategy> m_PlayerStrategys;
    public PlayerState m_PlayerState;

    public void Start()
    {
        // インスタンス化
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public void Update()
    {
        // --- 遷移状態による入力確認 ---
        m_PlayerStrategys[(int)m_PlayerState].UpdateInput();

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

        //if (Input.GetKeyDown(IS_XBoxInput.B))
        //{
        //    if(GameManager.IsDebug())
        //        Debug.Log("HitGenerate");

        //    ON_HitManager.instance.GenerateHit(this.gameObject.transform.position + new Vector3(1f, 0f, 0f),
        //        GetSetScale / 2f, true, HitType.ATTACK, m_nObjID);
        //}

        //if (Input.GetKeyDown(IS_XBoxInput.A) || Input.GetKeyDown(KeyCode.A))
        //{
        //    if (GameManager.IsDebug())
        //        Debug.Log("Jump");
        //    m_vSpeed.y = 0.7f;
        //    m_Ground.GetSetStand = false;
        //}
    }

    public override void UpdateObj()
    {
        m_PlayerStrategys[(int)m_PlayerState].UpdatePlayer();
    }

    public override void UpdateDebug()
    {
        //Debug.Log("Player");
    }

    public override void DamageAttack()
    {

    }
}
