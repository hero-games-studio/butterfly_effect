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

    #endregion

    #region Functions

    private void Start()
    {
        currentPart = 0;
        currentLevel = 0;
    }

    private void transformPart(int part)
    {


        Debug.Log("transformPart(" + part + ")");
        Vector3 pos = blocks[part].transform.position;
        blocks[part].transform.position = new Vector3(pos.x, pos.y, pos.z + 42);
        butterflySpawner[part].nextPart();

       
    }

    public void nextPart()
    {

        transformPart(currentPart);
        currentPart++;
        if (currentPart == 3)
        {
            magnet.restartScale();
            currentLevel++;
            currentPart = 0;
        }
    }

    #endregion
}
