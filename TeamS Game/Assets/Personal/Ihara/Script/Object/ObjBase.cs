/**
 * @file   ObjBase.cs
 * @brief  �I�u�W�F�N�g���N���X
 * @author IharaShota
 * @date   2023/10/13
 * @Update 2023/10/13 �쐬
 **/


// memo �n�ʂɓ������Ă��邩��
//      �U������炤���z�֐����~������
//      �������~������
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// --- �I�u�W�F�N�g�̌��� ---
public enum ObjDir
{
    NONE = 0,
    RIGHT,
    LEFT,

    MaxDir
}

// --- �I�u�W�F�N�g�̃^�C�v ---
public enum ObjType
{
    Player,
    Enemy,
    Field,

    MaxType
}

// ----- �n�ʂ̏����i�[����N���X ------------------------------------------------------
[System.Serializable]
public class Ground
{
    [SerializeField] public bool m_bStand;    // �n�ʂɗ����Ă��邩
    [SerializeField] public Vector2 m_vCenter;// ���S���W 
    [SerializeField] public Vector2 m_vSize;  // �傫��
}
// ----------------------------------------------------------------------------------------

// ----- ���G��Ԃ��Ǘ�����N���X ------------------------------------------
[System.Serializable]
public class Invincible
{
    [SerializeField] public bool m_bInvincible; // ���G���ǂ���
    [SerializeField] public float m_fTime;      // ���G����

    public void SetInvincible(float i)
    {
        m_bInvincible = true;
        m_fTime = i;
    }
}

// --- �C���X�y�N�^�[�ŏ����l��ݒ肷��N���X ------------------------------
[System.Serializable]
public class InitParam
{
    //[SerializeField] private int m_nHp;           // ���݂�HP
    //[SerializeField] private int m_nMaxHp;        // �ő�HP
    [SerializeField] private float m_fAccel;      // �����x
    [SerializeField] private float m_fWeight;     // �d��
    [SerializeField] private Vector2 m_vMaxSpeed; // �ő呬�x
    [SerializeField] private bool m_bExist;       // ���݂��Ă��邩
    [SerializeField] private ObjDir m_eDir;       // ����
    [SerializeField] private ObjType m_eType;     // �^�C�v

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

// --- �C���X�y�N�^�[�Ńp�����[�^�[���Q�Ƃ���N���X -----------------------
[System.Serializable]
public class CheckParam
{
    [SerializeField] private Ground m_Ground;     // �n�ʂ̏��
    [SerializeField] private Invincible m_Invincible; // ���G���
    [SerializeField] private int m_nObjID;        // obj��ID
    [SerializeField] private int m_nHitID;        // �����蔻���ID
    //[SerializeField] private int m_nHp;           // ���݂�HP
    //[SerializeField] private int m_nMaxHp;        // �ő�HP
    [SerializeField] private float m_fAccel;      // �����x
    [SerializeField] private float m_fWeight;     // �d��
    [SerializeField] private Vector2 m_vSpeed;    // ���x
    [SerializeField] private Vector2 m_vMaxSpeed; // �ő呬�x
    [SerializeField] private Vector2 m_vMove;     // �ړ���
    [SerializeField] private bool m_bExist;       // ���݂��Ă��邩
    [SerializeField] private ObjDir m_eDir;       // ����
    [SerializeField] private ObjType m_eType;     // �^�C�v

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
    // ----- �ϐ��錾 ------
    [SerializeField]
    public SpriteRenderer texObj;     // �I�u�W�F�N�g�̉摜
    [SerializeField]
    protected InitParam InitParam;    // �������p�p�����[�^
    [SerializeField]
    protected CheckParam CheckParam;  // �Q�Ɨp�p�����[�^
    [HideInInspector]
    public Ground m_Ground;           // �n�ʂ̏��
    [HideInInspector]
    public Invincible m_Invincible;   // ���G���
    protected int m_nObjID;           // obj��ID
    protected int m_nHitID;           // �����蔻���ID
    //protected int m_nHp;              // ���݂�HP
    //protected int m_nMaxHp;           // �ő�HP
    protected float m_fAccel;         // �����x
    protected float m_fWeight;        // �d��
    protected Vector2 m_vSpeed;       // ���݂̑��x
    protected Vector2 m_vMaxSpeed;    // �ő呬�x
    protected Vector2 m_vMove;        // �ړ���
    //protected bool m_bStand;          // �n�ʂɗ����Ă��邩
    protected bool m_bExist;          // ���݂��Ă��邩
    protected ObjDir m_eDir;          // ����
    protected ObjType m_eType;        // �^�C�v
    // ---------------------

    // --- �������֐� ---
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

    // --- �Q�ƃp�����[�^�[�X�V�֐� ---
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

    // --- �X�V�֐� ---
    public virtual void UpdateObj()
    {
    }

    // --- �f�o�b�O�X�V���� ---
    public virtual void UpdateDebug()
    {
        Debug.Log("base");
    }

    // --- �U���𓖂Ă�ꂽ ---
    public virtual void DamageAttack()
    {

    }
    
    // --- �m�b�N�o�b�N�֐� ---
    public virtual void KnockBackObj(ObjDir dir)
    {

    }

    // --- �I�u�W�F�N�g���� ---
    public virtual void DestroyObj()
    {
        // �����蔻��폜 �� �I�u�W�F�N�g����
        ON_HitManager.instance.DeleteHit(m_nHitID);
        Destroy(this.gameObject);
    }

    //  ---- �����蔻��𐶐������ۂ̏��� ----
    public virtual void GenerateHit()
    {
        // �������ɓ����蔻��ID���擾���A�����l��ݒ肵�Ă���(���S���W,�傫��,�����蔻��̎��,obj��ID)
        m_nHitID = ON_HitManager.instance.GenerateHit(this.gameObject.transform.position,
        this.gameObject.transform.localScale / 2,
        true, HitType.BODY, GetSetObjID);

        if (GameManager.IsDebug())
            Debug.Log("�����蔻�萶�� HitID: " + GetSetHitID +
                "scale: " + this.gameObject.transform.localScale / 2);
    }

    // --- �n�ʔ��菈�� ---
    public virtual void CheckObjGround()
    {
        // �I�u�W�F�N�g�̃^�C�v���uFIELD�v��������
        // �n�ʂɗ����Ă����Ԃɂ��� �� �������x��0�ŏI��
        if(m_eType == ObjType.Field)
        {
            m_Ground.m_bStand = true;
            m_vSpeed.y = 0f;
            return;
        }

        // �����ݗ����Ă���n�ʂ𗣂ꂽ��c
        if (GetSetPos.x + GetSetScale.x / 2f < m_Ground.m_vCenter.x - (m_Ground.m_vSize.x / 2f) ||
            GetSetPos.x - GetSetScale.x / 2f > m_Ground.m_vCenter.x + (m_Ground.m_vSize.x / 2f))
        �@   m_Ground.m_bStand = false;

        // �n�ʂɂ��Ă��Ȃ�������A������
        if (!GetSetGround.m_bStand)
        {
            if (GameManager.IsDebug())
                Debug.Log("�n�ʂ��痣�ꂽ ObjID: " + m_nObjID);

            m_vSpeed.y -= 0.05f;
        }
        else m_vSpeed.y = 0f;
    }

    // --- �v���p�e�B�֐� ---------------------------------------------------------------------
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
