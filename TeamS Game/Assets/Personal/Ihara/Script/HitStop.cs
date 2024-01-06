/**
 * @file   HitStop.cs
 * @brief  ヒットストップ
 * @author IharaShota
 * @date   2023/12/05
 * @Update 2023/12/05 作成
 **/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ===============================================
// HitStopState
// … HitStopの状態を管理する列挙体
// ===============================================
public enum HitStopState
{
    None,
    ON,
}

public class HitStop : MonoBehaviour
{
    public static HitStop instance;
    public HitStopState hitStopState;
    private float m_time;
    // Start is called before the first frame update
    void Start()
    {
        // --- 初期化 ---
        if (instance == null)
        {
            instance = this;
        }
        else Destroy(this.gameObject);
    }


    private void FixedUpdate()
    {
        if (hitStopState == HitStopState.ON)
        {
            m_time -= Time.deltaTime;
            if(m_time <= 0f)
            {
                m_time = 0;
                hitStopState = HitStopState.None;
            }
        }
    }

    // ヒットストップを開始する
    public void StartHitStop(float time)
    {
        if(hitStopState == HitStopState.None)
        {
            hitStopState = HitStopState.ON;
            m_time = time;
        }
    }
}
