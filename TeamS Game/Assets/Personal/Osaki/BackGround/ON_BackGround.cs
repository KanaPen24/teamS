using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
@file   ON_BackGround.cs
@brief  背景の視差効果
@author Noriaki Osaki
@date   2023/11/24

カメラの進行方向を取得
カメラ範囲外のオブジェを探索
進行方向にないオブジェを進行方向へ移動
カメラ内のオブジェは位置に応じて流れる速度をゆっくりに
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

        // 式が違うor画面内の場合のみ式を動かす
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


        // 更新
        if (currentX != cam.transform.position.x)
            currentX = cam.transform.position.x;
    }
}
