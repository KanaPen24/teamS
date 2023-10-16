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
        /**
         * ���݂̃X�R�A��\�����邽�߂�Text�R���|�[�l���g���X�V���܂��B
         * �X�R�A��5���̃[�����ߌ`���ŕ\������܂��B
         */
        scoreText.text = "Score:" + m_nScore.ToString("D5");
    }

    /**
     * @brief �X�R�A�Ɏw�肳�ꂽ�������Z
     * @param num �X�R�A�ɉ��Z���鐔�B
     */
    private void AddScore(int num)
    {
        m_nScore += num;
    }

    /**
     * @brief �X�R�A��0�Ƀ��Z�b�g
     */
    private void ResetScore()
    {
        m_nScore = 0;
    }
}
