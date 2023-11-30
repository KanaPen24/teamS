using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour
{
    private int Missilenum;
    [SerializeField] private Spider m_Spider;
    [SerializeField] private float m_YSpeed;
    [SerializeField] private float m_XSpeed;
    private void Start()
    {
        Missilenum = ON_HitManager.instance.GenerateHit(transform.position, transform, true, HitType.Ballet, m_Spider.GetSetObjID);
    }

    private void FixedUpdate()
    {
        
    }
}
