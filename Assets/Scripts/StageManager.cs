using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoSingleton<StageManager>
{
    #region Variables

    [SerializeField] int currentLevel;

    GroundManager groundManager;

    GoldenButterfly goldenButterfly;

    #endregion

    #region Functions

    private void Awake()
    {
        if (PlayerPrefs.GetInt("Level") == 0)
        {
            PlayerPrefs.SetInt("Level",10);
        }
        groundManager = GroundManager.Instance;
        Application.targetFrameRate = 60;
        //currentLevel = PlayerPrefs.GetInt("Level");

    }

    private void Start()
    {
        setStageDesign();
        goldenButterfly = GameObject.Find("GoldenButterfly").GetComponent<GoldenButterfly>();

        normalMotion();
    }


    private void setStageDesign()
    {
        groundManager.groundSpawn(currentLevel * 10);
    }

    public void setLevel(int currentLevel)
    {
        this.currentLevel = currentLevel;
    }

    public void restart()
    {

    }

    public IEnumerator slowMotion()
    {

        while (true)
        {
            Time.timeScale -= 0.01f;

            if (Time.timeScale <= 0.01f)
            {
                break;
            }

            yield return null;
        }
        Time.timeScale = 0.01f;

    }

    public void normalMotion()
    {
        Time.timeScale = 1;
    }

    public void touchGoldenGround()
    {
        StartCoroutine(goldenButterfly.fly());
    }

    public void stopRoutines()
    {
        goldenButterfly.StopAllCoroutines();
    }

    public int getLevel()
    {
        return currentLevel;
    }

    #endregion

}
