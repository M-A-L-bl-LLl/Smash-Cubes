using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Localization;


public class ScoreController : MonoBehaviour
{
    #region Dll

    [DllImport("__Internal")]
    private static extern bool IsMobile();
     
    public bool isMobile()
    {
#if !UNITY_EDITOR && UNITY_WEBGL
                 return IsMobile();
#endif
        return false;
    }

    [DllImport("__Internal")]
    private static extern void SetToLeaderboard(int value);
    #endregion
    public YandexSDK sdk;
    public static ScoreController instance;
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_Text finalScore;
    [SerializeField] private TMP_Text bestScore;
    [SerializeField] private TMP_Text finalScoreMoblie;
    [SerializeField] private TMP_Text bestScoreMobile;
    [SerializeField] private TMP_Text newBestScore;
    [SerializeField] private GameObject touchSlider;
    [Space] 
    [SerializeField] private LocalizedString _localizedFinalScore;
    [SerializeField] private LocalizedString _localizedBestScore;

    int score = 0;
    int newScore = 0;
    int hightScore;
    private int _scoreAdCount;
    private AdsManager _adsManager;

    private void OnEnable()
    {
        _localizedBestScore.Arguments = new object[] {hightScore};
        _localizedBestScore.StringChanged += UpdateTextB;
        _localizedFinalScore.Arguments = new object[] {score};
        _localizedFinalScore.StringChanged += UpdateTextF;
    }

    private void OnDisable()
    {
        _localizedBestScore.StringChanged -= UpdateTextB;
        _localizedFinalScore.StringChanged -= UpdateTextF;
    }

    private void UpdateTextB(string value)
    {
        if (isMobile() == false)
        {
            bestScore.text = value;
        }
        else
        {
            bestScoreMobile.text = value;
        }
    }
    private void UpdateTextF(string value)
    {
        if (isMobile()==false)
        {
            finalScore.text = value;
        }
        else
        {
            finalScoreMoblie.text = value;
        }
    }

    private void Awake()
    {
        instance = this;
        
    }


    private void Start()
    {
        //_adsManager = GameObject.Find("UnityAds").GetComponent<AdsManager>();
        scoreText.text = score.ToString();
        hightScore = Progress.Instance.PlayerInfo.BestScore;
        Debug.Log(PlayerPrefs.GetInt("hightScore"));
        
        sdk.onRewardedAdReward += ShowReward;
        sdk.onRewardedAdOpened += BonusOpen;
        sdk.onRewardedAdClosed += BonusClose;
        sdk.onRewardedAdError += SDKNull;
        sdk.onInterstitialShown += SDKNull;
        sdk.onInterstitialFailed += SDKNull;
        
        //sdk.ShowInterstitial();
    }

    public void ShowReward(string parametr)
    {

        //название метода можно изменить.

    }
    //--------------события, на который подписываемся для показа рекламы---------------------
    void BonusOpen(int i)
    {
        //здесь прописываем действия при открытии рекламы поверх игры, обычно это постановка на паузу
    }
    void BonusClose(int i)
    {
        //здесь прописываем действия при закрытии рекламы, обычно это снятие с паузы
    }
    void SDKNull(int i)
    {

    }
    void SDKNull(string s)
    {

    }
    void SDKNull()
    {

    }
    public void ShowFinalResault()
    {
        
        
        hightScore = Progress.Instance.PlayerInfo.BestScore;
        
        //finalScore.text = "score:" + score.ToString();
        _localizedFinalScore.Arguments[0] = score;
        _localizedFinalScore.RefreshString();
        //bestScore.text = "best score:" + hightScore.ToString();
        _localizedBestScore.Arguments[0] = hightScore;
        _localizedBestScore.RefreshString();
        touchSlider.SetActive(false);
    }

    public void AddScore()
    {        
        newScore = CubeCollision.cubeNumberToScore;
        score += newScore;
        if (_scoreAdCount / 4000 >= 1)
        {
            //_adsManager.ShowAd();
            sdk.ShowInterstitial();
            _scoreAdCount = 0;
        }
        else
        {
            _scoreAdCount += newScore;
        }
        scoreText.text = score.ToString();
        if (hightScore < score)
        {
            newBestScore.gameObject.SetActive(true);
            Progress.Instance.PlayerInfo.BestScore = score;
            Progress.Instance.Save();
            SetToLeaderboard(Progress.Instance.PlayerInfo.BestScore);
            PlayerPrefs.SetInt("hightScore", score);
        }

        Debug.Log(PlayerPrefs.GetInt("hightScore"));
    }
}
