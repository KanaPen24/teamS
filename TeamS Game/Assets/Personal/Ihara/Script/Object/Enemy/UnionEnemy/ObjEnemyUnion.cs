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

        if(Input.GetKey(KeyCode.Space))
        {
            // 合成していた敵を表示させる
            for(int i = 0; i < m_nEnemyIDs.Count; ++i)
            {
                ObjManager.instance.GetObjs(m_nEnemyIDs[i]).GetSetExist = true;
                ON_HitManager.instance.SetActive(ObjManager.instance.GetObjs(m_nEnemyIDs[i]).GetSetObjID, true);
            }

            // 合成していた敵のIDを全削除
            m_nEnemyIDs.Clear();

            // 自身のオブジェクトを非表示にする
            for (int j = 0; j < ObjManager.instance.GetObj().Count; ++j)
            {
                if (ObjManager.instance.GetObjs(j).gameObject == gameObject)
                {
                    GetSetExist = false;
                    ON_HitManager.instance.SetActive(ObjManager.instance.GetObjs(j).GetSetObjID, false);
                    break;
                }
            }
        }
    }

    //  ---- 当たり判定を生成した際の処理 ----
    public override void GenerateHit()
    {
        // 生成時に当たり判定IDを取得し、初期値を設定している(中心座標,大きさ,当たり判定の種類,objのID)
        m_nHitID = ON_HitManager.instance.GenerateHit(this.gameObject.transform.position,
        this.gameObject.transform.localScale / 2,
        false, HitType.BODY, GetSetObjID);

        Debug.Log("合成的");
    }
}
