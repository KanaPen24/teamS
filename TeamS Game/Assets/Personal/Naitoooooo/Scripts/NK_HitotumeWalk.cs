using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NK_HitotumeWalk : NK_HitotumeStrategy
{
    [SerializeField] private HitotumeProto m_Hitotume;
    [SerializeField] private float t;
    [SerializeField] private float s;
    //�����p�ϐ�
    [SerializeField] private float acceleration;
    //���x�{��
    [SerializeField] private float magnification;
    public override void UpdateStrategy()
    {

        if (s < 90)
        {
            s += acceleration;
        }
        t = s * Mathf.Deg2Rad;
        if (ObjPlayer.instance.GetSetPos.x > m_Hitotume.GetSetPos.x)//������Ńv���C���[�̍��W�킩��
        {
            m_Hitotume.GetSetSpeed = new Vector2(Mathf.Sin(t) * magnification, 0.0f);
            Debug.Log("a");
        }
        else
        {
            m_Hitotume.GetSetSpeed = new Vector2(-Mathf.Sin(t) * 0.3f, 0.0f);
            Debug.Log("b");
        }
            //m_Hitotume.GetSetSpeed = new Vector2(5.0f,0.0f);
    }
}
