using System;
using UnityEngine;
using UnityEngine.Rendering.Universal;

[Serializable]
public class FlareRendererFeature : ScriptableRendererFeature
{
    [SerializeField] private Shader _shader;
    [SerializeField] private PostprocessTiming _timing = PostprocessTiming.AfterPostprocess;
    [SerializeField] private bool _applyToSceneView = true;

    private FlareRenderPass _postProcessPass;
    public override void Create()
    {
        _postProcessPass = new FlareRenderPass(_applyToSceneView, _shader);
    }

    public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
    {
        _postProcessPass.Setup(renderer.cameraColorTarget, _timing);
        renderer.EnqueuePass(_postProcessPass);
    }
}
