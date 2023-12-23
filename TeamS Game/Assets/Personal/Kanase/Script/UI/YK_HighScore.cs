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
using UnityEngine.SceneManagement;
using DG.Tweening;

public class YK_HighScore : YK_UI
{
    private List<int> m_nHighScore;
    [SerializeField] private int m_nRank = 6;
    public static YK_HighScore instance;         // YK_HighScore�̃C���X�^���X
    [SerializeField] List<Text> scoreText; // �X�R�A��\�����邽�߂�Text�R���|�[�l���g�ւ̎Q��
    [SerializeField] Text MyscoreText; // �����̃X�R�A��\�����邽�߂�Text
    private int storage;
    private bool m_bDrawflg = false;         //���ʕϓ����Ȃ������ꍇ�̃t���O
    [SerializeField] private float dotweenInterval;
    [SerializeField] private float Movetime;
    [SerializeField] private Image New;

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
        //�^�C�v�̐ݒ�
        m_eUIType = UIType.HighScore;
        Obj = this.gameObject;
        UpdateHighScore();
    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.GetSetGameState==GameState.Result&& Input.GetKeyDown(IS_XBoxInput.A))
        {
            //�g�����W�V�������|���ăV�[���J�ڂ���
            Fade.instance.FadeIn(1f, () =>
            {
                GameManager.GetSetGameState = GameState.Title;
                SaveHighScore();
                SceneManager.LoadScene("TitleScene");
            });
        }
    }

    private void SaveHighScore()
    {
        YK_JsonSave.instance.HighScoreSave(m_nHighScore);
    }

    private void ChangeHighScore()
    {
        m_nHighScore = YK_JsonSave.instance.HighScoreLoad();
        for (int i = 0; i < m_nRank; i++)
        {
            //���U���g�V�[���l�ɕύX
            if (m_nHighScore[i] <= YK_JsonSave.instance.MyScoreLoad())
            {
                int a = 0;  //�z��ړ��p
                storage = i;    //�X�V���ꂽ���ʂ�ۑ�
                for (int j = i; j <= m_nRank - 1; j++)
                {
                    //���̏��ʂɌ��X�̃n�C�X�R�A���ړ�������
                    m_nHighScore[m_nRank - a] = m_nHighScore[m_nRank - 1 - a];
                    a++;
                }
                m_nHighScore[i] = YK_JsonSave.instance.MyScoreLoad();    //�n�C�X�R�A���X�V����
                //���ʕϓ�������ꍇ
                m_bDrawflg = false;
                break;
            }
            else    //���ʕϓ����Ȃ��ꍇ
            {
                m_bDrawflg = true;
            }
                
        }
    }

    //�n�C�X�R�A�̕\��
    private void DrawHighScore()
    {
        for (int i = 0; i < m_nRank; i++)
        {
            // �e�L�X�g�̍X�V
            scoreText[i].text = m_nHighScore[i].ToString("D7");
        }
        //�����̃X�R�A��\��
        MyscoreText.text= YK_JsonSave.instance.MyScoreLoad().ToString("D7");
        //���ʕϓ����Ȃ������牉�o�͂���Ȃ�
        if (m_bDrawflg) return;
        Vector3 RectTransform_get;
        Vector3 RectTransform_New;
        RectTransform_get = scoreText[storage].rectTransform.position;
        RectTransform_New = New.rectTransform.position;
        New.rectTransform.anchoredPosition = new Vector3(-226f, -200, 0);
        scoreText[storage].rectTransform.anchoredPosition = new Vector3(-2.0f, -200, 0); 
        scoreText[storage].rectTransform.DOMove(RectTransform_get, Movetime).OnComplete(() =>
        {
            scoreText[storage].DOFade(0.0f, dotweenInterval)   // �A���t�@�l��0�ɂ��Ă���
                       .SetLoops(-1, LoopType.Yoyo);    // �s�����𖳌��ɌJ��Ԃ�
        });
        New.rectTransform.DOMove(new Vector3(RectTransform_New.x, RectTransform_get.y + 5.0f), Movetime);
    }

    //�Q�b�^�[�Z�b�^�[�n�C�X�R�A������
    public List<int> GetSetHighScore
    {
        get { return m_nHighScore; }
        set { m_nHighScore = value; }
    }

    //�����L���O�̓n��
    public int GetRank()
    {
        return m_nRank;
    }
    
    //�n�C�X�R�A�X�V�̈ꏄ�֐�
    public void UpdateHighScore()
    {
        ChangeHighScore();
        //SaveHighScore();
        DrawHighScore();
    }
}
