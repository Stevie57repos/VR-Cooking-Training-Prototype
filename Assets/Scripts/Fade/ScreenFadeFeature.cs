using System;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class ScreenFadeFeature : ScriptableRendererFeature
{
    public FadeSettings Settings = null;
    private ScreenFadePass _renderPass = null;
    public override void Create()
    {
        // Create a new pass using the Settings
        _renderPass = new ScreenFadePass(Settings);
    }
    public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
    {
        // Add the pass we have the necessary values
        if (Settings.AreValid())
            renderer.EnqueuePass(_renderPass);
    }
}

[Serializable]
public class FadeSettings
{
    public bool isEnabled = true;
    public string ProfilerTag = "Screen Fade";

    public RenderPassEvent renderPassEvent = RenderPassEvent.AfterRenderingPostProcessing;
    public Material material = null;

    [NonSerialized] public Material runTimeMaterial = null;
    public bool AreValid()
    {
        return (runTimeMaterial != null) && isEnabled;
    }
}
