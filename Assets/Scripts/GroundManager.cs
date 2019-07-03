using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundManager : MonoBehaviour
{
    GroundPoolManager groundPoolManager;

    Vector3 nextGroundPosition;

    [Header("Variables")]
    [SerializeField] private Vector3 nextGroundOffsetZ = new Vector3(0, 0, 50);

    private void Start()
    {
        groundPoolManager = GroundPoolManager.Instance;

        nextGroundPosition = nextGroundOffsetZ;

        groundSpawn();
    }

    void groundSpawn()
    {
        int type = Random.Range(0, 12);

        groundPoolManager.spawnGround(type, nextGroundPosition);

        nextGroundPosition += nextGroundOffsetZ;

        Invoke("groundSpawn", 1);
    }
}
