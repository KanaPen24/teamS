/**
 * @file  EnemyUnion_Jump.cs
 * @brief  EnemyUnion�́u����v�N���X
 * @author IharaShota
 * @date   2023/11/24
 * @Update 2023/11/24 �쐬
 **/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyUnion_Jump : EnemyStrategy
{
    private ObjEnemyUnion enemyUnion; // ���g���i�[���邽�߂̃R���|�[�l���g

    private void Start()
    {
        enemyUnion = this.gameObject.GetComponent<ObjEnemyUnion>();
    }
    public override void UpdateState()
    {
        // �u���� �� �����v
        if (enemyUnion.GetSetSpeed.y < 0f�@&& !enemyUnion.GetSetGround.m_bStand)
        {
            enemyUnion.GetSetEnemyState = EnemyState.Drop;
            return;
        }
    }

    public override void UpdateStrategy()
    {
        if (enemyUnion.GetSetDir == ObjDir.RIGHT)
        {
            enemyUnion.GetSetSpeed = new Vector2(0.1f, 0f);
        }
        else if (enemyUnion.GetSetDir == ObjDir.LEFT)
        {
            enemyUnion.GetSetSpeed = new Vector2(-0.1f, 0f);
        }
    }
}