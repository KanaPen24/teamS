using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class ON_VolumeManager : MonoBehaviour
{
    private Volume _volume;
    private FlareVolume flare;
    private ON_GaussianVolume gaussian;
    // Start is called before the first frame update
    void Start()
    {
        _volume = GetComponent<Volume>();
    }
    //// フレアの遷移
    //public void SetBraunRate(float rate)
    //{
    //    if(_volume.profile.TryGet<FlareVolume>(out flare))
    //    {
    //        rate = rate > 1 ? 1 : rate;
    //        rate = rate < 0 ? 0 : rate;
    //        flare.rate.value = rate;
    //    }
    //}

    // ガウシアンブラーの遷移
    public void SetGaussianRate(float rate)
    {
        if(_volume.profile.TryGet<ON_GaussianVolume>(out gaussian))
        {
            rate = rate > 1 ? 1 : rate;
            rate = rate < 0 ? 0 : rate;
            gaussian.rate.value = rate;
        }
    }
}
