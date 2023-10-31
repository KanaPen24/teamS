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
        if (Input.GetKeyDown(KeyCode.B))
        {
            AddScore(100);
        }
    }

    /**
     * @brief �X�R�A�Ɏw�肳�ꂽ�������Z
     * @param num �X�R�A�ɉ��Z���鐔�B
     */
    private void AddScore(int num)
    {
        //�X�R�A+num m_fTime�����߂�
        StartCoroutine(ScoreAnimation(num, m_fTime));
    }

    /**
     * @brief �X�R�A��0�Ƀ��Z�b�g
     */
    private void ResetScore()
    {
        m_nScore = 0;
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
            scoreText.text = "Score:" + num.ToString("D5");

            elapsedTime += Time.deltaTime;
            // 0.01�b�҂�
            yield return new WaitForSeconds(0.01f);
        }
        // �ŏI�I�Ȓ��n�̃X�R�A
        scoreText.text = "Score:" + m_nScore.ToString("D5");
    }
}
