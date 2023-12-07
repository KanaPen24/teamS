using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private List<YK_UI> UIs; // UIを格納する配列
    [SerializeField] private YK_Pause Pause;  // Pauseを格納
    public static UIManager instance;
    [SerializeField] ON_VolumeManager PostEffect;    // ポストエフェクト

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
        //テスト
        if (Input.GetKeyDown(KeyCode.F4))
            GameManager.GetSetGameState = GameState.GameGoal;
        if (GameManager.GetSetGameState == GameState.GameGoal)
        {
            //トランジションを掛けてイベントを発生させる
            Fade.instance.FadeIn(1f, () =>
             {
                 GameManager.GetSetGameState = GameState.Result;
                 UIs[(int)UIType.HighScore].Active(true);
                 YK_HighScore.instance.UpdateHighScore();
             });
        }
        //ポーズ処理
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
