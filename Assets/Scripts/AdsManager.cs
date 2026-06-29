using Unity.Services.LevelPlay;
using UnityEngine;

public class AdsManager : MonoBehaviour
{
    [SerializeField] private string appKey = "26ed8889d";
    [SerializeField] private string rewardedAdUnitId = "xoj1eywp61681wge";

    private LevelPlayRewardedAd rewardedAd;

    private void Start()
    {
        LevelPlay.OnInitSuccess += OnInitSuccess;
        LevelPlay.OnInitFailed += OnInitFailed;

        LevelPlay.Init(appKey);
    }

    public void ShowRewardedAd()
    {
        if (rewardedAd != null)
        {
            rewardedAd.ShowAd();
        }
    }

    private void OnInitSuccess(LevelPlayConfiguration configuration)
    {
        Debug.Log("LevelPlay inicializado");

        rewardedAd = new LevelPlayRewardedAd(rewardedAdUnitId);
        rewardedAd.OnAdLoaded += OnRewardedLoaded;
        rewardedAd.OnAdLoadFailed += OnRewardedLoadFailed;

        rewardedAd.LoadAd();

        rewardedAd.OnAdDisplayed += OnAdDisplayed;
        rewardedAd.OnAdClosed += OnAdClosed;
        rewardedAd.OnAdRewarded += OnAdRewarded;
        rewardedAd.OnAdDisplayFailed += OnAdDisplayFailed;

        Debug.Log("Rewarded creada");
    }

    private void OnInitFailed(LevelPlayInitError error)
    {
        Debug.LogError("Error al inicializar: " + error);
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
        Debug.Log("Anuncio mostrado");
    }

    private void OnAdClosed(LevelPlayAdInfo adInfo)
    {
        Debug.Log("Anuncio cerrado");
    }

    private void OnAdRewarded(LevelPlayAdInfo adInfo, LevelPlayReward reward)
    {
        Debug.Log("Recompensa obtenida");
    }

    private void OnAdDisplayFailed(LevelPlayAdInfo adInfo, LevelPlayAdError error)
    {
        Debug.LogError("Error mostrando anuncio: " + error);
    }
}

