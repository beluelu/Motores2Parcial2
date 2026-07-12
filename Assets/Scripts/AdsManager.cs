using Unity.Services.LevelPlay;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AdsManager : MonoBehaviour
{
    [SerializeField] private string appKey = "26ed8889d";
    [SerializeField] private string rewardedAdUnitId = "xoj1eywp61681wge";

    private LevelPlayRewardedAd rewardedAd;

    private bool userFinishedVideo = false;

    private void Start()
    {
        CreateRewarded();
    }

    public void ShowRewardedAd()
    {
        Debug.Log("Mostrando Rewarded");

        Time.timeScale = 1f;
        AudioListener.pause = true;

        userFinishedVideo = false;

        if (rewardedAd != null)
        {
            rewardedAd.ShowAd();
        }
    }

    private void CreateRewarded()
    {
        Debug.Log("Creando Rewarded");

        rewardedAd = new LevelPlayRewardedAd(rewardedAdUnitId);

        rewardedAd.OnAdLoaded += OnRewardedLoaded;
        rewardedAd.OnAdLoadFailed += OnRewardedLoadFailed;
        rewardedAd.OnAdDisplayed += OnAdDisplayed;
        rewardedAd.OnAdClosed += OnAdClosed;
        rewardedAd.OnAdRewarded += OnAdRewarded;
        rewardedAd.OnAdDisplayFailed += OnAdDisplayFailed;

        rewardedAd.LoadAd();
    }

    private void OnRewardedLoaded(LevelPlayAdInfo adInfo)
    {
        Debug.Log("Rewarded cargada");
    }

    private void OnRewardedLoadFailed(LevelPlayAdError error)
    {
        Debug.LogError("Error al cargar Rewarded: " + error);
    }

    private void OnAdDisplayed(LevelPlayAdInfo adInfo)
    {
        Debug.Log("Anuncio en pantalla - Físicas del nivel congeladas");
        Time.timeScale = 0f;
    }

    private void OnAdClosed(LevelPlayAdInfo adInfo)
    {
        Debug.Log("Ventana del anuncio cerrada físicamente");

        if (userFinishedVideo == false)
        {
            Debug.Log("Cerró antes. Inyectando 10 monedas en tu DataManager nativo.");

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

            Time.timeScale = 1f;
            AudioListener.pause = false;

            SceneManager.LoadScene("MainMenu");
            return;
        }

        rewardedAd.LoadAd();
        AudioListener.pause = false;
    }

    private void OnAdRewarded(LevelPlayAdInfo adInfo, LevelPlayReward reward)
    {
        Debug.Log("¡Video completado! Reviviendo en carrera...");

        userFinishedVideo = true;

        PauseScreen pause = FindFirstObjectByType<PauseScreen>();
        if (pause != null)
        {
            Time.timeScale = 1f;
            pause.RevivePlayer();
        }
    }

    private void OnAdDisplayFailed(LevelPlayAdInfo adInfo, LevelPlayAdError error)
    {
        Debug.LogError("Error mostrando anuncio: " + error);
        Time.timeScale = 1f;
        AudioListener.pause = false;
    }
}

