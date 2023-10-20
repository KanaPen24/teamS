using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
@file   ON_HitDebug.cs
@brief  �����蔻��f�o�b�N�\��
@author Noriaki Osaki
@date   2023/10/20

�����蔻��f�[�^���󂯎��A�w�肳�ꂽ��`��\������
*/

// �����蔻��`��p�N���X
public class HitDebugDraw
{
    public int hitID;
    public GameObject obj;
}


public class ON_HitDebug
{
    private List<HitDebugDraw> hitDebugs = new List<HitDebugDraw>();

    public static ON_HitDebug instance = null;

    private Sprite image = null;

    public ON_HitDebug()
    {
        image = (Sprite)Resources.Load("Square", typeof(Sprite));
        hitDebugs.Clear();
    }

    // Start is called before the first frame update
    public void StartHitDebug()
    {
        
    }

    public void FinHitDebug()
    {
        for(int i = 0; i < hitDebugs.Count; ++i)
        {
            Object.Destroy(hitDebugs[i].obj);
        }

        hitDebugs.Clear();
    }

    void Update()
    {
        // �f�o�b�N���[�h�J�n�ȍ~�ɐ������ꂽ�����蔻���`��N���X�ɒǉ�

        // �����蔻��ID���g���A�Ή����������蔻��`��N���X�̍��W�ω�

        // �����蔻�肪��Active�̏ꍇ�A�F��ω�

    }

    public void DebugHit()
    {
        hitDebugs.Add(new HitDebugDraw());
        var sprite = hitDebugs[0].obj.AddComponent<SpriteRenderer>();
        sprite.color = new Color(0.5f, 0.5f, 0.8f, 0.3f);
        sprite.sprite = image;
    }
}
