/**
 * @file   ObjEnemyBase.cs
 * @brief  敵の基底クラス
 * @author IharaShota
 * @date   2023/10/13
 * @Update 2023/10/13 作成
 **/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjEnemyBase : ObjBase
{
    public override void UpdateObj()
    {
    }

    public override void UpdateDebug()
    {
        //Debug.Log("EnemyBase");
    }
    public override void DamageAttack()
    {
        //攻撃をくらったらノックバックする
        YK_KnockBack.instance.KnockBack(GetSetDir);
    }
}
