/**
 * @file   YK_Time.cs
 * @brief  経過時間標示
 * @author 吉田叶聖
 * @date   2023/11/07
 */
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class YK_Time : MonoBehaviour
{

    [SerializeField]
    private int m_nMinute;
    [SerializeField]
    private float m_fSeconds;
    //　前のUpdateの時の秒数
    private float oldSeconds;
    //　タイマー表示用テキスト
    private Text timerText;

    void Start()
    {
        m_nMinute = 0;
        m_fSeconds = 0f;
        oldSeconds = 0f;
        timerText = GetComponentInChildren<Text>();
       
    }

    void Update()
    {
        //時間経過
        m_fSeconds += Time.deltaTime;
        if (m_fSeconds >= 60f)
        {
            m_nMinute++;
            m_fSeconds = m_fSeconds - 60;
        }
        //　値が変わった時だけテキストUIを更新
        if ((int)m_fSeconds != (int)oldSeconds)
        {
            timerText.text = m_nMinute.ToString("00") + ":" + ((int)m_fSeconds).ToString("00");
        }
        oldSeconds = m_fSeconds;
    }
}
