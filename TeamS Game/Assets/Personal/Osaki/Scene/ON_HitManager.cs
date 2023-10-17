/**
@file   ON_HitManager.cs
@brief  �����蔻��}�l�[�W���[
@author Noriaki Osaki
@date   2023/10/13

�����蔻������X�g���A
�����蔻��̒ǉ��A�폜
�����蔻��̌v�Z
�V���O���g�����g�p
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �Փˎ��f�[�^
public class HitData
{
    public int myID;       // ������ID
    public int otherID;    // �����ID
    public HitState state; // �Փˎ����
    public HitData(int myid, int otherid, HitState hit)
    {
        myID = myid; otherID = otherid; state = hit;
    }
}

// �Փˎ����
public enum HitState
{
    NONE = 0,

    ATTACK,     // �U������
    DEFENCE,    // �U�������
    GOURND,     // �t�B�[���h�ɓ�������
    BODYS,       // �̓��m���ڐG
    BALANCE,    // �U�����m��������


    MAX_STATE
}


public class ON_HitManager
{
    public static ON_HitManager instance;

    private List<ON_HitBase> m_hits = new List<ON_HitBase>();   // �����蔻��̃��X�g
    private int hitCnt = 0;     // ���������g�ݍ��킹��
    private List<HitData> m_hitDatas = new List<HitData>();     // �Փ˃f�[�^���X�g
    
    public void Init()
    {
        if(instance == null)
        {
            instance = this;
            m_hits.Clear();
        }
        else
        {
            instance = null;
        }
    }

    public void Update()
    {
        // ���X�g�̏�����
        m_hitDatas.Clear();
        hitCnt = 0;

        // �����蔻��̌v�Z
        for(int i = 0; i < m_hits.Count; ++i)
        {
            for(int j = i; j < m_hits.Count; ++j)
            {
                // ���E
                if (m_hits[i].GetCenter().x + m_hits[i].GetSize().x <= m_hits[j].GetCenter().x - m_hits[j].GetSize().x) continue;
                if (m_hits[i].GetCenter().x - m_hits[i].GetSize().x >= m_hits[j].GetCenter().x + m_hits[j].GetSize().x) continue;

                // �㉺
                if (m_hits[i].GetCenter().y + m_hits[i].GetSize().y <= m_hits[j].GetCenter().y - m_hits[j].GetSize().y) continue;
                if (m_hits[i].GetCenter().y - m_hits[i].GetSize().y >= m_hits[j].GetCenter().y + m_hits[j].GetSize().y) continue;

                // ����I�u�W�F�N�g��
                if (m_hits[i].GetObjID() == m_hits[j].GetObjID()) continue;

                // �������Ă���
                // �����蔻��̏�Ԕ��肵�A�ǉ�
                m_hitDatas.Add(new HitData(m_hits[i].GetObjID(), m_hits[j].GetObjID(), DecideState(i, j)));
            }
        }

        hitCnt = m_hitDatas.Count;
    }

    // �����蔻��̏�ԓ���
    private HitState DecideState(int i, int j)
    {
        HitState state = HitState.NONE;
        if(m_hits[i].GetHitType() == HitType.FIELD || m_hits[j].GetHitType() == HitType.FIELD)
        {
            state = HitState.GOURND;
            return state;
        }

        switch(m_hits[i].GetHitType())
        {
            case HitType.ATTACK:
                if (m_hits[j].GetHitType() == HitType.ATTACK) state = HitState.BALANCE;
                if (m_hits[j].GetHitType() == HitType.BODY) state = HitState.ATTACK;
                break;
            case HitType.BODY:
                if (m_hits[j].GetHitType() == HitType.ATTACK) state = HitState.DEFENCE;
                if (m_hits[j].GetHitType() == HitType.BODY) state = HitState.BODYS;
                break;
        }

        return state;
    }

    // �����蔻��̐���
    public int GenerateHit(Vector2 center, Vector2 size, bool active, HitType type, int ID)
    {
        m_hits.Add(new ON_HitBase(center, size, active, type, ID));

        if(m_hits.Count > 0)
        {
            m_hits[m_hits.Count - 1].SetHitID(m_hits[m_hits.Count - 2].GetHitID() + 1);
        }
        return m_hits[m_hits.Count - 1].GetHitID();
    }

    // �����蔻��̍폜( �����F�����蔻��ID
    public int DeleteHit(int id)
    {
        for(int i = 0; i < m_hits.Count; ++i)
        {
            if(m_hits[i].GetHitID() == id)
            {
                m_hits.RemoveAt(i);
                break;
            }
        }
        return -1;
    }

    // �����蔻��̍폜( �����F�I�u�W�F�N�gID
    public int DeleteHits(int id)
    {
        for (int i = 0; i < m_hits.Count; ++i)
        {
            if (m_hits[i].GetObjID() == id)
            {
                m_hits.RemoveAt(i);
            }
        }
        return -1;
    }

    // �����蔻��̑g�ݍ��킹��
    public int GetHitCnt()
    {
        return hitCnt;
    }

    // ���Ă���ID�擾( �����F�g�ݍ��킹�ԍ�
    public int GetMyID(int num)
    {
        return m_hitDatas[num].myID;
    }

    // ���Ă�ꂽ��ID�擾( �����F�g�ݍ��킹�ԍ�
    public int GetOtherID(int num)
    {
        return m_hitDatas[num].otherID;
    }

    // �����蔻��̏�Ԏ擾( �����F�g�ݍ��킹�ԍ�
    public HitState GetState(int num)
    {
        return m_hitDatas[num].state;
    }

    // �Փ˔���f�[�^�擾( �����F�g�ݍ��킹�ԍ�
    public HitData GetData(int num)
    {
        return m_hitDatas[num];
    }

    // �����蔻��̈ړ�( �����F�����蔻��ID, �ړ���
    public void MoveHit(int id, Vector2 vec)
    {
        // �w�肳�ꂽ����̓����蔻��݈̂ړ�
        for(int i = 0; i < m_hits.Count; ++i)
        {
            if(m_hits[i].GetHitID() == id)
            {
                m_hits[i].MoveHit(vec);
                break;
            }
        }
    }

    // �����蔻��̈ړ�( ����:�I�u�W�F�N�gID, �ړ���
    public void MoveHits(int objID, Vector2 vec)
    {
        // �w�肳�ꂽ�I�u�W�F�N�g�̓����蔻������ׂĈړ�
        for(int i = 0; i < m_hits.Count; ++i)
        {
            if(m_hits[i].GetObjID() == objID)
            {
                m_hits[i].MoveHit(vec);
            }
        }
    }

    // �����蔻��̒��S���W�ύX( �����F�����蔻��ID
    public void SetCenter(int id, Vector2 pos)
    {
        for (int i = 0; i < m_hits.Count; ++i)
        {
            if (m_hits[i].GetHitID() == id)
            {
                m_hits[i].SetCenter(pos);
                break;
            }
        }
    }

    // �����蔻���On/Off ( �����F�����蔻��ID, flg
    public void SetActive(int id, bool flg)
    {
        // ����̓����蔻���On/Off
        for (int i = 0; i < m_hits.Count; ++i)
        {
            if (m_hits[i].GetHitID() == id)
            {
                m_hits[i].SetActive(flg);
                break;
            }
        }
    }

    // �����蔻���On/Off ( ����:�I�u�W�F�N�gID, flg
    public void SetActives(int objID, bool flg)
    {
        // �w�肳�ꂽ�I�u�W�F�N�g�̓����蔻���On/Off
        for (int i = 0; i < m_hits.Count; ++i)
        {
            if (m_hits[i].GetObjID() == objID)
            {
                m_hits[i].SetActive(flg);
            }
        }
    }

    // �����蔻��̑傫���ύX( ����:�����蔻��id, �傫��
    public void SetSize(int id, Vector2 size)
    {
        for (int i = 0; i < m_hits.Count; ++i)
        {
            if (m_hits[i].GetHitID() == id)
            {
                m_hits[i].SetSize(size);
                break;
            }
        }
    }
}
