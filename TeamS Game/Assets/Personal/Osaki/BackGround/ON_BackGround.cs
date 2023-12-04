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
    private Vector3 start;
    private SpriteRenderer renderer = null;
    // Start is called before the first frame update
    void Start()
    {
        currentX = cam.transform.position.x;
        start = transform.position;
        if (cam == null)
            cam = Camera.main.gameObject;
        renderer = GetComponent<SpriteRenderer>();

        rate = 1 - rate;
    }


    // Update is called once per frame
    void Update()
    {
        float dir = cam.transform.position.x - currentX;

        // �����Ⴄor��ʓ��̏ꍇ�̂ݎ��𓮂���
        transform.position = new Vector3(transform.position.x - Mathf.Lerp(0.0f, dir, rate), transform.position.y, transform.position.z);
        if(renderer != null)
        {
            if(renderer.bounds.max.x < cam.transform.position.x - 9.0f)
            {
                var pos = transform.position;
                pos.x = cam.transform.position.x + 9.0f + (renderer.bounds.size.x / 2.0f);
                transform.position = pos;
            }

        }


        // �X�V
        if (currentX != cam.transform.position.x)
            currentX = cam.transform.position.x;
    }
}
