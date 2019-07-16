using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundManager : MonoSingleton<GroundManager>
{
    GroundPoolManager groundPoolManager;
    [SerializeField] CameraMovement cameraMovement;
    bool isDone;

    Vector3 nextGroundPosition;

    [Header("Variables")]
    [SerializeField] private Vector3 nextGroundOffsetZ = new Vector3(0, 0, 50);
    private Vector3 stepVector = new Vector3(0, 0, 400);
    private Vector3 firstGroundPosition = new Vector3(-21.58825f, -50.125f, 100);
    [SerializeField] Vector3[] groundFirstPositions;

    [SerializeField] private GameObject[] groundArray;
    Player player;


    private void Start()
    {
        nextGroundPosition = nextGroundOffsetZ;
        isDone = false;
    }

    private void Awake()
    {
        groundPoolManager = GroundPoolManager.Instance;
        player = Player.getInstance();
    }

    public IEnumerator moveGround(int value)
    {
        yield return new WaitForSeconds(0.5f);
        if (!isDone)
            groundArray[value].transform.position += stepVector;
    }

    public void restart()
    {
        StopAllCoroutines();
        cameraMovement.StopAllCoroutines();
        cameraMovement.setGround();
        nextGroundPosition = nextGroundOffsetZ;

        for (int i = 0; i < groundArray.Length; i++)
        {
            groundArray[i].transform.localPosition = groundFirstPositions[i];
        }

        groundPoolManager.restartPoolObjects();

        Invoke("startMoveGround", 1);
    }

    void startMoveGround()
    {
        isDone=false;
    }

    public void setISDone(bool setting)
    {
        isDone = setting;
    }
    public bool getISDone()
    {
        return isDone;
    }
    public void groundSpawn(int number)
    {

        for (int i = 0; i < number; i++)
        {
            if (number / 2 == i)
            {
                groundPoolManager.spawnGround(nextGroundPosition, "Golden");
            }
            else
            {
                int type = Random.Range(0, 23);

                groundPoolManager.spawnGround(type, nextGroundPosition);
            }
            nextGroundPosition += nextGroundOffsetZ;

        }
        groundPoolManager.spawnGround(nextGroundPosition, "Last");
    }
}
