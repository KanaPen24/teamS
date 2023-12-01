using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spider : ObjEnemyBase
{
    [SerializeField] private List<EnemyStrategy> m_SpiderStrategy;
    public GameObject myMissile;

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

        m_SpiderStrategy[(int)m_EnemyState].UpdateState();
        m_SpiderStrategy[(int)m_EnemyState].UpdateStrategy();
    }

    public void DeleteMissile()
    {
        if (myMissile != null)
        {
            ON_HitManager.instance.DeleteHit(myMissile.GetComponent<Missile>().Missilenum);
            Destroy(myMissile.gameObject);
            myMissile = null;
        }
    }
}
