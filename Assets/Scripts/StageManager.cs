﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoSingleton<StageManager>
{
    #region Variables

    [SerializeField] int currentLevel;

    GroundManager groundManager;
    [SerializeField] PlayerController playerController;
    UIManager uiManager;

    GoldenButterfly goldenButterfly;


    #endregion

    #region Functions

    private void Awake()
    {
        if (PlayerPrefs.GetInt("Level") == 0)
        {
            PlayerPrefs.SetInt("Level", 1);
        }
        groundManager = GroundManager.Instance;
        uiManager = UIManager.Instance;


        currentLevel = PlayerPrefs.GetInt("Level");

    }

    private void Start()
    {
        setStageDesign();
        goldenButterfly = GameObject.Find("GoldenButterfly").GetComponent<GoldenButterfly>();

        normalMotion();
    }


    private void setStageDesign()
    {
        groundManager.groundSpawn((currentLevel * 10) - currentLevel);
    }

    public void setLevel(int currentLevel)
    {
        this.currentLevel = currentLevel;
    }

    public void restart()
    {

    }

    public void levelUp()
    {
        groundManager.restart();
        currentLevel++;
        PlayerPrefs.SetInt("Level", currentLevel);
        uiManager.setActivePanel(false);
        playerController.restart();
        setStageDesign();
        goldenButterfly = GameObject.Find("GoldenButterfly").GetComponent<GoldenButterfly>();
    }

    public void normalMotion()
    {
        Time.timeScale = 1;
    }

    public void touchGoldenGround()
    {
        if (goldenButterfly != null)
            StartCoroutine(goldenButterfly.fly());
    }

    public void stopRoutines()
    {
        Destroy(goldenButterfly);
    }

    public int getLevel()
    {
        return currentLevel;
    }

    #endregion

}
