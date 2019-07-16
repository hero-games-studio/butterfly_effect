using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileGround : MonoBehaviour
{
    [SerializeField] GameObject[] butterflies;
    [SerializeField] GameObject[] Obstacles;


    public void restart()
    {
        for (int i = 0; i < butterflies.Length; i++)
        {
            butterflies[i].SetActive(true);
        }
        for (int i = 0; i < Obstacles.Length; i++)
        {
            Obstacles[i].SetActive(true);
        }

    }

}
