using Unity.Services.LevelPlay;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InterstitialAds : MonoBehaviour
{
    [SerializeField] private string interstitialAdUnitId = "0es1fn59hwgg8otk";

    private LevelPlayInterstitialAd interstitialAd;

    private string sceneToLoad;

    private void Start()
    {
        interstitialAd = new LevelPlayInterstitialAd(interstitialAdUnitId);

        interstitialAd.OnAdLoaded += OnAdLoaded;
        interstitialAd.OnAdLoadFailed += OnAdLoadFailed;
        interstitialAd.OnAdClosed += OnAdClosed;

        interstitialAd.LoadAd();
    }

    public void ShowInterstitial(string sceneName)
    {
        sceneToLoad = sceneName;

        if (interstitialAd != null)
            interstitialAd.ShowAd();
    }

    private void OnAdLoaded(LevelPlayAdInfo adInfo)
    {
        Debug.Log("Interstitial cargado");
    }

    private void OnAdLoadFailed(LevelPlayAdError error)
    {
        Debug.LogError(error);
    }

    private void OnAdClosed(LevelPlayAdInfo adInfo)
    {
        LoadingManager.LoadScene(sceneToLoad);

        interstitialAd.LoadAd();
    }
}
