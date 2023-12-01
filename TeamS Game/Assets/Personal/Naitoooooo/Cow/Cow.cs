using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cow : ObjEnemyBase
{
    [SerializeField] private List<EnemyStrategy> m_CowStrategy;
    [SerializeField] private CowWalk m_MoveReset;
    // Start is called before the first frame update
    public override void UpdateObj()
    {
        if (ObjPlayer.instance.GetSetPos.x > GetSetPos.x)
        {
            GetSetDir = ObjDir.RIGHT;
        }
        else
        {
            GetSetDir = ObjDir.LEFT;
        }
        if(GetSetEnemyState != EnemyState.Walk)
        {
            m_MoveReset.ReSetMoveSpeed();
        }

        m_CowStrategy[(int)m_EnemyState].UpdateState();
        m_CowStrategy[(int)m_EnemyState].UpdateStrategy();
    }
}
