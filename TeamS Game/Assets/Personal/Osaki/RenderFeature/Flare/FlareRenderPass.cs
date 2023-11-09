using System;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public enum PostprocessTiming
{
    AfterOpaque,
    BeforePostprocess,
    AfterPostprocess
}

public class FlareRenderPass : ScriptableRenderPass
{
    private const string RenderPassName = nameof(FlareRenderPass);
    private const string ProfilingSamplerName = "ScrToDest";

    private readonly bool _applyToSceneView;
    private readonly int _mainTexPropertyId = Shader.PropertyToID("_MainTex");
    private readonly Material _material;
    private readonly ProfilingSampler _profilingSampler;
    private readonly int _flareColorPropertyId = Shader.PropertyToID("_FlareColor");
    private readonly int _flareVectorPropertyId = Shader.PropertyToID("_FlareVec");
    private readonly int _paraColorPropertyId = Shader.PropertyToID("_ParaColor");
    private readonly int _paraVectorPropertyId = Shader.PropertyToID("_ParaVec");

    private RenderTargetHandle _afterPostProcessTexture;
    private RenderTargetIdentifier _cameraColorTarget;
    private RenderTargetHandle _tempRenderTargetHandle;
    private FlareVolume _volume;

    public FlareRenderPass(bool applyToSceneView, Shader shader)
    {
        if (shader == null)
        {
            return;
        }

        _applyToSceneView = applyToSceneView;
        _profilingSampler = new ProfilingSampler(ProfilingSamplerName);
        _tempRenderTargetHandle.Init("_TempRT");

        // マテリアルを作成
        _material = CoreUtils.CreateEngineMaterial(shader);

        // RenderPassEvent.AfterRenderingではポストエフェクトを掛けた後のカラーテクスチャがこの名前で取得できる
        _afterPostProcessTexture.Init("_AfterPostProcessTexture");
    }

    public void Setup(RenderTargetIdentifier cameraColorTarget, PostprocessTiming timing)
    {
        _cameraColorTarget = cameraColorTarget;

        renderPassEvent = GetRenderPassEvent(timing);

        // Volumeコンポーネントを取得
        var volumeStack = VolumeManager.instance.stack;
        _volume = volumeStack.GetComponent<FlareVolume>();
    }

    public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
    {
        if (_material == null)
        {
            return;
        }

        // カメラのポストプロセス設定が無効になっていたら何もしない
        if (!renderingData.cameraData.postProcessEnabled)
        {
            return;
        }

        // カメラがシーンビューカメラかつシーンビューに適用しない場合には何もしない
        if (!_applyToSceneView && renderingData.cameraData.cameraType == CameraType.SceneView)
        {
            return;
        }

        // if (!_volume.IsActive())
        // {
        //     return;
        // }

        // renderPassEventがAfterRenderingの場合、カメラのカラーターゲットではなく_AfterPostProcessTextureを使う
        var source = renderPassEvent == RenderPassEvent.AfterRendering && renderingData.cameraData.resolveFinalTarget
            ? _afterPostProcessTexture.Identifier()
            : _cameraColorTarget;

        // コマンドバッファを作成
        var cmd = CommandBufferPool.Get(RenderPassName);
        cmd.Clear();

        // Cameraのターゲットと同じDescription（Depthは無し）のRenderTextureを取得する
        var tempTargetDescriptor = renderingData.cameraData.cameraTargetDescriptor;
        tempTargetDescriptor.depthBufferBits = 0;
        cmd.GetTemporaryRT(_tempRenderTargetHandle.id, tempTargetDescriptor);

        using (new ProfilingScope(cmd, _profilingSampler))
        {
            // VolumeからTintColorを取得して反映
            _material.SetColor(_flareColorPropertyId, _volume.flareColor.value);
            _material.SetVector(_flareVectorPropertyId, new Vector4(_volume.flarePosition.value.x, _volume.flarePosition.value.y, _volume.flareSize.value, 0.0f));
            _material.SetColor(_paraColorPropertyId, _volume.paraColor.value);
            _material.SetVector(_paraVectorPropertyId, new Vector4(_volume.paraPosition.value.x, _volume.paraPosition.value.y, _volume.paraSize.value, 0.0f));
            cmd.SetGlobalTexture(_mainTexPropertyId, source);

            // 元のテクスチャから一時的なテクスチャにエフェクトを適用しつつ描画
            Blit(cmd, source, _tempRenderTargetHandle.Identifier(), _material);
        }

        // 一時的なテクスチャから元のテクスチャに結果を書き戻す
        Blit(cmd, _tempRenderTargetHandle.Identifier(), source);

        // 一時的なRenderTextureを解放する
        cmd.ReleaseTemporaryRT(_tempRenderTargetHandle.id);

        context.ExecuteCommandBuffer(cmd);
        CommandBufferPool.Release(cmd);
    }

    private static RenderPassEvent GetRenderPassEvent(PostprocessTiming postprocessTiming)
    {
        switch (postprocessTiming)
        {
            case PostprocessTiming.AfterOpaque:
                return RenderPassEvent.AfterRenderingSkybox;
            case PostprocessTiming.BeforePostprocess:
                return RenderPassEvent.BeforeRenderingPostProcessing;
            case PostprocessTiming.AfterPostprocess:
                return RenderPassEvent.AfterRendering;
            default:
                throw new ArgumentOutOfRangeException(nameof(postprocessTiming), postprocessTiming, null);
        }
    }
}
