/**
@file   ON_HitManager.cs
@brief  当たり判定マネージャー
@author Noriaki Osaki
@date   2023/10/13

当たり判定をリスト化、
当たり判定の追加、削除
当たり判定の計算
シングルトンを使用
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 衝突時データ
public struct HitData
{
    public int myID;       // 自分のID
    public int otherID;    // 相手のID
    public HitState state; // 衝突時状態 
}

// 衝突時状態
public enum HitState
{
    NONE = 0,

    ATTACK,     // 攻撃する
    DEFENCE,    // 攻撃される
    STAND,      // 上に乗る
    CARRY,      // 上に乗られる


    MAX_STATE
}


public class ON_HitManager
{
    public static ON_HitManager instance;

    private List<ON_HitBase> m_hits = new List<ON_HitBase>();   // 当たり判定のリスト
    private int hitCnt = 0;     // 当たった組み合わせ数
    private List<HitData> m_hitDatas = new List<HitData>();     // 衝突データリスト
    
    void Init()
    {
        if(instance == null)
        {
            instance = this;
            m_hits.Clear();
        }
        else
        {
            instance = null;
        }
    }

    void Update()
    {
        // リストの初期化
        m_hitDatas.Clear();

        // 当たり判定の計算


    }

    // 当たり判定の生成
    public int GenerateHit(Vector2 center, Vector2 size, bool active, HitType type, int ID)
    {
        m_hits.Add(new ON_HitBase(center, size, active, type, ID));

        if(m_hits.Count > 0)
        {
            m_hits[m_hits.Count - 1].SetHitID(m_hits[m_hits.Count - 2].GetHitID());
        }
        return m_hits[m_hits.Count - 1].GetHitID();
    }

    // 当たり判定の削除( 引数：当たり判定ID
    public int DeleteHit(int id)
    {
        for(int i = 0; i < m_hits.Count; ++i)
        {
            if(m_hits[i].GetHitID() == id)
            {
                m_hits.RemoveAt(i);
                break;
            }
        }
        return -1;
    }

    // 当たり判定の削除( 引数：オブジェクトID
    public int DeleteHits(int id)
    {
        for (int i = 0; i < m_hits.Count; ++i)
        {
            if (m_hits[i].GetObjID() == id)
            {
                m_hits.RemoveAt(i);
            }
        }
        return -1;
    }

    // 当たり判定の組み合わせ数
    public int GetHitCnt()
    {
        return hitCnt;
    }

    // 当てた側ID取得( 引数：組み合わせ番号
    public int GetMyID(int num)
    {
        return m_hitDatas[num].myID;
    }

    // 当てられた側ID取得( 引数：組み合わせ番号
    public int GetOtherID(int num)
    {
        return m_hitDatas[num].otherID;
    }

    // 当たり判定の状態取得( 引数：組み合わせ番号
    public HitState GetState(int num)
    {
        return m_hitDatas[num].state;
    }

    // 衝突判定データ取得( 引数：組み合わせ番号
    public HitData GetData(int num)
    {
        return m_hitDatas[num];
    }

    // 当たり判定の移動( 引数：当たり判定ID, 移動量
    public void MoveHit(int id, Vector2 vec)
    {
        // 指定された特定の当たり判定のみ移動
        for(int i = 0; i < m_hits.Count; ++i)
        {
            if(m_hits[i].GetHitID() == id)
            {
                m_hits[i].MoveHit(vec);
                break;
            }
        }
    }

    // 当たり判定の移動( 引数:オブジェクトID, 移動量
    public void MoveHits(int objID, Vector2 vec)
    {
        // 指定されたオブジェクトの当たり判定をすべて移動
        for(int i = 0; i < m_hits.Count; ++i)
        {
            if(m_hits[i].GetObjID() == objID)
            {
                m_hits[i].MoveHit(vec);
            }
        }
    }

    // 当たり判定の中心座標変更( 引数：当たり判定ID
    public void SetCenter(int id, Vector2 pos)
    {
        for (int i = 0; i < m_hits.Count; ++i)
        {
            if (m_hits[i].GetHitID() == id)
            {
                m_hits[i].SetCenter(pos);
                break;
            }
        }
    }

    // 当たり判定のOn/Off ( 引数：当たり判定ID, flg
    public void SetActive(int id, bool flg)
    {
        // 特定の当たり判定のOn/Off
        for (int i = 0; i < m_hits.Count; ++i)
        {
            if (m_hits[i].GetHitID() == id)
            {
                m_hits[i].SetActive(flg);
                break;
            }
        }
    }

    // 当たり判定のOn/Off ( 引数:オブジェクトID, flg
    public void SetActives(int objID, bool flg)
    {
        // 指定されたオブジェクトの当たり判定のOn/Off
        for (int i = 0; i < m_hits.Count; ++i)
        {
            if (m_hits[i].GetObjID() == objID)
            {
                m_hits[i].SetActive(flg);
            }
        }
    }

    // 当たり判定の大きさ変更( 引数:当たり判定id, 大きさ
    public void SetSize(int id, Vector2 size)
    {
        for (int i = 0; i < m_hits.Count; ++i)
        {
            if (m_hits[i].GetHitID() == id)
            {
                m_hits[i].SetSize(size);
                break;
            }
        }
    }
}
