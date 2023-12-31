/**
 * @file   ObjEnemyBase.cs
 * @brief  敵の基底クラス
 * @author IharaShota
 * @date   2023/10/13
 * @Update 2023/10/13 作成
 * @Update 2023/10/27 ノックバックの処理追記 Kanase
 * @Update 2023/11/02 ノックバックの処理修正 Ihara
 **/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//敵の状態
public enum EnemyState
{
    Idle,       //待機
    Walk,       //歩く
    Drop,       //落ちる
    Jump,       //跳ぶ
    KnockBack,  //ノックバック
    Atk,        //攻撃
    Death,      //死亡
    RePop,      //復活
}

//インスペクターでノックバック用の変数を見れるように
[System.Serializable]
public class KnockBack
{
    [HideInInspector]
    public Vector2 m_vSpeed;     // 現在の速度
    public Vector2 m_vInitSpeed; //初速速度
    public float m_fDamping;     //減衰率
    public float m_fGravity;     //重力

    public float m_fStartTime;
}

// 分散用のクラス
[System.Serializable]
public class Division
{
    [HideInInspector]
    public bool m_bDivisionTrigger;
    [HideInInspector]
    public float m_fDivisionTime;
    [HideInInspector]
    public float m_fBlinkTime;
    [HideInInspector]
    public float m_fMaxBlinkTime;
}

// ブレる用のクラス
[System.Serializable]
public class Shake
{
    public enum ShakeState
    {
        StartShake,
        Shake,
        Idle,
    }

    public ShakeState m_ShakeState;
    public float m_fPow = 0.5f;
    public float m_fTime;
}


public class ObjEnemyBase : ObjBase
{
    [SerializeField] protected KnockBack knockBack;
    [SerializeField] protected EnemyState m_EnemyState;
    [HideInInspector]
    public Division m_division;
    [HideInInspector]
    public Shake m_Shake;
    public bool m_bStop;

    // ----- オーバーライド関数  ------
    // ************************************************************************************************
    // 初期化処理
    public override void InitObj()
    {
        base.InitObj();
        m_division.m_bDivisionTrigger = false;
        m_division.m_fDivisionTime = 1.0f;
        m_division.m_fMaxBlinkTime = 0.1f;
        m_division.m_fBlinkTime = m_division.m_fMaxBlinkTime;
    }

    // 更新処理
    public override void UpdateObj()
    {
    }

    // 攻撃を貰う処理
    public override void DamageAttack()
    {
        //攻撃をくらったらノックバックする
        //KnockBackObj();
    }

    // ノックバック関数
    public override void KnockBackObj(ObjDir dir = ObjDir.NONE)
    {
        //ノックバック中なら
        if (m_EnemyState == EnemyState.KnockBack)
        {
            //連撃の場合
            GetSetSpeed = Vector2.zero; //リセットする
            if (dir == ObjDir.RIGHT)
                GetSetSpeed += new Vector2(knockBack.m_vInitSpeed.x * 0.53f, knockBack.m_vInitSpeed.y * 0.7f);
            else if (dir == ObjDir.LEFT)
                GetSetSpeed += new Vector2(-knockBack.m_vInitSpeed.x * 0.53f, knockBack.m_vInitSpeed.y * 0.7f);
            //限界値は越えない
            //if (GetSetMaxSpeed.x <= GetSetSpeed.x || GetSetMaxSpeed.y <= GetSetSpeed.y)
            //    GetSetSpeed = GetSetMaxSpeed;
            Debug.Log("連撃発生");
        }
        else
        {
            //最初のノックバック処理
            m_EnemyState = EnemyState.KnockBack; //ノックバックに変更
            GetSetGround.m_bStand = false;       // 地面に立っていない状態にする

            // 向きによって初速度を設定
            if (dir == ObjDir.RIGHT)
                GetSetSpeed = knockBack.m_vInitSpeed;
            else if (dir == ObjDir.LEFT)
                GetSetSpeed = new Vector2(-knockBack.m_vInitSpeed.x, knockBack.m_vInitSpeed.y);
        }

        // 現在の速度をノックバック計算用の速度に格納(Ihara)
        knockBack.m_vSpeed = GetSetSpeed;
    }

    // 地面判定処理
    public override void CheckObjGround()
    {
        // 今現在立っている地面を離れたら…
        if (GetSetPos.x + GetSetScale.x / 2f < m_Ground.m_vCenter.x - (m_Ground.m_vSize.x / 2f) ||
            GetSetPos.x - GetSetScale.x / 2f > m_Ground.m_vCenter.x + (m_Ground.m_vSize.x / 2f))
            m_Ground.m_bStand = false;

        // 地面についていなかったら、落ちる
        if (!GetSetGround.m_bStand)
        {
            if (GameManager.IsDebug())
                Debug.Log("地面から離れた ObjID: " + m_nObjID);
            //重力を使って落とす
            m_vSpeed.y -= knockBack.m_fGravity;
        }        
    }

    // ヒットストップ更新
    public override void UpdateHitStop()
    {
        // ヒットストップ時に
        // 敵の動きを止める(座標は動かない) → ブレさせる処理(あくまで見た目を動かす)
        // 
        if (m_HitStopParam.m_bHitStop)
        {
            m_vSpeed = Vector2.zero;

            GameObject visualObj = transform.GetChild(0).gameObject;

            m_Shake.m_fTime -= Time.deltaTime;

            if (m_Shake.m_fTime <= 0f)
            {
                switch (m_Shake.m_ShakeState)
                {
                    case Shake.ShakeState.StartShake:
                        m_Shake.m_fPow = 0.25f;

                        visualObj.transform.localPosition =
                            new Vector3(m_Shake.m_fPow, 0f, 0f);

                        m_Shake.m_fPow = -m_Shake.m_fPow;
                        m_Shake.m_ShakeState = Shake.ShakeState.Shake;
                        break;

                    case Shake.ShakeState.Shake:
                        m_Shake.m_fPow *= 0.8f;
                        m_Shake.m_fTime = 0.02f;

                        visualObj.transform.localPosition =
                            new Vector3(m_Shake.m_fPow, 0f, 0f);

                        m_Shake.m_fPow = -m_Shake.m_fPow;
                        m_Shake.m_ShakeState = Shake.ShakeState.Idle;
                        break;
                    case Shake.ShakeState.Idle:
                        m_Shake.m_ShakeState = Shake.ShakeState.Shake;
                        break;
                }
            }
        }
        else
        {
            m_Shake.m_ShakeState = Shake.ShakeState.StartShake;
            m_Shake.m_fTime = 0f;
        }
        // 基底の処理
        base.UpdateHitStop();
    }
    // *************************************************************************************************
    // ---------------------------------

    // ------ 独自の関数 ------
    // *************************************************************************************************
    // 点滅チェック
    protected void CheckDivision()
    {
        if (m_division.m_bDivisionTrigger)
        {
            m_division.m_fDivisionTime -= Time.deltaTime;
            if (m_division.m_fDivisionTime <= 0f)
            {
                GetSetDestroy = true;
            }
            else UpdateDivision();
        }
    }

    // 点滅の更新
    private void UpdateDivision()
    {
        if (m_division.m_fBlinkTime <= 0f)
        {
            texObj.enabled = !texObj.enabled;
            m_division.m_fBlinkTime = m_division.m_fMaxBlinkTime;
        }
        else m_division.m_fBlinkTime -= Time.deltaTime;
    }

    // 敵の状態取得
    public EnemyState GetSetEnemyState
    {
         get { return m_EnemyState; }
         set { m_EnemyState = value; } 
    }

    // ノックバックの取得・セット
    public KnockBack GetSetKnockBack
    {
        get { return knockBack; }
        set { knockBack = value; }
    }
    // *************************************************************************************************
    // ----------------------
}
