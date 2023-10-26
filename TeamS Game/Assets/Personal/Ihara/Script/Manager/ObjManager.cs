/**
 * @file   ObjManager.cs
 * @brief  �I�u�W�F�N�g�Ǘ��N���X
 * @author IharaShota
 * @date   2023/10/13
 * @Update 2023/10/13 �쐬
 * @Update 2023/10/19 �����蔻��擾����
 * @Update 2023/10/24 �U������擾����(��)
 * @Update 2023/10/26 �n�ʔ���I��
 **/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjManager : MonoBehaviour
{
    [SerializeField] private List<ObjBase> Objs; // �I�u�W�F�N�g���i�[����z��

    private void Start()
    {
        // �����蔻��Ǘ��N���X��null��������c
        if(ON_HitManager.instance == null)
        {
            // �C���X�^���X���쐬���ď���������
            ON_HitManager.instance = new ON_HitManager();
            ON_HitManager.instance.Init();
        }

        // �i�[����Ă���I�u�W�F�N�g�̐�������
        for (int i = 0; i < Objs.Count; ++i)
        {
            // �I�u�W�F�N�gID��ݒ� �� �����蔻�萶��
            // �� �I�u�W�F�N�g�̏����� �� �I�u�W�F�N�g�̎Q�ƃp�����[�^�[���X�V
            Objs[i].GetSetObjID = i;
            Objs[i].GenerateHit();
            Objs[i].InitObj();
            Objs[i].UpdateCheckParam();
        }

    }

    private void FixedUpdate()
    {
        if (GameManager.m_sGameState != GameState.GamePlay)
            return;

        // --- �I�u�W�F�N�g�̍X�V���� ---
        for(int i = 0; i < Objs.Count; ++i)
        {
            // �X�V���� �� �n�ʔ��� �� �ړ��ʂɑ��x���i�[ ��
            // ���x���� �� �Q�ƃp�����[�^�[���X�V �� ���W�X�V
            Objs[i].UpdateObj();
            Objs[i].CheckObjGround();
            SaveObjSpeed(i);
            Objs[i].GetSetMove = Objs[i].GetSetSpeed;
            Objs[i].GetSetPos += new Vector3(Objs[i].GetSetMove.x, Objs[i].GetSetMove.y, 0f);
            Objs[i].UpdateCheckParam();

            // �I�u�W�F�N�g���ړ��������W�ɓ����蔻����ړ�������
            ON_HitManager.instance.SetCenter(Objs[i].GetSetHitID, Objs[i].GetSetPos);
        }

        // --- �����蔻�菈�� ---
        CollisionUpdate();
    }

    // --- ���x�������� ---
    // ���ő�E�ŏ����x�𒴂��Ă�����A�ő�E�ŏ����x�ɒ�������
    private void SaveObjSpeed(int i)
    {
        if (Objs[i].GetSetSpeed.x > Objs[i].GetSetMaxSpeed.x)
        {
            Objs[i].GetSetSpeed = new Vector2(Objs[i].GetSetMaxSpeed.x, Objs[i].GetSetSpeed.y);
        }
        if (Objs[i].GetSetSpeed.x < -Objs[i].GetSetMaxSpeed.x)
        {
            Objs[i].GetSetSpeed = new Vector2(-Objs[i].GetSetMaxSpeed.x, Objs[i].GetSetSpeed.y);
        }
        if (Objs[i].GetSetSpeed.y > Objs[i].GetSetMaxSpeed.y)
        {
            Objs[i].GetSetSpeed = new Vector2(Objs[i].GetSetSpeed.x, Objs[i].GetSetMaxSpeed.y);
        }
        if (Objs[i].GetSetSpeed.y < -Objs[i].GetSetMaxSpeed.y)
        {
            Objs[i].GetSetSpeed = new Vector2(Objs[i].GetSetSpeed.x, -Objs[i].GetSetMaxSpeed.y);
        }
    }

    // --- �����蔻�蔽�f�֐� ---
    private void CollisionUpdate()
    {
        // �����蔻��X�V(�Փˎ��f�[�^�����X�g�Ɋi�[���Ă���)
        ON_HitManager.instance.UpdateHit();

        // --- �Փˎ��f�[�^������ ---
        for (int i = 0; i < ON_HitManager.instance.GetHitCombiCnt(); ++i)
        {
            int myID = -1;
            int otherID = -1;

            // --- ���g�Ƒ���̃I�u�W�F�N�g�𓖂��蔻��ID�Ō��� ---
            for (int j = 0; j < Objs.Count; ++j)
            {
                // ���g��ID������
                if (Objs[j].GetSetHitID == ON_HitManager.instance.GetData(i).myID)
                {
                    myID = j;
                }
                // �����ID������
                if (Objs[j].GetSetHitID == ON_HitManager.instance.GetData(i).otherID)
                {
                    otherID = j;
                }

                // �����̌������I����Ă�����I������
                if (myID != -1 && otherID != -1)
                {
                    // ���g��ID�������ID�̑傫�����傫��������c
                    // ID�����ւ���
                    if (myID > otherID)
                        (myID, otherID) = (otherID, myID);

                    break;
                }
            }
            // -----------------------------------------------------

            // �ǂ��炩��ID������U���Ă��Ȃ����̂�������X�L�b�v����
            if (myID == -1 || otherID == -1) continue;

            // �m�F�p
            if (GameManager.IsDebug())
                Debug.Log("����: " + ON_HitManager.instance.GetData(i).state +
                " �Փ˕���: " + ON_HitManager.instance.GetData(i).dir +
                " ���g: " + myID +
                " ����: " + otherID);

            // --- ����ɂ���ăQ�[���ɔ��f ---
            // �n�ʐڐG�̔��肾������
            if (ON_HitManager.instance.GetData(i).state == HitState.GRUOND)
            {
                // �E�ɓ������Ă�����
                if (ON_HitManager.instance.GetData(i).dir == HitDir.RIGHT)
                {
                    // ���W����
                    Objs[myID].GetSetPos = new Vector3(Objs[otherID].GetSetPos.x, Objs[myID].GetSetPos.y, 0f) -
                                           new Vector3(Objs[otherID].GetSetScale.x / 2f +
                                                       Objs[myID].GetSetScale.x / 2f, 0f, 0f);

                    // ���x��0�ɂ���
                    Objs[myID].GetSetSpeed = new Vector3(0f, Objs[myID].GetSetSpeed.y, 0f);
                }
                // ���ɓ������Ă�����
                if (ON_HitManager.instance.GetData(i).dir == HitDir.LEFT)
                {
                    // ���W����
                    Objs[myID].GetSetPos = new Vector3(Objs[otherID].GetSetPos.x, Objs[myID].GetSetPos.y, 0f) +
                                           new Vector3(Objs[otherID].GetSetScale.x / 2f +
                                                       Objs[myID].GetSetScale.x / 2f, 0f, 0f);

                    // ���x��0�ɂ���
                    Objs[myID].GetSetSpeed = new Vector3(0f, Objs[myID].GetSetSpeed.y, 0f);
                }
                // ��ɓ������Ă�����
                if (ON_HitManager.instance.GetData(i).dir == HitDir.UP)
                {
                    // ���W����
                    Objs[myID].GetSetPos = new Vector3(Objs[myID].GetSetPos.x, Objs[otherID].GetSetPos.y, 0f) -
                                           new Vector3(0f, Objs[otherID].GetSetScale.y / 2f +
                                                       Objs[myID].GetSetScale.y / 2f, 0f);

                    // ���x��0�ɂ���
                    Objs[myID].GetSetSpeed = new Vector3(Objs[myID].GetSetSpeed.x, 0f, 0f);
                }
                // ���ɓ������Ă�����
                if (ON_HitManager.instance.GetData(i).dir == HitDir.DOWN)
                {
                    // ���W����
                    Objs[myID].GetSetPos = new Vector3(Objs[myID].GetSetPos.x, Objs[otherID].GetSetPos.y, 0f) +
                                           new Vector3(0f, Objs[otherID].GetSetScale.y / 2f +
                                                       Objs[myID].GetSetScale.y / 2f, 0f);

                    // ���x��0�ɂ���
                    Objs[myID].GetSetSpeed = new Vector3(Objs[myID].GetSetSpeed.x, 0f, 0f);

                    // �n�ʂ̏����i�[
                    Objs[myID].GetSetGround.GetSetStand = true;
                    Objs[myID].GetSetGround.GetSetCenter = Objs[otherID].GetSetPos;
                    Objs[myID].GetSetGround.GetSetSize = Objs[otherID].GetSetScale;

                    if(GameManager.IsDebug())
                        Debug.Log("�n�ʂɓ�������");
                }
            }

            //�U���̔��肾������
            if (ON_HitManager.instance.GetData(i).state == HitState.DEFENCE)
            {
                Objs[otherID].DamageAttack();
            }
            // -------------------------------
        }
    }
}
