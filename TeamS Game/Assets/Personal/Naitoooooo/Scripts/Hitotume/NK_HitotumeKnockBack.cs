using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NK_HitotumeKnockBack : NK_HitotumeStrategy
{
    private HitotumeProto enemyProto; // ���g���i�[���邽�߂̃R���|�[�l���g
    private bool endFlg = false;

    private void Start()
    {
        enemyProto = this.gameObject.GetComponent<HitotumeProto>();
    }
    public override void UpdateState()
    {
        // �u���� �� �҂��v
        if (endFlg)
        {
            enemyProto.GetSetEnemyState = EnemyState.Idle;
            endFlg = false;
            return;
        }
    }

    public override void UpdateStrategy()
    {
        if (enemyProto.m_Ground.m_bStand)
        {
            //��������
            enemyProto.GetSetSpeed =
                new Vector2(enemyProto.GetSetKnockBack.m_vSpeed.x * enemyProto.GetSetKnockBack.m_fDamping,
                            enemyProto.GetSetKnockBack.m_vSpeed.y * enemyProto.GetSetKnockBack.m_fDamping);
            enemyProto.GetSetKnockBack.m_vSpeed = enemyProto.GetSetSpeed;
            //�e�ނ̂�0.1f�ȉ��ɂȂ�����
            if (enemyProto.GetSetSpeed.y <= 0.1f)
            {
                enemyProto.GetSetSpeed = Vector2.zero;     //�~�߂�
                endFlg = true;
            }
            enemyProto.m_Ground.m_bStand = false;
        }
    }
}

