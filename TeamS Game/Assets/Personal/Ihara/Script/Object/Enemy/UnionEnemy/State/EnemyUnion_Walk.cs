/**
 * @file  EnemyUnion_Walk.cs
 * @brief  EnemyUnionの「移動」クラス
 * @author IharaShota
 * @date   2023/11/24
 * @Update 2023/11/24 作成
 **/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyUnion_Walk : EnemyStrategy
{
    private ObjEnemyUnion enemyUnion; // 自身を格納するためのコンポーネント

    private void Start()
    {
        enemyUnion = this.gameObject.GetComponent<ObjEnemyUnion>();
    }
    public override void UpdateState()
    {
        //// 「移動 → 待ち」
        //if (enemyUnion.GetSetGround.m_bStand)
        //{
        //    enemyUnion.GetSetEnemyState = EnemyState.Walk;
        //    return;
        //}
        // 「移動 → 落下」
        if (!enemyUnion.GetSetGround.m_bStand)
        {
            enemyUnion.GetSetEnemyState = EnemyState.Drop;
            return;
        }
    }

    public override void UpdateStrategy()
    {
        if(enemyUnion.GetSetDir == ObjDir.RIGHT)
        {
            enemyUnion.GetSetSpeed = new Vector2(0.02f, 0f);
        }
        else if (enemyUnion.GetSetDir == ObjDir.LEFT)
        {
            enemyUnion.GetSetSpeed = new Vector2(-0.02f, 0f);
        }
    }
}
