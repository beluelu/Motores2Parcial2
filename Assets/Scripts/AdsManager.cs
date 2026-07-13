using Unity.Services.LevelPlay;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class AdsManager : MonoBehaviour
{
    [SerializeField] private string appKey = "26ed8889d";
    [SerializeField] private string rewardedAdUnitId = "xoj1eywp61681wge";

    private LevelPlayRewardedAd rewardedAd;

    [Header("Interfaz de Penalización")]
    public GameObject canvasGameOverSeleccionado;

    private float timeWhenAdStarted = 0f;
    private bool userEarnedReward = false;

    private void Start()
    {
        CreateRewarded();
    }

    public void ShowRewardedAd()
    {
        StartCoroutine(SafeShowAdRoutine());
    }

    private IEnumerator SafeShowAdRoutine()
    {
        Time.timeScale = 1f;
        AudioListener.pause = true;

        userEarnedReward = false;
        timeWhenAdStarted = 0f;

        yield return new WaitForSecondsRealtime(0.15f);

        if (rewardedAd != null)
        {
            rewardedAd.ShowAd();
        }
    }

    private void CreateRewarded()
    {
        rewardedAd = new LevelPlayRewardedAd(rewardedAdUnitId);

        rewardedAd.OnAdLoaded += OnRewardedLoaded;
        rewardedAd.OnAdLoadFailed += OnRewardedLoadFailed;
        rewardedAd.OnAdDisplayed += OnAdDisplayed;
        rewardedAd.OnAdClosed += OnAdClosed;
        rewardedAd.OnAdRewarded += OnAdRewarded;
        rewardedAd.OnAdDisplayFailed += OnAdDisplayFailed;

        rewardedAd.LoadAd();
    }

    private void OnRewardedLoaded(LevelPlayAdInfo adInfo) { }

    private void OnRewardedLoadFailed(LevelPlayAdError error) { }

    private void OnAdDisplayed(LevelPlayAdInfo adInfo)
    {
        Time.timeScale = 1f;
        timeWhenAdStarted = Time.realtimeSinceStartup;
    }

    private void OnAdClosed(LevelPlayAdInfo adInfo)
    {
        AudioListener.pause = false;

        float totalSecondsWatched = Time.realtimeSinceStartup - timeWhenAdStarted;

        // Si cerró antes de los 4.5 segundos reales de reloj
        if (userEarnedReward == false || totalSecondsWatched < 4.5f)
        {
            if (DataManager.Instance != null)
            {
                DataManager.Instance.currency += 10;
                DataManager.Instance.SaveData();
            }
            else
            {
                int currentCoins = PlayerPrefs.GetInt("MonedasTotales", 0);
                PlayerPrefs.SetInt("MonedasTotales", currentCoins + 10);
                PlayerPrefs.Save();
            }

            Section.isGameOver = true;
            Time.timeScale = 0f;

            if (canvasGameOverSeleccionado != null)
            {
                canvasGameOverSeleccionado.SetActive(true);
            }
            else
            {
                PauseScreen pause = FindFirstObjectByType<PauseScreen>();
                if (pause != null) pause.GameOver();
            }

            rewardedAd.LoadAd();
            return;
        }

        rewardedAd.LoadAd();
    }

    private void OnAdRewarded(LevelPlayAdInfo adInfo, LevelPlayReward reward)
    {
        float totalSecondsWatched = Time.realtimeSinceStartup - timeWhenAdStarted;

        if (totalSecondsWatched >= 4.5f)
        {
            userEarnedReward = true;

            if (GameManager.instance != null)
            {
                PlayerPrefs.SetInt("MonedasGuardadasAd", GameManager.instance.currentCoins);
                PlayerPrefs.SetInt("VengoDeAd", 1);
                PlayerPrefs.Save();
            }

            Time.timeScale = 1f;
            AudioListener.pause = false;

            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    private void OnAdDisplayFailed(LevelPlayAdInfo adInfo, LevelPlayAdError error)
    {
        Time.timeScale = 1f;
        AudioListener.pause = false;
    }
}

