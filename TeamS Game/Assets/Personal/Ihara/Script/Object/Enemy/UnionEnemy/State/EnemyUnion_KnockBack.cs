/**
 * @file  EnemyUnion_KnockBack.cs
 * @brief  EnemyUnion�́u�m�b�N�o�b�N�v�N���X
 * @author IharaShota
 * @date   2023/11/24
 * @Update 2023/11/24 �쐬
 **/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyUnion_KnockBack : EnemyStrategy
{
    private ObjEnemyUnion enemyUnion; // ���g���i�[���邽�߂̃R���|�[�l���g
    private bool endFlg = false;
    public ParticleSystem burstEffect;

    private void Start()
    {
        enemyUnion = this.gameObject.GetComponent<ObjEnemyUnion>();
    }
    public override void UpdateState()
    {
        // �u���� �� �҂��v(����)
        if (endFlg)
        {
            enemyUnion.GetSetEnemyState = EnemyState.Idle;
            endFlg = false;
            enemyUnion.DivisionEnemy();
            burstEffect.Play();
            burstEffect.transform.position = enemyUnion.GetSetPos;
            return;
        }
    }

    public override void UpdateStrategy()
    {
        if (enemyUnion.m_Ground.m_bStand)
        {
            //��������
            enemyUnion.GetSetSpeed =
                new Vector2(enemyUnion.GetSetSpeed.x * enemyUnion.GetSetKnockBack.m_fDamping,
                            -enemyUnion.GetSetSpeed.y * enemyUnion.GetSetKnockBack.m_fDamping);
            //�e�ނ̂�0.1f�ȉ��ɂȂ�����
            if (Mathf.Abs(enemyUnion.GetSetSpeed.y) <= 0.2f)
            {
                enemyUnion.GetSetSpeed = Vector2.zero;     //�~�߂�
                endFlg = true;
            }
        }
    }
}
