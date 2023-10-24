/**
 * @file   GameManager.cs
 * @brief  GameManagerクラス
 * @author IharaShota
 * @date   2023/03/27
 * @Update 2023/03/27 作成
 * @Update 2023/07/12 シーンようのステート追加
 * @Update 2023/10/10 ソース削減
 **/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


// ===============================================
// GameState
// … Gameの状態を管理する列挙体
// ===============================================
public enum GameState
{
    GameStart,
    GamePlay,
    GamePause,
    GameGoal,
    GameOver,

    MaxGameState
}

// ===============================================
// GameMode
// … Gameモードを管理する列挙体
// ===============================================
public enum GameMode
{
    Debug,
    Release,
}

public class GameManager : MonoBehaviour
{
    public static GameState m_sGameState;                // 現在のゲーム状態
    public static GameMode  m_sGameMode;                 // 現在のゲームモード
    [SerializeField] private GameState m_CheckGameState; // 現在のゲーム状態(確認用)
    [SerializeField] private GameMode m_GameMode;        // 現在のゲームモード(変更用)
    public static bool m_bDebugStart;                    // デバッグの開始時かどうか

    private void Awake()
    {
        m_sGameState = GameState.GameStart;
        if(ON_HitDebug.instance == null)
        {
            ON_HitDebug.instance = new ON_HitDebug();
        }
    }

    private void FixedUpdate()
    {
        m_bDebugStart = false;

        if (m_sGameMode != m_GameMode)
        {
            if (m_GameMode == GameMode.Debug)
            {
                ON_HitDebug.instance.StartHitDebug();
                Debug.Log("DebugStart");
                m_bDebugStart = true;
            }
            else ON_HitDebug.instance.FinHitDebug();
        }
        m_CheckGameState = m_sGameState;
        m_sGameMode = m_GameMode;

        ON_HitDebug.instance.Update();
    }

    /**
     * @fn
     * Gameの状態のgetter・setter
     * @return m_GameState
     * @brief Gameの状態を返す・セット
     */
    public static GameState GetSetGameState
    {
        get { return m_sGameState; }
        set { m_sGameState = value; }
    }

    /**
     * @fn
     * Debugモードかどうか
     * @return bool型
     * @brief 
     */
    public static bool IsDebug()
    {
        if (m_sGameMode == GameMode.Debug)
        {
            return true;
        }
        else return false;
    }

    public static bool IsDebugStart()
    {
        if (m_bDebugStart)
        {
            return true;
        }
        else return false;
    }
}
