using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyEnemy : ObjEnemyBase
{
    [SerializeField] private List<FlyStrategy> m_FlyStrategy;
    public override void UpdateObj()
    {
        m_FlyStrategy[(int)m_EnemyState].UpdateState();
        m_FlyStrategy[(int)m_EnemyState].UpdateStrategy();
    }
}
