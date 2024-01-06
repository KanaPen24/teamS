/**
 * @file   PlayerStrategy.cs
 * @brief  Player�̑J�ڏ�Ԃ̃N���X
 * @author IharaShota
 * @date   2023/10/27
 * @Update 2023/10/27 �쐬
 **/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStrategy : MonoBehaviour
{
    [HideInInspector]
    public bool m_bStartFlg;

    // State�̏���������
    public void InitState()
    {
        m_bStartFlg = true;
    }

    // Player�̑J�ڏ�Ԃ̓��͏���
    public virtual void UpdateState()
    {

    }

    // Player�̑J�ڏ�Ԃ̍X�V����
    public virtual void UpdatePlayer()
    {

    }

    // Player�̑J�ڎ��̏���
    public virtual void StartState()
    {

    }

    // Player�̑J�ڏI�����̏���
    public virtual void EndState()
    {

    }
}
