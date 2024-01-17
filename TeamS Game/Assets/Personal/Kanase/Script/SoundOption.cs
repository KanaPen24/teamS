using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundOption : MonoBehaviour
{
    public static float MasterVolume = 0.5f; // データ保存用
    public static float BGMVolume = 0.5f;
    public static float SEVolume = 0.5f;
    public AudioMixer audioMixer;
    public Slider masterSlider;
    public Slider bgmSlider;
    public Slider seSlider;

    // Start is called before the first frame update
    void Start()
    {
        masterSlider.value = MasterVolume;
        bgmSlider.value = BGMVolume;
        seSlider.value = SEVolume;


    }    
    public float ConvertVolumeToDb(float volume)
    {
        //AudioMixerの-80～20dbの値をvalueの0～1に当てはめる
        return Mathf.Clamp(Mathf.Log10(Mathf.Clamp(volume, 0f, 1f)) * 20f, -80f, 0f);
    }

    public void SetMaster(float volume)
    {
        MasterVolume = masterSlider.value;
        audioMixer.SetFloat("Master_Volume", ConvertVolumeToDb(masterSlider.value));
    }

    public void SetBGM(float volume)
    {
        BGMVolume = bgmSlider.value;
        audioMixer.SetFloat("BGM_Volume", ConvertVolumeToDb(bgmSlider.value));
    }

    public void SetSE(float volume)
    {
        SEVolume = seSlider.value;
        audioMixer.SetFloat("SE_Volume", ConvertVolumeToDb(seSlider.value));
    }

}
