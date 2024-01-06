using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;


public class MoveScene : MonoBehaviour
{
    public Fade fade;    //フェードキャンバス取得
    public static bool FadeFlg;

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
        // 現在のキーボード情報
        var current = Keyboard.current;

        // キーボード接続チェック
        if (current == null)
        {
            // キーボードが接続されていないと
            // Keyboard.currentがnullになる
            return;
        }
        // Aキーの入力状態取得
        var enterKey = current.enterKey;
        var escKey = current.escapeKey;

        if (escKey.wasPressedThisFrame)   //Esc入力
        {
            Application.Quit();             //ゲーム終了処理
        }
    }
    public void ReturnTitle()
    {
        //トランジションを掛けてシーン遷移する
        fade.FadeIn(1f, () =>
        {
            FadeFlg = true;
            Time.timeScale = 1.0f;  //タイムスケールリセット(無いとタイトルでタイムスケール弄ってる場合バグる)
            Debug.Log(FadeFlg);

            SceneManager.LoadScene("TitleScene");
        });
    }

    public void ReturnSelect()
    {
        fade.FadeIn(1f, () =>
         {
             FadeFlg = true;
             Time.timeScale = 1.0f;
             Debug.Log(FadeFlg);
             //GameManager.ClearFlg_player = false;
             //GameManager.WarningFlg = false;

             SceneManager.LoadScene("SelectScene");
         });
    }
}
