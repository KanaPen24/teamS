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
using UnityEngine.SceneManagement;
using DG.Tweening;

public class YK_HighScore : YK_UI
{
    private List<int> m_nHighScore;
    [SerializeField] private int m_nRank = 5;
    public static YK_HighScore instance;         // YK_HighScoreのインスタンス
    [SerializeField] List<Text> scoreText; // スコアを表示するためのTextコンポーネントへの参照
    [SerializeField] Text MyscoreText; // 自分のスコアを表示するためのText
    private int storage;
    private bool m_bDrawflg = false;         //順位変動がなかった場合のフラグ
    [SerializeField] private float dotweenInterval;
    [SerializeField] private float Movetime;

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
        //タイプの設定
        m_eUIType = UIType.HighScore;
        Obj = this.gameObject;
        UpdateHighScore();    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.GetSetGameState==GameState.Result&& Input.GetKeyDown(IS_XBoxInput.A))
        {
            //トランジションを掛けてシーン遷移する
            Fade.instance.FadeIn(1f, () =>
            {
                GameManager.GetSetGameState = GameState.Title;
                SceneManager.LoadScene("TitleScene");
            });
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
            //リザルトシーン様に変更
            if (m_nHighScore[i] <= YK_JsonSave.instance.MyScoreLoad())
            {
                storage = i;    //更新された順位を保存
                for (int j = i; j < m_nRank - 1; j++)
                    //次の順位に元々のハイスコアを移動させる
                    m_nHighScore[m_nHighScore.Count - j - 1] = m_nHighScore[m_nHighScore.Count - j - 2];
                m_nHighScore[i] = YK_JsonSave.instance.MyScoreLoad();    //ハイスコアを更新する
                break;
            }
            else    //順位変動がない場合
            {
                m_bDrawflg = true;
            }
                
        }
    }

    //ハイスコアの表示
    private void DrawHighScore()
    {
        for (int i = 0; i < m_nRank; i++)
        {
            // テキストの更新
            scoreText[i].text = m_nHighScore[i].ToString("D7");
        }
        //自分のスコアを表示
        MyscoreText.text= YK_JsonSave.instance.MyScoreLoad().ToString("D7");
        //順位変動がなかったら演出はいれない
        if (m_bDrawflg) return;
        Vector3 RectTransform_get;
        RectTransform_get = scoreText[storage].rectTransform.position;
        scoreText[storage].rectTransform.anchoredPosition = new Vector3(0.0f, -200, 0); 
        scoreText[storage].rectTransform.DOMove(RectTransform_get, Movetime).OnComplete(() =>
        {
            scoreText[storage].DOFade(0.0f, dotweenInterval)   // アルファ値を0にしていく
                       .SetLoops(-1, LoopType.Yoyo);    // 行き来を無限に繰り返す
        });
    }

    //ゲッターセッターハイスコアを書く
    public List<int> GetSetHighScore
    {
        get { return m_nHighScore; }
        set { m_nHighScore = value; }
    }

    //ランキングの渡す
    public int GetRank()
    {
        return m_nRank;
    }
    
    //ハイスコア更新の一巡関数
    public void UpdateHighScore()
    {
        ChangeHighScore();
        //SaveHighScore();
        DrawHighScore();
    }
}
