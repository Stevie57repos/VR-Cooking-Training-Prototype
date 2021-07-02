using UnityEngine;
using UnityEngine.Rendering.Universal;
using Pixelplacement;

public class ScreenFade : MonoBehaviour
{
    // References
    public ForwardRendererData rendererData = null;

    // Settings
    [Range(0, 5)] public float duration = 0.5f;

    // Runtime
    private Material fadeMaterial = null;

    private void Start()
    {
        // Find the, and set the feature's material
        SetupFadeFeature();
    }

    private void SetupFadeFeature()
    {
        // Look for the screen fade feature
        ScriptableRendererFeature feature = rendererData.rendererFeatures.Find(item => item is ScreenFadeFeature);

        // Ensure it's the correct feature
        if (feature is ScreenFadeFeature screenFade)
        {
            // Duplicate material so we don't change the renderer's asset
            fadeMaterial = Instantiate(screenFade.settings.material);
            screenFade.settings.runTimeMaterial = fadeMaterial;
        }
    }

    public float FadeIn()
    {
        // Fade to black
        Tween.ShaderFloat(fadeMaterial, "_Alpha", 1, duration, 0);
        return duration;
    }

    public float FadeOut()
    {
        // Fade to clear
        Tween.ShaderFloat(fadeMaterial, "_Alpha", 0, duration, 0);
        return duration;
    }
}
