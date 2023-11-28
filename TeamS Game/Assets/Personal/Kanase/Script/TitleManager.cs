using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // UnityEngine.SceneManagemntの機能を使用
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TitleManager : MonoBehaviour
{
    [SerializeField] Canvas GameTitleCanvas = null;     //タイトル用
    [SerializeField] Canvas GameOptionCanvas = null;    //オプション用
    public Button FirstSelectOption;
    public Button FirstSelectTitle;
    private bool OptionOnFlg = false;

    public bool TitleStartFlg;               //ゲーム開始フラグ(音の判定用)
    AudioSource audioSource;
    [SerializeField] AudioClip selectSE;           //選択音
    private GameObject SelectObj;                           //選択しているオブジェクト格納用

    // Start is called before the first frame update
    void Start()
    {
        TitleStartFlg = false;
        GameTitleCanvas.gameObject.SetActive(true);     //タイトル画面は最初から表示
        GameOptionCanvas.gameObject.SetActive(false);   //オプション画面は非表示

        audioSource = GetComponent<AudioSource>();      //オーディオソースを取得しておく
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //選択されたボタンが切り替わった際に効果音を鳴らす処理
        if (SelectObj != EventSystem.current.currentSelectedGameObject)
        {
            //開始時に音が鳴らないようにするための処理
            if (TitleStartFlg == false)
            {
                SelectObj = EventSystem.current.currentSelectedGameObject;
                TitleStartFlg = true;    //フラグオンに
            }
            //以降はフォーカスしたオブジェクトが切り替わったタイミングで音が鳴る
            else
            {
                SelectObj = EventSystem.current.currentSelectedGameObject;
                audioSource.PlayOneShot(selectSE);      //選択音
            }
        }

        if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();             //ゲーム終了処理
        }
    }

    //呼び出された際、タイトル画面を消してオプション用画面を描画
    public void OptionOn()
    {
        GameTitleCanvas.gameObject.SetActive(false);    //タイトル画面非表示
        GameOptionCanvas.gameObject.SetActive(true);    //オプション画面表示
        FirstSelectOption.Select();
    }
    public void OptionOff()
    {
        GameTitleCanvas.gameObject.SetActive(true);    //タイトル画面表示
        GameOptionCanvas.gameObject.SetActive(false);  //オプション画面非表示
        FirstSelectTitle.Select();
        OptionOnFlg = false;
    }

    //オプションボタン選択時に行いたい処理
    public void OnClickOptionButton()
    {
        OptionOnFlg = true;
    }

    //スライダーの値が変わった時に呼び出して音を鳴らしたい
    public void ChangeSliderValue()
    {
        audioSource.PlayOneShot(selectSE);
    }
}

