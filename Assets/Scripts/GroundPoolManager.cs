using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundPoolManager : MonoSingleton<GroundPoolManager>
{
    #region GroundPool Class
    [System.Serializable]
    public class GroundPool
    {
        public int groundType;
        public int length;

        public GameObject groundPrefab;
    }
    #endregion

    #region Pools
    [Header("Pools")]
    [SerializeField] List<GroundPool> pools;
    Dictionary<int, Queue<GameObject>> poolDictionary;
    #endregion

    #region Functions

    private void Awake()
    {
        poolDictionary = new Dictionary<int, Queue<GameObject>>();

        foreach (GroundPool pool in pools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();

            for (int i = 0; i < pool.length; i++)
            {
                GameObject ground = Instantiate(pool.groundPrefab);
                ground.SetActive(false);
                objectPool.Enqueue(ground);
            }
            poolDictionary.Add(pool.groundType, objectPool);
        }
    }

    public GameObject spawnGround(int groundType, Vector3 position)
    {
        if (!poolDictionary.ContainsKey(groundType))
        {
            return null;
        }

        GameObject ground = poolDictionary[groundType].Dequeue();

        ground.SetActive(true);
        ground.transform.position = position;

        poolDictionary[groundType].Enqueue(ground);

        return ground;
    }

    #endregion
}
