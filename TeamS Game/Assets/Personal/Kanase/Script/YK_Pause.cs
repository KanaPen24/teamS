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

public class YK_Pause : MonoBehaviour
{
    [SerializeField] private GameObject Pause;
    private bool m_bPause;

    private void Start()
    {
        Pause.SetActive(false);
        m_bPause = false;
    }
    // Update is called once per frame
    void Update()
    {
     
        if ((Input.GetKeyDown(KeyCode.P)|| Input.GetKeyDown(IS_XBoxInput.Menu)) && !m_bPause)
        {
            GameManager.GetSetGameState = GameState.GamePause;
            Debug.Log("ポーズ");
            m_bPause = true;
        }
        else if ((Input.GetKeyDown(KeyCode.P)|| Input.GetKeyDown(IS_XBoxInput.Menu)) && m_bPause)
        {
            GameManager.GetSetGameState = GameState.GamePlay;
            Debug.Log("解除");
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
