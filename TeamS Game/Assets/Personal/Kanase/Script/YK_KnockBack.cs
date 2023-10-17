using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class YK_KnockBack : MonoBehaviour
{
    GameObject target;
    public float speed, ratio;
    public float P1_Y;
    public float m_LastSpeed;
    /// <summary> �q�b�g�X�g�b�v����(�b) </summary>
    public float HitStopTime = 0.23f;

    void Start()
    {
        
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            OnStart(GameObject.Find("Target").gameObject);
        }
    }

    public void OnStart(GameObject target)
    {
        this.target = target;
        StartCoroutine(Throw());
    }

    IEnumerator Throw()
    {
        var seq = DOTween.Sequence();
        seq.SetDelay(HitStopTime);
        float t = 0f;
        float distance = Vector3.Distance(transform.position, target.transform.position);

        Vector3 offset = transform.position;
        Vector3 P2 = target.transform.position - offset;

        //���x�ݒ�
        float angle = 45f;
        float base_range = 5f;
        float max_angle = 50f;

        angle = angle * distance / base_range;
        if (angle > max_angle)
        {
            angle = max_angle;
        }


        float P1x = P2.x * ratio;
        //angle * Mathf.Deg2Rad �p�x���烉�W�A���֕ϊ�
        float P1y = Mathf.Sin(angle * Mathf.Deg2Rad) * Mathf.Abs(P1x) / Mathf.Cos(angle * Mathf.Deg2Rad)+P1_Y;
        Vector3 P1 = new Vector3(P1x, P1y, 0);

        Vector3 look = P1;
        //transform.rotation = Quaternion.FromToRotation(Vector3.up, look);
        float slerp_start_point = ratio * 0.5f;

        while (t <= 1 && target)
        {
            float Vx = 2 * (1f - t) * t * P1.x + Mathf.Pow(t, 2) * P2.x + offset.x;
            float Vy = 2 * (1f - t) * t * P1.y + Mathf.Pow(t, 2) * P2.y + offset.y;
            transform.position = new Vector3(Vx, Vy, 0);

            if (t > slerp_start_point)
            {
                look = target.transform.position - transform.position;
                Quaternion to = Quaternion.FromToRotation(Vector3.up, look);
               // transform.rotation = Quaternion.Slerp(transform.rotation, to, speed * 0.5f * Time.deltaTime);
            }

            t += 1 / distance / speed * Time.deltaTime;
            //���x�ω�
            if (speed < m_LastSpeed) 
            speed += 0.017f;
            yield return null;
        }

       // Destroy (this.gameObject);
    }
}

