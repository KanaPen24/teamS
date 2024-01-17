using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class ON_RainbowEffect : MonoBehaviour
{
    [Header("エフェクト")]
    [SerializeField] private VisualEffect effect;
    [Header("現在の色")]
    [SerializeField] private Color Color;
    [Header("現在のコンボ")]
    public int nowCombo;
    [Header("色の変更コンボ数")]
    [SerializeField] private int[] comboValue;
    [Header("色変更パターン")]
    [SerializeField] private Color[] colors;
    [Header("エフェクトの量パターン")]
    [SerializeField] private float[] rates;

    [Header("色変更スパン")]
    public float Chnge_Color_Time = 0.1f;

    [Header("変更の滑らかさ")]
    public float Smooth = 0.01f;

    [Header("色彩")]
    [Range(0, 1)] public float HSV_Hue = 1.0f;// 0 ~ 1

    [Header("彩度")]
    [Range(0, 1)] public float HSV_Saturation = 1.0f;// 0 ~ 1

    [Header("明度")]
    [Range(0, 1)] public float HSV_Brightness = 1.0f;// 0 ~ 1

    [Header("色彩 MAX")]
    [Range(0, 1)] public float HSV_Hue_max = 1.0f;// 0 ~ 1

    [Header("色彩 MIN")]
    [Range(0, 1)] public float HSV_Hue_min = 0.0f;// 0 ~ 1

    // Start is called before the first frame update
    void Start()
    {
        HSV_Hue = HSV_Hue_min;
        StartCoroutine("Change_Color");
    }

    // Update is called once per frame
    void Update()
    {
        Color = Color.black;
        float num = 0.0f;
        nowCombo = YK_Combo.GetCombo();

        for(int i = 0; i < comboValue.Length; ++i)
        {
            if(nowCombo >= comboValue[i])
            {
                Color = colors[i];
                num = rates[i];
            }
        }

        effect.SetFloat("Rate", num);
        effect.SetVector4("Color", Color);
    }

    IEnumerator Change_Color()
    {
        HSV_Hue += Smooth;

        if (HSV_Hue >= HSV_Hue_max)
        {
            HSV_Hue = HSV_Hue_min;
        }

        colors[colors.Length - 1] = Color.HSVToRGB(HSV_Hue, HSV_Saturation, HSV_Brightness);

        yield return new WaitForSeconds(Chnge_Color_Time);

        StartCoroutine("Change_Color");
    }
}
