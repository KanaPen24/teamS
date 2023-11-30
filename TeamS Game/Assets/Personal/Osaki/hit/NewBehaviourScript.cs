using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    ObjEnemyBase ParantEnemy;
    int ID;
    public void Generate()
    {
        ID = ON_HitManager.instance.GenerateHit(Vector3.back, Vector3.back, true, HitType.BULLET, ParantEnemy.GetSetObjID);
    }

    private void FixedUpdate()
    {
        
    }
}
