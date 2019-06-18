using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolManager : MonoBehaviour
{

    [SerializeField] private GameObject[] butterflyPool;
    [SerializeField] private bool[] butterflyPoolState;
    [SerializeField] private int lastObjectIndex;

    [SerializeField] List<GameObject> pool = new List<GameObject>();

    private void Start()
    {
        for(int i=0; i<butterflyPool.Length; i++)
        {
            pool.Add(butterflyPool[i]);
        }
    }
   
    public GameObject[] getButterflies(int value)
    {
        GameObject[] tempObjects = new GameObject[value];

        for (int i = 0; i < value; i++)
        {
            tempObjects[i] =pool[pool.Count-1];
            pool.RemoveAt(pool.Count-1);
        }

        return tempObjects;
    }

    public void addElementToList(GameObject[] objects)
    {
        for(int i=0; i<objects.Length; i++)
        {
            pool.Add(objects[i]);
        }

    }

}
