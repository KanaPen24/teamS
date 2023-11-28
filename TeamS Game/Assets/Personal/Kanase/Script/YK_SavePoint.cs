/**
 * @file   YK_SavePoint.cs
 * @brief  セーブポイント
 * @author 吉田叶聖
 * @date   2023/11/24
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YK_SavePoint : MonoBehaviour
{
    private void Update()
    {
        if(ObjPlayer.instance.GetSetPos.x>=this.gameObject.transform.position.x)
        {
            YK_JsonSave.instance.Save(true);
            Destroy(this.gameObject);
        }
    }
}
