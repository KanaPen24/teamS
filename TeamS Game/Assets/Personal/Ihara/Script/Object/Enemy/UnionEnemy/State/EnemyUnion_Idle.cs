/**
 * @file  EnemyUnion_Idle.cs
 * @brief  EnemyUnion�́u�҂��v�N���X
 * @author IharaShota
 * @date   2023/11/24
 * @Update 2023/11/24 �쐬
 **/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyUnion_Idle : EnemyStrategy
{
    private ObjEnemyUnion enemyUnion; // ���g���i�[���邽�߂̃R���|�[�l���g

    private void Start()
    {
        enemyUnion = this.gameObject.GetComponent<ObjEnemyUnion>();
    }
    public override void UpdateState()
    {
        // �u�҂� �� �ړ��v
        if(enemyUnion.GetSetGround.m_bStand)
        {
            enemyUnion.GetSetEnemyState = EnemyState.Walk;
            return;
        }
        // �u�҂� �� �����v
        if (!enemyUnion.GetSetGround.m_bStand)
        {
            enemyUnion.GetSetEnemyState = EnemyState.Drop;
            return;
        }
    }

    public override void UpdateStrategy()
    {
        enemyUnion.GetSetSpeed = new Vector2(0f, 0f);
    }
}
