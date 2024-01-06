/**
 * @file  EnemyUnion_Drop.cs
 * @brief  EnemyUnionの「落下」クラス
 * @author IharaShota
 * @date   2023/11/24
 * @Update 2023/11/24 作成
 **/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyUnion_Drop : EnemyStrategy
{
    private ObjEnemyUnion enemyUnion; // 自身を格納するためのコンポーネント

    private void Start()
    {
        enemyUnion = this.gameObject.GetComponent<ObjEnemyUnion>();
    }
    public override void UpdateState()
    {
        // 「落下 → 待ち」
        if (enemyUnion.GetSetGround.m_bStand)
        {
            enemyUnion.GetSetEnemyState = EnemyState.Idle;
            return;
        }
    }

    public override void UpdateStrategy()
    {
        if (enemyUnion.GetSetDir == ObjDir.RIGHT)
        {
            enemyUnion.GetSetSpeed = new Vector2(0.01f, enemyUnion.GetSetSpeed.y);
        }
        else if (enemyUnion.GetSetDir == ObjDir.LEFT)
        {
            enemyUnion.GetSetSpeed = new Vector2(-0.01f, enemyUnion.GetSetSpeed.y);
        }
    }
}
