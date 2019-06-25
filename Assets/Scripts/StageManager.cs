using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    #region Variables
    [Header("Blocks")]
    [SerializeField] private GameObject[] blocks;
    [SerializeField] private ButterflySpawner[] butterflySpawner;

    [SerializeField] Magnet magnet;

    [SerializeField] private int currentPart;
    [SerializeField] private int currentLevel;

    [SerializeField] private UIManager uiManager;

    [SerializeField] private Vector3 shitVector=new Vector3(0,0,42);

    #endregion

    #region Functions

    private void Start()
    {
        currentPart = 0;
        currentLevel = 1;
    }

    IEnumerator transformPart(int part)
    {
        yield return new WaitForSeconds(1);

        if (part == 3)
        {
            part = 0;
        }
      
        Vector3 pos = blocks[part].transform.position+shitVector;
        blocks[part].transform.position = pos;
        butterflySpawner[part].nextPart();

        yield return null;
    }

    public void nextPart()
    {
  
        StartCoroutine(transformPart(currentPart));
        currentPart++;
        if (currentPart == 3)
        {
            magnet.restartScale();
            currentLevel++;
            currentPart = 0;
        }
    }

    private void Update()
    {
        uiManager.setCurrentLevetText((currentLevel).ToString());
        uiManager.setNextLevetText((currentLevel + 1).ToString());
    }

    #endregion
}
