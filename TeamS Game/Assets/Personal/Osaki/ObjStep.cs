using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjStep : ObjBase
{

    //  ---- �����蔻��𐶐������ۂ̏��� ----
    public override void GenerateHit()
    {
        // �������ɓ����蔻��ID���擾���A�����l��ݒ肵�Ă���(���S���W,�傫��,�����蔻��̎��,obj��ID)
        m_nHitID = ON_HitManager.instance.GenerateHit(this.gameObject.transform.position,
        this.gameObject.transform.localScale / 2,
        true, HitType.STEP, GetSetObjID);

        Debug.Log("�����蔻�萶�� HitID:" + GetSetHitID);
    }
}
