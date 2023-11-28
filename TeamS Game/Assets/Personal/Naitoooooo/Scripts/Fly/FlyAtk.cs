using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyAtk : FlyStrategy
{
    [SerializeField] private FlyEnemy m_Fly;
    [SerializeField] private float m_DownTiming;
    private float m_Cnt;


    public override void UpdateState()
    {
        m_Cnt += Time.deltaTime;
        if(m_Cnt>m_DownTiming)
        {
            m_Cnt = 0;
            m_Fly.GetSetEnemyState = EnemyState.Jump;
        }
    }

    public override void UpdateStrategy()
    {
        //this.transform.rotation = Quaternion.AngleAxis(GetAim(), Vector3.forward);
        //var direction = Quaternion.Euler(angles) * Vector3.forward;
        transform.rotation = Quaternion.AngleAxis(0.0f, new Vector3(0, 0, 1));
        m_Fly.GetSetSpeed = new Vector2(0.0f, 0.0f);
    }

    //private float GetAim()
    //{
    //    Vector2 p1 = m_Fly.GetSetPos;
    //    Vector2 p2 = ObjPlayer.instance.GetSetPos;
    //    float dx = p2.x - p1.x;
    //    float dy = p2.y - p1.y;
    //    float rad = Mathf.Atan2(dy, dx);

    //    return rad * Mathf.Rad2Deg;
    //}
}
