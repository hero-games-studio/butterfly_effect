using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButterflySpawner : MonoBehaviour
{
    #region Variables

    [Header("Prefab")]
    [SerializeField] GameObject butterflyPrefab;

    [Header("Random Values")]
    [SerializeField] float randomLowerLimit = -0.2f;
    [SerializeField] float randomUpperLimit = 0.2f;
    [SerializeField] float firstButterFlyRandomUpperLimitX = -0.2f;
    [SerializeField] float firstButterFlyRandomLowerLimitX = -0.25f;
    [SerializeField] float firstButterFlyRandomUpperLimitY = 0.175f;
    [SerializeField] float firstButterFlyRandomLowerLimitY = 1.7f;
    [Header("Values")]
    [SerializeField] int numberOfButterfly;
    [SerializeField] float butterflyOffsetZ = -0.2f;
    [Header("Pool")]
    [SerializeField] GameObject pool;

    [SerializeField] Vector2[] boundry = { new Vector2(-0.5f, -0.2f), new Vector2(0.375f, 2.2f) };

    [SerializeField] ObjectPoolManager objectPoolManager;

    [SerializeField] GameObject[] butterfliesArray;

    float length = 6.47f;

    #endregion

    #region Functions

    private void Start()
    {
        setButterflies(this.numberOfButterfly);
        generateButterflyPositions(this.numberOfButterfly);
    }

    GameObject spawnButterfly(Vector3 position)//Spawn butterfly in position
    {
        GameObject butterfly = Instantiate(butterflyPrefab, position, Quaternion.identity, this.gameObject.transform);
        return butterfly;
    }

    public void generateButterflyPositions(int numberOfButterfly)//set butterflies position automaticly
    {
        //GameObject[] butterflies = new GameObject[numberOfButterfly];

        GameObject[] butterflies = this.butterfliesArray;

        Vector3 previousButterflyPosition = Vector3.zero;

        float space = calculateSpace(numberOfButterfly);

        for (int i = 0; i < numberOfButterfly; i++)
        {
            Vector3 position;
            if (i == 0)
            {
                position = randomFirstButterflyPosition();
                //butterflies[i] = spawnButterfly(position);
                butterflies[i].transform.position = position;
            }
            else
            {
                position = randomButterflyPosition(previousButterflyPosition.x, previousButterflyPosition.y, (i * space) + butterflyOffsetZ);
                //butterflies[i] = spawnButterfly(position);
                butterflies[i].transform.position = position;
            }
            Objects butterflyObject = butterflies[i].GetComponent<Objects>();
            previousButterflyPosition = butterflies[i].transform.position;
            butterflies[i].transform.SetParent(this.transform);
            butterflies[i].SetActive(true);
            butterflyObject.setParentName();
            butterflyObject.setFirstPosition(butterflies[i].transform.position);
        }
        
    }

    float calculateSpace(int numberOfButterfly)//calculate space between butterflies
    {
        return length / numberOfButterfly;
    }

    public void setButterflies(int value)
    {
        butterfliesArray = new GameObject[value];
        butterfliesArray = objectPoolManager.getButterflies(value);
    }

    Vector3 randomButterflyPosition(float xPos, float yPos, float zPos)
    { //random position based on previous butterfly position
        float randomX = Random.Range(randomLowerLimit, randomUpperLimit);
        float randomY = Random.Range(randomLowerLimit, randomUpperLimit);
        float parentZ = transform.parent.transform.parent.position.z;

        Debug.Log(parentZ);
        if (xPos + randomX < boundry[0].x || xPos + randomX > boundry[1].x)
        {
            randomX = -randomX;
        }
        if (yPos + randomY < boundry[0].y || yPos + randomY > boundry[1].y)
        {
            randomY = -randomY;
        }

        Vector3 butterflyPosition = new Vector3(xPos + randomX, yPos + randomY, zPos + parentZ);

        return butterflyPosition;
    }

    Vector3 randomFirstButterflyPosition()
    {//random position for first butterfly

        float randomX = Random.Range(firstButterFlyRandomLowerLimitX, firstButterFlyRandomUpperLimitX);
        float randomY = Random.Range(firstButterFlyRandomLowerLimitY, firstButterFlyRandomUpperLimitY);
        float parentZ = transform.parent.transform.parent.position.z;

        Vector3 butterflyPosition = new Vector3(randomX, randomY, parentZ + butterflyOffsetZ);

        return butterflyPosition;
    }


    public void nextPart()
    {
        Debug.Log("aa");
        objectPoolManager.addElementToList(butterfliesArray);
        for (int i=0; i<butterfliesArray.Length; i++)
        {
            butterfliesArray[i].SetActive(false);
            butterfliesArray[i].transform.SetParent(pool.transform);       
        }
    }
    #endregion
}
