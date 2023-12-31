using UnityEngine;
using UnityEngine.Rendering.Universal;
public class YK_Light : MonoBehaviour
{
    [SerializeField]
    float maxIntensity;

    [SerializeField]
    float blinkSpeed;

    Light2D blinkLight;

    int flashAdjustValue = 7;

    void Start()
    {
        blinkLight = this.gameObject.GetComponent<Light2D>();
    }

    void FixedUpdate()
    {
        if (blinkLight.intensity > maxIntensity / flashAdjustValue)
        {
            blinkLight.intensity = Mathf.PerlinNoise(Time.time * blinkSpeed, 0) * maxIntensity;
        }
        else //消えかけると激しく点滅
        {
            blinkLight.intensity = Random.Range(0, maxIntensity / 2);
        }

    }
}

