/**
 * @file   IS_Pause.cs
 * @brief  ポーズクラス
 * @author IharaShota
 * @date   2023/03/28
 * @Update 2023/03/28 作成
 **/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering;

public class YK_Pause : MonoBehaviour
{
    [SerializeField] private GameObject Pause;
    private bool m_bPause;
    [SerializeField] ON_VolumeManager PostEffect;    // ポストエフェクト

    private void Start()
    {
        PostEffect.SetGaussianRate(0.0f);
        Pause.SetActive(false);
        m_bPause = false;
    }
    // Update is called once per frame
    void Update()
    {
     
        if ((Input.GetKeyDown(KeyCode.P)|| Input.GetKeyDown(IS_XBoxInput.Menu)) && !m_bPause)
        {
            GameManager.GetSetGameState = GameState.GamePause;
            PostEffect.SetGaussianRate(1.0f);
            m_bPause = true;
        }
        else if ((Input.GetKeyDown(KeyCode.P)|| Input.GetKeyDown(IS_XBoxInput.Menu)) && m_bPause)
        {
            GameManager.GetSetGameState = GameState.GamePlay;
            PostEffect.SetGaussianRate(0.0f);
            m_bPause = false;
        }

        if (GameManager.GetSetGameState == GameState.GamePause)
        {
            Pause.SetActive(true);
        }
        else
        {
            Pause.SetActive(false);
        }
    }
}
