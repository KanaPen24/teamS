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
    Jump,       //跳ぶ
    KnockBack,  //ノックバック
    Atk,        //攻撃
    Death       //死亡
}

//インスペクターでノックバック用の変数を見れるように
[System.Serializable]
public class KnockBack
{
    [HideInInspector]
    public Vector2 m_vSpeed;     // 現在の速度
    public Vector2 m_vInitSpeed; //初速速度
    public float m_fDamping;     //減衰率
}


public class ObjEnemyBase : ObjBase
{
    [SerializeField] protected KnockBack knockBack;
    [SerializeField] protected EnemyState m_EnemyState;
    public override void UpdateObj()
    {
    }

    public override void UpdateDebug()
    {
        //Debug.Log("EnemyBase");
    }
    public override void DamageAttack()
    {
        //攻撃をくらったらノックバックする
        //KnockBackObj();
    }

    public override void KnockBackObj(ObjDir dir)
    {
        //ノックバック中なら
        if (m_EnemyState == EnemyState.KnockBack)
        {
            //連撃の場合
            //GetSetSpeed = knockBack.m_fSpeed * 0.2f;
            if (dir == ObjDir.RIGHT)
                GetSetSpeed += knockBack.m_vInitSpeed;
            else if (dir == ObjDir.LEFT)
                GetSetSpeed += new Vector2(-knockBack.m_vInitSpeed.x, knockBack.m_vInitSpeed.y);
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

    public override void CheckObjGround()
    {
        // オブジェクトのタイプが「FIELD」だったら
        // 地面に立っている状態にする → 落下速度を0で終了
        if (m_EnemyState == EnemyState.KnockBack && m_Ground.m_bStand) 
        {
            //減衰処理
            GetSetSpeed = 
                new Vector2(knockBack.m_vSpeed.x * knockBack.m_fDamping, knockBack.m_vSpeed.y * knockBack.m_fDamping);
            knockBack.m_vSpeed = GetSetSpeed;
            //弾むのが0.1f以下になったら
            if (GetSetSpeed.y <= 0.1f)
            {   
                GetSetSpeed = Vector2.zero;     //止める
                //if(体力がゼロなら)
                m_EnemyState = EnemyState.Idle;
            }
            m_Ground.m_bStand = false;
            return;
        }

        // 今現在立っている地面を離れたら…
        if (GetSetPos.x + GetSetScale.x / 2f < m_Ground.m_vCenter.x - (m_Ground.m_vSize.x / 2f) ||
            GetSetPos.x - GetSetScale.x / 2f > m_Ground.m_vCenter.x + (m_Ground.m_vSize.x / 2f))
            m_Ground.m_bStand = false;

        // 地面についていなかったら、落ちる
        if (!GetSetGround.m_bStand)
        {
            if (GameManager.IsDebug())
                Debug.Log("地面から離れた ObjID: " + m_nObjID);

            m_vSpeed.y -= 0.05f;
        }        
    }

    public EnemyState GetSetEnemyState
    {
         get { return m_EnemyState; }
         set { m_EnemyState = value; } 
    }
}
