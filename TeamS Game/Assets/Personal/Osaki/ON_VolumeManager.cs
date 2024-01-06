using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class ON_VolumeManager : MonoBehaviour
{
    private Volume _volume;
    private FlareVolume flare;
    private ON_GaussianVolume gaussian;
    private float uvX;

    // Start is called before the first frame update
    void Start()
    {
        _volume = GetComponent<Volume>();
    }
    private void FixedUpdate()
    {
        if (GameManager.m_sGameState != GameState.Title)
            return;
        float randX = Random.Range(-1.9f, -10.1f); // ※ -2.0～-10.0の範囲でランダムな値が返る)
        uvX -= 0.04f;
        SetFlarePosX(uvX);
        if (uvX <= randX) // ※ -2.0～-5.0の範囲でランダムな値が返る)
        uvX = 3.0f;
    }
    // フレアの遷移
    public void SetFlarePosX(float x)
    {
         if(_volume.profile.TryGet<FlareVolume>(out flare))
        {
            flare.flarePosition.value = new Vector2(x, 0.0f);
        }
    }

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
