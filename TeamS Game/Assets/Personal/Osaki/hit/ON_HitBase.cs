/**
@file   ON_HitBase.cs
@brief  当たり判定クラス
@author Norialo Osaki
@date   2023/10/13
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 当たり判定のタイプ
public enum HitType
{
    NONE = 0,

    ATTACK,     // 攻撃
    SPECIAL,    // 必殺
    BODY,       // 体
    BULLET,     // 弾系統
    FIELD,      // ステージ
    STEP,       // 足場

    MAX_TYPE,
}

public class ON_HitBase
{
    // コンストラクタ
    public ON_HitBase(Vector3 center, Vector3 size, int ID) : this(center, size, true, HitType.BODY, ID)
    {
        m_hitID = 0;
    }

    public ON_HitBase(Vector3 center, Vector3 size, bool active, HitType type, int ID)
    {
        m_center = center;
        m_size = size;
        m_active = active;
        m_type = type;
        m_objID = ID;
        m_hitID = 0;
    }

    // 中心
    public void SetCenter(Vector3 value) { m_center = value; }
    public Vector3 GetCenter() { return m_center; }

    // 大きさ
    public void SetSize(Vector3 value) { m_size = value; }
    public Vector3 GetSize() { return m_size; }

    // 有効
    public void SetActive(bool value) { m_active = value; }
    public bool GetActive() { return m_active; }

    // タイプ
    public void SetHitType(HitType value) { m_type = value; }
    public HitType GetHitType() { return m_type; }

    // objID
    public void SetObjID(int value) { m_objID = value; }
    public int GetObjID() { return m_objID; }

    // hitID
    public void SetHitID(int value) { m_hitID = value; }
    public int GetHitID() { return m_hitID; }

    // 当たり判定の移動
    public void MoveHit(Vector3 vec) { m_center += vec; }

    private Vector3   m_center; // 当たり判定の中心
    private Vector3   m_size;   // 当たり判定の大きさ(中心からの距離
    private bool      m_active; // 当たり判定が有効か(true:有効 false:無効
    private HitType   m_type;   // 当たり判定のタイプ
    private int       m_objID;  // 当たり判定のあるオブジェクトID
    private int       m_hitID;  // 当たり判定の識別ID
}
