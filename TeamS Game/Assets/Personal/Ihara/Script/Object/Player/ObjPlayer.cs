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
    Atk01,
    Atk02,
    Atk03,
    Special,

    MaxPlayerState
}

public class ObjPlayer : ObjBase
{
    public static ObjPlayer instance;
    public PlayerState m_PlayerState;
    public List<PlayerStrategy> m_PlayerStrategys;
    public PlayerAnim Anim;

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
        if (GameManager.GetSetGameState != GameState.GamePlay)
            return;

        // --- 遷移状態による状態更新 ---
        m_PlayerStrategys[(int)m_PlayerState].UpdateState();
    }

    public override void UpdateObj()
    {
        // --- 遷移状態による更新処理 ---
        m_PlayerStrategys[(int)m_PlayerState].UpdatePlayer();

        if(GetSetDir == ObjDir.RIGHT)
        {
            transform.localScale = new Vector3(2.5f, 2.5f, 3f);
        }
        else if (GetSetDir == ObjDir.LEFT)
        {
            transform.localScale = new Vector3(-2.5f, 2.5f, 3f);
        }
    }

    // 初期化関数
    public override void InitObj()
    {
        base.InitObj();
        for(int i = 0; i < m_PlayerStrategys.Count; ++i)
        {
            m_PlayerStrategys[i].InitState();
        }
    }

    // ヒットストップ更新
    public override void UpdateHitStop()
    {
        // ヒットストップ時にアニメーションをスローにする
        if (m_HitStopParam.m_bHitStop)
        {
            Anim.m_bSlow = true;
        }
        else Anim.m_bSlow = false;

        // 基底の処理
        base.UpdateHitStop();
    }
}
