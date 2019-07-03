using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Collections;

public class ButterflyManager : MonoBehaviour
{

    ButterflyPoolManager butterflyPoolManager;

    Vector3 nextSetPosition;

    [Header("Variables")]
    [SerializeField] private Vector3 nextButteflyOffsetZ = new Vector3(0, 0, 50);
    [SerializeField] private Vector3 firstSetPosition = new Vector3(0, 3.5f, -10);

    [SerializeField] private int butterflyCountRandomValueLowerLimit = 0;
    [SerializeField] private int butterflyCountRandomValueUpperLimit = 5;

    [SerializeField] [ReadOnly] private int typeRandomValueLowerLimit = 0;
    [SerializeField] [ReadOnly] private int typeRandomValueUpperLimit = 4;
    [SerializeField] [ReadOnly] private bool specialButterfly;

    private void Start()
    {
        typeRandomValueLowerLimit = 0;
        typeRandomValueUpperLimit = 4;

        nextSetPosition = firstSetPosition;

        butterflyPoolManager = ButterflyPoolManager.Instance;

        butterflySpawn();

        specialButterfly = false;
    }

    public void butterflySpawn()
    {
        if (specialButterfly)
        {
            typeRandomValueUpperLimit = 3;
        }

        int type = Random.Range(typeRandomValueLowerLimit, typeRandomValueUpperLimit);
        int count = Random.Range(butterflyCountRandomValueLowerLimit, butterflyCountRandomValueUpperLimit);

        Vector3 pos = nextSetPosition;

        if (type == 3)
        {
            count = 1;
            specialButterfly = true;
        }

        for (int i = 0; i < count; i++)
        {
            pos.z += i * 5;
            butterflyPoolManager.spawnButterfly(type, pos);
        }
        nextSetPosition += nextButteflyOffsetZ;

        Invoke("butterflySpawn", 1);
    }


}
