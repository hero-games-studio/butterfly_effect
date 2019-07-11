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
        groundManager = GroundManager.Instance;
        Application.targetFrameRate = 60;
        //currentLevel = 1;

    }

    private void Start()
    {
        setStageDesign();
        goldenButterfly = GameObject.Find("GoldenButterfly").GetComponent<GoldenButterfly>();
    }


    private void setStageDesign()
    {
        groundManager.groundSpawn(currentLevel * 10);
    }

    public void setLevel(int currentLevel){
        this.currentLevel=currentLevel;
    }

    public void touchGoldenGround()
    {
        StartCoroutine(goldenButterfly.fly());
    }

    public void stopRoutines()
    {
        goldenButterfly.StopAllCoroutines();
    }

    public int getLevel(){
        return currentLevel;
    }

    #endregion

}
