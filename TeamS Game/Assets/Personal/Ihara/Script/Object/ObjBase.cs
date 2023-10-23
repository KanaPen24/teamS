/**
 * @file   ObjBase.cs
 * @brief  �I�u�W�F�N�g���N���X
 * @author IharaShota
 * @date   2023/10/13
 * @Update 2023/10/13 �쐬
 **/


// memo �n�ʂɓ������Ă��邩
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

//[System.Serializable]
//public class Ground
//{
//    [SerializeField] private bool m_bStand; // �n�ʂɗ����Ă��邩
//    [SerializeField] private Vector2 m_vLength; // �n�ʂ̒���

//    public bool GetSetStand { get { return m_bStand; } set { m_bStand = value; } }
//    public Vector2 GetSetLength { get { return m_vLength; } set { m_vLength = value; } }
//}

// --- �C���X�y�N�^�[�ŏ����l��ݒ肷��N���X ------------------------------
[System.Serializable]
public class InitParam
{
    [SerializeField] private int m_nHp;           // ���݂�HP
    [SerializeField] private int m_nMaxHp;        // �ő�HP
    [SerializeField] private float m_fAccel;      // �����x
    [SerializeField] private float m_fWeight;     // �d��
    [SerializeField] private Vector2 m_vSpeed;    // ���x
    [SerializeField] private Vector2 m_vMaxSpeed; // �ő呬�x
    [SerializeField] private bool m_bExist;       // ���݂��Ă��邩
    [SerializeField] private ObjDir m_eDir;       // ����

    public int GetSetHp { get { return m_nHp; } set { m_nHp = value; } }
    public int GetSetMaxHp { get { return m_nMaxHp; } set { m_nMaxHp = value; } }
    public float GetSetAccel { get { return m_fAccel; } set { m_fAccel = value; } }
    public float GetSetWeight { get { return m_fWeight; } set { m_fWeight = value; } }
    public Vector2 GetSetSpeed { get { return m_vSpeed; } set { m_vSpeed = value; } }
    public Vector2 GetSetMaxSpeed { get { return m_vMaxSpeed; } set { m_vMaxSpeed = value; } }
    public bool GetSetExist { get { return m_bExist; } set { m_bExist = value; } }
    public ObjDir GetSetDir { get { return m_eDir; } set { m_eDir = value; } }
}
// -------------------------------------------------------------------------

// --- �C���X�y�N�^�[�Ńp�����[�^�[���Q�Ƃ���N���X -----------------------
[System.Serializable]
public class CheckParam
{
    [SerializeField] private int m_nObjID;        // obj��ID
    [SerializeField] private int m_nHitID;        // �����蔻���ID
    [SerializeField] private int m_nHp;           // ���݂�HP
    [SerializeField] private int m_nMaxHp;        // �ő�HP
    [SerializeField] private float m_fAccel;      // �����x
    [SerializeField] private float m_fWeight;     // �d��
    [SerializeField] private Vector2 m_vSpeed;    // ���x
    [SerializeField] private Vector2 m_vMaxSpeed; // �ő呬�x
    [SerializeField] private Vector2 m_vMove;     // �ړ���
    [SerializeField] private Vector2 m_vGndLength;// �n�ʂ̒���
    [SerializeField] private bool m_bStand;       // �n�ʂɗ����Ă��邩
    [SerializeField] private bool m_bExist;       // ���݂��Ă��邩
    [SerializeField] private ObjDir m_eDir;       // ����

    public int GetSetObjID { get { return m_nObjID; } set { m_nObjID = value; } }
    public int GetSetHitID { get { return m_nHitID; } set { m_nHitID = value; } }
    public int GetSetHp { get { return m_nHp; } set { m_nHp = value; } }
    public int GetSetMaxHp { get { return m_nMaxHp; } set { m_nMaxHp = value; } }
    public float GetSetAccel { get { return m_fAccel; } set { m_fAccel = value; } }
    public float GetSetWeight { get { return m_fWeight; } set { m_fWeight = value; } }
    public Vector2 GetSetSpeed { get { return m_vSpeed; } set { m_vSpeed = value; } }
    public Vector2 GetSetMaxSpeed { get { return m_vMaxSpeed; } set { m_vMaxSpeed = value; } }
    public Vector2 GetSetMove { get { return m_vMove; } set { m_vMove = value; } }
    public Vector2 GetSetGndLen { get { return m_vGndLength; } set { m_vGndLength = value; } }
    public bool GetSetStand { get { return m_bStand; } set { m_bStand = value; } }
    public bool GetSetExist { get { return m_bExist; } set { m_bExist = value; } }
    public ObjDir GetSetDir { get { return m_eDir; } set { m_eDir = value; } }
}
// ------------------------------------------------------------------------

public class ObjBase : MonoBehaviour
{
    // ----- �ϐ��錾 ------
    [SerializeField]
    protected InitParam InitParam;    // �������p�p�����[�^
    [SerializeField]
    protected CheckParam CheckParam;  // �Q�Ɨp�p�����[�^
    protected int m_nObjID;           // obj��ID
    protected int m_nHitID;           // �����蔻���ID
    protected int m_nHp;              // ���݂�HP
    protected int m_nMaxHp;           // �ő�HP
    protected float m_fAccel;         // �����x
    protected float m_fWeight;        // �d��
    protected Vector2 m_vSpeed;       // ���݂̑��x
    protected Vector2 m_vMaxSpeed;    // �ő呬�x
    protected Vector2 m_vMove;        // �ړ���
    protected Vector2 m_vGndPos;      // �n�ʂ̒���
    protected bool m_bStand;          // �n�ʂɗ����Ă��邩
    protected bool m_bExist;          // ���݂��Ă��邩
    protected ObjDir m_eDir;          // ����
    // ---------------------

    // --- �������֐� ---
    public virtual void InitObj()
    {
        m_nHp = InitParam.GetSetHp;
        m_nMaxHp = InitParam.GetSetMaxHp;
        m_fAccel = InitParam.GetSetAccel;
        m_fWeight = InitParam.GetSetWeight;
        m_vSpeed = InitParam.GetSetSpeed;
        m_vMaxSpeed = InitParam.GetSetMaxSpeed;
        m_bExist = InitParam.GetSetExist;
        m_eDir = InitParam.GetSetDir;
    }

    // --- �Q�ƃp�����[�^�[�X�V�֐� ---
    public virtual void UpdateCheckParam()
    {
        CheckParam.GetSetObjID = m_nObjID;
        CheckParam.GetSetHitID = m_nHitID;
        CheckParam.GetSetHp = m_nHp;
        CheckParam.GetSetMaxHp = m_nMaxHp;
        CheckParam.GetSetAccel = m_fAccel;
        CheckParam.GetSetWeight = m_fWeight;
        CheckParam.GetSetSpeed = m_vSpeed;
        CheckParam.GetSetMaxSpeed = m_vMaxSpeed;
        CheckParam.GetSetMove = m_vMove;
        CheckParam.GetSetGndLen = m_vGndPos;
        CheckParam.GetSetStand = m_bStand;
        CheckParam.GetSetExist = m_bExist;
        CheckParam.GetSetDir = m_eDir;
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

    //  ---- �����蔻��𐶐������ۂ̏��� ----
    public virtual void GenerateHit()
    {
        // �������ɓ����蔻��ID���擾���A�����l��ݒ肵�Ă���(���S���W,�傫��,�����蔻��̎��,obj��ID)
        m_nHitID = ON_HitManager.instance.GenerateHit(this.gameObject.transform.position,
        this.gameObject.transform.localScale / 2,
        true, HitType.BODY, GetSetObjID);

        Debug.Log("�����蔻�萶�� HitID:" + GetSetHitID);
    }

    // --- �v���p�e�B�֐� ---------------------------------------------------------------------
    public int GetSetObjID { get { return m_nObjID; } set { m_nObjID = value; } }
    public int GetSetHitID { get { return m_nHitID; } set { m_nHitID = value; } }
    public int GetSetHp { get { return m_nHp; } set { m_nHp = value; } }
    public int GetSetMaxHp { get { return m_nMaxHp; } set { m_nMaxHp = value; } }
    public float GetSetAccel { get { return m_fAccel; } set { m_fAccel = value; } }
    public float GetSetWeight { get { return m_fWeight; } set { m_fWeight = value; } }
    public Vector2 GetSetSpeed { get { return m_vSpeed; } set { m_vSpeed = value; } }
    public Vector2 GetSetMaxSpeed { get { return m_vMaxSpeed; } set { m_vMaxSpeed = value; } }
    public Vector2 GetSetMove { get { return m_vMove; } set { m_vMove = value; } }
    public Vector2 GetSetGndLen { get { return m_vGndPos; } set { m_vGndPos = value; } }
    public bool GetSetStand { get { return m_bStand; } set { m_bStand = value; } }
    public bool GetSetExist { get { return m_bExist; } set { m_bExist = value; } }
    public ObjDir GetSetDir { get { return m_eDir; } set { m_eDir = value; } }
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
    // ----------------------------------------------------------------------------------------
}
