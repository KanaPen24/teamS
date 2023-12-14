using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyEnemy : ObjEnemyBase
{
    [SerializeField] private List<FlyStrategy> m_FlyStrategy;
    private float m_localScalex;

    private void Start()
    {
        m_localScalex = this.transform.localScale.x;
    }

    public override void UpdateObj()
    {
        if (GetSetEnemyState == EnemyState.Idle ||
            GetSetEnemyState == EnemyState.Walk)
        {
            if (ObjPlayer.instance.GetSetPos.x > GetSetPos.x)
            {
                GetSetDir = ObjDir.RIGHT;
                this.transform.localScale =
                    new Vector3(-m_localScalex, this.transform.localScale.y, this.transform.localScale.z);
            }
            else
            {
                GetSetDir = ObjDir.LEFT;
                this.transform.localScale =
                    new Vector3(m_localScalex, this.transform.localScale.y, this.transform.localScale.z);
            }
        }
        m_FlyStrategy[(int)m_EnemyState].UpdateState();
        m_FlyStrategy[(int)m_EnemyState].UpdateStrategy();
        CheckDivision();
    }
}
