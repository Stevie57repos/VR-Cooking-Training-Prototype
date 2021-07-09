using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // for debugging - skip loading main menu and load level
    public bool isLoadMainMenu;
    public bool isLoadLevel;

    public ScreenFade screenFade = null;
    public Scene currentScene;

    public float levelDuration = 60f;

    public GameManagerEventChannelSO Startbutton;
    public GameManagerEventChannelSO QuitButton;

    [SerializeField] Timer _timer;

    public Animator GateAnimator;

    public AudioSource BGAudioSource;
    public AudioSO BackgroundMusicSO;

    public AudioSource CustomerEffectsAudioSource;
    public AudioEventChannelSO CustomerEffects;

    private void OnEnable()
    {
        Startbutton.GameManagerEvent += LoadLevelAsync;
        QuitButton.GameManagerEvent += QuitGame;
        CustomerEffects.AudioManagerEvent += PlayCustomerAudioEffect;
    }

    private void OnDisable()
    {
        Startbutton.GameManagerEvent -= LoadLevelAsync;
        QuitButton.GameManagerEvent -= QuitGame;
        CustomerEffects.AudioManagerEvent -= PlayCustomerAudioEffect;
    }
    private void OnValidate()
    {
        if (!screenFade)
            screenFade = FindObjectOfType<ScreenFade>();
    }

    private void Start()
    {
        if(isLoadMainMenu)
            LoadMainMenu();
        if (isLoadLevel)
            LoadLevelAsync();
    }

    public void LoadMainMenu()
    {
        StartCoroutine(LoadMainMenuCoroutine());
    }

    IEnumerator LoadMainMenuCoroutine()
    {
        PlayBackgroundMusic(BackgroundMusicSO.BGClipList[0]);
        yield return SceneManager.LoadSceneAsync("MenuUI", LoadSceneMode.Additive);
        yield return SceneManager.SetActiveScene(SceneManager.GetSceneByName("MenuUI"));
    }

    public void LoadLevelAsync()
    {
        StartCoroutine(LoadAsynchronously());
    }

    IEnumerator LoadAsynchronously()
    {
        if(SceneManager.GetActiveScene().name == "MenuUI")
            yield return UnloadMenu("MenuUI");
        else if (SceneManager.GetActiveScene().name == "GameEndUI")
            yield return UnloadMenu("GameEndUI");

        PlayBackgroundMusic(BackgroundMusicSO.BGClipList[1]);

        yield return screenFade.FadeIn();
        yield return new WaitForSeconds(1f);
        yield return StartCoroutine(LoadScene("Level"));
        SceneManager.SetActiveScene(SceneManager.GetSceneByName("Level"));

        yield return screenFade.FadeOut();
        GateAnimator.SetTrigger("GameStart");
        yield return new WaitForSeconds(2f);
        _timer.StartTimer(levelDuration);
    }

    bool UnloadMenu(string scene)
    {
        SceneManager.UnloadSceneAsync(scene);
        return true;
    }

    private IEnumerator UnloadCurrent()
    {
        AsyncOperation unloadOperation = SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());

        while (!unloadOperation.isDone)
            yield return null;
    }

    private IEnumerator LoadScene(string sceneName)
    {
        AsyncOperation loadOperation = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);

        while (!loadOperation.isDone)
            yield return null;
    }

    public void LevelComplete()
    {
        Debug.Log($"level is complete");
        StartCoroutine(EndLevel());     
    }
    private IEnumerator EndLevel()
    {
        PlayBackgroundMusic(BackgroundMusicSO.BGClipList[0]);

        GateAnimator.SetTrigger("GameEnd");
        yield return new WaitForSeconds(2f);
        yield return screenFade.FadeIn();
        yield return new WaitForSeconds(1f);
        yield return UnloadCurrent();
        yield return LoadScene("GameEndUI");
        SceneManager.SetActiveScene(SceneManager.GetSceneByName("GameEndUI"));
        yield return screenFade.FadeOut();
    }

    void QuitGame()
    {
        Application.Quit();
    }

    void PlayBackgroundMusic(AudioClip clip)
    {
        BGAudioSource.Stop();
        BGAudioSource.clip = clip;
        BGAudioSource.Play();
    }

    void PlayCustomerAudioEffect(AudioClip clip)
    {
        CustomerEffectsAudioSource.PlayOneShot(clip);
    }
}
