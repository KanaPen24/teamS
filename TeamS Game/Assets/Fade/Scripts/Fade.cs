﻿/*
 The MIT License (MIT)

Copyright (c) 2013 yamamura tatsuhiko

Permission is hereby granted, free of charge, to any person obtaining a copy of
this software and associated documentation files (the "Software"), to deal in
the Software without restriction, including without limitation the rights to
use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of
the Software, and to permit persons to whom the Software is furnished to do so,
subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS
FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR
COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER
IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN
CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
*/
using UnityEngine;
using System.Collections;
using UnityEngine.Assertions;

public class Fade : MonoBehaviour
{
	IFade fade;
    public bool StartFade;      //トランジション用変数
    [SerializeField] private float FadeSpeed = 0.02f;
    [SerializeField] private YK_JsonSave Json;
    public static Fade instance;         // Fadeのインスタンス
     /**
     * @fn
     * 初期化処理(外部参照を除く)
     * @brief  メンバ初期化処理
     * @detail 特に無し
     */
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void Start ()
	{
        Init();
        //シーン立ち上げ時にトランジションを掛けるか
        if (StartFade == true)//||MoveScene.FadeFlg)
        {
            cutoutRange = 1;
        }
        fade.Range = cutoutRange;
       // if (MoveScene.FadeFlg)
            FadeOut(1f);
    }

    private void Update()
    {
        //テスト
        if (Input.GetKeyDown(KeyCode.F6))
        {
            ReturnDeathFade();
        }
    }

    float cutoutRange;

	void Init ()
	{
		fade = GetComponent<IFade> ();
	}

	void OnValidate ()
	{
		Init ();
		fade.Range = cutoutRange;
	}

	IEnumerator FadeoutCoroutine (float time, System.Action action)
	{
		float endTime = Time.timeSinceLevelLoad + time * (cutoutRange);

		var endFrame = new WaitForEndOfFrame ();

        while (cutoutRange >= -0.5f)  {
            cutoutRange -= FadeSpeed;
			fade.Range = cutoutRange;
			yield return endFrame;
		}
		cutoutRange = 0;
		fade.Range = cutoutRange;

		if (action != null) {
			action ();
		}
	}

	IEnumerator FadeinCoroutine (float time, System.Action action)
	{
		float endTime = Time.timeSinceLevelLoad + time * (1 - cutoutRange);
		
		var endFrame = new WaitForEndOfFrame ();

        while (cutoutRange <= 1.5f)  {
            cutoutRange += FadeSpeed;
			fade.Range = cutoutRange;
			yield return endFrame;
		}
		cutoutRange = 1;
		fade.Range = cutoutRange;

		if (action != null) {
			action ();
		}
    }
    IEnumerator FadeInOutCoroutine(float time, System.Action action)
    {
        float endTime = Time.timeSinceLevelLoad + time * (1 - cutoutRange);

        var endFrame = new WaitForEndOfFrame();

        while (cutoutRange <= 1.5f)
        {
            cutoutRange += FadeSpeed;
            fade.Range = cutoutRange;
            yield return endFrame;
        }
        cutoutRange = 1;
        fade.Range = cutoutRange;

        if (action != null)
        {
            action();
        }
        FadeOut(1f);
    }


    public Coroutine FadeOut (float time, System.Action action)
	{
		StopAllCoroutines ();
		return StartCoroutine (FadeoutCoroutine (time, action));
	}

	public Coroutine FadeOut (float time)
	{
		return FadeOut (time, null);
	}

	public Coroutine FadeIn (float time, System.Action action)
	{
		StopAllCoroutines ();
		return StartCoroutine (FadeinCoroutine (time, action));
	}

	public Coroutine FadeIn (float time)
	{
		return FadeIn (time, null);
	}

    public Coroutine FadeInOut(float time, System.Action action)
    {
        StopAllCoroutines();
        return StartCoroutine(FadeInOutCoroutine(time, action));
    }

    public Coroutine FadeInOut(float time)
    {
        return FadeInOut(time, null);
    }

    private void OnGUI()
    {
        GUI.skin.label.fontSize = 17;
        GUI.color = Color.black;
       // GUILayout.Label(StartFade.ToString());
    }

    public void ReturnDeathFade()
    {
        FadeInOut(1f, () =>
        {
            Json.Load();
            YK_Score.instance.LoadScore();
        });
    }
}