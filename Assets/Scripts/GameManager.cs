using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GameManagerNameSpace
{
    public class GameManager : MonoBehaviour
    {
        // for debugging - skip loading main menu and load level
        [Header("Debug level")]
        public bool isLoadMainMenu;
        public bool isLoadLevel;
        public bool unlimitedTime;

        [Header("Level settings")]
        public ScreenFade screenFade = null;
        public Scene currentScene;
        public float levelDuration = 60f;
        [SerializeField] private Timer _timer;
        public Animator GateAnimator;

        [Header("Events")]
        public GameManagerEventChannelSO Startbutton;
        public GameManagerEventChannelSO QuitButton;

        [Header("Audio")]
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
            if (unlimitedTime)
                levelDuration = 999f;
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
        private IEnumerator LoadAsynchronously()
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
        private bool UnloadMenu(string scene)
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
        private void QuitGame()
        {
            Application.Quit();
        }
        private void PlayBackgroundMusic(AudioClip clip)
        {
            BGAudioSource.Stop();
            BGAudioSource.clip = clip;
            BGAudioSource.Play();
        }
        private void PlayCustomerAudioEffect(AudioClip clip)
        {
            CustomerEffectsAudioSource.PlayOneShot(clip);
        }
    }
}