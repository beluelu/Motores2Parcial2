using Unity.Services.LevelPlay;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class AdsManager : MonoBehaviour
{
    [SerializeField] private string appKey = "26ed8889d";
    [SerializeField] private string rewardedAdUnitId = "xoj1eywp61681wge";

    private LevelPlayRewardedAd rewardedAd;

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
        Debug.Log("Despertando el tiempo de Unity antes de lanzar el anuncio...");

        Time.timeScale = 1f;
        AudioListener.pause = true;

        yield return new WaitForSecondsRealtime(0.15f);

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
        Debug.Log("Anuncio en pantalla fluido");
        Time.timeScale = 1f;
    }

    private void OnAdClosed(LevelPlayAdInfo adInfo)
    {
        Debug.Log("Ventana de LevelPlay cerrada por el usuario");

        AudioListener.pause = false;

        rewardedAd.LoadAd();
    }

    private void OnAdRewarded(LevelPlayAdInfo adInfo, LevelPlayReward reward)
    {
        Debug.Log("<color=green><b>[ADS REWARDED SUCCESS]</b> ¡Video completado! Procesando reinicio seguro...</color>");

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

    private void OnAdDisplayFailed(LevelPlayAdInfo adInfo, LevelPlayAdError error)
    {
        Debug.LogError("Error mostrando anuncio: " + error);
        Time.timeScale = 1f;
        AudioListener.pause = false;

        if (DataManager.Instance != null)
        {
            DataManager.Instance.currency += 10;
            DataManager.Instance.SaveData();
        }
        SceneManager.LoadScene("MainMenu");
    }
}

