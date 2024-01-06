/**
 * @file   PlayerStrategy.cs
 * @brief  Player‚Ì‘JˆÚó‘Ô‚ÌƒNƒ‰ƒX
 * @author IharaShota
 * @date   2023/10/27
 * @Update 2023/10/27 ì¬
 **/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStrategy : MonoBehaviour
{
    [HideInInspector]
    public bool m_bStartFlg;

    // State‚Ì‰Šú‰»ˆ—
    public void InitState()
    {
        m_bStartFlg = true;
    }

    // Player‚Ì‘JˆÚó‘Ô‚Ì“ü—Íˆ—
    public virtual void UpdateState()
    {

    }

    // Player‚Ì‘JˆÚó‘Ô‚ÌXVˆ—
    public virtual void UpdatePlayer()
    {

    }

    // Player‚Ì‘JˆÚ‚Ìˆ—
    public virtual void StartState()
    {

    }

    // Player‚Ì‘JˆÚI—¹‚Ìˆ—
    public virtual void EndState()
    {

    }
}
