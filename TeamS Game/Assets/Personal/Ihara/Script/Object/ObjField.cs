/**
 * @file   ObjField.cs
 * @brief  �t�B�[���h�N���X
 * @author IharaShota
 * @date   2023/10/20
 * @Update 2023/10/20 �쐬
 **/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjField : ObjBase
{
    public bool m_bHitWall;
    public float m_fpos;

    // --- �X�V�֐� ---
    public override void UpdateObj()
    {
        if (m_bHitWall)
            GetSetPos = new Vector2(Camera.main.transform.position.x + m_fpos, GetSetPos.y);
    }

    //  ---- �����蔻��𐶐������ۂ̏��� ----
    public override void GenerateHit()
    {
        // �������ɓ����蔻��ID���擾���A�����l��ݒ肵�Ă���(���S���W,�傫��,�����蔻��̎��,obj��ID)
        m_nHitID = ON_HitManager.instance.GenerateHit(this.gameObject.transform.position,
        this.gameObject.transform.localScale / 2,
        true, HitType.FIELD, GetSetObjID);

        Debug.Log("�����蔻�萶�� HitID:" + GetSetHitID);
    }
}
