using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjManager : MonoBehaviour
{
    public List<ObjBase> Objs;

    private void Start()
    {
        if(ON_HitManager.instance == null)
        {
            ON_HitManager.instance = new ON_HitManager();
            ON_HitManager.instance.Init();
        }

        for (int i = 0; i < Objs.Count; ++i)
        {
            Objs[i].GetSetObjID = i;
            Objs[i].GenerateHit();
        }

    }

    private void FixedUpdate()
    {
        // --- �I�u�W�F�N�g�̍X�V���� ---
        for(int i = 0; i < Objs.Count; ++i)
        {
            Objs[i].UpdateObj();

            // �I�u�W�F�N�g���ړ��������W�ɓ����蔻����ړ�������
            ON_HitManager.instance.SetCenter(Objs[i].GetSetHitID, Objs[i].transform.position);
            //ON_HitManager.instance.DebugHitID(Objs[i].GetSetHitID);
        }
        // ------------------------------


        // --- �����蔻�菈�� ---
        // �����ɋL�q�\��
        ON_HitManager.instance.UpdateHit();
        for(int i = 0; i < ON_HitManager.instance.GetHitCnt(); ++i)
        {
            if(ON_HitManager.instance.GetData(i).state == HitState.GRUOND)
            {
                // ����𒲂ׂ�
                Debug.Log("����������");
            }
        }

        // ----------------------
        

        // --- �ŏI�I�ȏ����������ōs�� ---
        //

        // --------------------------------

        // --- �f�o�b�O�\�� ---
        if(GameManager.IsDebug())
        {
            for (int i = 0; i < Objs.Count; ++i)
            {
                //Objs[i].UpdateDebug();
            }
        }
        // ---------------------

    }
}
