/**
 * @file  EnemyUnion_RePop.cs
 * @brief  EnemyUnion�́u�����v�N���X
 * @author IharaShota
 * @date   2023/11/24
 * @Update 2023/11/24 �쐬
 **/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyUnion_RePop : EnemyStrategy
{
    private ObjEnemyUnion enemyUnion; // ���g���i�[���邽�߂̃R���|�[�l���g

    private void Start()
    {
        enemyUnion = this.gameObject.GetComponent<ObjEnemyUnion>();
    }
    public override void UpdateState()
    {
    }

    public override void UpdateStrategy()
    {
    }
}
