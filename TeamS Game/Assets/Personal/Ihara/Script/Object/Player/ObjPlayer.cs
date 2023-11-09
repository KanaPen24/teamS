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
    Jump,
    Drop,
    Atk,
    //Def,

    MaxPlayerState
}

public class ObjPlayer : ObjBase
{
    public static bool m_bWalkFlg = false;
    public static bool m_bJumpFlg = false;
    public static bool m_bDropFlg = false;
    public static bool m_bAtkFlg = false;
    public static bool m_bDefFlg = false;

    public static ObjPlayer instance;
    public PlayerState m_PlayerState;
    public List<PlayerStrategy> m_PlayerStrategys;

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
        // --- 遷移状態による状態更新 ---
        m_PlayerStrategys[(int)m_PlayerState].UpdateState();
    }

    public override void UpdateObj()
    {
        // --- 遷移状態による更新処理 ---
        m_PlayerStrategys[(int)m_PlayerState].UpdatePlayer();
    }

    // 初期化関数
    public override void InitObj()
    {
        base.InitObj();
    }

    // オブジェクトの破壊
    public override void DestroyObj()
    {
        base.DestroyObj();
    }
}
