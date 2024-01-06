/**
 * @file   HitStop.cs
 * @brief  �q�b�g�X�g�b�v
 * @author IharaShota
 * @date   2023/12/05
 * @Update 2023/12/05 �쐬
 **/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ===============================================
// HitStopState
// �c HitStop�̏�Ԃ��Ǘ�����񋓑�
// ===============================================
public enum HitStopState
{
    None,
    ON,
}

public class HitStop : MonoBehaviour
{
    public static HitStop instance;
    public HitStopState hitStopState;
    private float m_time;
    // Start is called before the first frame update
    void Start()
    {
        // --- ������ ---
        if (instance == null)
        {
            instance = this;
        }
        else Destroy(this.gameObject);
    }


    private void FixedUpdate()
    {
        if (hitStopState == HitStopState.ON)
        {
            m_time -= Time.deltaTime;
            if(m_time <= 0f)
            {
                m_time = 0;
                hitStopState = HitStopState.None;
            }
        }
    }

    // �q�b�g�X�g�b�v���J�n����
    public void StartHitStop(float time)
    {
        if(hitStopState == HitStopState.None)
        {
            hitStopState = HitStopState.ON;
            m_time = time;
        }
    }
}
