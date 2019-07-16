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
    [SerializeField] GameObject GoldenButterflyGroundPrefab;
    [SerializeField] GameObject LastGroundPrefab;
    GameObject GoldenButterflyGround;
    GameObject LastGround;
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
        GoldenButterflyGround = Instantiate(GoldenButterflyGroundPrefab);
        GoldenButterflyGround.SetActive(false);
        LastGround = Instantiate(LastGroundPrefab);
        LastGround.SetActive(false);
    }

    public void restartPoolObjects()
    {
        for (int i = 0; i < poolDictionary.Count; i++)
        {
            for (int j = 0; j < poolDictionary[i].Count; j++)
            {
                GameObject objects = poolDictionary[i].Dequeue();
                objects.SetActive(false);
                objects.GetComponent<TileGround>().restart();
                poolDictionary[i].Enqueue(objects);
            }
        }
    }

    public GameObject getButterflyParent()
    {
        return GoldenButterflyGround;
    }


    public GameObject spawnGround(Vector3 position, string tag)
    {
        GameObject ground;
        if (tag == "Golden")
            ground = GoldenButterflyGround;
        else
            ground = LastGround;

        ground.SetActive(true);
        ground.transform.position = position;

        return ground;
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
