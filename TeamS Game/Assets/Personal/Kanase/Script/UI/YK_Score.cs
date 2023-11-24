/**
 * @file   YK_Score.cs
 * @brief  �X�R�A���Ǘ�����ѕ\��
 * @author �g�c����
 * @date   2023/10/13
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class YK_Score : YK_UI
{
    Text scoreText; // �X�R�A��\�����邽�߂�Text�R���|�[�l���g�ւ̎Q��
    [SerializeField] private int m_nScore; //�X�R�A�̒l���i�[���邽�߂̃v���C�x�[�g�Ȑ���
    [SerializeField] private float m_fTime; //�X�R�A�̉��Z����
    [SerializeField] private int m_nDbgNum; //�f�o�b�N�p�̓G�̐��l
    public static YK_Score instance;         // YK_Score�̃C���X�^���X

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
    /**
     * @brief Start�͍ŏ��̃t���[���̑O�ɌĂяo����܂��B
     */
    void Start()
    {
        scoreText = GetComponent<Text>(); //����GameObject�ɃA�^�b�`���ꂽText�R���|�[�l���g���擾���܂�
        m_eUIType = UIType.Score; //< UI�̃^�C�v�� "Score" �ɐݒ�
        m_nScore = 0; // �X�R�A��0�ɏ��������܂� (�X�R�A�̃��Z�b�g)
    }

    /**
     * @brief Update�͊e�t���[��
     */
    void Update()
    {
        //�e�X�g
        if (Input.GetKeyDown(KeyCode.F9))
        {
            AddScore(10);
        }
        //�e�X�g
        if (Input.GetKeyDown(KeyCode.F10))
        {
            FieldAddScore(m_nDbgNum);
        }
        //�e�X�g
        if (Input.GetKeyDown(KeyCode.F11))
        {
            SkyAddScore(m_nDbgNum);
        }
        //�e�X�g
        if (Input.GetKeyDown(KeyCode.F12))
        {
            FarAddScore(m_nDbgNum);
        }
    }

    /**
     * @brief �X�R�A�Ɏw�肳�ꂽ�������Z
     * @param num �X�R�A�ɉ��Z���鐔�B
     */
    public void AddScore(int num)
    {
        //�X�R�A+num m_fTime�����߂�
        StartCoroutine(ScoreAnimation(num, m_fTime));
    }

    /**
     * @brief �X�R�A��0�Ƀ��Z�b�g
     */
    public void ResetScore()
    {
        m_nScore = 0;
    }
    /**
     * @brief �X�R�A��ǂ݂��݂Ȃ���
     */
    public void LoadScore()
    {
        // �e�L�X�g�̍X�V
        scoreText.text = "Score:" + m_nScore.ToString("D7");
    }

    // �X�R�A���A�j���[�V����������
    IEnumerator ScoreAnimation(int addScore, float time)
    {
        //�O��̃X�R�A
        int befor = m_nScore;
        //����̃X�R�A
        int after = m_nScore + addScore;
        //���_���Z
        m_nScore += addScore;
        //0f���o�ߎ��Ԃɂ���
        float elapsedTime = 0.0f;

        //time��0�ɂȂ�܂Ń��[�v������
        while (elapsedTime < time)
        {
            float rate = elapsedTime / time;
            int num;
            num = (int)(befor + (after - befor) * rate);
            // �e�L�X�g�̍X�V
            scoreText.text = "Score:" + num.ToString("D7");

            elapsedTime += Time.deltaTime;
            // 0.01�b�҂�
            yield return new WaitForSeconds(0.01f);
        }
        // �ŏI�I�Ȓ��n�̃X�R�A
        scoreText.text = "Score:" + m_nScore.ToString("D7");
    }
    /**
     * @brief �X�R�A�Ɏw�肳�ꂽ�������Z
     * @param num �G�̐�
     */
    public void FieldAddScore(int num)
    {
        float score;
        if (num == 1) 
            score = 100;
        else
        score = 100.0f * num * (1.1f + 0.05f * num);
        //�X�R�A+score m_fTime�����߂�
        StartCoroutine(ScoreAnimation((int)score, m_fTime));
    }
    /**
     * @brief �X�R�A�Ɏw�肳�ꂽ�������Z
     * @param num �G�̐�
     */
    public void SkyAddScore(int num)
    {
        float score;
        if (num == 1)
            score = 120;
        else
            score = 120.0f * num * (1.1f + 0.05f * num);
        //�X�R�A+score m_fTime�����߂�
        StartCoroutine(ScoreAnimation((int)score, m_fTime));
    }
    /**
     * @brief �X�R�A�Ɏw�肳�ꂽ�������Z
     * @param num �G�̐�
     */
    public void FarAddScore(int num)
    {
        float score;
        if (num == 1)
            score = 150;
        else
            score = 150.0f * num * (1.1f + 0.05f * num);
        //�X�R�A+score m_fTime�����߂�
        StartCoroutine(ScoreAnimation((int)score, m_fTime));
    }
    /**
     * @brief �X�R�A�Ɏw�肳�ꂽ�������Z
     * @param num �G�̐�
     */
    public void BlowAddScore(int num)
    {
        float score;
        score = 100.0f * num * (1.1f + 0.05f * num);
        //�X�R�A+score m_fTime�����߂�
        StartCoroutine(ScoreAnimation((int)score, m_fTime));
    }

    public int GetSetScore
    {
        get { return m_nScore; }
        set { m_nScore = value; }
    }
}
