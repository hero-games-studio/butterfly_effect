using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundManager : MonoSingleton<GroundManager>
{
    GroundPoolManager groundPoolManager;

    Vector3 nextGroundPosition;

    [Header("Variables")]
    [SerializeField] private Vector3 nextGroundOffsetZ = new Vector3(0, 0, 50);
    private Vector3 stepVector = new Vector3(0, 0, 400);

    [SerializeField] private GameObject[] groundArray;


    private void Start()
    {
        nextGroundPosition = nextGroundOffsetZ;
    }

    private void Awake()
    {
        groundPoolManager = GroundPoolManager.Instance;
    }

    public IEnumerator moveGround(int value)
    {
        yield return new WaitForSeconds(0.5f);
        groundArray[value].transform.position += stepVector;
    }

    public void groundSpawn(int number)
    {
        for (int i = 0; i < number; i++)
        {
            if (number / 2 == i)
            {
                groundPoolManager.spawnGround(nextGroundPosition);
            }
            else
            {
                int type = Random.Range(0, 12);

                groundPoolManager.spawnGround(type, nextGroundPosition);
            }
            nextGroundPosition += nextGroundOffsetZ;

        }
    }
}
