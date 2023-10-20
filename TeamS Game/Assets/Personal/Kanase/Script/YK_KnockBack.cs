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
    private Vector3 m_fTargetStorage;    //�^�[�Q�b�g�̍��W�ۑ��p
    [SerializeField] private float m_fSpeed, m_fRatio;
    [SerializeField] private float m_fP1Y;    //�x�W�G�Ȑ��̓r���̑œ_Y�@
                                            //�����̐��l���グ��ƃm�b�N�o�b�N��Y�_��������
    [SerializeField] private float m_fLastSpeed; //������яI�������̃X�s�[�h�}��
    [SerializeField] private bool m_bDirection = true;   //�����t���O
    [SerializeField] private bool m_bAtkHit = false;        //�U�������蔻��t���O
    [SerializeField] private bool m_bGroundHit = false;     //�n�ʓ����蔻��t���O
    /// <summary> �q�b�g�X�g�b�v����(�b) </summary>
    [SerializeField] private float m_fHitStopTime;

    void Start()
    {
        //�^�[�Q�b�g�̍��W�w��
        m_fTargetStorage = m_Target;
        m_Target = m_Target + transform.position;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            m_bAtkHit = true;
            m_bGroundHit = false;
            OnStart();
        }
    }

    public void OnStart()
    {
        StartCoroutine(Throw());
    }

    IEnumerator Throw()
    {

        //�^�[�Q�b�g�̍��W�X�V
        Vector3 pos = transform.position;
        pos.y = m_Target.y;
        m_Target = transform.position + m_fTargetStorage;
        m_Target.y = pos.y;

        //�q�b�g�X�g�b�v
        var seq = DOTween.Sequence();
        seq.SetDelay(m_fHitStopTime);

        float t = 0f;
        float distance = Vector3.Distance(transform.position, m_Target);

        Vector3 offset = transform.position;
        Vector3 P2 = Vector3.zero;
        //���E�̐؂�ւ�
        if (m_bDirection)
            P2.x = m_Target.x - offset.x;    //�E�ɐ������
        else
            P2 = -m_Target + offset;    //���ɐ������
        Debug.Log(P2);
        //���Ԑ��Y���͒n�ʂɍ��킹��
        //P2.y = m_TargetStorage.y;

        //���x�ݒ�
        float angle = 45f;
        float base_range = 5f;
        float max_angle = 50f;

        angle = angle * distance / base_range;
        if (angle > max_angle)
        {
            angle = max_angle;
        }


        float P1x = P2.x * m_fRatio;
        //angle * Mathf.Deg2Rad �p�x���烉�W�A���֕ϊ�
        float P1y = Mathf.Sin(angle * Mathf.Deg2Rad) * Mathf.Abs(P1x) / Mathf.Cos(angle * Mathf.Deg2Rad)+m_fP1Y;
        Vector3 P1 = new Vector3(P1x, P1y, 0);

        Vector3 look = P1;
        float slerp_start_point = m_fRatio * 0.5f;
        m_bAtkHit=false;
        while (!m_bAtkHit && !m_bGroundHit) 
        {
            float Vx = 2 * (1f - t) * t * P1.x + Mathf.Pow(t, 2) * P2.x + offset.x;
            float Vy;
               Vy = 2 * (1f - t) * t * P1.y + Mathf.Pow(t, 2) * P2.y + offset.y;
            transform.position = new Vector3(Vx, Vy, 0);

            if (t > slerp_start_point)
            {
               // look = target.transform.position - transform.position;
                Quaternion to = Quaternion.FromToRotation(Vector3.up, look);
            }
            if (Input.GetKeyDown(KeyCode.N))
            {
                m_bAtkHit = true;               
            }
            if(transform.position.y<0.0f)
            {
                m_bGroundHit = true;
                //�n�ʂ̍���(Y)�ɍ��킹��
                Vector3 pos2 = transform.position;
                pos2.y = m_fTargetStorage.y;
                transform.position = pos2;
            }
            t += 1 / distance / m_fSpeed * Time.deltaTime;
            //���x�ω�
            if (m_fSpeed < m_fLastSpeed) 
            m_fSpeed += 0.017f;
            yield return null;
        }

        
    }
}

