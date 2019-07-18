using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameAnalyticsSDK;

public class StageManager : MonoSingleton<StageManager>
{
    #region Variables

    [SerializeField] int currentLevel;

    GroundManager groundManager;
    [SerializeField] PlayerController playerController;
    UIManager uiManager;
    Player player;

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
        player = Player.getInstance();


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
        int stageLength = 15 + (currentLevel * 2);

        groundManager.groundSpawn(stageLength);
        GameAnalytics.NewProgressionEvent(GAProgressionStatus.Start, Application.version, PlayerPrefs.GetInt("Level"));

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
        GameAnalytics.NewProgressionEvent(GAProgressionStatus.Complete, Application.version, PlayerPrefs.GetInt("Level").ToString(), player.getButterflyCount().ToString());
    }

    public void setSlide(bool slide)
    {
        goldenButterfly.setSlide(slide);
    }

    public void normalMotion()
    {
        Time.timeScale = 1;
    }

    public void touchGoldenGround()
    {
        try
        {
            if (goldenButterfly != null)
                StartCoroutine(goldenButterfly.fly());
        }
        catch
        {

        }
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
