using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class SJ_RainPlay : MonoBehaviour
{
    [SerializeField] public VisualEffect RainEff;
    [SerializeField] private bool bOnOff;
    [SerializeField] private bool bPlay;

    // Start is called before the first frame update
    void Start()
    {
        RainEff = GetComponent<VisualEffect>();
        RainEff.Reinit();
        bOnOff = false;
        bPlay = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
            bOnOff = !bOnOff;

        if (bOnOff && !bPlay)
        {
            RainPlay();
            bPlay = true;
        }
        else if (!bOnOff && bPlay)
        {
            RainStop();
            bPlay = false;
        }

    }
    //雨のエフェクト再生
    public void RainPlay()
    {
        RainEff.Play();
    }
    //雨のエフェクト停止
    public void RainStop()
    {
        RainEff.Stop();
    }
}
