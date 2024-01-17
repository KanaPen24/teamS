using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class ON_RainbowEffect : MonoBehaviour
{
    [Header("�G�t�F�N�g")]
    [SerializeField] private VisualEffect effect;
    [Header("���݂̐F")]
    [SerializeField] private Color Color;
    [Header("���݂̃R���{")]
    public int nowCombo;
    [Header("�F�̕ύX�R���{��")]
    [SerializeField] private int[] comboValue;
    [Header("�F�ύX�p�^�[��")]
    [SerializeField] private Color[] colors;
    [Header("�G�t�F�N�g�̗ʃp�^�[��")]
    [SerializeField] private float[] rates;

    [Header("�F�ύX�X�p��")]
    public float Chnge_Color_Time = 0.1f;

    [Header("�ύX�̊��炩��")]
    public float Smooth = 0.01f;

    [Header("�F��")]
    [Range(0, 1)] public float HSV_Hue = 1.0f;// 0 ~ 1

    [Header("�ʓx")]
    [Range(0, 1)] public float HSV_Saturation = 1.0f;// 0 ~ 1

    [Header("���x")]
    [Range(0, 1)] public float HSV_Brightness = 1.0f;// 0 ~ 1

    [Header("�F�� MAX")]
    [Range(0, 1)] public float HSV_Hue_max = 1.0f;// 0 ~ 1

    [Header("�F�� MIN")]
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
