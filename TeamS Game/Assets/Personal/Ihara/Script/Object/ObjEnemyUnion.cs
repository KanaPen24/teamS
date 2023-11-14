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

public class ObjEnemyUnion : ObjEnemyBase
{
    public List<int> m_nEnemyIDs = new List<int>();

    public override void UpdateObj()
    {
        if(GetSetGround.m_bStand)
        {
            // 削除
            for(int i = 0; i < m_nEnemyIDs.Count; ++i)
            {
                ObjManager.instance.GetObjs(m_nEnemyIDs[i]).GetSetExist = true;
                ON_HitManager.instance.SetActive(ObjManager.instance.GetObjs(m_nEnemyIDs[i]).GetSetObjID, true);
            }

            for (int j = 0; j < ObjManager.instance.GetObj().Count; ++j)
            {
                if (ObjManager.instance.GetObjs(j).gameObject != null)
                {
                    //ObjManager.instance.GetObj().Remove(this.gameObject.GetComponent<ObjBase>());
                    //ON_HitManager.instance.DeleteHit(GetSetHitID);
                    GetSetExist = false;
                    //Destroy(this.gameObject);
                    break;
                }
            }
        }
    }
}
