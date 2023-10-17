/**
 * @file   YK_KnockBack.cs
 * @brief  ノックバック処理
 * @author 吉田叶聖
 * @date   2023/10/15
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class YK_KnockBack : MonoBehaviour
{    
    [SerializeField] private Vector3 m_Target;
    private Vector3 m_TargetStorage;    //ターゲットの座標保存用
    [SerializeField] private float m_Speed, m_Ratio;
    [SerializeField] private float m_P1Y;    //ベジエ曲線の途中の打点Y　
                                            //ここの数値を上げるとノックバックのY点があがる
    [SerializeField] private float m_LastSpeed; //吹っ飛び終わった後のスピード抑制
    [SerializeField] private bool m_Direction = true;
    /// <summary> ヒットストップ時間(秒) </summary>
    public float HitStopTime;

    void Start()
    {
        //ターゲットの座標指定
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
        //ヒットストップ
        var seq = DOTween.Sequence();
        seq.SetDelay(HitStopTime);

        float t = 0f;
        float distance = Vector3.Distance(transform.position, m_Target);

        Vector3 offset = transform.position;
        Vector3 P2;
        //左右の切り替え
        if (m_Direction)
            P2 = m_Target - offset;    //右に吹っ飛ぶ
        else
            P2 = -m_Target + offset;    //左に吹っ飛ぶ

        //高度設定
        float angle = 45f;
        float base_range = 5f;
        float max_angle = 50f;

        angle = angle * distance / base_range;
        if (angle > max_angle)
        {
            angle = max_angle;
        }


        float P1x = P2.x * m_Ratio;
        //angle * Mathf.Deg2Rad 角度からラジアンへ変換
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
            //速度変化
            if (m_Speed < m_LastSpeed) 
            m_Speed += 0.017f;
            yield return null;
        }
        //ターゲットの座標更新
        m_Target = transform.position + m_TargetStorage;
    }
}

