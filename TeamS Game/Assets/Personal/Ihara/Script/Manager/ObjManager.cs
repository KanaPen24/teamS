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
using Cinemachine;

public class ObjManager : MonoBehaviour
{
    [SerializeField] private List<ObjBase> Objs; // �I�u�W�F�N�g���i�[����z��
    [SerializeField] private CinemachineImpulseSource cinema;
    public static ObjManager instance;
    public ParticleSystem hitEffect;
    public ObjEnemyUnion enemyUnionPrefab;

    private int myID;    // ���g�̃I�u�W�F�N�gID
    private int otherID; // ����̃I�u�W�F�N�gID
    private int maxID;   // ���݊���U���Ă���I�u�W�F�N�gID�̍ő吔

    private void Start()
    {
        // --- ������ ---
        if(instance == null)
        {
            instance = this;
        }
        // �����蔻��Ǘ��N���X��null��������c
        if(ON_HitManager.instance == null)
        {
            // �C���X�^���X���쐬���ď���������
            ON_HitManager.instance = new ON_HitManager();
            ON_HitManager.instance.Init();
        }

        // ���ꂼ���ID��������
        myID = otherID = maxID = 0;

        // �i�[����Ă���I�u�W�F�N�g�̐�������
        for (int i = 0; i < Objs.Count; ++i)
        {
            // �I�u�W�F�N�gID��ݒ� �� �����蔻�萶��
            // �� �I�u�W�F�N�g�̏����� �� �I�u�W�F�N�g�̎Q�ƃp�����[�^�[���X�V
            Objs[i].GetSetObjID = SetObjID();
            Objs[i].GenerateHit();
            Objs[i].InitObj();
            Objs[i].UpdateCheckParam();
        }

    }

    private void FixedUpdate()
    {
        if (GameManager.m_sGameState != GameState.GamePlay || 
            HitStop.instance.hitStopState == HitStopState.ON)
            return;

        // --- �I�u�W�F�N�g�̍X�V���� ---
        for(int i = 0; i < Objs.Count; ++i)
        {
            // --- �I�u�W�F�N�g�����݂��Ă���ꍇ ---
            if (Objs[i].GetSetExist)
            {
                // �I�u�W�F�N�g�̕\��
                if (Objs[i].texObj != null)
                {
                    if(Objs[i].GetComponent<ObjEnemyBase>() != null)
                    {
                        if(!Objs[i].GetComponent<ObjEnemyBase>().m_division.m_bDivisionTrigger)
                            Objs[i].texObj.enabled = true;
                    }
                    else Objs[i].texObj.enabled = true;
                }

                // �X�V����  �� �n�ʔ��� �� �������� �� �ړ��ʂɑ��x���i�[ �� ���x���� ��
                // ���G���ԍX�V �� ���W�X�V
                Objs[i].UpdateObj();
                Objs[i].CheckObjGround();
                SaveObjSpeed(i);
                FixObjDir(i);
                UpdateInvincible(i);
                Objs[i].GetSetMove = Objs[i].GetSetSpeed;
                Objs[i].GetSetPos += new Vector3(Objs[i].GetSetMove.x, Objs[i].GetSetMove.y, 0f);

                // �q�b�g�X�g�b�v�X�V
                Objs[i].UpdateHitStop();

                if (!Objs[i].m_HitStopParam.m_bHitStop)
                {
                    // �I�u�W�F�N�g���ړ��������W�ɓ����蔻����ړ�������
                    ON_HitManager.instance.SetCenter(Objs[i].GetSetHitID, Objs[i].GetSetPos);
                }
            }
            // --- ���݂��Ă��Ȃ��ꍇ ---
            else
            {
                // �I�u�W�F�N�g�̔�\��
                if (Objs[i].texObj != null)
                    Objs[i].texObj.enabled = false;
            }         

            // �I�u�W�F�N�g�̃p�����[�^�[�X�V
            Objs[i].UpdateCheckParam();

            // �q�b�g��������
            Objs[i].GetSetHit = false;
        }

        // --- �����蔻�菈�� ---
        CollisionUpdate();

        // --- �I�u�W�F�N�g�̍폜 ---
        // ��m_bDestroy��true�ɂȂ��Ă���ꍇ
        // �Ō�����璲�ׂȂ��ƃo�O��
        int ObjMaxNum = Objs.Count - 1;
        for(int i = ObjMaxNum; i > 0; --i)
        {
            if(Objs[i].GetSetDestroy)
            {
                Objs[i].DestroyObj();
                Objs.RemoveAt(i);
            }
        }

        // --- �I�u�W�F�N�g�̍U�����菈�� ---
        for (int i = 0; i < Objs.Count; ++i)
        {
            // --- �I�u�W�F�N�g���q�b�g���Ă���ꍇ ---
            if (Objs[i].GetSetHit)
            {
                // ���G���Ԃ�ݒ�
                Objs[i].GetSetInvincible.SetInvincible(1f);

                // �q�b�g�X�g�b�v����
                Objs[i].m_HitStopParam.SetHitStop(0.4f);

                // �G�������ꍇ�A�m�b�N�o�b�N�̃J�E���g�_�E�����n�߂�
                if(Objs[i].GetComponent<ObjEnemyBase>() != null)
                {
                    Objs[i].GetComponent<ObjEnemyBase>().GetSetKnockBack.m_fStartTime = 0.5f;
                }
            }

            // �m�b�N�o�b�N�����Ǘ�(�]��ɉ����̂ŏC���K�{)
            if (Objs[i].GetComponent<ObjEnemyBase>() != null)
            {
                if (Objs[i].GetComponent<ObjEnemyBase>().GetSetKnockBack.m_fStartTime > 0)
                {
                    Objs[i].GetComponent<ObjEnemyBase>().GetSetKnockBack.m_fStartTime -= Time.deltaTime;
                    if (Objs[i].GetComponent<ObjEnemyBase>().GetSetKnockBack.m_fStartTime <= 0)
                    {
                        Objs[i].KnockBackObj(Objs[0].GetSetDir);
                        Objs[i].GetComponent<ObjEnemyBase>().GetSetKnockBack.m_fStartTime = 0;
                    }
                }
            }
        }
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
        if (Objs[i].GetSetSpeed.y < -Objs[i].GetSetMaxSpeed.y)
        {
            Objs[i].GetSetSpeed = new Vector2(Objs[i].GetSetSpeed.x, -Objs[i].GetSetMaxSpeed.y);
        }
    }

    // --- ���������֐� ---
    // ��X���x���v���X���ƁuRight�v,�}�C�i�X���ƁuLeft�v
    //   Field�^�C�v�̃I�u�W�F�N�g�́uNone�v
    private void FixObjDir(int i)
    {
        if(Objs[i].GetSetType == ObjType.Field)
        {
            Objs[i].GetSetDir = ObjDir.NONE;
            return;
        }

        if (Objs[i].GetSetSpeed.x > 0f)
            Objs[i].GetSetDir = ObjDir.RIGHT;
        else if(Objs[i].GetSetSpeed.x < 0f)
            Objs[i].GetSetDir = ObjDir.LEFT;
    }

    // --- ���G���ԍX�V ---
    private void UpdateInvincible(int i)
    {
        if (Objs[i].GetSetInvincible.m_fTime <= 0f)
        {
            Objs[i].GetSetInvincible.m_bInvincible = false;
            Objs[i].GetSetInvincible.m_fTime = 0f;
        }
        else Objs[i].GetSetInvincible.m_fTime -= Time.deltaTime;
    }

    // --- �����蔻�蔽�f�֐� ---
    private void CollisionUpdate()
    {
        // �����蔻��X�V(�Փˎ��f�[�^�����X�g�Ɋi�[���Ă���)
        ON_HitManager.instance.UpdateHit();

        // --- �Փˎ��f�[�^������ ---
        for (int i = 0; i < ON_HitManager.instance.GetHitCombiCnt(); ++i)
        {
            myID = -1;
            otherID = -1;

            // --- ���g�Ƒ���̃I�u�W�F�N�g�𓖂��蔻��ID�Ō��� ---
            for (int j = 0; j < Objs.Count; ++j)
            {
                // ���g��ID������
                if (Objs[j].GetSetObjID == ON_HitManager.instance.GetData(i).myID)
                {
                    myID = j;
                }
                // �����ID������
                if (Objs[j].GetSetObjID == ON_HitManager.instance.GetData(i).otherID)
                {
                    otherID = j;
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
            // �U���𓖂Ă����肾������
            if (ON_HitManager.instance.GetData(i).state == HitState.ATTACK)
            {
                if (!Objs[otherID].GetSetInvincible.m_bInvincible)
                {
                    //Player�̏ꍇ��
                    if (Objs[myID].GetComponent<ObjPlayer>() != null)
                    {
                        // �R���{���Z
                        YK_Combo.AddCombo();

                        // �q�b�g�G�t�F�N�g�Đ�
                        hitEffect.Play();
                        hitEffect.transform.position = Objs[otherID].GetSetPos;

                        // �q�b�g�X�g�b�v��������
                        Objs[myID].GetSetHitStopParam.SetHitStop(0.5f);
                    }
                }
            }
            // �U�����󂯂����肾������
            if (ON_HitManager.instance.GetData(i).state == HitState.DEFENCE)
            {
                // ���g�����G�łȂ���΁c
                if (!Objs[myID].GetSetInvincible.m_bInvincible)
                {
                    // ���g���v���C���[��������
                    if (Objs[myID].GetComponent<ObjPlayer>() != null)
                    {
                        // �U�����󂯂�
                        Objs[myID].DamageAttack();
                        Objs[myID].GetSetHit = true;
                    }
                    // ���g���G��������
                    if (Objs[myID].GetComponent<ObjEnemyBase>() != null)
                    {
                        // �m�b�N�o�b�N����
                        //Objs[myID].KnockBackObj(Objs[otherID].GetSetDir);
                        Objs[myID].GetSetHit = true;
                    }

                }
            }
            // �̓��m�̐ڐG���肾������
            if (ON_HitManager.instance.GetData(i).state == HitState.BODYS)
            {
                //// �E�ɓ������Ă�����
                //if (ON_HitManager.instance.GetData(i).dir == HitDir.RIGHT)
                //{
                //    // ���W����
                //    Objs[myID].GetSetPos = new Vector3(Objs[otherID].GetSetPos.x, Objs[myID].GetSetPos.y, 0f) -
                //                           new Vector3(Objs[otherID].GetSetScale.x / 2f +
                //                                       Objs[myID].GetSetScale.x / 2f, 0f, 0f);

                //    // ���x��0�ɂ���
                //    Objs[myID].GetSetSpeed = new Vector3(0f, Objs[myID].GetSetSpeed.y, 0f);

                //    if(Objs[myID].GetComponent<ObjEnemyBase>() != null)
                //    {
                //        Objs[myID].GetComponent<ObjEnemyBase>().GetSetEnemyState = EnemyState.Idle;
                //    }
                //}
                //// ���ɓ������Ă�����
                //if (ON_HitManager.instance.GetData(i).dir == HitDir.LEFT)
                //{
                //    // ���W����
                //    Objs[myID].GetSetPos = new Vector3(Objs[otherID].GetSetPos.x, Objs[myID].GetSetPos.y, 0f) +
                //                           new Vector3(Objs[otherID].GetSetScale.x / 2f +
                //                                       Objs[myID].GetSetScale.x / 2f, 0f, 0f);

                //    // ���x��0�ɂ���
                //    Objs[myID].GetSetSpeed = new Vector3(0f, Objs[myID].GetSetSpeed.y, 0f);

                //    if (Objs[myID].GetComponent<ObjEnemyBase>() != null)
                //    {
                //        Objs[myID].GetComponent<ObjEnemyBase>().GetSetEnemyState = EnemyState.Idle;
                //    }
                //}
            }
            // �U�����m�̐ڐG���肾������
            if (ON_HitManager.instance.GetData(i).state == HitState.BALANCE)
            {
            }
            // �G���m�̔��肾������
            if (ON_HitManager.instance.GetData(i).state == HitState.ENEMY)
            {
                ObjEnemyBase Enemy_1 = Objs[myID].GetComponent<ObjEnemyBase>();
                ObjEnemyBase Enemy_2 = Objs[otherID].GetComponent<ObjEnemyBase>();

                // �݂��ɓG�����݂��Ă�����c
                if(Enemy_1.GetSetExist && Enemy_2.GetSetExist)
                {
                    // �ǂ�����m�b�N�o�b�N�̏�� on
                    // �ǂ�������G��Ԃł͂Ȃ������ꍇ
                    //if ((Enemy_1.GetSetEnemyState == EnemyState.KnockBack && Enemy_2.GetSetEnemyState == EnemyState.KnockBack) &&
                    //    (!Enemy_1.GetSetInvincible.m_bInvincible && !Enemy_2.GetSetInvincible.m_bInvincible))
                    if (Enemy_1.GetSetEnemyState == EnemyState.KnockBack && Enemy_2.GetSetEnemyState == EnemyState.KnockBack)
                    {
                        // --- �G�̍��� ---
                        UnionEnemy(Enemy_1.GetSetObjID, Enemy_2.GetSetObjID);
                    }
                }
            }
            // �K�E�Z�𓖂Ă����肾������
            if (ON_HitManager.instance.GetData(i).state == HitState.SPECIAL)
            {
                if (!Objs[otherID].GetSetInvincible.m_bInvincible)
                {
                    // Player���G�ɕK�E�Z�𓖂Ă���
                    if (Objs[myID].GetComponent<ObjPlayer>() != null &&
                    Objs[otherID].GetComponent<ObjEnemyBase>() != null)
                    {
                        int EnemyNum; // �G�̐����i�[����
                        // ���肪���̓G��������
                        if (Objs[otherID].GetComponent<ObjEnemyUnion>() != null)
                        {
                            EnemyNum = Objs[otherID].GetComponent<ObjEnemyUnion>().m_nEnemyIDs.Count;
                            YK_Score.instance.FieldAddScore(EnemyNum);
                        }
                        // �P�̂̓G��������
                        else YK_Score.instance.FieldAddScore(1);

                        // �q�b�g�G�t�F�N�g�Đ�
                        hitEffect.Play();
                        hitEffect.transform.position = Objs[otherID].GetSetPos;
                    }
                }
            }
            // �K�E�Z���󂯂����肾������
            if (ON_HitManager.instance.GetData(i).state == HitState.DEFSPECIAL)
            {
                // ���g�����G�łȂ���΁c
                if (!Objs[myID].GetSetInvincible.m_bInvincible)
                {
                    // ���g�����̓G��������
                    if (Objs[myID].GetComponent<ObjEnemyUnion>() != null)
                    {
                        // �j��g���K�[��ON
                        Objs[myID].GetSetDestroy = true;
                        Objs[myID].GetComponent<ObjEnemyUnion>().DestroyTriggerChildEnemy();
                        continue;
                    }

                    // ���g���G��������
                    if (Objs[myID].GetComponent<ObjEnemyBase>() != null)
                    {
                        // �m�b�N�o�b�N����
                        ON_HitManager.instance.SetActive(Objs[myID].GetSetObjID, false);
                        Objs[myID].GetSetDestroy = true;
                        continue;
                    }
                }
            }
            // �e�𓖂Ă����肾������
            if (ON_HitManager.instance.GetData(i).state == HitState.BULLET)
            {
                if (Objs[myID].GetComponent<Spider>() != null)
                {
                    hitEffect.Play();
                    hitEffect.transform.position = Objs[otherID].GetSetPos;

                    Objs[myID].GetComponent<Spider>().DeleteMissile();
                }
            }
            // �e���󂯂����肾������
            if (ON_HitManager.instance.GetData(i).state == HitState.DEFBULLET)
            {
            }
            // �e��j�󂷂锻�肾������
            if (ON_HitManager.instance.GetData(i).state == HitState.BULLET2DESTROY)
            {
            }
            // �n�ʂɓ����������肾������
            if (ON_HitManager.instance.GetData(i).state == HitState.GRUOND)
            {
                float pos;
                // �E�ɓ������Ă�����
                if (ON_HitManager.instance.GetData(i).dir == HitDir.RIGHT)
                {
                    // ���W����(����̍��[ - ���g�̏k�� / 2)
                    pos = Objs[otherID].GetSetPos.x - Mathf.Abs(Objs[otherID].GetSetScale.x / 2f) -
                          Mathf.Abs(Objs[myID].GetSetScale.x / 2f);

                    Objs[myID].GetSetPos = new Vector3(pos, Objs[myID].GetSetPos.y, 0f);

                    // ���x��0�ɂ���
                    Objs[myID].GetSetSpeed = new Vector3(0f, Objs[myID].GetSetSpeed.y, 0f);
                }
                // ���ɓ������Ă�����
                if (ON_HitManager.instance.GetData(i).dir == HitDir.LEFT)
                {
                    // ���W����(����̉E�[ + ���g�̏k�� / 2)
                    pos = Objs[otherID].GetSetPos.x + Mathf.Abs(Objs[otherID].GetSetScale.x / 2f) +
                          Mathf.Abs(Objs[myID].GetSetScale.x / 2f);

                    Objs[myID].GetSetPos = new Vector3(pos, Objs[myID].GetSetPos.y, 0f);

                    // ���x��0�ɂ���
                    Objs[myID].GetSetSpeed = new Vector3(0f, Objs[myID].GetSetSpeed.y, 0f);
                }
                // ��ɓ������Ă�����
                if (ON_HitManager.instance.GetData(i).dir == HitDir.UP)
                {
                    // ���W����(����̉��[ - ���g�̏k�� / 2)
                    pos = Objs[otherID].GetSetPos.y - Mathf.Abs(Objs[otherID].GetSetScale.y / 2f) -
                          Mathf.Abs(Objs[myID].GetSetScale.y / 2f);

                    Objs[myID].GetSetPos = new Vector3(Objs[myID].GetSetPos.x, pos, 0f);

                    // ���x��0�ɂ���
                    Objs[myID].GetSetSpeed = new Vector3(Objs[myID].GetSetSpeed.x, 0f, 0f);
                }
                // ���ɓ������Ă�����
                if (ON_HitManager.instance.GetData(i).dir == HitDir.DOWN)
                {
                    // ���W����(����̏�[ + ���g�̏k�� / 2)
                    pos = Objs[otherID].GetSetPos.y + Mathf.Abs(Objs[otherID].GetSetScale.y / 2f) +
                          Mathf.Abs(Objs[myID].GetSetScale.y / 2f);

                    Objs[myID].GetSetPos = new Vector3(Objs[myID].GetSetPos.x, pos, 0f);

                    // ���x��0�ɂ���
                    Objs[myID].GetSetSpeed = new Vector3(Objs[myID].GetSetSpeed.x, 0f, 0f);

                    // �n�ʂ̏����i�[
                    Objs[myID].GetSetGround.m_bStand = true;
                    Objs[myID].GetSetGround.m_vCenter = Objs[otherID].GetSetPos;
                    Objs[myID].GetSetGround.m_vSize = Objs[otherID].GetSetScale;

                    if(GameManager.IsDebug())
                        Debug.Log("�n�ʂɓ�������");
                }
            }
            // �n�ʂ��猩�ĉ����ɓ��Ă�ꂽ���肾������
            if (ON_HitManager.instance.GetData(i).state == HitState.DEFGRUOND)
            {
            }

            // -------------------------------
        }
    }

    // --- �G�̍��̏��� ---
    private void UnionEnemy(int id_1, int id_2)
    {
        int enemy_id_1 = -1;
        int enemy_id_2 = -1;
        // ������ID�����I�u�W�F�N�gID������
        for (int i = 0; i < Objs.Count; ++i)
        {
            if (Objs[i].GetSetObjID == id_1)
                enemy_id_1 = i; 
            if (Objs[i].GetSetObjID == id_2)
                enemy_id_2 = i;
        }

        // �������Ă��o�Ă��Ȃ������ꍇ�͏I��
        if (enemy_id_1 == -1 || enemy_id_2 == -1)
            return;

        // �G���m�̑���,�����蔻�������
        Objs[enemy_id_1].GetSetExist = false;
        Objs[enemy_id_2].GetSetExist = false;
        Objs[enemy_id_1].GetComponent<ObjEnemyBase>().GetSetEnemyState = EnemyState.RePop;
        Objs[enemy_id_2].GetComponent<ObjEnemyBase>().GetSetEnemyState = EnemyState.RePop;
        Objs[enemy_id_1].GetSetSpeed = new Vector2(Random.RandomRange(-0.1f, 0.1f), Random.RandomRange(0.3f, 0.5f));
        Objs[enemy_id_2].GetSetSpeed = new Vector2(Random.RandomRange(-0.1f, 0.1f), Random.RandomRange(0.3f, 0.5f));
        ON_HitManager.instance.SetActive(id_1, false);
        ON_HitManager.instance.SetActive(id_2, false);

        // ���̓G�̐��� �� ������
        ObjEnemyUnion enemyUnion = 
            Instantiate(enemyUnionPrefab, Vector3.zero,Quaternion.Euler(Vector3.zero));
        enemyUnion.GetSetObjID = SetObjID();
        enemyUnion.GenerateHit();
        enemyUnion.InitObj();

        // ���W�ݒ�
        enemyUnion.GetSetPos = Objs[enemy_id_1].GetSetPos + new Vector3(0f, 5f, 0f);

        // ���̌��̃I�u�W�F�N�gID���i�[
        enemyUnion.m_nEnemyIDs.Add(Objs[enemy_id_1].GetSetObjID);
        enemyUnion.m_nEnemyIDs.Add(Objs[enemy_id_2].GetSetObjID);

        // ���̌��̃I�u�W�F�N�g���u���̓G�v�������ꍇ�A
        // �i�[����Ă���ID�������p��
        int id;
        if(Objs[enemy_id_1].GetComponent<ObjEnemyUnion>() != null)
        {
            for(int i = 0; i < Objs[enemy_id_1].GetComponent<ObjEnemyUnion>().m_nEnemyIDs.Count;++i)
            {
                id = Objs[enemy_id_1].GetComponent<ObjEnemyUnion>().m_nEnemyIDs[i];
                enemyUnion.m_nEnemyIDs.Add(id);
            }
        }
        if (Objs[enemy_id_2].GetComponent<ObjEnemyUnion>() != null)
        {
            for (int i = 0; i < Objs[enemy_id_2].GetComponent<ObjEnemyUnion>().m_nEnemyIDs.Count; ++i)
            {
                id = Objs[enemy_id_2].GetComponent<ObjEnemyUnion>().m_nEnemyIDs[i];
                enemyUnion.m_nEnemyIDs.Add(id);
            }
        }

        // ��Ԃ�ݒ�
        enemyUnion.GetSetEnemyState = EnemyState.Drop;
        enemyUnion.GetSetHit = true;

        // �I�u�W�F�N�g�̃��X�g�ɒǉ�
        Objs.Add(enemyUnion);
    }

    // �I�u�W�F�N�gID��ݒ肷��
    public int SetObjID()
    {
        int selfID = maxID;
        maxID++;
        return selfID;
    }

    // �I�u�W�F�N�g�P�̂��擾����
    // int i �c �I�u�W�F�N�g��ID
    public ObjBase GetObj(int id)
    {
        for(int i = 0; i < Objs.Count; ++i)
        {
            if(Objs[i].GetSetObjID == id)
            return Objs[i];
        }

        return null;
    }

    // �I�u�W�F�N�g�̃��X�g���擾����
    public List<ObjBase> GetObjList()
    {
        return Objs;
    }
}
