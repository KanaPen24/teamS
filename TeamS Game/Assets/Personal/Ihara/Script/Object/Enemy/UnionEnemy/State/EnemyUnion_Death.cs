/**
 * @file  EnemyUnion_Death.cs
 * @brief  EnemyUnionの「死」クラス
 * @author IharaShota
 * @date   2023/11/24
 * @Update 2023/11/24 作成
 **/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyUnion_Death : EnemyStrategy
{
    private ObjEnemyUnion enemyUnion; // 自身を格納するためのコンポーネント

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
