/**
 * @file  IS_Hitotume_RePop.cs
 * @brief  Hitotume�́u�����v�N���X
 * @author IharaShota
 * @date   2023/11/24
 * @Update 2023/11/24 �쐬
 **/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IS_Hitotume_RePop : NK_HitotumeStrategy
{
    private HitotumeProto Hitotume; // ���g���i�[���邽�߂̃R���|�[�l���g

    private void Start()
    {
        Hitotume = this.gameObject.GetComponent<HitotumeProto>();
    }

    public override void UpdateState()
    {
        // �u���� �� �҂��v
        if(Hitotume.GetSetGround.m_bStand)
        {
            Hitotume.GetSetEnemyState = EnemyState.Idle;
            return;
        }
    }

    public override void UpdateStrategy()
    {
    }
}
