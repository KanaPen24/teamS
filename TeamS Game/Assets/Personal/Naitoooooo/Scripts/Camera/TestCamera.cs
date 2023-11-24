using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class TestCamera : MonoBehaviour
{
    public CinemachineImpulseSource Source;//âΩÇ©ÇµÇÁÇÃï˚ñ@Ç≈ê›íË
    private void Start()
    {

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            Shake(new Vector3(0, 10, 10), 0.2f, 0.2f);
        }
    }

    /// <summary>
    /// ÉJÉÅÉâÇóhÇÁÇ∑
    /// </summary>
    /// <param name="dire"></param>
    /// <param name="decelerationTime"></param>
    /// <param name="maxTime"></param>
    public void Shake(Vector3 dire, float decelerationTime, float maxTime)
    {
        Source.m_ImpulseDefinition.m_TimeEnvelope.m_AttackTime = maxTime;
        Source.m_ImpulseDefinition.m_TimeEnvelope.m_DecayTime = decelerationTime;
        Source.GenerateImpulse(dire);
    }
}