/**
 * @file  EnemyUnion_KnockBack.cs
 * @brief  EnemyUnionの「ノックバック」クラス
 * @author IharaShota
 * @date   2023/11/24
 * @Update 2023/11/24 作成
 **/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyUnion_KnockBack : EnemyStrategy
{
    private ObjEnemyUnion enemyUnion; // 自身を格納するためのコンポーネント
    private bool endFlg = false;
    public ParticleSystem burstEffect;

    private void Start()
    {
        enemyUnion = this.gameObject.GetComponent<ObjEnemyUnion>();
    }
    public override void UpdateState()
    {
        // 「落下 → 待ち」(分裂)
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
            //減衰処理
            enemyUnion.GetSetSpeed =
                new Vector2(enemyUnion.GetSetSpeed.x * enemyUnion.GetSetKnockBack.m_fDamping,
                            -enemyUnion.GetSetSpeed.y * enemyUnion.GetSetKnockBack.m_fDamping);
            //弾むのが0.1f以下になったら
            if (Mathf.Abs(enemyUnion.GetSetSpeed.y) <= 0.2f)
            {
                enemyUnion.GetSetSpeed = Vector2.zero;     //止める
                endFlg = true;
            }
        }
    }
}
