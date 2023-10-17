/**
 * @file   YK_KnockBack.cs
 * @brief  �m�b�N�o�b�N����
 * @author �g�c����
 * @date   2023/10/15
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class YK_KnockBack : MonoBehaviour
{    
    [SerializeField] private Vector3 m_Target;
    private Vector3 m_TargetStorage;    //�^�[�Q�b�g�̍��W�ۑ��p
    [SerializeField] private float m_Speed, m_Ratio;
    [SerializeField] private float m_P1Y;    //�x�W�G�Ȑ��̓r���̑œ_Y�@
                                            //�����̐��l���グ��ƃm�b�N�o�b�N��Y�_��������
    [SerializeField] private float m_LastSpeed; //������яI�������̃X�s�[�h�}��
    [SerializeField] private bool m_Direction = true;
    /// <summary> �q�b�g�X�g�b�v����(�b) </summary>
    public float HitStopTime;

    void Start()
    {
        //�^�[�Q�b�g�̍��W�w��
        m_TargetStorage = m_Target;
        m_Target = m_Target + transform.position;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            OnStart();
        }
    }

    public void OnStart()
    {
        StartCoroutine(Throw());
    }

    IEnumerator Throw()
    {
        //�q�b�g�X�g�b�v
        var seq = DOTween.Sequence();
        seq.SetDelay(HitStopTime);

        float t = 0f;
        float distance = Vector3.Distance(transform.position, m_Target);

        Vector3 offset = transform.position;
        Vector3 P2;
        //���E�̐؂�ւ�
        if (m_Direction)
            P2 = m_Target - offset;    //�E�ɐ������
        else
            P2 = -m_Target + offset;    //���ɐ������

        //���x�ݒ�
        float angle = 45f;
        float base_range = 5f;
        float max_angle = 50f;

        angle = angle * distance / base_range;
        if (angle > max_angle)
        {
            angle = max_angle;
        }


        float P1x = P2.x * m_Ratio;
        //angle * Mathf.Deg2Rad �p�x���烉�W�A���֕ϊ�
        float P1y = Mathf.Sin(angle * Mathf.Deg2Rad) * Mathf.Abs(P1x) / Mathf.Cos(angle * Mathf.Deg2Rad)+m_P1Y;
        Vector3 P1 = new Vector3(P1x, P1y, 0);

        Vector3 look = P1;
        float slerp_start_point = m_Ratio * 0.5f;

        while (t <= 1)
        {
            float Vx = 2 * (1f - t) * t * P1.x + Mathf.Pow(t, 2) * P2.x + offset.x;
            float Vy = 2 * (1f - t) * t * P1.y + Mathf.Pow(t, 2) * P2.y + offset.y;
            transform.position = new Vector3(Vx, Vy, 0);

            if (t > slerp_start_point)
            {
               // look = target.transform.position - transform.position;
                Quaternion to = Quaternion.FromToRotation(Vector3.up, look);
            }

            t += 1 / distance / m_Speed * Time.deltaTime;
            //���x�ω�
            if (m_Speed < m_LastSpeed) 
            m_Speed += 0.017f;
            yield return null;
        }
        //�^�[�Q�b�g�̍��W�X�V
        m_Target = transform.position + m_TargetStorage;
    }
}

