using System.Collections;
using System;
using UnityEngine;
using UnityEngine.Rendering;

[Serializable]
[VolumeComponentMenu("FlarePara")]
public class FlareVolume : VolumeComponent
{
    public ColorParameter flareColor = new ColorParameter(Color.white);
    public Vector2Parameter flarePosition = new Vector2Parameter(Vector2.zero);
    public FloatParameter flareSize = new FloatParameter(0.0f);
    public ColorParameter paraColor = new ColorParameter(Color.white);
    public Vector2Parameter paraPosition = new Vector2Parameter(Vector2.zero);
    public FloatParameter paraSize = new FloatParameter(0.0f);
}
