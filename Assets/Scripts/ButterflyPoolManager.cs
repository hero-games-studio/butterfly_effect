using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButterflyPoolManager : MonoSingleton<ButterflyPoolManager>
{
    #region Butterfly Pool
    [System.Serializable]
    public class ButterflyPool
    {
        public int butterflyType;
        public int length;

        public GameObject butterflyPrefab;
    }
    #endregion

    #region Pools
    [Header("Pools")]
    [SerializeField]  List<ButterflyPool> pools;
    Dictionary<int, Queue<GameObject>> poolDictionary;
    #endregion

    public void Awake()
    {
        poolDictionary = new Dictionary<int, Queue<GameObject>>();

        foreach(ButterflyPool pool in pools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();

            for(int i=0; i<pool.length; i++)
            {
                GameObject butterfly = Instantiate(pool.butterflyPrefab);
                butterfly.SetActive(false);
                objectPool.Enqueue(butterfly);
            }

            poolDictionary.Add(pool.butterflyType, objectPool);

        }
    }

    public GameObject spawnButterfly(int butterflyType,Vector3 position)
    {
        if (!poolDictionary.ContainsKey(butterflyType))
            return null;

        GameObject butterfly = poolDictionary[butterflyType].Dequeue();

        butterfly.transform.position = position;
        butterfly.SetActive(true);

        poolDictionary[butterflyType].Enqueue(butterfly);

        return butterfly;

    }

}
