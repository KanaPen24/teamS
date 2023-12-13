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
    Title,
    GameStart,
    GamePlay,
    GamePause,
    GameGoal,
    GameOver,
    ReturnDeath,
    Result,

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
    public static GameState m_sGameState;                // 現在のゲーム状態(プログラム用)
    public static GameMode  m_sGameMode;                 // 現在のゲームモード(プログラム用)
    [SerializeField] private GameState m_CheckGameState; // 現在のゲーム状態(確認用)
    [SerializeField] private GameMode m_GameMode;        // 現在のゲームモード(変更用)
    public static bool m_bDebugStart;                    // デバッグの開始時かどうか
    public static float m_fTime = 3f;
    public HitType hitType;
    private bool m_bOnce = false;

    private void Start()
    {
        // ゲーム開始時はスタート状態
        m_sGameState = GameState.GameStart;

        // 当たり判定デバッグ表示クラスがnullだったらnewで作成
        if(ON_HitDebug.instance == null)
        {
            ON_HitDebug.instance = new ON_HitDebug();
        }

        AudioManager.instance.PlayBGM(BGMType.BGM_GAME);
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();             //ゲーム終了処理
        }

        m_fTime -= Time.deltaTime;
        if (m_fTime <= 0f && !m_bOnce)
        {
            m_sGameState = GameState.GamePlay;
            m_bOnce = true;
            m_fTime = 0f;
        }

        // テスト
        if (Input.GetKeyDown(IS_XBoxInput.LB))
        {
            //トランジションを掛けてシーン遷移する
            Fade.instance.FadeIn(1f, () =>
            {
                GameManager.GetSetGameState = GameState.Result;
                SceneManager.LoadScene("ResultScene");
            });
        }
        
    }

    private void FixedUpdate()
    {
        m_bDebugStart = false;

        // デバッグモード切替時は…
        if (m_sGameMode != m_GameMode)
        {
            // デバッグの場合…
            if (m_GameMode == GameMode.Debug)
            {
                // 当たり判定のデバッグ表示開始
                ON_HitDebug.instance.StartHitDebug();
                m_bDebugStart = true;
            }
            // リリースの場合は当たり判定のデバッグ表示終了
            else ON_HitDebug.instance.FinHitDebug();
        }
        m_CheckGameState = m_sGameState;
        m_sGameMode = m_GameMode;

        // 当たり判定デバッグ表示更新
        ON_HitDebug.instance.Update(hitType,true);
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
