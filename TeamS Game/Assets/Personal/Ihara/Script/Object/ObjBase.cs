/**
 * @file   ObjBase.cs
 * @brief  �I�u�W�F�N�g���N���X
 * @author IharaShota
 * @date   2023/10/13
 * @Update 2023/10/13 �쐬
 **/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjBase : MonoBehaviour
{
    protected int m_nObjID;           // obj��ID
    protected int m_nHitID;           // �����蔻���ID
    protected int m_nHp;              // ���݂�HP
    protected int m_nMaxHp;           // �ő�HP
    protected float m_fAccel;         // �����x
    protected float m_fWeight;        // �d��
    protected float m_fSpeed;         // ���݂̑��x
    protected float m_fMaxSpeed;      // �ő呬�x
    protected float m_fFallSpeed;     // ���݂̗������x
    protected float m_fMaxFallSpeed;  // �ő嗎�����x
    protected bool m_bExist;          // ���݂��Ă��邩

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

        Debug.Log("�����蔻�萶�� ID:" + GetSetHitID);
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
