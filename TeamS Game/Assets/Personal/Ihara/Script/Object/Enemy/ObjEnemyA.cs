/**
 * @file   ObjEnemyA.cs
 * @brief  敵Aのクラス
 * @author IharaShota
 * @date   2023/10/13
 * @Update 2023/10/13 作成
 **/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjEnemyA : ObjEnemyBase
{
    public override void UpdateObj()
    {
        if (m_EnemyState == EnemyState.Idle)
            m_vSpeed = new Vector2(0f, 0f);
        if (m_EnemyState == EnemyState.Drop && GetSetGround.m_bStand)
            m_EnemyState = EnemyState.Idle;
    }
}