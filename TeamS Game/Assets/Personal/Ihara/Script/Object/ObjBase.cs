/**
 * @file   ObjBase.cs
 * @brief  オブジェクト基底クラス
 * @author IharaShota
 * @date   2023/10/13
 * @Update 2023/10/13 作成
 **/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjBase : MonoBehaviour
{
    private int m_nObjID; // objのID
    private int m_nHitID; // 当たり判定のID

    public virtual void UpdateObj()
    {
    }

    public virtual void UpdateDebug()
    {
        Debug.Log("base");
    }

    public int GetSetObjID { get { return m_nObjID; } set { m_nObjID = value; } }
    public int GetSetHitID { get { return m_nHitID; } set { m_nHitID = value; } }
}
