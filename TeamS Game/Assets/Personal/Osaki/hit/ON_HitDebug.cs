using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
@file   ON_HitDebug.cs
@brief  当たり判定デバック表示
@author Noriaki Osaki
@date   2023/10/20

当たり判定データを受け取り、指定された矩形を表示する
*/

// 当たり判定描画用クラス
public class HitDebugDraw
{
    public int hitID;
    public GameObject obj;

    public HitDebugDraw()
    {
        obj = new GameObject("DebugHit");
    }
}


public class ON_HitDebug
{
    // 当たり判定デバック描画用
    private List<HitDebugDraw> hitDebugs = new List<HitDebugDraw>();

    public static ON_HitDebug instance = null;

    // デバック表示用テクスチャ
    private Sprite image = null;

    public ON_HitDebug()
    {
        image = (Sprite)Resources.Load("Square", typeof(Sprite));
        hitDebugs.Clear();
    }
    
    // デバックモード開始時
    public void StartHitDebug()
    {
        for(int i = 0; i < ON_HitManager.instance.GetHitCnt(); ++i)
        {
            hitDebugs.Add(new HitDebugDraw());

            // ID, 座標, 大きさ設定
            hitDebugs[i].hitID = ON_HitManager.instance.GetHit(i).GetHitID();
            hitDebugs[i].obj.transform.position = ON_HitManager.instance.GetHit(i).GetCenter();
            hitDebugs[i].obj.transform.localScale = ON_HitManager.instance.GetHit(i).GetSize() * 2f;

            // スプライトの設定
            var sprite = hitDebugs[i].obj.AddComponent<SpriteRenderer>();
            sprite.color = SetColor(ON_HitManager.instance.GetHit(i).GetHitType());
            sprite.sprite = image;
            sprite.sortingOrder = 1000;
        }
    }

    // デバックモード終了時
    public void FinHitDebug()
    {
        for(int i = 0; i < hitDebugs.Count; ++i)
        {
            Object.Destroy(hitDebugs[i].obj);
        }

        hitDebugs.Clear();
    }

    // 指定されたHitTypeのみ表示(NONEはすべて表示
    public void Update(HitType type = HitType.NONE)
    {
        // デバックが開始されたか確認
        if (hitDebugs.Count < 1) return;

        // hitIDのオブジェクトが存在するか確認用
        Dictionary<int, bool> keys = new Dictionary<int, bool>();
        for(int i = 0; i < hitDebugs.Count; ++i)
        {
            keys.Add(hitDebugs[i].hitID, true);
        }

        for (int i = 0; i < ON_HitManager.instance.GetHitCnt(); ++i)
        {
            for (int j = 0; j < hitDebugs.Count; ++j)
            {
                // 当たり判定IDを使い、対応した当たり判定描画クラスの座標変化
                if(ON_HitManager.instance.GetHit(i).GetHitID() == hitDebugs[j].hitID)
                {
                    hitDebugs[j].obj.transform.position = ON_HitManager.instance.GetHit(i).GetCenter();
                    hitDebugs[j].obj.transform.localScale = ON_HitManager.instance.GetHit(i).GetSize() * 2f;

                    // 当たり判定が非Activeの場合、色を変化
                    if (!ON_HitManager.instance.GetHit(i).GetActive())
                        hitDebugs[j].obj.GetComponent<SpriteRenderer>().color = new Color(1.0f, 1.0f, 1.0f, 0.6f);

                    SetActive(type, ON_HitManager.instance.GetHit(i).GetHitType(), hitDebugs[j].obj);

                    // 使用済み
                    keys[hitDebugs[j].hitID] = false;

                    continue;
                }
            }

            // デバック開始以降に当たり判定が生成された場合
            if(!keys.ContainsKey(ON_HitManager.instance.GetHit(i).GetHitID()))
            {
                hitDebugs.Add(new HitDebugDraw());
                int idx = hitDebugs.Count - 1;

                // ID, 座標, 大きさ設定
                hitDebugs[idx].hitID = ON_HitManager.instance.GetHit(i).GetHitID();
                hitDebugs[idx].obj.transform.position = ON_HitManager.instance.GetHit(i).GetCenter();
                hitDebugs[idx].obj.transform.localScale = ON_HitManager.instance.GetHit(i).GetSize() * 2f;

                // スプライトの設定
                var sprite = hitDebugs[idx].obj.AddComponent<SpriteRenderer>();
                sprite.color = SetColor(ON_HitManager.instance.GetHit(i).GetHitType());
                sprite.sprite = image;
                sprite.sortingOrder = 1000;

                SetActive(type, ON_HitManager.instance.GetHit(i).GetHitType(), hitDebugs[idx].obj);
            }
        }

        // デバックモード開始以降に当たり判定が削除された場合
        for (int i = 0; i < hitDebugs.Count; ++i)
        {
            if(keys.ContainsKey(hitDebugs[i].hitID) && keys[hitDebugs[i].hitID])
            {
                Object.Destroy(hitDebugs[i].obj);
                hitDebugs.RemoveAt(i);
            }
        }
    }

    // HitType毎に色を変更
    private Color SetColor(HitType type)
    {
        Color col = new Color(1.0f, 0.0f, 0.0f, 0.5f);
        switch (type)
        {
            case HitType.ATTACK:
                col = new Color(1.0f, 0.0f, 0.0f, 0.6f);
                break;
            case HitType.BODY:
                col = new Color(1.0f, 1.0f, 0.0f, 0.6f);
                break;
            case HitType.FIELD:
                col = new Color(0.0f, 1.0f, 0.0f, 0.6f);
                break;
        }
        return col;
    }

    // 特定のHitType以外を非Activeに
    private void SetActive(HitType confType, HitType objType, GameObject obj)
    {
        if(confType == HitType.NONE)
        {
            // すべて表示
            obj.SetActive(true);
        }
        else
        {
            // 特定の当たり判定のみ表示
            if (objType == confType)
                obj.SetActive(true);
            else
                obj.SetActive(false);
        }
    }

    // デバック表示が可能か確認用
    public void DebugHit()
    {
        hitDebugs.Add(new HitDebugDraw());
        var sprite = hitDebugs[0].obj.AddComponent<SpriteRenderer>();
        sprite.color = new Color(0.5f, 0.5f, 0.8f, 0.3f);
        sprite.sprite = image;
    }
}
