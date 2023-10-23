/**
 * @file   ObjEnemyBase.cs
 * @brief  �G�̊��N���X
 * @author IharaShota
 * @date   2023/10/13
 * @Update 2023/10/13 �쐬
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
        //�U�������������m�b�N�o�b�N����
        YK_KnockBack.instance.KnockBack(GetSetDir);
    }
}
