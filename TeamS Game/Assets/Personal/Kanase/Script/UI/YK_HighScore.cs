/**
 * @file   YK_HighScore.cs
 * @brief  �n�C�X�R�A���Ǘ�����ѕ\��
 * @author �g�c����
 * @date   2023/11/28
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YK_HighScore : MonoBehaviour
{
    private List<int> m_nHighScore;
    [SerializeField] private int m_nRank;
    public static YK_HighScore instance;         // YK_HighScore�̃C���X�^���X

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
                m_nHighScore[i + 1] = m_nHighScore[i];              //���̏��ʂɌ��X�̃n�C�X�R�A���ړ�������
                m_nHighScore[i] = YK_Score.instance.GetSetScore;    //�n�C�X�R�A���X�V����
            }
        }
    }

    //�Q�b�^�[�Z�b�^�[�n�C�X�R�A������
    public List<int> GetSetHighScore
    {
        get { return m_nHighScore; }
        set { m_nHighScore = value; }
    }
}
