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
        if (Input.GetKeyDown(KeyCode.B))
        {
            AddScore(100);
        }
    }

    /**
     * @brief スコアに指定された数を加算
     * @param num スコアに加算する数。
     */
    private void AddScore(int num)
    {
        //スコア+num m_fTimeすすめる
        StartCoroutine(ScoreAnimation(num, m_fTime));
    }

    /**
     * @brief スコアを0にリセット
     */
    private void ResetScore()
    {
        m_nScore = 0;
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
            scoreText.text = "Score:" + num.ToString("D5");

            elapsedTime += Time.deltaTime;
            // 0.01秒待つ
            yield return new WaitForSeconds(0.01f);
        }
        // 最終的な着地のスコア
        scoreText.text = "Score:" + m_nScore.ToString("D5");
    }
}
