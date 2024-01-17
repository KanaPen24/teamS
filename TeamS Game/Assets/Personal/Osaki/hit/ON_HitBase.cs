/**
@file   ON_HitBase.cs
@brief  �����蔻��N���X
@author Norialo Osaki
@date   2023/10/13
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �����蔻��̃^�C�v
public enum HitType
{
    NONE = 0,

    ATTACK,     // �U��
    SPECIAL,    // �K�E
    BODY,       // ��
    BULLET,     // �e�n��
    FIELD,      // �X�e�[�W
    STEP,       // ����

    MAX_TYPE,
}

public class ON_HitBase
{
    // �R���X�g���N�^
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

    // ���S
    public void SetCenter(Vector3 value) { m_center = value; }
    public Vector3 GetCenter() { return m_center; }

    // �傫��
    public void SetSize(Vector3 value) { m_size = value; }
    public Vector3 GetSize() { return m_size; }

    // �L��
    public void SetActive(bool value) { m_active = value; }
    public bool GetActive() { return m_active; }

    // �^�C�v
    public void SetHitType(HitType value) { m_type = value; }
    public HitType GetHitType() { return m_type; }

    // objID
    public void SetObjID(int value) { m_objID = value; }
    public int GetObjID() { return m_objID; }

    // hitID
    public void SetHitID(int value) { m_hitID = value; }
    public int GetHitID() { return m_hitID; }

    // �����蔻��̈ړ�
    public void MoveHit(Vector3 vec) { m_center += vec; }

    private Vector3   m_center; // �����蔻��̒��S
    private Vector3   m_size;   // �����蔻��̑傫��(���S����̋���
    private bool      m_active; // �����蔻�肪�L����(true:�L�� false:����
    private HitType   m_type;   // �����蔻��̃^�C�v
    private int       m_objID;  // �����蔻��̂���I�u�W�F�N�gID
    private int       m_hitID;  // �����蔻��̎���ID
}
