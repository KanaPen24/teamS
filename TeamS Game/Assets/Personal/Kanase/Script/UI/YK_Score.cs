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
        /**
         * 現在のスコアを表示するためにTextコンポーネントを更新します。
         * スコアは5桁のゼロ埋め形式で表示されます。
         */
        scoreText.text = "Score:" + m_nScore.ToString("D5");
    }

    /**
     * @brief スコアに指定された数を加算
     * @param num スコアに加算する数。
     */
    private void AddScore(int num)
    {
        m_nScore += num;
    }

    /**
     * @brief スコアを0にリセット
     */
    private void ResetScore()
    {
        m_nScore = 0;
    }
}
