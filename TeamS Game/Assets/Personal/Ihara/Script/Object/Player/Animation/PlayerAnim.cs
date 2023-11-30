/**
 * @file   PlayerAnim.cs
 * @brief  Player�̃A�j���[�V�����Ǘ��N���X
 * @author IharaShota
 * @date   2023/11/29
 * @Update 2023/11/29 �쐬
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ===============================================
// PlayerAnimState
// �c Player�̃A�j���[�V������Ԃ��Ǘ�����񋓑�
// ��Animator��int�̐��������Ă���m�F���邱��
// ===============================================
public enum PlayerAnimState
{
    Idle,
    Walk,
    Jump,
    Drop,
    Atk,
    Special,

    MaxPlayerAnimState
}

public class PlayerAnim : MonoBehaviour
{
    [SerializeField] private Animator m_animator; // Player�̃A�j���[�V����
    private const string s_intName = "motionNum";

    public void ChangeAnim(PlayerAnimState anim)
    {
        m_animator.SetInteger(s_intName, (int)anim);
    }

    public bool AnimEnd(PlayerAnimState anim)
    {
        if (m_animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f &&
            m_animator.GetCurrentAnimatorStateInfo(0).IsName(anim.ToString()))
            return true;
        else return false;
    }

    public bool GetAnimNormalizeTime(PlayerAnimState anim, float time)
    {
        if (m_animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= time &&
            m_animator.GetCurrentAnimatorStateInfo(0).IsName(anim.ToString()))
            return true;
        else return false;
    }
}
