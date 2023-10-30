/**
 * @file   ObjEnemyBase.cs
 * @brief  敵の基底クラス
 * @author IharaShota
 * @date   2023/10/13
 * @Update 2023/10/13 作成
 * @Update 2023/10/27 ノックバックの処理追記 Kanase
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
    public Vector2 m_fSpeed;    //速度
    public float m_fDamping;    //減衰率
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
        KnockBackObj();
    }

    public override void KnockBackObj()
    {
        //ノックバック中なら
        if (m_EnemyState == EnemyState.KnockBack)
        {
            //連撃の場合
            GetSetSpeed = knockBack.m_fSpeed * 0.2f;
        }
        else
        {
            //最初のノックバック処理
            m_EnemyState = EnemyState.KnockBack; //ノックバックに変更
            GetSetGround.GetSetStand = false;
            GetSetSpeed = knockBack.m_fSpeed;
        }
        

    }

    public override void CheckObjGround()
    {
        // オブジェクトのタイプが「FIELD」だったら
        // 地面に立っている状態にする → 落下速度を0で終了
        if (m_EnemyState == EnemyState.KnockBack && m_Ground.GetSetStand) 
        {
            //減衰処理
            GetSetSpeed = new Vector2(knockBack.m_fSpeed.x*knockBack.m_fDamping, knockBack.m_fSpeed.y *knockBack.m_fDamping);
            knockBack.m_fSpeed = GetSetSpeed;
            //弾むのが0.1f以下になったら
            if (GetSetSpeed.y <= 0.1f)
            {   
                GetSetSpeed = Vector2.zero;     //止める
                //if(体力がゼロなら)
                m_EnemyState = EnemyState.Idle;
            }
            m_Ground.GetSetStand = false;
            return;
        }

        // 今現在立っている地面を離れたら…
        if (GetSetPos.x + GetSetScale.x / 2f < m_Ground.GetSetCenter.x - (m_Ground.GetSetSize.x / 2f) ||
            GetSetPos.x - GetSetScale.x / 2f > m_Ground.GetSetCenter.x + (m_Ground.GetSetSize.x / 2f))
            m_Ground.GetSetStand = false;

        // 地面についていなかったら、落ちる
        if (!GetSetGround.GetSetStand)
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
