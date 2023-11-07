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
public class HitData
{
    public int myID;       // 自分のID
    public int otherID;    // 相手のID
    public HitState state; // 衝突時状態
    public HitDir dir;     // 衝突方向
    public HitData(int myid, int otherid, HitState hit, HitDir hitdir)
    {
        myID = myid; otherID = otherid; state = hit; dir = hitdir;
    }
}

// 衝突時状態
public enum HitState
{
    NONE = 0,

    ATTACK,     // 攻撃する
    DEFENCE,    // 攻撃される
    GRUOND,     // フィールドに当たった
    BODYS,      // 体同士が接触
    BALANCE,    // 攻撃同士が当たる
    ENEMY,      // 敵同士が当たる


    MAX_STATE
}

// 衝突方向
public enum HitDir
{
    NONE = 0,

    UP,   // 上
    DOWN, // 下
    RIGHT,// 右
    LEFT, // 左

    MAX_STATE
}

public class ON_HitManager
{
    public static ON_HitManager instance;

    private List<ON_HitBase> m_hits = new List<ON_HitBase>();   // 当たり判定のリスト
    private int hitCnt = 0;     // 当たった組み合わせ数
    private List<HitData> m_hitDatas = new List<HitData>();     // 衝突データリスト
    
    public void Init()
    {
        m_hits.Clear();
    }

    public void UpdateHit()
    {
        // リストの初期化
        m_hitDatas.Clear();
        hitCnt = 0;

        // 当たり判定の計算
        for(int i = 0; i < m_hits.Count; ++i)
        {
            // 当たり判定がoffだったらスキップする
            if (m_hits[i].GetActive() == false) continue;

            for (int j = i; j < m_hits.Count; ++j)
            {
                // 当たり判定がoffだったらスキップする
                if (m_hits[j].GetActive() == false) continue;
                // 左右
                if (m_hits[i].GetCenter().x + m_hits[i].GetSize().x <= m_hits[j].GetCenter().x - m_hits[j].GetSize().x) continue;
                if (m_hits[i].GetCenter().x - m_hits[i].GetSize().x >= m_hits[j].GetCenter().x + m_hits[j].GetSize().x) continue;

                // 上下
                if (m_hits[i].GetCenter().y + m_hits[i].GetSize().y <= m_hits[j].GetCenter().y - m_hits[j].GetSize().y) continue;
                if (m_hits[i].GetCenter().y - m_hits[i].GetSize().y >= m_hits[j].GetCenter().y + m_hits[j].GetSize().y) continue;

                // 同一オブジェクトか
                if (m_hits[i].GetObjID() == m_hits[j].GetObjID()) continue;

                // 攻撃と地面の判定かどうか(基本的には何もしない)
                if ((m_hits[i].GetHitType() == HitType.ATTACK && m_hits[j].GetHitType() == HitType.FIELD) &&
                    (m_hits[j].GetHitType() == HitType.ATTACK && m_hits[i].GetHitType() == HitType.FIELD)) continue;

                // 当たっている
                // 当たり判定の状態判定し、追加
                m_hitDatas.Add(new HitData(m_hits[i].GetObjID(),
                    m_hits[j].GetObjID(),
                    DecideState(i, j),
                    DecideHitDir(i, j)));
            }
        }

        hitCnt = m_hitDatas.Count;
    }

    // 当たり判定の状態特定
    private HitState DecideState(int i, int j)
    {
        HitState state = HitState.NONE;
        // 地面と当たっているか
        if(m_hits[i].GetHitType() == HitType.FIELD || m_hits[j].GetHitType() == HitType.FIELD)
        {
            state = HitState.GRUOND;
            return state;
        }
        // 敵同士が当たった時
        if(ObjManager.instance.GetObjs(m_hits[i].GetObjID()).GetComponent<ObjEnemyBase>() != null &&
           ObjManager.instance.GetObjs(m_hits[j].GetObjID()).GetComponent<ObjEnemyBase>() != null){
            state = HitState.ENEMY;
            return state;
        }

        // 自分の状態によって処理派生
        switch(m_hits[i].GetHitType())
        {
            case HitType.ATTACK:
                if (m_hits[j].GetHitType() == HitType.ATTACK) state = HitState.BALANCE;
                if (m_hits[j].GetHitType() == HitType.BODY) state = HitState.ATTACK;
                break;
            case HitType.BODY:
                if (m_hits[j].GetHitType() == HitType.ATTACK) state = HitState.DEFENCE;
                if (m_hits[j].GetHitType() == HitType.BODY) state = HitState.BODYS;
                break;
        }

        return state;
    }

    // 衝突方向の確定
    private HitDir DecideHitDir(int i, int j)
    {
        HitDir hitDir = HitDir.NONE;

        // 上に当たっているかどうか
        if ((m_hits[i].GetCenter().y + m_hits[i].GetSize().y > m_hits[j].GetCenter().y - m_hits[j].GetSize().y &&
             m_hits[i].GetCenter().y + m_hits[i].GetSize().y < m_hits[j].GetCenter().y + m_hits[j].GetSize().y) &&
            (m_hits[i].GetCenter().x - m_hits[i].GetSize().x / 2f < m_hits[j].GetCenter().x + m_hits[j].GetSize().x &&
             m_hits[i].GetCenter().x + m_hits[i].GetSize().x / 2f > m_hits[j].GetCenter().x - m_hits[j].GetSize().x))
            return hitDir = HitDir.UP;
        // 下に当たっているかどうか
        if ((m_hits[i].GetCenter().y - m_hits[i].GetSize().y > m_hits[j].GetCenter().y - m_hits[j].GetSize().y &&
             m_hits[i].GetCenter().y - m_hits[i].GetSize().y < m_hits[j].GetCenter().y + m_hits[j].GetSize().y) &&
            (m_hits[i].GetCenter().x - m_hits[i].GetSize().x / 2f < m_hits[j].GetCenter().x + m_hits[j].GetSize().x &&
             m_hits[i].GetCenter().x + m_hits[i].GetSize().x / 2f > m_hits[j].GetCenter().x - m_hits[j].GetSize().x))
            return hitDir = HitDir.DOWN;
        // 右に当たっているかどうか
        if ((m_hits[i].GetCenter().x + m_hits[i].GetSize().x > m_hits[j].GetCenter().x - m_hits[j].GetSize().x &&
             m_hits[i].GetCenter().x + m_hits[i].GetSize().x < m_hits[j].GetCenter().x + m_hits[j].GetSize().x) &&
            (m_hits[i].GetCenter().y + m_hits[i].GetSize().y / 2f > m_hits[j].GetCenter().y - m_hits[j].GetSize().y &&
             m_hits[i].GetCenter().y - m_hits[i].GetSize().y / 2f < m_hits[j].GetCenter().y + m_hits[j].GetSize().y ))
            return hitDir = HitDir.RIGHT;
        // 左に当たっているかどうか
        if ((m_hits[i].GetCenter().x - m_hits[i].GetSize().x > m_hits[j].GetCenter().x - m_hits[j].GetSize().x &&
             m_hits[i].GetCenter().x - m_hits[i].GetSize().x < m_hits[j].GetCenter().x + m_hits[j].GetSize().x) &&
            (m_hits[i].GetCenter().y + m_hits[i].GetSize().y / 2f > m_hits[j].GetCenter().y - m_hits[j].GetSize().y &&
             m_hits[i].GetCenter().y - m_hits[i].GetSize().y / 2f < m_hits[j].GetCenter().y + m_hits[j].GetSize().y))
            return hitDir = HitDir.LEFT;

        return hitDir;
    }

    // 当たり判定の生成
    public int GenerateHit(Vector3 center, Vector3 size, bool active, HitType type, int ID)
    {
        m_hits.Add(new ON_HitBase(center, size, active, type, ID));

        if(m_hits.Count > 1)
        {
            m_hits[m_hits.Count - 1].SetHitID(m_hits[m_hits.Count - 2].GetHitID() + 1);
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

    // 当たり判定の数取得
    public int GetHitCnt()
    {
        return m_hits.Count;
    }

    // 当たり判定のデータ取得
    public ON_HitBase GetHit(int num)
    {
        return m_hits[num];
    }

    // 当たり判定の組み合わせ数
    public int GetHitCombiCnt()
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
    public void MoveHit(int id, Vector3 vec)
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
    public void MoveHits(int objID, Vector3 vec)
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
    public void SetCenter(int id, Vector3 pos)
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
    public void SetSize(int id, Vector3 size)
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

    // デバッグ用で当たり判定のIDとパラメータを表示する
    public void DebugHitID(int myId)
    {
        for (int i = 0; i < m_hits.Count; ++i)
        {
            if (m_hits[i].GetHitID() == myId)
            {
                Debug.Log("hitID:" + i + "　pos:" + m_hits[i].GetCenter());
                break;
            }
        }
    }
}
