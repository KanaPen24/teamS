/**
 * @file   PlayerSpecial.cs
 * @brief  Player�́u�K�E�v�N���X
 * @author IharaShota
 * @date   2023/11/30
 * @Update 2023/11/30 �쐬
 **/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpecial : PlayerStrategy
{
    [SerializeField] private Vector3 m_vSpecialArea;
    [SerializeField] private float m_fLength;
    private int specialNum;

    public override void UpdateState()
    {
        //if (m_fTime <= 0 && !ObjPlayer.m_bAtkFlg)
        if (ObjPlayer.instance.Anim.GetAnimNormalizeTime(PlayerAnimState.Atk, 1f) && !ObjPlayer.m_bAtkFlg)
        {
            // �K�E �� �҂�
            if (ObjPlayer.instance.GetSetGround.m_bStand)
            {
                ObjPlayer.instance.m_PlayerState = PlayerState.Idle;
                ON_HitManager.instance.DeleteHit(specialNum);
                return;
            }
        }
    }

    public override void UpdatePlayer()
    {
        // �A�j���\�V����
        ObjPlayer.instance.Anim.ChangeAnim(PlayerAnimState.Atk);

        if (ObjPlayer.m_bSpecialFlg)
        {
            // �U���̓����蔻�萶��(�����ɂ���Đ����ʒu���ς��)
            if (ObjPlayer.instance.GetSetDir == ObjDir.RIGHT)
            {
                specialNum = ON_HitManager.instance.GenerateHit(ObjPlayer.instance.GetSetPos + new Vector3(m_fLength, 0f, 0f),
                m_vSpecialArea, true, HitType.ATTACK, ObjPlayer.instance.GetSetObjID);

                ObjPlayer.instance.GetSetSpeed += new Vector2(5f, 0f);
            }
            else if (ObjPlayer.instance.GetSetDir == ObjDir.LEFT)
            {
                specialNum = ON_HitManager.instance.GenerateHit(ObjPlayer.instance.GetSetPos - new Vector3(m_fLength, 0f, 0f),
                m_vSpecialArea, true, HitType.ATTACK, ObjPlayer.instance.GetSetObjID);

                ObjPlayer.instance.GetSetSpeed += new Vector2(-5f, 0f);
            }

            AudioManager.instance.PlaySE(SEType.SE_PlayerAtk);
            ObjPlayer.m_bSpecialFlg = false;
        }
        // ���x�͌���(��)
        ObjPlayer.instance.GetSetSpeed *= new Vector2(0f, 0f);
    }
}
