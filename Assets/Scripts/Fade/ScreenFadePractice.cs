using UnityEngine;
using UnityEngine.Rendering.Universal;
using Pixelplacement;

public class ScreenFadePractice : MonoBehaviour
{
    // references
    public ForwardRendererData rendererData = null;

    // Settings
    [Range(0, 1)] public float alpha = 1.0f;
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

        // Ensure its the correct feature
        if(feature is ScreenFadeFeature screenFade)
        {
            // Duplicate material so we don't change the renderer's asset
            fadeMaterial = Instantiate(screenFade.Settings.material);
            screenFade.Settings.runTimeMaterial = fadeMaterial;
        }
    }

    public float FadeIn()
    {
        Tween.ShaderFloat(fadeMaterial, "_Alpha", 1, duration, 0);
        return duration;
    }

    public float FadeOut()
    {
        Tween.ShaderFloat(fadeMaterial, "_Alpha", 0, duration, 0);
        return duration;
    }
}
