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
            RainEff.Play();
            bPlay = true;
        }
        else if (!bOnOff && bPlay)
        {
            RainEff.Stop();
            bPlay = false;
        }

    }
}
