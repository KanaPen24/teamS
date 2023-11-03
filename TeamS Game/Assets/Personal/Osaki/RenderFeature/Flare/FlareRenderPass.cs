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

        // �}�e���A�����쐬
        _material = CoreUtils.CreateEngineMaterial(shader);

        // RenderPassEvent.AfterRendering�ł̓|�X�g�G�t�F�N�g���|������̃J���[�e�N�X�`�������̖��O�Ŏ擾�ł���
        _afterPostProcessTexture.Init("_AfterPostProcessTexture");
    }

    public void Setup(RenderTargetIdentifier cameraColorTarget, PostprocessTiming timing)
    {
        _cameraColorTarget = cameraColorTarget;

        renderPassEvent = GetRenderPassEvent(timing);

        // Volume�R���|�[�l���g���擾
        var volumeStack = VolumeManager.instance.stack;
        _volume = volumeStack.GetComponent<FlareVolume>();
    }

    public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
    {
        if (_material == null)
        {
            return;
        }

        // �J�����̃|�X�g�v���Z�X�ݒ肪�����ɂȂ��Ă����牽�����Ȃ�
        if (!renderingData.cameraData.postProcessEnabled)
        {
            return;
        }

        // �J�������V�[���r���[�J�������V�[���r���[�ɓK�p���Ȃ��ꍇ�ɂ͉������Ȃ�
        if (!_applyToSceneView && renderingData.cameraData.cameraType == CameraType.SceneView)
        {
            return;
        }

        // if (!_volume.IsActive())
        // {
        //     return;
        // }

        // renderPassEvent��AfterRendering�̏ꍇ�A�J�����̃J���[�^�[�Q�b�g�ł͂Ȃ�_AfterPostProcessTexture���g��
        var source = renderPassEvent == RenderPassEvent.AfterRendering && renderingData.cameraData.resolveFinalTarget
            ? _afterPostProcessTexture.Identifier()
            : _cameraColorTarget;

        // �R�}���h�o�b�t�@���쐬
        var cmd = CommandBufferPool.Get(RenderPassName);
        cmd.Clear();

        // Camera�̃^�[�Q�b�g�Ɠ���Description�iDepth�͖����j��RenderTexture���擾����
        var tempTargetDescriptor = renderingData.cameraData.cameraTargetDescriptor;
        tempTargetDescriptor.depthBufferBits = 0;
        cmd.GetTemporaryRT(_tempRenderTargetHandle.id, tempTargetDescriptor);

        using (new ProfilingScope(cmd, _profilingSampler))
        {
            // Volume����TintColor���擾���Ĕ��f
            _material.SetColor(_flareColorPropertyId, _volume.flareColor.value);
            _material.SetVector(_flareVectorPropertyId, new Vector4(_volume.flarePosition.value.x, _volume.flarePosition.value.y, _volume.flareSize.value, 0.0f));
            _material.SetColor(_paraColorPropertyId, _volume.paraColor.value);
            _material.SetVector(_paraVectorPropertyId, new Vector4(_volume.paraPosition.value.x, _volume.paraPosition.value.y, _volume.paraSize.value, 0.0f));
            cmd.SetGlobalTexture(_mainTexPropertyId, source);

            // ���̃e�N�X�`������ꎞ�I�ȃe�N�X�`���ɃG�t�F�N�g��K�p���`��
            Blit(cmd, source, _tempRenderTargetHandle.Identifier(), _material);
        }

        // �ꎞ�I�ȃe�N�X�`�����猳�̃e�N�X�`���Ɍ��ʂ������߂�
        Blit(cmd, _tempRenderTargetHandle.Identifier(), source);

        // �ꎞ�I��RenderTexture���������
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
