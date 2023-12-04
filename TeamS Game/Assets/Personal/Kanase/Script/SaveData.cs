﻿/**
 * @file   SaveData.cs
 * @brief  何をセーブするか定義する場所
 * @author 吉田叶聖
 * @date   2023/06/20
 */
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
//保存するデータクラス
public class SaveData
{
    public Vector3 pos; //座標
    public int Score;    //スコア
    public bool RetryFlg;
}
[System.Serializable]
public class SaveData2
{
    public List<int> HighScore = new List<int>(5); //ハイスコア
}
