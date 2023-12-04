/**
 * @file   IS_Pause.cs
 * @brief  ポーズクラス
 * @author IharaShota
 * @date   2023/03/28
 * @Update 2023/03/28 作成
 **/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering;

public class YK_Pause : YK_UI
{    
    private bool m_bPause;

    private void Start()
    {
        m_eUIType = UIType.Pause;     
        m_bPause = false;
    }
    // Update is called once per frame
    
    public bool GetSetPause
    {
        get { return m_bPause; }
        set { m_bPause = value; }
    }
}
