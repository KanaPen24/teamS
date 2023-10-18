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
    protected int m_nObjID;           // objのID
    protected int m_nHitID;           // 当たり判定のID
    protected int m_nHp;              // 現在のHP
    protected int m_nMaxHp;           // 最大HP
    protected float m_fAccel;         // 加速度
    protected float m_fWeight;        // 重さ
    protected float m_fSpeed;         // 現在の速度
    protected float m_fMaxSpeed;      // 最大速度
    protected float m_fFallSpeed;     // 現在の落下速度
    protected float m_fMaxFallSpeed;  // 最大落下速度
    protected bool m_bExist;          // 存在しているか

    public virtual void UpdateObj()
    {
    }

    public virtual void UpdateDebug()
    {
        Debug.Log("base");
    }

    public virtual void GenerateHit()
    {
        m_nHitID = ON_HitManager.instance.GenerateHit(this.gameObject.transform.position,
        this.gameObject.transform.localScale / 2,
        true, HitType.BODY, GetSetObjID);

        Debug.Log("当たり判定生成 ID:" + GetSetHitID);
    }

    public virtual void CheckHit(HitData hitData)
    {
        int myID = hitData.myID;
        int otherID = hitData.otherID;
        HitState hitState = hitData.state;
    }

    public int GetSetObjID { get { return m_nObjID; } set { m_nObjID = value; } }
    public int GetSetHitID { get { return m_nHitID; } set { m_nHitID = value; } }
    public int GetSetHp    { get { return m_nHp; } set { m_nHp = value; } }
    public int GetSetMaxHp { get { return m_nMaxHp; } set { m_nMaxHp = value; } }
    public float GetSetAccel { get { return m_fAccel; } set { m_fAccel = value; } }
    public float GetSetWeight { get { return m_fWeight; } set { m_fWeight = value; } }
    public float GetSetSpeed { get { return m_fSpeed; } set { m_fSpeed = value; } }
    public float GetSetMaxSpeed { get { return m_fMaxSpeed; } set { m_fMaxSpeed = value; } }
    public float GetSetFallSpeed { get { return m_fFallSpeed; } set { m_fFallSpeed = value; } }
    public float GetSetMaxFallSpeed { get { return m_fMaxFallSpeed; } set { m_fMaxFallSpeed = value; } }
    public bool GetSetExist { get { return m_bExist; } set { m_bExist = value; } }
}
