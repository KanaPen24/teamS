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
                new Vector2(enemyProto.GetSetSpeed.x * enemyProto.GetSetKnockBack.m_fDamping,
                            -enemyProto.GetSetSpeed.y * enemyProto.GetSetKnockBack.m_fDamping);
            //弾むのが0.1f以下になったら
            if (Mathf.Abs(enemyProto.GetSetSpeed.y) <= 0.2f)
            {
                enemyProto.GetSetSpeed = Vector2.zero;     //止める
                endFlg = true;
            }
            enemyProto.m_Ground.m_bStand = false;
        }

        enemyProto.GetSetSpeed = new Vector2(enemyProto.GetSetSpeed.x * 0.99f, enemyProto.GetSetSpeed.y);
    }
}

