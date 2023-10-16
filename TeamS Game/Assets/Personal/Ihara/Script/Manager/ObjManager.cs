using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjManager : MonoBehaviour
{
    public List<ObjBase> Objs;
    private void FixedUpdate()
    {
        // --- �I�u�W�F�N�g�̍X�V���� ---
        for(int i = 0; i < Objs.Count; ++i)
        {
            Objs[i].UpdateObj();
        }
        // ------------------------------


        // --- �����蔻�菈�� ---
        // �����ɋL�q�\��
        //ON_HitManager.instance.Update();

        // ----------------------
        

        // --- �ŏI�I�ȏ����������ōs�� ---
        //

        // --------------------------------

        // --- �f�o�b�O�\�� ---
        if(GameManager.IsDebug())
        {
            for (int i = 0; i < Objs.Count; ++i)
            {
                Objs[i].UpdateDebug();
            }
        }
        // ---------------------

    }
}
