/**
 * @file YK_Combo.cs
 * @brief コンボUIの処理を行うクラス
 *        YK_UIクラスを継承
 *        コンボの数値表示や色の変更、コンボの加算やリセットなどの処理
 *        DOTweenパッケージを使用
 * @author 吉田叶聖
 * @date   2023/05/15
 * @Update 2023/06/30 一部変数,関数を静的化しました(Ihara)
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;


public class YK_Combo : YK_UI
{
    // --- 静的メンバ(AddComboを外部で使用するため) ---
    static private Text ComboNumber;   　      // コンボの数値表示テキスト
    static private int m_nCombo;                // 現在のコンボ数
    static private float a_color = 0f;         // コンボ表示のアルファ値
    static private int m_nCountComboTime = 0;  // コンボが表示されている時間
    static private bool m_bHitFlg = false;     // コンボがヒットしたかどうかのフラグ
    // -----------------------------------------------------------------------------------

    // --- 動的メンバ ---
    [SerializeField] private Image ComboTxt;      // コンボの画像
    [SerializeField] private List<TextMeshProUGUI> ComboText; //コンボ演出の時に出すやつ
    private float f_colordown = 0.016f;           // コンボ表示のアルファ値減少量
    private int ComboS = 0;                       // 小コンボの閾値
    private int ComboM = 5;                       // 中コンボの閾値
    private int ComboL = 10;                      // 大コンボの閾値
    private int ComboXL = 15;                     // 特大コンボの閾値
    [SerializeField] private int m_nCountDownTime;    // コンボが消えるまでの時間（秒単位）
    [SerializeField] private bool m_bOnce = true; // 1回のみのフラグ
    private Vector3 Combo_Scale;                  // コンボ表示の初期スケール
    // ------------------------------------------------------------------------------------

    // Start is called before the first frame update
    /**
     * @fn
     * スタート時に呼ばれる関数
     * 初期化処理
     */
    void Start()
    {
        m_eUIType = UIType.Combo;                                 // UIのタイプ設定
        m_eFadeState = FadeState.FadeNone;
        ComboNumber = GetComponentInChildren<Text>();
        GetSetUIPos = ComboNumber.GetComponent<RectTransform>().anchoredPosition;    // UIの座標取得
        GetSetUIScale = ComboNumber.transform.localScale;                           // UIのスケール取得
        //Combo_Scale = ComboTxt.transform.localScale;                                // コンボ表示の初期スケール取得
        ComboNumber = GetComponent<Text>();                                         // Textコンポーネントの取得
        m_nCountDownTime *= 60;                                                     // 60FPSに合わせる
        a_color = 0.0f;                                                              // 最初は消しておく
        ComboNumber.color = new Color(0.5f, 0.5f, 1f, a_color);                       // コンボ表示の色を設定
        for (int i = 0; i < ComboText.Count; i++)
        {
            ComboText[i].DOFade(0.0f, 0.0f);
        }
        //ComboTxt.color = new Color(0.5f, 0.5f, 1f, a_color);                          // コンボテキストの色を設定
    }

    // Update is called once per frame
    /**
     * @fn
     * フレームごとに呼ばれる関数
     * コンボの更新や表示の色を変更
     */
    void Update()
    {
        //if (GameManager.GetSetGameState != GameState.GamePlay || GameManager.GetSetGameState != GameState.GameGoal)// ゲームがプレイ中またはゴール中は更新しない
        //    return;

        if (Input.GetKeyDown(KeyCode.F2))    // F2キーが押されたらコンボを加算する
        {
            AddCombo();
        }

        if (m_nCombo == 0)
        {   
            a_color = 0.0f; // コンボが0の場合、表示のアルファ値を0にする
            for (int i = 0; i < ComboText.Count; i++)
            {
                ResetEff(ComboText[i]);
            }
        }
        if (m_bHitFlg)    // コンボがヒットした場合
        {
            m_nCountComboTime++;
            a_color -= 1f / m_nCountDownTime;    // アルファ値を徐々に減少させる
            if (m_nCountComboTime >= m_nCountDownTime)
            {
                m_bHitFlg = false;
                ResetCombo();    // カウントダウン時間を超えたらコンボをリセットする
                return;
            }
        }

        //初期カラー
        ComboNumber.color = new Color(0.5f, 0.5f, 1f, a_color);    // コンボ表示の色を設定
        //ComboTxt.color = new Color(0.5f, 0.5f, 1f, a_color);       // コンボテキストの色を設定

        // コンボがComboM以上ComboL未満の場合、色を変更する
        if (m_nCombo >= ComboM && m_nCombo < ComboL)
        {
            ComboNumber.color = new Color(1f, 1f, 0.5f, a_color);
            // ComboTxt.color = new Color(1f, 1f, 0.5f, a_color);
            StampEff(ComboText[0]);            
            ComboText[0].color = new Color(1f, 1f, 0.5f, a_color);      // コンボテキスト表示の色を設定
        }

        // コンボがComboL以上ComboXL未満の場合、色を変更する
        if (m_nCombo >= ComboL && m_nCombo < ComboXL)
        {
            ResetEff(ComboText[0]);
            ComboNumber.color = new Color(1f, 0.5f, 0.5f, a_color);
            //   ComboTxt.color = new Color(1f, 0.5f, 0.5f, a_color);
            StampEff(ComboText[1]);
            ComboText[1].color = new Color(1f, 0.5f, 0.5f, a_color);      // コンボテキスト表示の色を設定
        }

        // コンボがComboXL以上の場合、色をHSVカラーモードで変更する
        if (m_nCombo >= ComboXL)
        {
            ResetEff(ComboText[1]);
            StampEff(ComboText[2]);
            ComboText[2].color = Color.HSVToRGB(Time.time % 1, 1, 1);      // コンボテキスト表示の色を設定
            ComboNumber.color = Color.HSVToRGB(Time.time % 1, 1, 1);
          //  ComboTxt.color = Color.HSVToRGB(Time.time % 1, 1, 1);
        }
    }

    /**
     * @fn
     * コンボを加算する静的関数
     */
    public static void AddCombo()
    {
        m_nCountComboTime = 0;
        m_bHitFlg = true;
        a_color = 1.0f;
        m_nCombo++;
        ComboNumber.text = m_nCombo + "";
    }

    /**
     * @fn
     * コンボをリセットする関数
     */
    public void ResetCombo()
    {
        m_nCombo = 0;
    }

    public void StampEff(TextMeshProUGUI text)
    {
        if (m_bOnce)
        {
            text.DOFade(1.0f, 1.5f);
            text.transform.DOScale(1.2f, 0.5f);
        }
    }

    public void ResetEff(TextMeshProUGUI text)
    {
        text.DOFade(0.0f, 0.0f);
        text.transform.DOScale(0.0f, 0.0f);
    }
}
