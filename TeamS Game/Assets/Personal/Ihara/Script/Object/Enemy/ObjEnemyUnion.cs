/**
 * @file   ObjEnemyUnion.cs
 * @brief  ���̂����G�̃N���X
 * @author IharaShota
 * @date   2023/11/09
 * @Update 2023/11/09 �쐬
 **/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjEnemyUnion : ObjEnemyBase
{
    public List<int> m_nEnemyIDs = new List<int>();

    public override void UpdateObj()
    {
        if(Input.GetKey(KeyCode.Space))
        {
            // �������Ă����G��\��������
            for(int i = 0; i < m_nEnemyIDs.Count; ++i)
            {
                ObjManager.instance.GetObjs(m_nEnemyIDs[i]).GetSetExist = true;
                ON_HitManager.instance.SetActive(ObjManager.instance.GetObjs(m_nEnemyIDs[i]).GetSetObjID, true);
            }

            // �������Ă����G��ID��S�폜
            m_nEnemyIDs.Clear();

            // ���g�̃I�u�W�F�N�g���\���ɂ���
            for (int j = 0; j < ObjManager.instance.GetObj().Count; ++j)
            {
                if (ObjManager.instance.GetObjs(j).gameObject == gameObject)
                {
                    GetSetExist = false;
                    ON_HitManager.instance.SetActive(ObjManager.instance.GetObjs(j).GetSetObjID, false);
                    break;
                }
            }
        }
    }

    //  ---- �����蔻��𐶐������ۂ̏��� ----
    public override void GenerateHit()
    {
        // �������ɓ����蔻��ID���擾���A�����l��ݒ肵�Ă���(���S���W,�傫��,�����蔻��̎��,obj��ID)
        m_nHitID = ON_HitManager.instance.GenerateHit(this.gameObject.transform.position,
        this.gameObject.transform.localScale / 2,
        false, HitType.BODY, GetSetObjID);

        Debug.Log("�����I");
    }
}
