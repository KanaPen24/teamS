using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class YK_Score : YK_UI
{

    Text scoreText;
    [SerializeField] private int m_nScore;

    // Start is called before the first frame update
    void Start()
    {
        scoreText = GetComponent<Text>();
        m_eUIType = UIType.Score;                     // UIのタイプ設定
        m_nScore = 0;   //スコアリセット
    }
    
    // Update is called once per frame
    void Update()
    {
        scoreText.text = "Score:" + m_nScore.ToString("D5");
    }

    private void AddScore(int num)
    {
        m_nScore += num;
    }

    private void ResetScore()
    {
        m_nScore = 0;
    }


}
