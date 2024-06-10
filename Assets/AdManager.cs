using UnityEngine;
using UnityEngine.Advertisements;

public class AdManager : MonoBehaviour, IUnityAdsInitializationListener, IUnityAdsLoadListener, IUnityAdsShowListener
{
    [SerializeField] string _androidBannerAdUnitId = "Banner_Android";
    [SerializeField] string _androidInterstitialAdUnitId = "Interstitial_Android";
    [SerializeField] string _iOSBannerAdUnitId = "Banner_iOS";
    [SerializeField] string _iOSInterstitialAdUnitId = "Interstitial_iOS";
    private string _bannerAdUnitId;
    private string _interstitialAdUnitId;
    private bool _isInterstitialAdReady = false;

    void Awake()
    {
        _bannerAdUnitId = (Application.platform == RuntimePlatform.IPhonePlayer) ? _iOSBannerAdUnitId : _androidBannerAdUnitId;
        _interstitialAdUnitId = (Application.platform == RuntimePlatform.IPhonePlayer) ? _iOSInterstitialAdUnitId : _androidInterstitialAdUnitId;
    }

    void Start()
    {
        Advertisement.Initialize(_bannerAdUnitId, true, this);
        LoadBannerAd();
        LoadInterstitialAd();
    }

    #region Banner
    public void LoadBannerAd()
    {
        Advertisement.Banner.SetPosition(BannerPosition.BOTTOM_CENTER);
        var options = new BannerLoadOptions
        {
            loadCallback = OnBannerLoaded,
            errorCallback = OnBannerError
        };
        Advertisement.Banner.Load(_bannerAdUnitId, options);
    }

    public void ShowBannerAd()
    {
        Advertisement.Banner.Show(_bannerAdUnitId);
    }

    public void HideBannerAd()
    {
        Advertisement.Banner.Hide();
    }

    private void OnBannerLoaded()
    {
        Debug.Log("Banner ad loaded successfully.");
        ShowBannerAd();
    }

    private void OnBannerError(string message)
    {
        Debug.LogError("Banner ad failed to load: " + message);
    }
    #endregion

    #region Interstitial
    public void LoadInterstitialAd()
    {
        Advertisement.Load(_interstitialAdUnitId, this);
    }

    public void ShowInterstitialAd()
    {
        if (_isInterstitialAdReady)
        {
            Advertisement.Show(_interstitialAdUnitId, this);
        }
        else
        {
            Debug.Log("Interstitial ad is not ready yet.");
        }
    }
    #endregion

    #region IUnityAdsInitializationListener
    public void OnInitializationComplete()
    {
        Debug.Log("Unity Ads initialization complete.");
    }

    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
        Debug.LogError($"Unity Ads Initialization Failed: {error.ToString()} - {message}");
    }
    #endregion

    #region IUnityAdsLoadListener
    public void OnUnityAdsAdLoaded(string adUnitId)
    {
        Debug.Log($"Ad loaded: {adUnitId}");
        if (adUnitId == _interstitialAdUnitId)
        {
            _isInterstitialAdReady = true;
        }
    }

    public void OnUnityAdsFailedToLoad(string adUnitId, UnityAdsLoadError error, string message)
    {
        Debug.LogError($"Error loading Ad Unit {adUnitId}: {error.ToString()} - {message}");
    }
    #endregion

    #region IUnityAdsShowListener
    public void OnUnityAdsShowFailure(string adUnitId, UnityAdsShowError error, string message)
    {
        Debug.LogError($"Error showing Ad Unit {adUnitId}: {error.ToString()} - {message}");
    }

    public void OnUnityAdsShowStart(string adUnitId)
    {
        Debug.Log($"Ad started: {adUnitId}");
    }

    public void OnUnityAdsShowClick(string adUnitId)
    {
        Debug.Log($"Ad clicked: {adUnitId}");
    }

    public void OnUnityAdsShowComplete(string adUnitId, UnityAdsShowCompletionState showCompletionState)
    {
        Debug.Log($"Ad completed: {adUnitId} - {showCompletionState.ToString()}");
        if (adUnitId == _interstitialAdUnitId)
        {
            _isInterstitialAdReady = false;
            LoadInterstitialAd(); // Load another ad after the current one is shown
        }
    }
    #endregion
}
