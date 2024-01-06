/**
 * @file YK_Start.cs
 * @brief StartUIの処理
 * @author 吉田叶聖
 * @date 2023/05/02
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class YK_Start : YK_UI
{
    [SerializeField] Fade fade;
    [SerializeField] private GameObject GameStart;   // ゲームスタートオブジェクト
    [SerializeField] private Image StartUI;          // スタートUIイメージ
    [SerializeField] private Image ExitUI;           // 終了UIイメージ
    [SerializeField] private Image TitleUI;          // タイトルUIイメージ
    private Outline outline;                         // アウトライン        
    private float m_fTime;                           // 経過時間    

    /**
     * @brief Start関数
     *        UIのタイプを設定し、アウトラインを非表示
     *        スタートの座標とスケールを取得
     */
    void Start()
    {
        m_eUIType = UIType.Start; //UIのタイプ設定
        m_eFadeState = FadeState.FadeNone;
        //UIが動くようならUpdateにかかなかん
        GetSetUIPos = StartUI.GetComponent<RectTransform>().anchoredPosition;
        //スケール取得
        GetSetUIScale = StartUI.transform.localScale;
        //アウトライン取得
        outline = this.GetComponent<Outline>();

        GameStart.SetActive(true);
    }
    /**
     * @brief Update関数
     *        カーソルが動き始めるまで当たり判定を無効にし、エフェクターを無効
     *        ブラウン管のポストエフェクトを減らしていく処理
     */
    private void Update()
    {
       
    }

    // ゲームスタート処理
    public void StartPlay()
    {
        //トランジションを掛けてシーン遷移する
        fade.FadeIn(1f, () =>
        {
            GameManager.m_sGameState = GameState.GameStart;
            //GameManager.m_fTime = 3f;
            SceneManager.LoadScene("GameScene");
        });
    }

    

    // イベントハンドラー（イベント発生時に動かしたい処理）
    void SceneLoaded(Scene nextScene, LoadSceneMode mode)
    {
        Debug.Log(nextScene.name);
        Debug.Log(mode);
    }

    void SceneUnloaded(Scene thisScene)
    {
        Debug.Log(thisScene.name);
    }
}
