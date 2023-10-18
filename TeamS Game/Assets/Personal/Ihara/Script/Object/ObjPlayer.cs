/**
 * @file   ObjPlayer.cs
 * @brief  Playerクラス
 * @author IharaShota
 * @date   2023/10/13
 * @Update 2023/10/13 作成
 **/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjPlayer : ObjBase
{
    public float Speed;   // 基本速度
    public float MaxSpeed;// 最高速度
    public float Accel;   // 加速度

    private void Start()
    {
        GetSetSpeed = Speed;
        GetSetMaxSpeed = MaxSpeed;
        GetSetAccel = Accel;
    }

    public void Update()
    {
        // --- 入力確認 ---
        // 移動
        if (IS_XBoxInput.LStick_H > 0.2f || IS_XBoxInput.LStick_H < -0.2f)
        {
            GetSetSpeed += IS_XBoxInput.LStick_H * Speed * Accel;
            if (GetSetSpeed > MaxSpeed)
            {
                GetSetSpeed = MaxSpeed;
            }
            if (GetSetSpeed < -MaxSpeed)
            {
                GetSetSpeed = -MaxSpeed;
            }
        }
        else
        {
            GetSetSpeed *= 0.99f;
            if(GetSetSpeed <= 0.01f && GetSetSpeed >= 0.01f)
            {
                GetSetSpeed = 0f;
            }
        }
        // ----------------
    }

    public override void UpdateObj()
    {
        this.transform.position += new Vector3(GetSetSpeed, GetSetFallSpeed, 0f);
    }

    public override void UpdateDebug()
    {
        //Debug.Log("Player");
    }
}
