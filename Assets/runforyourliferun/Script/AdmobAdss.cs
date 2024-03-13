using UnityEngine.Events;
using UnityEngine;
using GoogleMobileAds;
using GoogleMobileAds.Api;
using GoogleMobileAds.Common;
using UnityEngine.UI;
using System;
using System.Collections.Generic;

public class AdmobAdss : MonoBehaviour
{
    

    private RewardedAd rewardedAd;
    private InterstitialAd interstitialAd;
    private BannerView _bannerView;

#if UNITY_ANDROID
    public string BanerAd_ID;
    public string interstitialAD_ID;
    public string RewardedAd_ID;
#elif UNITY_IPHONE
  public string BanerAd_ID;
  public string interstitialAD_ID;
  public string RewardedAd_ID ;
#else
  public string BanerAd_ID = "unused";
  public string interstitialAD_ID = "unused";
  public string RewardedAd_ID = "unused";
#endif





    public static AdmobAdss instance;
    /// <summary>
    /// Creates a 320x50 banner at top of the screen.
    /// Loads the rewarded ad.
    /// Loads the interstitial ad.
    /// </summary>
    void Start()
    {
        MobileAds.Initialize((InitializationStatus initStatus) =>
        {
            // This callback is called once the MobileAds SDK is initialized.
        });

        
        // Get singleton reward based video ad reference.
        CreateBannerView();
        LoadInterstitialAd();
        LoadRewardedAd();


        rewardedAd.OnAdFullScreenContentClosed += OnAdFullScreenContentClosed;
        rewardedAd.OnAdFullScreenContentFailed += OnAdFullScreenContentFailed;
        
        interstitialAd.OnAdFullScreenContentFailed += OnAdFullScreenContentFailedIN;
        interstitialAd.OnAdFullScreenContentClosed += OnAdFullScreenContentClosedIN;

    }
    private void Update()
    {
        
    }

    public void CreateBannerView()
    {
        Debug.Log("Creating banner view");

        // If we already have a banner, destroy the old one.
        if (_bannerView != null)
        {
            DestroyAd();
        }

        // Create a 320x50 banner at top of the screen
        _bannerView = new BannerView(BanerAd_ID, AdSize.Banner, AdPosition.Top);
    }
    public void LoadAd()
    {
        // create an instance of a banner view first.
        if (_bannerView == null)
        {
            CreateBannerView();
        }
        // create our request used to load the ad.
        var adRequest = new AdRequest();
        adRequest.Keywords.Add("unity-admob-sample");

        // send the request to load the ad.
        Debug.Log("Loading banner ad.");
        _bannerView.LoadAd(adRequest);
    }
    public void DestroyAd()
    {
        if (_bannerView != null)
        {
            Debug.Log("Destroying banner ad.");
            _bannerView.Destroy();
            _bannerView = null;
        }
    }

    public void LoadRewardedAd()
    {
        // Clean up the old ad before loading a new one.
        if (rewardedAd != null)
        {
            rewardedAd.Destroy();
            rewardedAd = null;
        }

        Debug.Log("Loading the rewarded ad.");

        // create our request used to load the ad.
        var adRequest = new AdRequest();

        // send the request to load the ad.
        RewardedAd.Load(RewardedAd_ID, adRequest,
            (RewardedAd ad, LoadAdError error) =>
            {
              // if error is not null, the load request failed.
              if (error != null || ad == null)
              {
                    Debug.LogError("Rewarded ad failed to load an ad " +
                                   "with error : " + error);
                    return;
              }

                Debug.Log("Rewarded ad loaded with response : "
                          + ad.GetResponseInfo());

                rewardedAd = ad;


            });
    }

    public void ShowRewardedAd()
    {
        const string rewardMsg =
            "Rewarded ad rewarded the user. Type: {0}, amount: {1}.";

        if (rewardedAd != null && rewardedAd.CanShowAd())
        {
            rewardedAd.Show((Reward reward) =>
            {
                // TODO: Reward the user.
                Debug.Log(String.Format(rewardMsg, reward.Type, reward.Amount));
                GameObject.FindObjectOfType<Canvas_Manger>().Resume();
            });
        }
    }

    public void LoadInterstitialAd()
    {
        // Clean up the old ad before loading a new one.
        if (interstitialAd != null)
        {
            interstitialAd.Destroy();
            interstitialAd = null;
        }

        Debug.Log("Loading the interstitial ad.");

        // create our request used to load the ad.
        var adRequest = new AdRequest();
        adRequest.Keywords.Add("unity-admob-sample");

        // send the request to load the ad.
        InterstitialAd.Load(interstitialAD_ID, adRequest,
            (InterstitialAd ad, LoadAdError error) =>
            {
              // if error is not null, the load request failed.
              if (error != null || ad == null)
                {
                    Debug.LogError("interstitial ad failed to load an ad " +
                                   "with error : " + error);
                    return;
                }

                Debug.Log("Interstitial ad loaded with response : "
                          + ad.GetResponseInfo());

                interstitialAd = ad;
            });

    }
    public void ShowAd()
    {
        if (interstitialAd != null && interstitialAd.CanShowAd())
        {
            Debug.Log("Showing interstitial ad.");
            interstitialAd.Show();
            GameObject.FindObjectOfType<Canvas_Manger>().Reaplay();
        }
        else
        {
            Debug.LogError("Interstitial ad is not ready yet.");
            GameObject.FindObjectOfType<Canvas_Manger>().Reaplay();
        }
    }

    

    public void OnAdFullScreenContentClosed()
    {
        Debug.Log("Rewarded Ad full screen content closed.");
        LoadRewardedAd();
    }
    public void OnAdFullScreenContentFailed(AdError error)
    {
        Debug.LogError("Rewarded ad failed to open full screen content " +
                          "with error : " + error);

        // Reload the ad so that we can show another as soon as possible.
        LoadRewardedAd();
    }
    public void OnAdFullScreenContentFailedIN(AdError error)
    {
        Debug.LogError("Rewarded ad failed to open full screen content " +
                          "with error : " + error);

        // Reload the ad so that we can show another as soon as possible.
        LoadInterstitialAd();
    }
    public void OnAdFullScreenContentClosedIN()
    {
        Debug.Log("Rewarded Ad full screen content closed.");
        LoadInterstitialAd();
    }
}