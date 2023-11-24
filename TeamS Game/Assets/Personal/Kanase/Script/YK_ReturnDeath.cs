using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class YK_ReturnDeath : MonoBehaviour
{
    /// <summary>
    /// デバッグモード .
    /// </summary>
    public bool DebugMode = true;
    /// <summary>フェード中の透明度</summary>
    private float fadeAlpha = 0;
    /// <summary>フェード中かどうか</summary>
    private bool isFading = false;
    /// <summary>フェード色</summary>
    public Color fadeColor = Color.black;
    /// <summary>フェード色</summary>
    [SerializeField] private float interval = 0.2f;


    private void Update()
    {
        //テスト
        if (Input.GetKeyDown(KeyCode.F6))
        {
            StartCoroutine(Fade(interval));
        }
    }

    /// <summary>
    /// シーン遷移用コルーチン .
    /// </summary>
    /// <param name='scene'>シーン名</param>
    /// <param name='interval'>暗転にかかる時間(秒)</param>
    private IEnumerator Fade(float interval)
    {
        //だんだん暗く .
        this.isFading = true;
        float time = 0;
        while (time <= interval)
        {
            this.fadeAlpha = Mathf.Lerp(0f, 1f, time / interval);
            time += Time.deltaTime;
            yield return 0;
        }

        //だんだん明るく .
        time = 0;
        while (time <= interval)
        {
            this.fadeAlpha = Mathf.Lerp(1f, 0f, time / interval);
            time += Time.deltaTime;
            yield return 0;
        }

        this.isFading = false;
    }
}