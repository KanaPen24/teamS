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
    
    // �f�o�b�N���[�h�J�n��
    public void StartHitDebug()
    {
        for(int i = 0; i < ON_HitManager.instance.GetHitCnt(); ++i)
        {
            hitDebugs.Add(new HitDebugDraw());

            // ID, ���W, �傫���ݒ�
            hitDebugs[i].hitID = ON_HitManager.instance.GetHit(i).GetHitID();
            hitDebugs[i].obj.transform.position = ON_HitManager.instance.GetHit(i).GetCenter();
            hitDebugs[i].obj.transform.localScale = ON_HitManager.instance.GetHit(i).GetSize();

            // �X�v���C�g�̐ݒ�
            var sprite = hitDebugs[i].obj.AddComponent<SpriteRenderer>();
            sprite.color = SetColor(ON_HitManager.instance.GetHit(i).GetHitType());
            sprite.sprite = image;
            sprite.sortingOrder = 1000;
        }
    }

    // �f�o�b�N���[�h�I����
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
        // �f�o�b�N���J�n���ꂽ���m�F
        if (hitDebugs.Count < 1) return;

        // hitID�̃I�u�W�F�N�g�����݂��邩�m�F�p
        Dictionary<int, bool> keys = new Dictionary<int, bool>();
        for(int i = 0; i < hitDebugs.Count; ++i)
        {
            keys[hitDebugs[i].hitID] = true;
        }

        for (int i = 0; i < ON_HitManager.instance.GetHitCnt(); ++i)
        {
            for (int j = 0; j < hitDebugs.Count; ++j)
            {
                // �����蔻��ID���g���A�Ή����������蔻��`��N���X�̍��W�ω�
                if(ON_HitManager.instance.GetHit(i).GetHitID() == hitDebugs[j].hitID)
                {
                    hitDebugs[j].obj.transform.position = ON_HitManager.instance.GetHit(i).GetCenter();
                    hitDebugs[j].obj.transform.localScale = ON_HitManager.instance.GetHit(i).GetSize();

                    // �����蔻�肪��Active�̏ꍇ�A�F��ω�
                    if (!ON_HitManager.instance.GetHit(i).GetActive())
                        hitDebugs[j].obj.GetComponent<SpriteRenderer>().color = new Color(0.1f, 0.1f, 0.1f, 0.5f);

                    // �g�p�ς�
                    keys[hitDebugs[j].hitID] = false;

                    continue;
                }
            }

            // �f�o�b�N���[�h�J�n�ȍ~�ɓ����蔻�肪�������ꂽ�ꍇ
            hitDebugs.Add(new HitDebugDraw());

            // ID, ���W, �傫���ݒ�
            hitDebugs[i].hitID = ON_HitManager.instance.GetHit(i).GetHitID();
            hitDebugs[i].obj.transform.position = ON_HitManager.instance.GetHit(i).GetCenter();
            hitDebugs[i].obj.transform.localScale = ON_HitManager.instance.GetHit(i).GetSize();

            // �X�v���C�g�̐ݒ�
            var sprite = hitDebugs[i].obj.AddComponent<SpriteRenderer>();
            sprite.color = SetColor(ON_HitManager.instance.GetHit(i).GetHitType());
            sprite.sprite = image;
            sprite.sortingOrder = 1000;
        }

        // �f�o�b�N���[�h�J�n�ȍ~�ɓ����蔻�肪�폜���ꂽ�ꍇ
        for (int i = 0; i < hitDebugs.Count; ++i)
        {
            if(keys[hitDebugs[i].hitID])
            {
                Object.Destroy(hitDebugs[i].obj);
                hitDebugs.RemoveAt(i);
            }
        }
    }

    // HitType���ɐF��ύX
    private Color SetColor(HitType type)
    {
        Color col = new Color(1.0f, 0.0f, 0.0f, 0.5f);
        switch (type)
        {
            case HitType.ATTACK:
                col = new Color(1.0f, 0.0f, 0.0f, 0.5f);
                break;
            case HitType.BODY:
                col = new Color(0.0f, 0.0f, 1.0f, 0.5f);
                break;
            case HitType.FIELD:
                col = new Color(0.0f, 1.0f, 0.0f, 0.5f);
                break;
        }
        return col;
    }

    // �f�o�b�N�\�����\���m�F�p
    public void DebugHit()
    {
        hitDebugs.Add(new HitDebugDraw());
        var sprite = hitDebugs[0].obj.AddComponent<SpriteRenderer>();
        sprite.color = new Color(0.5f, 0.5f, 0.8f, 0.3f);
        sprite.sprite = image;
    }
}
