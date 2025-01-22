using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
public class CustomPostProcessRenderFeature : ScriptableRendererFeature
{

    [SerializeField]
    private Shader bloomShader;
    [SerializeField]
    private Shader compositeShader;


    private CustomPostProcessPass customPass;
    public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
    {
       renderer.EnqueuePass(customPass);
    }
    public override void Create()
    {
        customPass = new CustomPostProcessPass();
    }
}

