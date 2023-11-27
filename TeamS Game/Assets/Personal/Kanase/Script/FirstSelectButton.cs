using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 *1.First Select Buttonをパネルに追加
 *2.インスペクターのFirst Select のところに一番最初に選択させたいボタンを追加 
 */
public class FirstSelectButton : MonoBehaviour
{
    public Button FirstSelect;

    void Start()
    {
        FirstSelect.Select();
    }

    void Update()
    {

    }
}
