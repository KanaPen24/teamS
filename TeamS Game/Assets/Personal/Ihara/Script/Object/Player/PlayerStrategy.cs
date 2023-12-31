/**
 * @file   PlayerStrategy.cs
 * @brief  Playerの遷移状態のクラス
 * @author IharaShota
 * @date   2023/10/27
 * @Update 2023/10/27 作成
 **/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStrategy : MonoBehaviour
{
    [HideInInspector]
    public bool m_bStartFlg;

    // Stateの初期化処理
    public void InitState()
    {
        m_bStartFlg = true;
    }

    // Playerの遷移状態の入力処理
    public virtual void UpdateState()
    {

    }

    // Playerの遷移状態の更新処理
    public virtual void UpdatePlayer()
    {

    }

    // Playerの遷移時の処理
    public virtual void StartState()
    {

    }

    // Playerの遷移終了時の処理
    public virtual void EndState()
    {

    }
}
