using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField] private List<YK_UI> UIs; // UI���i�[����z��
    [SerializeField] private YK_Pause Pause;  // Pause���i�[
    public static UIManager instance;
    [SerializeField] ON_VolumeManager PostEffect;    // �|�X�g�G�t�F�N�g

    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        UIs[(int)UIType.Pause].Active(false);
    }

    // Update is called once per frame
    void Update()
    {
        //�e�X�g
        if (Input.GetKeyDown(KeyCode.F4))
            GameManager.GetSetGameState = GameState.GameGoal;
        if (GameManager.GetSetGameState == GameState.GameGoal)
        {
            //�g�����W�V�������|���ăC�x���g�𔭐�������
            Fade.instance.FadeIn(1f, () =>
             {
                 GameManager.GetSetGameState = GameState.Result;
                 YK_JsonSave.instance.MyScoreSave(YK_Score.instance.GetSetScore);
                 SceneManager.LoadScene("ResultScene");
             });
        }
        //�|�[�Y����
        if ((Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(IS_XBoxInput.Menu))&&!Pause.GetSetPause)
        {
            DrawPause(true);
        }
        else if ((Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(IS_XBoxInput.Menu))&& Pause.GetSetPause)
        {
            DrawPause(false);
        }
    }

    public void DrawPause(bool flg)
    {
        UIs[(int)UIType.Pause].Active(flg);
        if (flg)
        {
            PostEffect.SetGaussianRate(1.0f);
            GameManager.GetSetGameState = GameState.GamePause;
        }
        else if (!flg)
        {
            PostEffect.SetGaussianRate(0.0f);
            GameManager.GetSetGameState = GameState.GamePlay;
        }
        Pause.GetSetPause = flg;
    }
}
