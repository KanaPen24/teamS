/**
 * @file   ObjPlayer.cs
 * @brief  Player�N���X
 * @author IharaShota
 * @date   2023/10/13
 * @Update 2023/10/13 �쐬
 **/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerState
{
    Idle,
    Walk,
    Jump,
    Drop,
    Atk,
    Special,

    MaxPlayerState
}

public class ObjPlayer : ObjBase
{
    public static ObjPlayer instance;
    public PlayerState m_PlayerState;
    public List<PlayerStrategy> m_PlayerStrategys;
    public PlayerAnim Anim;

    public void Start()
    {
        // �C���X�^���X��
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public void Update()
    {
        // --- �J�ڏ�Ԃɂ���ԍX�V ---
        m_PlayerStrategys[(int)m_PlayerState].UpdateState();
    }

    public override void UpdateObj()
    {
        // --- �J�ڏ�Ԃɂ��X�V���� ---
        m_PlayerStrategys[(int)m_PlayerState].UpdatePlayer();
    }

    // �������֐�
    public override void InitObj()
    {
        base.InitObj();
        for(int i = 0; i < m_PlayerStrategys.Count; ++i)
        {
            m_PlayerStrategys[i].InitState();
        }
    }

    // �I�u�W�F�N�g�̔j��
    public override void DestroyObj()
    {
        base.DestroyObj();
    }
}
