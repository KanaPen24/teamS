/**
 * @file   YK_HighScore.cs
 * @brief  �n�C�X�R�A���Ǘ�����ѕ\��
 * @author �g�c����
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
    public static YK_HighScore instance;         // YK_HighScore�̃C���X�^���X
    [SerializeField] List<Text> scoreText; // �X�R�A��\�����邽�߂�Text�R���|�[�l���g�ւ̎Q��

    /**
    * @fn
    * ����������(�O���Q�Ƃ�����)
    * @brief  �����o����������
    * @detail ���ɖ���
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
        //�e�X�g
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
                    m_nHighScore[m_nHighScore.Count - j - 1] = m_nHighScore[m_nHighScore.Count - j - 2];              //���̏��ʂɌ��X�̃n�C�X�R�A���ړ�������
                m_nHighScore[i] = YK_Score.instance.GetSetScore;    //�n�C�X�R�A���X�V����
                break;
            }
        }
    }

    //�n�C�X�R�A�̕\��
    private void DrawHighScore()
    {
        for (int i = 0; i < m_nRank; i++)
        {
            // �e�L�X�g�̍X�V
            scoreText[i].text = "Score:" + m_nHighScore[i].ToString("D7");
        }
    }

    //�Q�b�^�[�Z�b�^�[�n�C�X�R�A������
    public List<int> GetSetHighScore
    {
        get { return m_nHighScore; }
        set { m_nHighScore = value; }
    }
}
