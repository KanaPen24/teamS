using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
@file   ON_BackGround.cs
@brief  �w�i�̎�������
@author Noriaki Osaki
@date   2023/11/24

�J�����̐i�s�������擾
�J�����͈͊O�̃I�u�W�F��T��
�i�s�����ɂȂ��I�u�W�F��i�s�����ֈړ�
�J�������̃I�u�W�F�͈ʒu�ɉ����ė���鑬�x����������
*/

public class ON_BackGround : MonoBehaviour
{
    [SerializeField] private GameObject cam;
    [SerializeField] private float rate = 1;
    private float currentX;
    private bool inScene = false;
    private bool oldinScene;
    private Vector3 start;
    // Start is called before the first frame update
    void Start()
    {
        currentX = cam.transform.position.x;
        oldinScene = inScene;
        start = transform.position;
        if (cam == null)
            cam = Camera.main.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        float dir = cam.transform.position.x - currentX;

        if(inScene)
        {
            // ��ʓ��ňړ�
            transform.position = new Vector3(start.x + Mathf.Lerp(0, cam.transform.position.x, rate) , start.y, start.z);
        }

        if(!inScene && oldinScene || false/*���͈̔͊O�̏ꍇ*/)
        {
            // �Ĕz�u
        }

        // �X�V
        if(currentX != cam.transform.position.x)
            currentX = cam.transform.position.x;
        oldinScene = inScene;
    }

    private void OnBecameVisible()
    {
        inScene = true;
    }

    private void OnBecameInvisible()
    {
        inScene = false;
    }
}
