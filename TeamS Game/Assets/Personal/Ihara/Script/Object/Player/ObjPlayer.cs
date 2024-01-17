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
    Atk01,
    Atk02,
    Atk03,
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
        if (GameManager.GetSetGameState != GameState.GamePlay)
            return;

        // --- �J�ڏ�Ԃɂ���ԍX�V ---
        m_PlayerStrategys[(int)m_PlayerState].UpdateState();
    }

    public override void UpdateObj()
    {
        // --- �J�ڏ�Ԃɂ��X�V���� ---
        m_PlayerStrategys[(int)m_PlayerState].UpdatePlayer();

        if(GetSetDir == ObjDir.RIGHT)
        {
            transform.localScale = new Vector3(2.5f, 2.5f, 3f);
        }
        else if (GetSetDir == ObjDir.LEFT)
        {
            transform.localScale = new Vector3(-2.5f, 2.5f, 3f);
        }
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

    // �q�b�g�X�g�b�v�X�V
    public override void UpdateHitStop()
    {
        // �q�b�g�X�g�b�v���ɃA�j���[�V�������X���[�ɂ���
        if (m_HitStopParam.m_bHitStop)
        {
            Anim.m_bSlow = true;
        }
        else Anim.m_bSlow = false;

        // ���̏���
        base.UpdateHitStop();
    }
}
