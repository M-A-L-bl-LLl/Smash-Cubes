using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class AdsManager : MonoBehaviour, IUnityAdsInitializationListener, IUnityAdsLoadListener, IUnityAdsShowListener
{
    [Header("TEST MODE!")]
    [SerializeField] private bool _testMode = true;
    [Space] 
    [Header("Android settings")]
    [SerializeField] private string _androidGameId;
    [SerializeField] private string _androidInterstitialName = "Interstitial_Android";

    [Space] 
    [Header("IOS settings")] 
    [SerializeField] private string _iosGameId;
    [SerializeField] private string _iosInterstitialName = "Interstitial_iOS";

     
    private string _gameId;
    private string _adId;

    private void Awake()
    {
        InitializeAds();

        _adId = (Application.platform == RuntimePlatform.IPhonePlayer)
            ? _iosInterstitialName
            : _androidInterstitialName;

        LoadAd();
    }

    public void LoadAd()
    {
        Debug.Log($"Loading Ad: {_adId}");
        Advertisement.Load(_adId, this);
    }
    public void ShowAd()
    {
        Debug.Log($"Showing Ad: {_adId}");
        Advertisement.Show(_adId, this);
    }

    private void InitializeAds()
    {
        _gameId = (Application.platform == RuntimePlatform.IPhonePlayer) ? _iosGameId : _androidGameId;
        Advertisement.Initialize(_gameId, _testMode, this);
    }

    public void OnInitializationComplete()
    {
        Debug.Log("Unity Ads initialization complete!");
    }

    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
        Debug.Log($"Unity Ads Initialization failed: {error.ToString()} - {message}");
    }

    public void OnUnityAdsAdLoaded(string placementId)
    {
        throw new NotImplementedException();
    }

    public void OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message)
    {
        throw new NotImplementedException();
    }

    public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message)
    {
        throw new NotImplementedException();
    }

    public void OnUnityAdsShowStart(string placementId)
    {
        throw new NotImplementedException();
    }

    public void OnUnityAdsShowClick(string placementId)
    {
        throw new NotImplementedException();
    }

    public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
    {
        LoadAd();
    }
}
