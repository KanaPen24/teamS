/**
 * @file   ObjBase.cs
 * @brief  オブジェクト基底クラス
 * @author IharaShota
 * @date   2023/10/13
 * @Update 2023/10/13 作成
 **/


// memo 地面に当たっているか○
//      攻撃を喰らう仮想関数が欲しい○
//      向きが欲しい○
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// --- オブジェクトの向き ---
public enum ObjDir
{
    NONE = 0,
    RIGHT,
    LEFT,

    MaxDir
}

// --- オブジェクトのタイプ ---
public enum ObjType
{
    Player,
    Enemy,
    Field,

    MaxType
}

// ----- 地面の情報を格納するクラス ------------------------------------------------------
[System.Serializable]
public class Ground
{
    [SerializeField] public bool m_bStand;    // 地面に立っているか
    [SerializeField] public Vector2 m_vCenter;// 中心座標 
    [SerializeField] public Vector2 m_vSize;  // 大きさ
}
// ----------------------------------------------------------------------------------------

// ----- 無敵状態を管理するクラス ------------------------------------------
[System.Serializable]
public class Invincible
{
    [SerializeField] public bool m_bInvincible; // 無敵かどうか
    [SerializeField] public float m_fTime;      // 無敵時間

    public void SetInvincible(float i)
    {
        m_bInvincible = true;
        m_fTime = i;
    }
}

// --- インスペクターで初期値を設定するクラス ------------------------------
[System.Serializable]
public class InitParam
{
    //[SerializeField] private int m_nHp;           // 現在のHP
    //[SerializeField] private int m_nMaxHp;        // 最大HP
    [SerializeField] private float m_fAccel;      // 加速度
    [SerializeField] private float m_fWeight;     // 重さ
    [SerializeField] private Vector2 m_vMaxSpeed; // 最大速度
    [SerializeField] private bool m_bExist;       // 存在しているか
    [SerializeField] private ObjDir m_eDir;       // 向き
    [SerializeField] private ObjType m_eType;     // タイプ

    //public int GetSetHp { get { return m_nHp; } set { m_nHp = value; } }
    //public int GetSetMaxHp { get { return m_nMaxHp; } set { m_nMaxHp = value; } }
    public float GetSetAccel { get { return m_fAccel; } set { m_fAccel = value; } }
    public float GetSetWeight { get { return m_fWeight; } set { m_fWeight = value; } }
    public Vector2 GetSetMaxSpeed { get { return m_vMaxSpeed; } set { m_vMaxSpeed = value; } }
    public bool GetSetExist { get { return m_bExist; } set { m_bExist = value; } }
    public ObjDir GetSetDir { get { return m_eDir; } set { m_eDir = value; } }
    public ObjType GetSetType { get { return m_eType; } set { m_eType = value; } }
}
// -------------------------------------------------------------------------

// --- インスペクターでパラメーターを参照するクラス -----------------------
[System.Serializable]
public class CheckParam
{
    [SerializeField] private Ground m_Ground;     // 地面の情報
    [SerializeField] private Invincible m_Invincible; // 無敵情報
    [SerializeField] private int m_nObjID;        // objのID
    [SerializeField] private int m_nHitID;        // 当たり判定のID
    //[SerializeField] private int m_nHp;           // 現在のHP
    //[SerializeField] private int m_nMaxHp;        // 最大HP
    [SerializeField] private float m_fAccel;      // 加速度
    [SerializeField] private float m_fWeight;     // 重さ
    [SerializeField] private Vector2 m_vSpeed;    // 速度
    [SerializeField] private Vector2 m_vMaxSpeed; // 最大速度
    [SerializeField] private Vector2 m_vMove;     // 移動量
    [SerializeField] private bool m_bExist;       // 存在しているか
    [SerializeField] private ObjDir m_eDir;       // 向き
    [SerializeField] private ObjType m_eType;     // タイプ

    public int GetSetObjID { get { return m_nObjID; } set { m_nObjID = value; } }
    public int GetSetHitID { get { return m_nHitID; } set { m_nHitID = value; } }
    //public int GetSetHp { get { return m_nHp; } set { m_nHp = value; } }
    //public int GetSetMaxHp { get { return m_nMaxHp; } set { m_nMaxHp = value; } }
    public float GetSetAccel { get { return m_fAccel; } set { m_fAccel = value; } }
    public float GetSetWeight { get { return m_fWeight; } set { m_fWeight = value; } }
    public Vector2 GetSetSpeed { get { return m_vSpeed; } set { m_vSpeed = value; } }
    public Vector2 GetSetMaxSpeed { get { return m_vMaxSpeed; } set { m_vMaxSpeed = value; } }
    public Vector2 GetSetMove { get { return m_vMove; } set { m_vMove = value; } }
    public bool GetSetExist { get { return m_bExist; } set { m_bExist = value; } }
    public ObjDir GetSetDir { get { return m_eDir; } set { m_eDir = value; } }
    public ObjType GetSetType { get { return m_eType; } set { m_eType = value; } }
    public Ground GetSetGround { get { return m_Ground; } set { m_Ground = value; } }
    public Invincible GetSetInvincible { get { return m_Invincible; } set { m_Invincible = value; } }
}
// ------------------------------------------------------------------------

public class ObjBase : MonoBehaviour
{
    // ----- 変数宣言 ------
    [SerializeField]
    public SpriteRenderer texObj;     // オブジェクトの画像
    [SerializeField]
    protected InitParam InitParam;    // 初期化用パラメータ
    [SerializeField]
    protected CheckParam CheckParam;  // 参照用パラメータ
    [HideInInspector]
    public Ground m_Ground;           // 地面の情報
    [HideInInspector]
    public Invincible m_Invincible;   // 無敵情報
    protected int m_nObjID;           // objのID
    protected int m_nHitID;           // 当たり判定のID
    //protected int m_nHp;              // 現在のHP
    //protected int m_nMaxHp;           // 最大HP
    protected float m_fAccel;         // 加速度
    protected float m_fWeight;        // 重さ
    protected Vector2 m_vSpeed;       // 現在の速度
    protected Vector2 m_vMaxSpeed;    // 最大速度
    protected Vector2 m_vMove;        // 移動量
    //protected bool m_bStand;          // 地面に立っているか
    protected bool m_bExist;          // 存在しているか
    protected ObjDir m_eDir;          // 向き
    protected ObjType m_eType;        // タイプ
    // ---------------------

    // --- 初期化関数 ---
    public virtual void InitObj()
    {
        //m_nHp = InitParam.GetSetHp;
        //m_nMaxHp = InitParam.GetSetMaxHp;
        m_fAccel = InitParam.GetSetAccel;
        m_fWeight = InitParam.GetSetWeight;
        m_vMaxSpeed = InitParam.GetSetMaxSpeed;
        m_bExist = InitParam.GetSetExist;
        m_eDir = InitParam.GetSetDir;
        m_eType = InitParam.GetSetType;
    }

    // --- 参照パラメーター更新関数 ---
    public virtual void UpdateCheckParam()
    {
        CheckParam.GetSetObjID = m_nObjID;
        CheckParam.GetSetHitID = m_nHitID;
        //CheckParam.GetSetHp = m_nHp;
        //CheckParam.GetSetMaxHp = m_nMaxHp;
        CheckParam.GetSetAccel = m_fAccel;
        CheckParam.GetSetWeight = m_fWeight;
        CheckParam.GetSetSpeed = m_vSpeed;
        CheckParam.GetSetMaxSpeed = m_vMaxSpeed;
        CheckParam.GetSetMove = m_vMove;
        CheckParam.GetSetExist = m_bExist;
        CheckParam.GetSetDir = m_eDir;
        CheckParam.GetSetType = m_eType;
        CheckParam.GetSetGround = m_Ground;
        CheckParam.GetSetInvincible = m_Invincible;
    }

    // --- 更新関数 ---
    public virtual void UpdateObj()
    {
    }

    // --- デバッグ更新処理 ---
    public virtual void UpdateDebug()
    {
        Debug.Log("base");
    }

    // --- 攻撃を当てられた ---
    public virtual void DamageAttack()
    {

    }
    
    // --- ノックバック関数 ---
    public virtual void KnockBackObj(ObjDir dir)
    {

    }

    // --- オブジェクト消去 ---
    public virtual void DestroyObj()
    {
        // 当たり判定削除 → オブジェクト消去
        ON_HitManager.instance.DeleteHit(m_nHitID);
        Destroy(this.gameObject);
    }

    //  ---- 当たり判定を生成した際の処理 ----
    public virtual void GenerateHit()
    {
        // 生成時に当たり判定IDを取得し、初期値を設定している(中心座標,大きさ,当たり判定の種類,objのID)
        m_nHitID = ON_HitManager.instance.GenerateHit(this.gameObject.transform.position,
        this.gameObject.transform.localScale / 2,
        true, HitType.BODY, GetSetObjID);

        if (GameManager.IsDebug())
            Debug.Log("当たり判定生成 HitID: " + GetSetHitID +
                "scale: " + this.gameObject.transform.localScale / 2);
    }

    // --- 地面判定処理 ---
    public virtual void CheckObjGround()
    {
        // オブジェクトのタイプが「FIELD」だったら
        // 地面に立っている状態にする → 落下速度を0で終了
        if(m_eType == ObjType.Field)
        {
            m_Ground.m_bStand = true;
            m_vSpeed.y = 0f;
            return;
        }

        // 今現在立っている地面を離れたら…
        if (GetSetPos.x + GetSetScale.x / 2f < m_Ground.m_vCenter.x - (m_Ground.m_vSize.x / 2f) ||
            GetSetPos.x - GetSetScale.x / 2f > m_Ground.m_vCenter.x + (m_Ground.m_vSize.x / 2f))
        　   m_Ground.m_bStand = false;

        // 地面についていなかったら、落ちる
        if (!GetSetGround.m_bStand)
        {
            if (GameManager.IsDebug())
                Debug.Log("地面から離れた ObjID: " + m_nObjID);

            m_vSpeed.y -= 0.05f;
        }
        else m_vSpeed.y = 0f;
    }

    // --- プロパティ関数 ---------------------------------------------------------------------
    public int GetSetObjID { get { return m_nObjID; } set { m_nObjID = value; } }
    public int GetSetHitID { get { return m_nHitID; } set { m_nHitID = value; } }
    //public int GetSetHp { get { return m_nHp; } set { m_nHp = value; } }
    //public int GetSetMaxHp { get { return m_nMaxHp; } set { m_nMaxHp = value; } }
    public float GetSetAccel { get { return m_fAccel; } set { m_fAccel = value; } }
    public float GetSetWeight { get { return m_fWeight; } set { m_fWeight = value; } }
    public Vector2 GetSetSpeed { get { return m_vSpeed; } set { m_vSpeed = value; } }
    public Vector2 GetSetMaxSpeed { get { return m_vMaxSpeed; } set { m_vMaxSpeed = value; } }
    public Vector2 GetSetMove { get { return m_vMove; } set { m_vMove = value; } }
    public bool GetSetExist { get { return m_bExist; } set { m_bExist = value; } }
    public ObjDir GetSetDir { get { return m_eDir; } set { m_eDir = value; } }
    public ObjType GetSetType { get { return m_eType; } set { m_eType = value; } }
    public Vector3 GetSetPos
    {
        get { return this.gameObject.transform.position; }
        set { this.gameObject.transform.position = value; }
    }
    public Vector3 GetSetScale
    {
        get { return this.gameObject.transform.localScale; }
        set { this.gameObject.transform.localScale = value; }
    }

    public Ground GetSetGround { get { return m_Ground; } set { m_Ground = value; } }
    public Invincible GetSetInvincible { get { return m_Invincible; } set { m_Invincible = value; } }
    // ----------------------------------------------------------------------------------------
}
