using UnityEngine;
using UnityEngine.Rendering.Universal;
using Pixelplacement;

public class ScreenFade : MonoBehaviour
{
    // References
    public ForwardRendererData RendererData = null;

    // Settings
    [Range(0, 1)] public float Alpha = 1.0f;
    [Range(0, 5)] public float Duration = 0.5f;

    // Runtime
    private Material FadeMaterial = null;

    private void Start()
    {
        SetupFadeFeature();
    }

    private void SetupFadeFeature()
    {
        // Look for the screen fade feature
        ScriptableRendererFeature feature = RendererData.rendererFeatures.Find(item => item is ScreenFadeFeature);

        // Ensure it's the correct feature
        if (feature is ScreenFadeFeature screenFade)
        {
            // Duplicate material so we don't change the renderer's asset
            FadeMaterial = Instantiate(screenFade.Settings.material);
            screenFade.Settings.runTimeMaterial = FadeMaterial;
        }
    }

    public float FadeIn()
    {
        // Fade to black
        Tween.ShaderFloat(FadeMaterial, "_Alpha", 1, Duration, 0);
        return (Duration) ;
    }
    public float FadeOut()
    {
        // Fade to clear
        Tween.ShaderFloat(FadeMaterial, "_Alpha", 0, Duration, 0);
        return Duration;
    }
}
