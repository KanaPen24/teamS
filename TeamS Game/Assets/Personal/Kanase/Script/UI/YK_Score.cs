/**
 * @file   YK_Score.cs
 * @brief  スコアを管理および表示
 * @author 吉田叶聖
 * @date   2023/10/13
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class YK_Score : YK_UI
{
    Text scoreText; // スコアを表示するためのTextコンポーネントへの参照
    [SerializeField] private int m_nScore; //スコアの値を格納するためのプライベートな整数
    [SerializeField] private float m_fTime; //スコアの加算時間
    [SerializeField] private int m_nDbgNum; //デバック用の敵の数値
    public static YK_Score instance;         // YK_Scoreのインスタンス

    /**
     * @fn
     * 初期化処理(外部参照を除く)
     * @brief  メンバ初期化処理
     * @detail 特に無し
     */
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    /**
     * @brief Startは最初のフレームの前に呼び出されます。
     */
    void Start()
    {
        scoreText = GetComponent<Text>(); //このGameObjectにアタッチされたTextコンポーネントを取得します
        m_eUIType = UIType.Score; //< UIのタイプを "Score" に設定
        m_nScore = 0; // スコアを0に初期化します (スコアのリセット)
    }

    /**
     * @brief Updateは各フレーム
     */
    void Update()
    {
        //テスト
        if (Input.GetKeyDown(KeyCode.F9))
        {
            AddScore(10);
        }
        //テスト
        if (Input.GetKeyDown(KeyCode.F10))
        {
            FieldAddScore(m_nDbgNum);
        }
        //テスト
        if (Input.GetKeyDown(KeyCode.F11))
        {
            SkyAddScore(m_nDbgNum);
        }
        //テスト
        if (Input.GetKeyDown(KeyCode.F12))
        {
            FarAddScore(m_nDbgNum);
        }
    }

    /**
     * @brief スコアに指定された数を加算
     * @param num スコアに加算する数。
     */
    public void AddScore(int num)
    {
        //スコア+num m_fTimeすすめる
        StartCoroutine(ScoreAnimation(num, m_fTime));
    }

    /**
     * @brief スコアを0にリセット
     */
    public void ResetScore()
    {
        m_nScore = 0;
    }
    /**
     * @brief スコアを読みこみなおす
     */
    public void LoadScore()
    {
        // テキストの更新
        scoreText.text = "Score:" + m_nScore.ToString("D7");
    }

    // スコアをアニメーションさせる
    IEnumerator ScoreAnimation(int addScore, float time)
    {
        //前回のスコア
        int befor = m_nScore;
        //今回のスコア
        int after = m_nScore + addScore;
        //得点加算
        m_nScore += addScore;
        //0fを経過時間にする
        float elapsedTime = 0.0f;

        //timeが0になるまでループさせる
        while (elapsedTime < time)
        {
            float rate = elapsedTime / time;
            int num;
            num = (int)(befor + (after - befor) * rate);
            // テキストの更新
            scoreText.text = "Score:" + num.ToString("D7");

            elapsedTime += Time.deltaTime;
            // 0.01秒待つ
            yield return new WaitForSeconds(0.01f);
        }
        // 最終的な着地のスコア
        scoreText.text = "Score:" + m_nScore.ToString("D7");
    }
    /**
     * @brief スコアに指定された数を加算
     * @param num 敵の数
     */
    public void FieldAddScore(int num)
    {
        float score;
        if (num == 1) 
            score = 100;
        else
        score = 100.0f * num * (1.1f + 0.05f * num);
        //スコア+score m_fTimeすすめる
        StartCoroutine(ScoreAnimation((int)score, m_fTime));
    }
    /**
     * @brief スコアに指定された数を加算
     * @param num 敵の数
     */
    public void SkyAddScore(int num)
    {
        float score;
        if (num == 1)
            score = 120;
        else
            score = 120.0f * num * (1.1f + 0.05f * num);
        //スコア+score m_fTimeすすめる
        StartCoroutine(ScoreAnimation((int)score, m_fTime));
    }
    /**
     * @brief スコアに指定された数を加算
     * @param num 敵の数
     */
    public void FarAddScore(int num)
    {
        float score;
        if (num == 1)
            score = 150;
        else
            score = 150.0f * num * (1.1f + 0.05f * num);
        //スコア+score m_fTimeすすめる
        StartCoroutine(ScoreAnimation((int)score, m_fTime));
    }
    /**
     * @brief スコアに指定された数を加算
     * @param num 敵の数
     */
    public void BlowAddScore(int num)
    {
        float score;
        score = 100.0f * num * (1.1f + 0.05f * num);
        //スコア+score m_fTimeすすめる
        StartCoroutine(ScoreAnimation((int)score, m_fTime));
    }

    public int GetSetScore
    {
        get { return m_nScore; }
        set { m_nScore = value; }
    }
}
