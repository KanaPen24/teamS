/**
 * @file   YK_HighScore.cs
 * @brief  ハイスコアを管理および表示
 * @author 吉田叶聖
 * @date   2023/11/28
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class YK_HighScore : MonoBehaviour
{
    private List<int> m_nHighScore;
    [SerializeField] private int m_nRank = 5;
    public static YK_HighScore instance;         // YK_HighScoreのインスタンス
    [SerializeField] List<Text> scoreText; // スコアを表示するためのTextコンポーネントへの参照

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

    // Start is called before the first frame update
    void Start()
    {
        m_nHighScore = new List<int>(m_nRank) { 5, 4, 3, 2, 1 };
        
        SaveHighScore();
    }

    // Update is called once per frame
    void Update()
    {
        //テスト
        if (Input.GetKeyDown(KeyCode.F7))
        {
            ChangeHighScore();
            SaveHighScore();
            DrawHighScore();
        }
    }

    private void SaveHighScore()
    {
        YK_JsonSave.instance.HighScoreSave();
    }

    private void ChangeHighScore()
    {
        m_nHighScore = YK_JsonSave.instance.HighScoreLoad();
        for (int i = 0; i < m_nRank; i++)
        {
            if(m_nHighScore[i]<=YK_Score.instance.GetSetScore)
            {
                for (int j = i; j < m_nRank - 1; j++)
                    m_nHighScore[m_nHighScore.Count - j - 1] = m_nHighScore[m_nHighScore.Count - j - 2];              //次の順位に元々のハイスコアを移動させる
                m_nHighScore[i] = YK_Score.instance.GetSetScore;    //ハイスコアを更新する
                break;
            }
        }
    }

    //ハイスコアの表示
    private void DrawHighScore()
    {
        for (int i = 0; i < m_nRank; i++)
        {
            // テキストの更新
            scoreText[i].text = "Score:" + m_nHighScore[i].ToString("D7");
        }
    }

    //ゲッターセッターハイスコアを書く
    public List<int> GetSetHighScore
    {
        get { return m_nHighScore; }
        set { m_nHighScore = value; }
    }
}
