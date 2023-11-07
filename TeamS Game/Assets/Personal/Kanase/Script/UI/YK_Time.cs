/**
 * @file   YK_Time.cs
 * @brief  �o�ߎ��ԕW��
 * @author �g�c����
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
    //�@�O��Update�̎��̕b��
    private float oldSeconds;
    //�@�^�C�}�[�\���p�e�L�X�g
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
        //���Ԍo��
        m_fSeconds += Time.deltaTime;
        if (m_fSeconds >= 60f)
        {
            m_nMinute++;
            m_fSeconds = m_fSeconds - 60;
        }
        //�@�l���ς�����������e�L�X�gUI���X�V
        if ((int)m_fSeconds != (int)oldSeconds)
        {
            timerText.text = m_nMinute.ToString("00") + ":" + ((int)m_fSeconds).ToString("00");
        }
        oldSeconds = m_fSeconds;
    }
}
