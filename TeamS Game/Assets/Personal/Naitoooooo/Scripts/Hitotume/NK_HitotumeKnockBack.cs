using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NK_HitotumeKnockBack : NK_HitotumeStrategy
{
    private HitotumeProto enemyProto; // 自身を格納するためのコンポーネント
    private bool endFlg = false;

    private void Start()
    {
        enemyProto = this.gameObject.GetComponent<HitotumeProto>();
    }
    public override void UpdateState()
    {
        // 「落下 → 待ち」
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
            //減衰処理
            enemyProto.GetSetSpeed =
                new Vector2(enemyProto.GetSetKnockBack.m_vSpeed.x * enemyProto.GetSetKnockBack.m_fDamping,
                            enemyProto.GetSetKnockBack.m_vSpeed.y * enemyProto.GetSetKnockBack.m_fDamping);
            enemyProto.GetSetKnockBack.m_vSpeed = enemyProto.GetSetSpeed;
            //弾むのが0.1f以下になったら
            if (enemyProto.GetSetSpeed.y <= 0.1f)
            {
                enemyProto.GetSetSpeed = Vector2.zero;     //止める
                endFlg = true;
            }
            enemyProto.m_Ground.m_bStand = false;
        }
    }
}

