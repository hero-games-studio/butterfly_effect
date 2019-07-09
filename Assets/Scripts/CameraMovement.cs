using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] GameObject player;
    Vector3 targetPosition;
    [SerializeField] Vector3 offset;
    GroundManager groundManager;
    int currentGround;

    void Awake()
    {
        groundManager = GroundManager.Instance;

        currentGround = 0;
    }

    private void FixedUpdate()
    {
        targetPosition.z = player.transform.position.z;
        transform.position = targetPosition + offset;
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Ground")
        {
            if (currentGround == 3)
            {
                currentGround=0;    
            }
            StartCoroutine(groundManager.moveGround(currentGround));
            currentGround++;
        }
    }

}
