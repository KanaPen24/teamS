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

[RequireComponent(typeof(EnemyUnion_Idle))]
[RequireComponent(typeof(EnemyUnion_Walk))]
[RequireComponent(typeof(EnemyUnion_Drop))]
[RequireComponent(typeof(EnemyUnion_Jump))]
[RequireComponent(typeof(EnemyUnion_KnockBack))]
[RequireComponent(typeof(EnemyUnion_Atk))]
[RequireComponent(typeof(EnemyUnion_Death))]
[RequireComponent(typeof(EnemyUnion_RePop))]

public class ObjEnemyUnion : ObjEnemyBase
{
    [SerializeField] private List<EnemyStrategy> enemyStrategys;
    public List<int> m_nEnemyIDs = new List<int>();

    public override void UpdateObj()
    {
        enemyStrategys[(int)m_EnemyState].UpdateState();
        enemyStrategys[(int)m_EnemyState].UpdateStrategy();
    }

    //  ---- �����蔻��𐶐������ۂ̏��� ----
    public override void GenerateHit()
    {
        // �������ɓ����蔻��ID���擾���A�����l��ݒ肵�Ă���(���S���W,�傫��,�����蔻��̎��,obj��ID)
        m_nHitID = ON_HitManager.instance.GenerateHit(this.gameObject.transform.position,
        this.gameObject.transform.localScale / 2,
        true, HitType.BODY, GetSetObjID);
    }

    // --- �G�̕������� ---
    public void DivisionEnemy()
    {
        // �������Ă����G��\��������
        for (int i = 0; i < m_nEnemyIDs.Count; ++i)
        {
            ObjEnemyBase enemy = ObjManager.instance.GetObj(m_nEnemyIDs[i]).GetComponent<ObjEnemyBase>();
            enemy.GetSetExist = true;
            enemy.GetSetPos = GetSetPos;
            enemy.GetSetEnemyState = EnemyState.RePop;
            ON_HitManager.instance.SetActive(ObjManager.instance.GetObj(m_nEnemyIDs[i]).GetSetObjID, true);

            // ���̂��Ă����G���u���̓G�v�������ꍇ�A�j��g���K�[��ON
            if (enemy.GetComponent<ObjEnemyUnion>() != null)
                enemy.GetSetDestroy = true;
        }

        // �������Ă����G��ID��S�폜
        m_nEnemyIDs.Clear();

        // ���g�̔j��g���K�[��ON
        GetSetDestroy = true;

        //// ���g�̃I�u�W�F�N�g���\���ɂ���
        //for (int j = 0; j < ObjManager.instance.GetObjList().Count; ++j)
        //{
        //    // Obj�̃��X�g���玩�g�Ɠ����I�u�W�F�N�gID������
        //    if (ObjManager.instance.GetObjList()[j].GetSetObjID == GetSetObjID)
        //    {
        //        GetSetExist = false;
        //        //ON_HitManager.instance.SetActive(ObjManager.instance.GetObj(j).GetSetObjID, false);
        //        ON_HitManager.instance.SetActive(ObjManager.instance.GetObjList()[j].GetSetObjID, false);
        //        break;
        //    }
        //}
    }

    // --- ���̂��Ă����G�����̔j��g���K�[��ON ---
    public void DestroyTriggerChildEnemy()
    {
        // �������Ă����G��\��������
        for (int i = 0; i < m_nEnemyIDs.Count; ++i)
        {
            ObjEnemyBase enemy = ObjManager.instance.GetObj(m_nEnemyIDs[i]).GetComponent<ObjEnemyBase>();
            enemy.GetSetDestroy = true;
        }
    }
}
