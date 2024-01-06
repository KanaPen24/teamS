using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class TestCamera : MonoBehaviour
{
    [SerializeField] private CinemachineImpulseSource Source;//‰½‚©‚µ‚ç‚Ì•û–@‚ÅÝ’è
    float cnt;
    [SerializeField] private float a;

    public void OnImpulse()
    {
        Source.GenerateImpulse();
    }

    private void Update()
    {
        cnt += Time.deltaTime;
        if(cnt>a)
        {
            OnImpulse();
            cnt = 0;
        }
    }
}