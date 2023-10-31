using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;

[SerializeField]
[VolumeComponentMenu("Test")]
public class TestPostProcessVolume : VolumeComponent
{
    public bool IsActive() => tintColor != Color.white;

    public ColorParameter tintColor = new ColorParameter(Color.white); 
}
