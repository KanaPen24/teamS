using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class Rain_Demo : MonoBehaviour
{
    [SerializeField] public  VisualEffect RainEff;
    [SerializeField] private bool bOnOff;
    [SerializeField] private bool bPlay;

    // Start is called before the first frame update
    void Start()
    {
        //RainEff = GetComponent<VisualEffect>();
        bOnOff = false;
        bPlay = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.R))
        {
            bOnOff = !bOnOff;
        }

        if(bOnOff == true)
        {
            RainEff.Play();
        }

        if(bOnOff == false)
        {
            RainEff.Stop();
        }
    }
}
