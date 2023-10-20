using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if(ON_HitDebug.instance == null)
        {
            ON_HitDebug.instance = new ON_HitDebug();
        }

        ON_HitDebug.instance.DebugHit();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
