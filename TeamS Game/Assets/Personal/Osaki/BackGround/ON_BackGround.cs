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
            // 画面内で移動
            transform.position = new Vector3(start.x + Mathf.Lerp(0, cam.transform.position.x, rate) , start.y, start.z);
        }

        if(!inScene && oldinScene || false/*一定の範囲外の場合*/)
        {
            // 再配置
        }

        // 更新
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
