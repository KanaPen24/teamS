using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
        UIs[(int)UIType.HighScore].Active(false);
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
                 UIs[(int)UIType.HighScore].Active(true);
                 YK_HighScore.instance.UpdateHighScore();
             });
        }
        //�|�[�Y����
        if ((Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(IS_XBoxInput.Menu))&&!Pause.GetSetPause)
        {
            UIs[(int)UIType.Pause].Active(true);
            GameManager.GetSetGameState = GameState.GamePause;
            PostEffect.SetGaussianRate(1.0f);
            Pause.GetSetPause = true;
        }
        else if ((Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(IS_XBoxInput.Menu))&& Pause.GetSetPause)
        {
            UIs[(int)UIType.Pause].Active(false);
            Pause.GetSetPause = false;
            PostEffect.SetGaussianRate(0.0f);
            GameManager.GetSetGameState = GameState.GamePlay;
        }
    }
}
