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
}


public class ON_HitDebug
{
    private List<HitDebugDraw> hitDebugs = new List<HitDebugDraw>();

    public static ON_HitDebug instance = null;

    private Sprite image = null;

    public ON_HitDebug()
    {
        image = (Sprite)Resources.Load("Square", typeof(Sprite));
        hitDebugs.Clear();
    }

    // Start is called before the first frame update
    public void StartHitDebug()
    {
        
    }

    public void FinHitDebug()
    {
        for(int i = 0; i < hitDebugs.Count; ++i)
        {
            Object.Destroy(hitDebugs[i].obj);
        }

        hitDebugs.Clear();
    }

    void Update()
    {
        // デバックモード開始以降に生成された当たり判定を描画クラスに追加

        // 当たり判定IDを使い、対応した当たり判定描画クラスの座標変化

        // 当たり判定が非Activeの場合、色を変化

    }

    public void DebugHit()
    {
        hitDebugs.Add(new HitDebugDraw());
        var sprite = hitDebugs[0].obj.AddComponent<SpriteRenderer>();
        sprite.color = new Color(0.5f, 0.5f, 0.8f, 0.3f);
        sprite.sprite = image;
    }
}
