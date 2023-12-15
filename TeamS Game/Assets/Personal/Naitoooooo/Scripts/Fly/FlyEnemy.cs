using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyEnemy : ObjEnemyBase
{
    [SerializeField] private List<FlyStrategy> m_FlyStrategy;
    private float m_localScalex;
    [SerializeField] private Animator m_Anim;

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

        if(GetSetEnemyState==EnemyState.Atk || 
            GetSetEnemyState==EnemyState.Drop)
        {
            m_Anim.SetBool("AtkFlag", true);
        }
        else
        {
            m_Anim.SetBool("AtkFlag", false);
        }

        if(GetSetEnemyState != EnemyState.KnockBack)
        {
            m_Anim.SetBool("KnockFlag", false);
        }
        else
        {
            m_Anim.SetBool("KnockFlag", true);
        }
        m_FlyStrategy[(int)m_EnemyState].UpdateState();
        m_FlyStrategy[(int)m_EnemyState].UpdateStrategy();
        CheckDivision();
    }
}
