﻿/**
 * @file YK_BossHP.cs
 * @brief ボスの体力
 *        このファイルはボスの体力を制御するスクリプト
 *        ボスのバーの表示やフェードイン・フェードアウトなどの機能を提供
 * @author 吉田叶聖
 *         このスクリプトの作成者
 * @date   2023/03/28
 *         初版作成日
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

/**
 * @class YK_BossHP
 * @brief ボスの体力を制御するクラス
 */
public class YK_BossHP : YK_UI
{
    [SerializeField] private Slider BossSlider;     // シーンに配置したSlider格納用
    [SerializeField] private GameObject BossBar;    // ボスのバーの大元
    //[SerializeField] private NK_Boss Boss;
    //[SerializeField] private YK_Clear Clear;
    [SerializeField] private Image FrontFill;       // バーの表面のテクスチャ
    [SerializeField] private Image BackFill;        // 後ろのバーの表面のテクスチャ
    [SerializeField] private Image BackGround;      // バーの裏のテクスチャ
    [SerializeField] private Image Frame;           // フレーム
    [SerializeField] private Image Refraction;      // 反射光
    [SerializeField] private YK_Time time;
    //[SerializeField] private NK_BossSlime_Aera m_Area;

    /**
     * @brief スタート時に呼ばれる関数
     *        初期化処理を行う
     */
    void Start()
    {
        m_eUIType = UIType.BossBar;                     // UIのタイプ設定
        m_eFadeState = FadeState.FadeNone;
        
            //    BossSlider.maxValue = Boss.GetSetMaxHp;         // Sliderの最大値を敵キャラのHP最大値と合わせる
            //    BossSlider.value = Boss.GetSetHp;               // Sliderの初期状態を設定（HP満タン）

        GetSetUIPos = BossSlider.GetComponent<RectTransform>().anchoredPosition;  // 座標取得
        GetSetUIScale = BossSlider.transform.localScale;                         // スケール取得

        UIFadeOUT();  // 最初は消しておく
    }

    /**
     * @brief フレームごとに呼ばれる関数
     *        ボス戦の進行やSliderの更新を行う
     */
    void Update()
    {
        if (GameManager.GetSetGameState != GameState.GamePlay)
            return;
        // ボス戦が始まったらフェードイン
        //if (m_Area.GetSetBattleFlag)
        //{
        //    UIFadeIN();
        //}


        //// Sliderの更新
        //BossSlider.value = Boss.GetSetHp;
        //// ボスのHPがなくなったら（例：ボスキャラを倒したら）
        //if (Boss.GetSetHp <= 0)
        //{
        //    //ゲームのステートをクリア状態にする
        //    GameManager.GetSetGameState = GameState.GameGoal;
        //}

        //もしゴールにたどり着いたら
        if (GameManager.GetSetGameState == GameState.GameGoal)
        {
            //ボスバーの非表示
            UIFadeOUT();            
        }    
    }

    /**
     * @brief UIのフェードイン処理
     *        バーとテクスチャのフェードインを行う
     */
    public void UIFadeIN()
    {
        m_eFadeState = FadeState.FadeIN;

        // 0秒で後X,Y方向を元の大きさに変更
        BossSlider.transform.DOScale(GetSetUIScale, 0f);
        // 0秒でテクスチャをフェードイン
        FrontFill.DOFade(1f, 0f);
        Frame.DOFade(1f, 0f);
        BackFill.DOFade(1f, 0f);
        Refraction.DOFade(1f, 0f);
        BackGround.DOFade(1f, 0f).OnComplete(() =>
        {
            GetSetFadeState = FadeState.FadeNone;
            Debug.Log("FadeIN終了");
        });
    }

    /**
     * @brief UIのフェードアウト処理
     *        バーとテクスチャのフェードアウトを行う
     */
    public void UIFadeOUT()
    {
        m_eFadeState = FadeState.FadeOUT;

        // m_fDelTime秒でm_MinScaleに変更
        BossSlider.transform.DOScale(m_MinScale, m_fDelTime);
        // m_fDelTime秒でテクスチャをフェードイン
        FrontFill.DOFade(0f, m_fDelTime);
        Frame.DOFade(0f, m_fDelTime);
        BackFill.DOFade(0f, m_fDelTime);
        Refraction.DOFade(0f, m_fDelTime);
        BackGround.DOFade(0f, m_fDelTime).OnComplete(() =>
        {
            GetSetFadeState = FadeState.FadeNone;
            Debug.Log("FadeOUT終了");
        });
    }
}
