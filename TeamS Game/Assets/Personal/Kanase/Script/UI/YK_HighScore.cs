/**
 * @file   YK_HighScore.cs
 * @brief  ハイスコアを管理および表示
 * @author 吉田叶聖
 * @date   2023/11/28
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YK_HighScore : MonoBehaviour
{
    private List<int> m_nHighScore;
    [SerializeField] private int m_nRank;
    public static YK_HighScore instance;         // YK_HighScoreのインスタンス

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
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SaveHighScore()
    {
        YK_JsonSave.instance.HighScoreSave(m_nHighScore);
    }

    public void ChangeHighScore()
    {
        YK_JsonSave.instance.HighScoreLoad();
        for (int i = 0; i < m_nRank; i++)
        {
            if(m_nHighScore[i]<=YK_Score.instance.GetSetScore)
            {
                m_nHighScore[i + 1] = m_nHighScore[i];              //次の順位に元々のハイスコアを移動させる
                m_nHighScore[i] = YK_Score.instance.GetSetScore;    //ハイスコアを更新する
            }
        }
    }

    //ゲッターセッターハイスコアを書く
    public List<int> GetSetHighScore
    {
        get { return m_nHighScore; }
        set { m_nHighScore = value; }
    }
}
