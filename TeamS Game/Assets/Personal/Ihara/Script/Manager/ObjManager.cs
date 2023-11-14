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
    public static ObjManager instance;
    public ObjEnemyUnion unionObj;
    public ParticleSystem hitEffect;

    private int myID;
    private int otherID;

    public ObjBase GetObjs(int i)
    {
        return Objs[i];
    }

    public List<ObjBase> GetObj()
    {
        return Objs;
    }

    private void Start()
    {
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
            // --- �I�u�W�F�N�g�����݂��Ă���ꍇ ---
            if (Objs[i].GetSetExist)
            {
                // �I�u�W�F�N�g�̕\��
                Objs[i].texObj.enabled = true;

                // �X�V����  �� �n�ʔ��� �� �������� �� �ړ��ʂɑ��x���i�[ �� ���x���� ��
                // ���G���ԍX�V �� ���W�X�V
                Objs[i].UpdateObj();
                Objs[i].CheckObjGround();
                SaveObjSpeed(i);
                FixObjDir(i);
                UpdateInvincible(i);
                Objs[i].GetSetMove = Objs[i].GetSetSpeed;
                Objs[i].GetSetPos += new Vector3(Objs[i].GetSetMove.x, Objs[i].GetSetMove.y, 0f);

                // �I�u�W�F�N�g���ړ��������W�ɓ����蔻����ړ�������
                ON_HitManager.instance.SetCenter(Objs[i].GetSetHitID, Objs[i].GetSetPos);
            }
            // --- ���݂��Ă��Ȃ��ꍇ ---
            else
            {
                // �I�u�W�F�N�g�̔�\��
                Objs[i].texObj.enabled = false;
            }

            // �I�u�W�F�N�g�̃p�����[�^�[�X�V
            Objs[i].UpdateCheckParam();
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

                // �����̌������I����Ă�����c
                if (myID != -1 && otherID != -1)
                {
                    // �C�����s���I������
                    CollisionFix(i, ON_HitManager.instance.GetData(i).state);
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
                    Objs[myID].GetSetGround.m_bStand = true;
                    Objs[myID].GetSetGround.m_vCenter = Objs[otherID].GetSetPos;
                    Objs[myID].GetSetGround.m_vSize = Objs[otherID].GetSetScale;

                    if(GameManager.IsDebug())
                        Debug.Log("�n�ʂɓ�������");
                }
            }

            //�U���̔��肾������
            if (ON_HitManager.instance.GetData(i).state == HitState.DEFENCE)
            {
                if(!Objs[otherID].GetSetInvincible.m_bInvincible)
                {
                    // �G�Ƀm�b�N�o�b�N���� �� ���G���Ԑݒ�
                    Objs[otherID].KnockBackObj(Objs[myID].GetSetDir);
                    Objs[otherID].GetSetInvincible.SetInvincible(0.3f);

                    // �q�b�g�G�t�F�N�g�Đ�
                    hitEffect.Play();
                    hitEffect.transform.position = Objs[otherID].GetSetPos;

                    // �R���{����
                    YK_Combo.AddCombo();
                }
            }

            // �G���m�̔��肾������
            if (ON_HitManager.instance.GetData(i).state == HitState.ENEMY)
            {
                // �ǂ��炩���m�b�N�o�b�N�̏�Ԃ�������
                if(Objs[myID].GetComponent<ObjEnemyBase>().GetSetEnemyState == EnemyState.KnockBack &&
                    Objs[otherID].GetComponent<ObjEnemyBase>().GetSetEnemyState == EnemyState.KnockBack)
                {
                    // �G���m�̑��݂𖳂���
                    Objs[myID].GetSetExist = false;
                    Objs[otherID].GetSetExist = false;

                    Objs[myID].GetComponent<ObjEnemyBase>().GetSetEnemyState = EnemyState.Idle;
                    Objs[otherID].GetComponent<ObjEnemyBase>().GetSetEnemyState = EnemyState.Idle;

                    // �G�̓����蔻��̑��݂𖳂���
                    ON_HitManager.instance.SetActive(myID,false);
                    ON_HitManager.instance.SetActive(otherID,false);

                    // ���̂����G�𐶐�
                    ObjEnemyUnion unionEnemy = Instantiate(unionObj,
                        Objs[myID].GetSetPos + new Vector3(0f,5f,0f),
                        Quaternion.Euler(new Vector3(0f,0f,0f)
                        ));

                    // ���������G�̏�����
                    Objs.Add(unionEnemy);
                    unionEnemy.GetSetObjID = Objs.Count - 1;
                    unionEnemy.m_nEnemyCnt = Objs[myID].GetComponent<ObjEnemyBase>().m_nEnemyCnt +
                                             Objs[otherID].GetComponent<ObjEnemyBase>().m_nEnemyCnt;
                    unionEnemy.m_nEnemyIDs.Add(Objs[myID].GetSetObjID);
                    unionEnemy.m_nEnemyIDs.Add(Objs[otherID].GetSetObjID);
                    unionEnemy.GenerateHit();
                    unionEnemy.InitObj();
                }
            }
            // -------------------------------
        }
    }

    // --- �����蔻��ɂ��C���֐� ---
    private void CollisionFix(int dataNum, HitState hitState)
    {
        switch(hitState)
        {
            case HitState.ATTACK:
                break;
            case HitState.BALANCE:
                break;
            case HitState.BODYS:
                break;
            case HitState.DEFENCE:
                if (Objs[myID].GetComponent<ObjEnemyBase>() != null)
                    (myID, otherID) = (otherID, myID);
                break;
            case HitState.ENEMY:
                break;
            case HitState.GRUOND:
                if (Objs[myID].GetComponent<ObjField>() != null)
                {
                    (myID, otherID) = (otherID, myID);
                    if(ON_HitManager.instance.GetData(dataNum).dir == HitDir.UP)
                    {
                        ON_HitManager.instance.GetData(dataNum).dir = HitDir.DOWN;
                        break;
                    }
                    if (ON_HitManager.instance.GetData(dataNum).dir == HitDir.DOWN)
                    {
                        ON_HitManager.instance.GetData(dataNum).dir = HitDir.UP;
                        break;
                    }
                    if (ON_HitManager.instance.GetData(dataNum).dir == HitDir.RIGHT)
                    {
                        ON_HitManager.instance.GetData(dataNum).dir = HitDir.LEFT;
                        break;
                    }
                    if (ON_HitManager.instance.GetData(dataNum).dir == HitDir.LEFT)
                    {
                        ON_HitManager.instance.GetData(dataNum).dir = HitDir.RIGHT;
                        break;
                    }
                }
                break;
        }
    }
}
