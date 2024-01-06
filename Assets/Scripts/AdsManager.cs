using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
using System;
//using GoogleMobileAds.Api.Mediation.Chartboost;
public class AdsManager : MonoBehaviour
{
    public static AdsManager instance;
    string inerstitial_Ad_Id = "ca-app-pub-3940256099942544/1033173712";
    string Banner_Ad_Id = "ca-app-pub-3940256099942544/6300978111";
    string Reward_Ad_Id = "ca-app-pub-3940256099942544/5224354917";

    private InterstitialAd interstitial_Ad;
    private BannerView Banner_Ad;
    private RewardedAd rewarded_Ad;



    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(this);
    }
    void Start()
    {


        MobileAds.Initialize(initStatus => { });
        RequestInterstitial();
        // RequestBanner();
        LoadrewadedAd();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void RequestBanner()
    {



        this.Banner_Ad = new BannerView(Banner_Ad_Id, AdSize.Banner, AdPosition.Top);

        // Called when an ad request has successfully loaded.
        this.Banner_Ad.OnAdLoaded += this.HandleOnAdLoaded;
        // Called when an ad request failed to load.
        this.Banner_Ad.OnAdFailedToLoad += this.HandleOnAdFailedToLoad;
        // Called when an ad is clicked.
        this.Banner_Ad.OnAdOpening += this.HandleOnAdOpened;
        // Called when the user returned from the app after an ad click.
        this.Banner_Ad.OnAdClosed += this.HandleOnAdClosed;
        // Called when the ad click caused the user to leave the application.
        // this.Banner_Ad.OnAdLeavingApplication += this.HandleOnAdLeavingApplication;

        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();

        // Load the banner with the request.
        this.Banner_Ad.LoadAd(request);
    }
    public void hidebaaner()
    {
        this.Banner_Ad.Hide();
    }
    public void RequestInterstitial()
    {


        // Initialize an InterstitialAd.
        this.interstitial_Ad = new InterstitialAd(inerstitial_Ad_Id);

        // Called when an ad request has successfully loaded.
        this.interstitial_Ad.OnAdLoaded += HandleOnAdLoaded;
        // Called when an ad request failed to load.
        this.interstitial_Ad.OnAdFailedToLoad += HandleOnAdFailedToLoad;
        // Called when an ad is shown.
        this.interstitial_Ad.OnAdOpening += HandleOnAdOpened;
        // Called when the ad is closed.
        this.interstitial_Ad.OnAdClosed += HandleOnAdClosed;
        // Called when the ad click caused the user to leave the application.
        // this.interstitial_Ad.OnAdLeavingApplication += HandleOnAdLeavingApplication;

        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();
        // Load the interstitial with the request.
        this.interstitial_Ad.LoadAd(request);
    }
    public void Showinterstitial()
    {
        if (this.interstitial_Ad.IsLoaded())
        {
            this.interstitial_Ad.Show();
        }
    }

    public void LoadrewadedAd()
    {


        this.rewarded_Ad = new RewardedAd(Reward_Ad_Id);
        //Chartboost.AddDataUseConsent(CBGDPRDataUseConsent.Behavioral);
        // Called when an ad request has successfully loaded.
        this.rewarded_Ad.OnAdLoaded += HandleRewardedAdLoaded;
        // Called when an ad request failed to load.
        // this.rewarded_Ad.OnAdFailedToLoad += HandleRewardedAdFailedToLoad;
        // Called when an ad is shown.
        this.rewarded_Ad.OnAdOpening += HandleRewardedAdOpening;
        // Called when an ad request failed to show.
        this.rewarded_Ad.OnAdFailedToShow += HandleRewardedAdFailedToShow;
        // Called when the user should be rewarded for interacting with the ad.
        this.rewarded_Ad.OnUserEarnedReward += HandleUserEarnedReward;
        // Called when the ad is closed.
        this.rewarded_Ad.OnAdClosed += HandleRewardedAdClosed;

        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();
        // Load the rewarded ad with the request.
        this.rewarded_Ad.LoadAd(request);
    }
    public void showvideoad()
    {
        if (rewarded_Ad.IsLoaded())
        {
            rewarded_Ad.Show();

        }
        else
        {
            Debug.Log("Rewarded videoad not loaded");
        }
    }
    private void HandleRewardedAdClosed(object sender, EventArgs e)
    {
        //MonoBehaviour.print("HandleRewardedAdClosed event closing");
        LoadrewadedAd();
        // Chartboost.AddDataUseConsent(CBGDPRDataUseConsent.NonBehavioral);
    }

    private void HandleUserEarnedReward(object sender, Reward e)
    {
        // SimpleCity.AI.AiDirector.instance.calledmoneystate();
        Uimanager.instance.getrewwrd();
    }

    private void HandleRewardedAdFailedToShow(object sender, AdErrorEventArgs e)
    {
        MonoBehaviour.print(
            "HandleRewardedAdFailedToShow event received with message: "
                             + e.Message);
    }

    private void HandleRewardedAdOpening(object sender, EventArgs e)
    {
        MonoBehaviour.print("HandleRewardedAdOpening event received");
    }

    private void HandleRewardedAdFailedToLoad(object sender, AdErrorEventArgs e)
    {
        MonoBehaviour.print(
            "HandleRewardedAdFailedToLoad event received with message: "
                             + e.Message);
    }

    private void HandleRewardedAdLoaded(object sender, EventArgs e)
    {
        MonoBehaviour.print("HandleRewardedAdLoaded event received");
    }

    private void HandleOnAdLeavingApplication(object sender, EventArgs e)
    {
        MonoBehaviour.print("hl e r");
    }

    private void HandleOnAdClosed(object sender, EventArgs e)
    {
        Debug.Log("ad closed");
        RequestInterstitial();
    }

    private void HandleOnAdOpened(object sender, EventArgs e)
    {
        MonoBehaviour.print(" h e r");
    }

    private void HandleOnAdFailedToLoad(object sender, AdFailedToLoadEventArgs e)
    {
        // Debug.Log("coldnit load" + e.Message);
    }

    private void HandleOnAdLoaded(object sender, EventArgs e)
    {
        Debug.Log("adloades");
    }
}
