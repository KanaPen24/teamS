using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

public class TitleButton : MonoBehaviour
{
    [SerializeField] private float InvokeTime = 0.5f;     //待ち時間
    [SerializeField] AudioClip decideSE;        //決定音
    [SerializeField] AudioClip selectSE;        //選択音
    AudioSource audioSource;
    [SerializeField]
    private GameObject OptionUI = null;
    [SerializeField]
    private GameObject OptionFirst; //オプション選択時最初にフォーカスさせたいボタン
    [SerializeField]
    private GameObject PauseFirst;  //ポーズ画面開いた時に最初にフォーカスさせたいボタン
    private bool OpitionFlg;        //オプション画面かどうかのフラグ

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        OptionUI.gameObject.SetActive(false);
    }
    //ボタンが押された時の関数名はなんでもよいが、
    //Unity側でボタンが押されたときに実行する
    //関数を設定するので、publicで公開する必要がある
    //ゲームスタート
    public void StartBt()
    {
        //トランジションを掛けてシーン遷移する
        Fade.instance.FadeIn(1f, () =>
        {
            GameManager.GetSetGameState = GameState.GameStart;
            SceneManager.LoadScene("GameScene");
        });
    }
    // Update is called once per frame
    private void FixedUpdate()
    {
        //オプション画面中にコントローラーのBを押したら
        if (OpitionFlg && Input.GetKeyDown(IS_XBoxInput.B))
        {
            OnClickReturn();
            OpitionFlg = false;
        }

    }
    //ゲーム終了
    public void GameExit()
    {
        #if UNITY_EDITOR    //EDITORで実行時用のゲーム終了処理
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        Application.Quit();
        #endif    
    }

    //ボタン押下時にフラグオン
    public void OnClickStart()
    {
        Invoke("StartBt", InvokeTime);  //指定の時間待ってからフェード開始(今は0.5秒)
        audioSource.PlayOneShot(decideSE);
    }
    //ボタン押下時にフラグオン
    public void OnClickOption()
    {
        //オプション画面を開いたとき
        OpitionFlg = true;
        OptionUI.gameObject.SetActive(true);        
        EventSystem.current.SetSelectedGameObject(OptionFirst);
        audioSource.PlayOneShot(selectSE);
    }
    //ボタン押下時にフラグオン
    public void OnClickExit()
    {
        Invoke("GameExit", InvokeTime * 2);  //指定の時間待ってからゲーム終了(今は1秒)
        audioSource.PlayOneShot(decideSE);
    }
    public void OnClickReturn()
    {
        //オプション画面を閉じた時        
        OptionUI.gameObject.SetActive(false);
        EventSystem.current.SetSelectedGameObject(PauseFirst);
    }
}
