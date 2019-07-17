using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Facebook.Unity;
using GameAnalyticsSDK;


public class GameManager : MonoBehaviour
{

    void Start()
    {
        FB.Init(OnFacebookInitialize);
    }

    void OnFacebookInitialize()
    {
        if (FB.IsInitialized)
        {
            FB.ActivateApp();
        }
    }

    void Awake()
    {
        Application.targetFrameRate = 60;
        GameAnalytics.Initialize();
    }

}
