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
    Atk,
    //Def,

    MaxPlayerState
}

public class ObjPlayer : ObjBase
{
    public static bool m_bJumpFlg = false;
    public static bool m_bDropFlg = false;
    public static bool m_bAtkFlg = false;
    public static bool m_bDefFlg = false;
    public static bool m_bWalkFlg = false;

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

        //if (Input.GetKeyDown(IS_XBoxInput.B))
        //{
        //    if(GameManager.IsDebug())
        //        Debug.Log("HitGenerate");

        //    ON_HitManager.instance.GenerateHit(this.gameObject.transform.position + new Vector3(1f, 0f, 0f),
        //        GetSetScale / 2f, true, HitType.ATTACK, m_nObjID);
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
