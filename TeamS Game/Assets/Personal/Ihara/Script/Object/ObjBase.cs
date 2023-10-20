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
    [SerializeField] private bool m_bExist;       // ���݂��Ă��邩�ǂ���

    public int GetSetHp { get { return m_nHp; } set { m_nHp = value; } }
    public int GetSetMaxHp { get { return m_nMaxHp; } set { m_nMaxHp = value; } }
    public float GetSetAccel { get { return m_fAccel; } set { m_fAccel = value; } }
    public float GetSetWeight { get { return m_fWeight; } set { m_fWeight = value; } }
    public Vector2 GetSetSpeed { get { return m_vSpeed; } set { m_vSpeed = value; } }
    public Vector2 GetSetMaxSpeed { get { return m_vMaxSpeed; } set { m_vMaxSpeed = value; } }
    public bool GetSetExist { get { return m_bExist; } set { m_bExist = value; } }
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
    [SerializeField] private bool m_bExist;       // ���݂��Ă��邩�ǂ���

    public int GetSetObjID { get { return m_nObjID; } set { m_nObjID = value; } }
    public int GetSetHitID { get { return m_nHitID; } set { m_nHitID = value; } }
    public int GetSetHp { get { return m_nHp; } set { m_nHp = value; } }
    public int GetSetMaxHp { get { return m_nMaxHp; } set { m_nMaxHp = value; } }
    public float GetSetAccel { get { return m_fAccel; } set { m_fAccel = value; } }
    public float GetSetWeight { get { return m_fWeight; } set { m_fWeight = value; } }
    public Vector2 GetSetSpeed { get { return m_vSpeed; } set { m_vSpeed = value; } }
    public Vector2 GetSetMaxSpeed { get { return m_vMaxSpeed; } set { m_vMaxSpeed = value; } }
    public Vector2 GetSetMove { get { return m_vMove; } set { m_vMove = value; } }
    public bool GetSetExist { get { return m_bExist; } set { m_bExist = value; } }
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
    protected bool m_bExist;          // ���݂��Ă��邩
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
        CheckParam.GetSetExist = m_bExist;
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
    public bool GetSetExist { get { return m_bExist; } set { m_bExist = value; } }
    // ----------------------------------------------------------------------------------------

    public Vector3 GetSetPos { get { return this.gameObject.transform.position; }
                               set { this.gameObject.transform.position = value; } }
    public Vector3 GetSetScale{ get { return this.gameObject.transform.localScale; }
                                set { this.gameObject.transform.localScale = value; }}
}
