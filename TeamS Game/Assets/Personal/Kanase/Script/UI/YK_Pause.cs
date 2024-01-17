/**
 * @file   IS_Pause.cs
 * @brief  ポーズクラス
 * @author KanaseYoshida
 * @date   2023/03/28
 * @Update 2023/03/28 作成
 **/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Rendering;
using UnityEngine.EventSystems;

public class YK_Pause : YK_UI
{    
    private bool m_bPause;
    [SerializeField]
    private GameObject pauseUI = null;
    [SerializeField]
    private GameObject ReturnTitleUI = null;
    [SerializeField]
    private GameObject OptionUI = null;
    [SerializeField]
    private GameObject PauseFirst;  //ポーズ画面開いた時に最初にフォーカスさせたいボタン
    [SerializeField]
    private GameObject RTFirst;     //タイトル戻る選択時に最初にフォーカスさせたいボタン
    [SerializeField]
    private GameObject OptionFirst; //オプション選択時最初にフォーカスさせたいボタン

    private bool OpitionFlg;        //オプション画面かどうかのフラグ

    private void Start()
    {
        m_eUIType = UIType.Pause;     
        m_bPause = false;
        OptionUI.gameObject.SetActive(false);
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
    public bool GetSetPause
    {
        get { return m_bPause; }
        set { m_bPause = value; }
    }
    public void OnclickReturnGame()
    {
        //ゲームに戻るを選択したとき
        Time.timeScale = 1;
       // EventSystem.current.SetSelectedGameObject(RTFirst); //一旦ゲームに戻るボタンからフォーカスを外す
        UIManager.instance.DrawPause(false);
        m_bPause = false;
    }
    public void OnClickOption()
    {
        //オプション画面を開いたとき
        OpitionFlg = true;
        OptionUI.gameObject.SetActive(true);
        pauseUI.gameObject.SetActive(false);
        EventSystem.current.SetSelectedGameObject(OptionFirst);
    }
    public void OnclickReturnTitle()
    {
        //タイトルへ戻るを選択したとき
        //ReturnTitleUI.gameObject.SetActive(true);
        //pauseUI.gameObject.SetActive(false);
        //EventSystem.current.SetSelectedGameObject(RTFirst);
        //トランジションを掛けてシーン遷移する
        Fade.instance.FadeIn(1f, () =>
        {
            GameManager.GetSetGameState = GameState.Title;
            SceneManager.LoadScene("TitleScene");
        });
    }
    public void OnClickYes()
    {
        //タイトルに戻る時
        Time.timeScale = 1;
        SceneManager.LoadScene("TitleScene");
    }
    public void OnClickNo()
    {
        //タイトルに戻らなかったとき
        ReturnTitleUI.gameObject.SetActive(false);
        pauseUI.gameObject.SetActive(true);
        EventSystem.current.SetSelectedGameObject(PauseFirst);
    }
    public void OnClickReturn()
    {
        //オプション画面を閉じた時
        pauseUI.gameObject.SetActive(true);
        OptionUI.gameObject.SetActive(false);
        EventSystem.current.SetSelectedGameObject(PauseFirst);
    }

}
