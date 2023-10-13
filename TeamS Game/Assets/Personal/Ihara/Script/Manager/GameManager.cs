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
    public static GameState m_GameState;                // 現在のゲーム状態
    [SerializeField] private GameState m_CheckGameState;// 現在のゲーム状態(確認用)
    [SerializeField] private GameMode m_GameMode;       // 現在のゲームモード

    private void Awake()
    {
        m_GameState = GameState.GameStart;
    }

    private void Update()
    {
        m_CheckGameState = m_GameState;
        if (m_GameMode == GameMode.Debug)
        {
            Debug.Log(m_GameState);
        }

        if(Input.GetKeyDown(KeyCode.A))
        {
            AudioManager.instance.PlaySE(SEType.SE_PROTO);
        }
    }

    /**
     * @fn
     * Gameの状態のgetter・setter
     * @return m_GameState
     * @brief Gameの状態を返す・セット
     */
    public static GameState GetSetGameState
    {
        get { return m_GameState; }
        set { m_GameState = value; }
    }

    /**
     * @fn
     * Debugモードかどうか
     * @return bool型
     * @brief 
     */
    public bool IsDebug()
    {
        if (m_GameMode != GameMode.Debug)
        {
            return true;
        }
        else return false;
    }
}
