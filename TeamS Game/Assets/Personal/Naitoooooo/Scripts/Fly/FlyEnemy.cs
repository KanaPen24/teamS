using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyEnemy : ObjEnemyBase
{
    [SerializeField] private List<FlyStrategy> m_FlyStrategy;
    public override void UpdateObj()
    {
        if (GetSetEnemyState == EnemyState.Idle ||
            GetSetEnemyState == EnemyState.Walk)
        {
            if (ObjPlayer.instance.GetSetPos.x > GetSetPos.x)
            {
                GetSetDir = ObjDir.RIGHT;
            }
            else
            {
                GetSetDir = ObjDir.LEFT;
            }
        }
        m_FlyStrategy[(int)m_EnemyState].UpdateState();
        m_FlyStrategy[(int)m_EnemyState].UpdateStrategy();
        CheckDivision();
    }
}
