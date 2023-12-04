/**
 * @file   ObjEnemyUnion.cs
 * @brief  合体した敵のクラス
 * @author IharaShota
 * @date   2023/11/09
 * @Update 2023/11/09 作成
 **/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemyUnion_Idle))]
[RequireComponent(typeof(EnemyUnion_Walk))]
[RequireComponent(typeof(EnemyUnion_Drop))]
[RequireComponent(typeof(EnemyUnion_Jump))]
[RequireComponent(typeof(EnemyUnion_KnockBack))]
[RequireComponent(typeof(EnemyUnion_Atk))]
[RequireComponent(typeof(EnemyUnion_Death))]
[RequireComponent(typeof(EnemyUnion_RePop))]

public class ObjEnemyUnion : ObjEnemyBase
{
    [SerializeField] private List<EnemyStrategy> enemyStrategys;
    public List<int> m_nEnemyIDs = new List<int>();

    public override void UpdateObj()
    {
        enemyStrategys[(int)m_EnemyState].UpdateState();
        enemyStrategys[(int)m_EnemyState].UpdateStrategy();
    }

    //  ---- 当たり判定を生成した際の処理 ----
    public override void GenerateHit()
    {
        // 生成時に当たり判定IDを取得し、初期値を設定している(中心座標,大きさ,当たり判定の種類,objのID)
        m_nHitID = ON_HitManager.instance.GenerateHit(this.gameObject.transform.position,
        this.gameObject.transform.localScale / 2,
        true, HitType.BODY, GetSetObjID);
    }

    // --- 敵の分離処理 ---
    public void DivisionEnemy()
    {
        // 合成していた敵を表示させる
        for (int i = 0; i < m_nEnemyIDs.Count; ++i)
        {
            ObjEnemyBase enemy = ObjManager.instance.GetObj(m_nEnemyIDs[i]).GetComponent<ObjEnemyBase>();
            enemy.GetSetExist = true;
            enemy.GetSetPos = GetSetPos;
            enemy.GetSetEnemyState = EnemyState.RePop;
            ON_HitManager.instance.SetActive(ObjManager.instance.GetObj(m_nEnemyIDs[i]).GetSetObjID, true);

            // 合体していた敵が「合体敵」だった場合、破壊トリガーをON
            if (enemy.GetComponent<ObjEnemyUnion>() != null)
                enemy.GetSetDestroy = true;
        }

        // 合成していた敵のIDを全削除
        m_nEnemyIDs.Clear();

        // 自身の破壊トリガーをON
        GetSetDestroy = true;

        //// 自身のオブジェクトを非表示にする
        //for (int j = 0; j < ObjManager.instance.GetObjList().Count; ++j)
        //{
        //    // Objのリストから自身と同じオブジェクトIDを検索
        //    if (ObjManager.instance.GetObjList()[j].GetSetObjID == GetSetObjID)
        //    {
        //        GetSetExist = false;
        //        //ON_HitManager.instance.SetActive(ObjManager.instance.GetObj(j).GetSetObjID, false);
        //        ON_HitManager.instance.SetActive(ObjManager.instance.GetObjList()[j].GetSetObjID, false);
        //        break;
        //    }
        //}
    }

    // --- 合体していた敵たちの破壊トリガーをON ---
    public void DestroyTriggerChildEnemy()
    {
        // 合成していた敵を表示させる
        for (int i = 0; i < m_nEnemyIDs.Count; ++i)
        {
            ObjEnemyBase enemy = ObjManager.instance.GetObj(m_nEnemyIDs[i]).GetComponent<ObjEnemyBase>();
            enemy.GetSetDestroy = true;
        }
    }
}
