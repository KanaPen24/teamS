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
    Atk01,
    Atk02,
    Atk03,
    Special,

    MaxPlayerAnimState
}

public class PlayerAnim : MonoBehaviour
{
    [SerializeField] private Animator m_animator; // Player�̃A�j���[�V����
    public bool m_bSlow;
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

    private void Update()
    {
        // Pause�ɂȂ��Ă�����A�j���[�V�������~�߂�
        if (GameManager.GetSetGameState == GameState.GamePause)
        {
            m_animator.SetFloat("Speed", 0.0f);
            return;
        }
        else m_animator.SetFloat("Speed", 1f);

        // �X���[�ɂȂ��Ă�����A�A�j���[�V�����̑��x��x������
        if (m_bSlow)
        {
            m_animator.SetFloat("Speed", 0.1f);
            return;
        }
        else m_animator.SetFloat("Speed", 1f);
    }

    public Animator GetSetAnimator
    {
        get { return m_animator; }
        set { m_animator = value; }
    }
}
