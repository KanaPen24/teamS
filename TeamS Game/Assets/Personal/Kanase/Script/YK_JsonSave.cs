/**
 * @file   SaveData.cs
 * @brief  Jsonを利用したセーブとロード
 * @author 吉田叶聖
 * @date   2023/06/20
 */
 using System.Collections;
using System.Collections.Generic;
using System.IO;  //StreamWriterなどを使うために追加
using UnityEngine;

public class YK_JsonSave : MonoBehaviour
{
    public static YK_JsonSave instance;         // インスタンス化
    [HideInInspector] public SaveData data;     // json変換するデータのクラス
    string filepath;                            // jsonファイルのパス
    string fileName = "SaveData.json";          // jsonファイル名
    [HideInInspector] public SaveData2 data2;   // json変換するデータのクラス
    string filepath2;                           // jsonファイルのパス
    string fileName2 = "SaveData2.json";        // jsonファイル名                                                
    private bool m_bOne = true;

    //-------------------------------------------------------------------
    // 開始時にファイルチェック、読み込み
    void Awake()
    {
        // パス名取得
        filepath = Application.dataPath + "/" + fileName;
        // パス名取得
        filepath2 = Application.dataPath + "/" + fileName2;

        //ファイルがあれば削除
        if (File.Exists(filepath))
        {
            DelFile();                     // セーブデータの削除
        }
        //ハイスコア用のファイルがなければ
        if(!File.Exists(filepath2))
        {
            data2.HighScore = new List<int> { 5, 4, 3, 2, 1, 0 };
            data2.MyScore = 0;
            Save2(data2);   //セーブデータを生成する
        }
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void FixedUpdate()
    {
        if (data.RetryFlg&&m_bOne)
        {
            //GameManager.instance.GetSetGameState = GameState.GamePlay;
            m_bOne = false;
        }
    }

    //-------------------------------------------------------------------
    // jsonとしてデータを保存
    void Save(SaveData data)
    {
        data.pos = ObjPlayer.instance.GetSetPos;          // プレイヤーの座標保存
        data.Score = YK_Score.instance.GetSetScore; //スコアの保存
        string json = JsonUtility.ToJson(data);                 // jsonとして変換
        StreamWriter wr = new StreamWriter(filepath, false);    // ファイル書き込み指定
        wr.WriteLine(json);                                     // json変換した情報を書き込み
        wr.Close();                                             // ファイル閉じる
        Debug.Log("セーブ!!!!!!");
    }
    //-------------------------------------------------------------------
    // jsonとしてデータを保存
    void Save2(SaveData2 data2)
    {
        string json = JsonUtility.ToJson(data2);                 // jsonとして変換
        StreamWriter wr = new StreamWriter(filepath2, false);    // ファイル書き込み指定
        wr.WriteLine(json);                                     // json変換した情報を書き込み
        wr.Close();                                             // ファイル閉じる
        Debug.Log("セーブ!!!!!!");
    }

    //-------------------------------------------------------------------
    // jsonとしてデータを保存
    void MyScoreSave(SaveData2 data2)
    {
        data2.HighScore = HighScoreLoad();
        string json = JsonUtility.ToJson(data2);                 // jsonとして変換
        StreamWriter wr = new StreamWriter(filepath2, false);    // ファイル書き込み指定
        wr.WriteLine(json);                                     // json変換した情報を書き込み
        wr.Close();                                             // ファイル閉じる
        Debug.Log("セーブ!!!!!!");
    }

    // jsonファイル読み込み
    SaveData Load(string path)
    {    
        StreamReader rd = new StreamReader(path);               // ファイル読み込み指定
        string json = rd.ReadToEnd();                           // ファイル内容全て読み込む
        rd.Close();                                             // ファイル閉じる        
        Debug.Log(json);
        Debug.Log("ロード!!!!!!");
        return JsonUtility.FromJson<SaveData>(json);            // jsonファイルを型に戻して返す
    }
    // jsonファイル2読み込み
    SaveData2 Load2(string path)
    {
        StreamReader rd = new StreamReader(path);               // ファイル読み込み指定
        string json = rd.ReadToEnd();                           // ファイル内容全て読み込む
        rd.Close();                                             // ファイル閉じる        
        Debug.Log(json);
        Debug.Log("ロード!!!!!!");
        return JsonUtility.FromJson<SaveData2>(json);            // jsonファイルを型に戻して返す
    }

    //プレイヤーとぶつかったら
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            Save(true);
    }

    //外部でもセーブが呼べるように
    public void Save(bool reflg)
    {
        data.RetryFlg = reflg;
        Save(data);
    }

    //外部でハイスコアのセーブが呼べるように
    public void HighScoreSave(List<int> HighScore)
    {
        data2.HighScore = HighScore;
        Save2(data2);
        Debug.Log("セーブ!!!!!!");
    }

    //外部でハイスコアのロードが呼べるように
    public List<int> HighScoreLoad()
    {
        //ファイルを読み込んでdataに格納
        data2 = Load2(filepath2);
        return data2.HighScore;     
    }
    //外部でハイスコアのロードが呼べるように
    public int MyScoreLoad()
    {
        //ファイルを読み込んでdataに格納
        data2 = Load2(filepath2);
        return data2.MyScore;
    }

    //外部でもロードが呼べるように
    public void Load()
    {
        //ファイルを読み込んでdataに格納
        data = Load(filepath);

        //データを反映
        ObjPlayer.instance.GetSetPos = data.pos;
        YK_Score.instance.GetSetScore = data.Score;
    }

    //自分のスコアセーブ用
    public void MyScoreSave(int score)
    {
        data2.MyScore = score;
        MyScoreSave(data2);
    }


    //ゲーム終了時
    private void OnApplicationQuit()
    {
        DelFile();
    }

    public void DelFile()
    {
        //ファイル削除
        File.Delete(filepath);
        Debug.Log("ファイル削除");
    }

    public bool GetSetResetFlg
    {
        get { return data.RetryFlg; }
        set { data.RetryFlg = value; }
    }

}
