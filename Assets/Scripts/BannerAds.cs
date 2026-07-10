using Unity.Services.LevelPlay;
using UnityEngine;

public class BannerAds : MonoBehaviour
{

    public static BannerAds Instance;

    private void Awake()
    {
        Instance = this;
    }

    [SerializeField] private string appKey = "26ed8889d";
    [SerializeField] private string bannerAdUnitId = "u8g25g2m0oj2ox1x";

    private LevelPlayBannerAd bannerAd;

    void Start()
    {
        LevelPlay.OnInitSuccess += OnInitSuccess;
        LevelPlay.OnInitFailed += OnInitFailed;

        LevelPlay.Init(appKey);
    }

    private void OnInitSuccess(LevelPlayConfiguration configuration)
    {
        Debug.Log("LevelPlay inicializado");

        bannerAd = new LevelPlayBannerAd(bannerAdUnitId);

        bannerAd.OnAdLoaded += BannerLoaded;
        bannerAd.OnAdLoadFailed += BannerLoadFailed;

        bannerAd.LoadAd();
    }

    private void OnInitFailed(LevelPlayInitError error)
    {
        Debug.LogError(error);
    }

    private void BannerLoaded(LevelPlayAdInfo adInfo)
    {
        Debug.Log("Banner cargado");

        bannerAd.ShowAd();
    }

    private void BannerLoadFailed(LevelPlayAdError error)
    {
        Debug.LogError(error);
    }

    public void HideBanner()
    {
        if (bannerAd != null)
        {
            Debug.Log("Ocultando Banner");
            bannerAd.HideAd();
        }
    }

   
}
