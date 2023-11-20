/**
 * @file   ObjEnemyA.cs
 * @brief  �GA�̃N���X
 * @author IharaShota
 * @date   2023/10/13
 * @Update 2023/10/13 �쐬
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
    }

    public override void UpdateDebug()
    {
        //Debug.Log("EnemyA");
    }
}