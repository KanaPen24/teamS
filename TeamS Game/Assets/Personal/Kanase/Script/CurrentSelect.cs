using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class CurrentSelect : MonoBehaviour
{
    // eventSystemを取得するための宣言
    private GameObject SelectButton;
    //private bool Keybordflg = true;          //キーボードフラグ
    private bool GetStart = false;  //フラグの格納用
    private bool GetOption = false; //フラグの格納用
    private bool GetExit = false;   //フラグの格納用
    private TitleButton TitleButtonScript;  //タイトルボタンスクリプト
    [SerializeField] private EventSystem eventSystem;
    [SerializeField] private Button StartButton;    //ボタンとして取得
    [SerializeField] private Button OptionButton;   //ボタンとして取得
    [SerializeField] private Button ExitButton;     //ボタンとして取得
    [SerializeField] private GameObject Cursor1;    //カーソルを入れる(スタート用)
    [SerializeField] private GameObject Cursor2;    //カーソルを入れる(オプション用)
    [SerializeField] private GameObject Cursor3;    //カーソルを入れる(終了用)
    //音関連
    [SerializeField] AudioClip selectSE;            //選択音
    AudioSource audioSource;
    [SerializeField] private GameObject StartBObj;  //ボタンをオブジェクトとして取得(SEに使ってるだけ)
    [SerializeField] private GameObject OptionBObj; //ボタンをオブジェクトとして取得(SEに使ってるだけ)
    [SerializeField] private GameObject ExitBObj;   //ボタンをオブジェクトとして取得(SEに使ってるだけ)
    private bool SEFlgSta; //判定用
    private bool SEFlgOp;  //判定用
    private bool SEFlgEx;  //判定用

    // Start is called before the first frame update
    void Start()
    {
        //ボタンの有効化
        StartButton.interactable = true;    
        OptionButton.interactable = true;
        ExitButton.interactable = true;
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        SelectButton = eventSystem.currentSelectedGameObject;   //選択されているボタンの取得

        #region ボタン拡縮
        //選択されているボタンの拡縮を繰り返すアニメーション呼び出し
        //if (SelectButton == StartButton)  //StartButtonが選択されているなら
        //{
        //    StartButton.animator.SetTrigger("Selected");    //スタートボタンのアニメーション呼び出し
        //}
        //else if(SelectButton == OptionButton)   //オプションボタンが選択されているなら
        //{
        //    OptionButton.animator.SetTrigger("Selected");   //オプションボタンのアニメーション呼び出し
        //}
        //else if(SelectButton == ExitButton)     //終了ボタンが選択されているなら
        //{
        //    ExitButton.animator.SetTrigger("Selected");     //終了ボタンのアニメーション呼び出し
        //}
        #endregion

        #region 選択音
        if (SceneManager.GetActiveScene().name == "SelectScene")
        {
            if (SelectButton == StartBObj)  //選択されているのがスタートボタンだったら
            {
                //他のボタンを選択時に音が鳴る用準備
                SEFlgOp = true;     //フラグオン
                SEFlgEx = true;     //フラグオン
                if (SEFlgSta == true)
                {
                    //一度だけ再生したいので条件内のフラグをオフに
                    audioSource.PlayOneShot(selectSE);  //SE再生
                    SEFlgSta = false;
                }
            }
            else if (SelectButton == OptionBObj)    //選択されているボタンがオプションボタンだったら
            {
                //他のボタンを選択時に音が鳴る用準備
                SEFlgSta = true;    //フラグオン
                SEFlgEx = true;     //フラグオン
                if (SEFlgOp == true)
                {
                    //一度だけ再生したいので条件内のフラグをオフに
                    audioSource.PlayOneShot(selectSE);  //SE再生
                    SEFlgOp = false;
                }
            }
            else if (SelectButton == ExitBObj)      //選択されているボタンが終了ボタンだったら
            {
                //他のボタンを選択時に音が鳴る用準備
                SEFlgSta = true;    //フラグオン
                SEFlgOp = true;     //フラグオン
                if (SEFlgEx == true)
                {
                    //一度だけ再生したいので条件内のフラグをオフに
                    audioSource.PlayOneShot(selectSE);  //SE再生
                    SEFlgEx = false;
                }
            }
        }
        #endregion

        #region ボタンの無効化
        //各カーソルがアクティブ化したらボタンを無効化
        //無効化しないとシーン遷移後、他のボタンをセレクトできてしまう
        //カーソルが表示されていたらモード選択が済んでいる状態なのでそのタイミングでボタン無効化
        if (Cursor1.activeInHierarchy)  //カーソルが表示されているか
        {
            StartButton.interactable = false;   //スタートボタンを無効化
        }
        else if (Cursor2.activeInHierarchy) //カーソルが表示されているか
        {
            OptionButton.interactable = false;  //オプションボタンを無効化
        }
        else if (Cursor3.activeInHierarchy) //カーソルが表示されているか
        {
            ExitButton.interactable = false;    //終了ボタンを無効化
        }
        #endregion
    }
    
    //タイトル画面に戻る
    public void OnClickReturn()
    {
        OptionButton.interactable = !OptionButton.interactable;
    }
}
