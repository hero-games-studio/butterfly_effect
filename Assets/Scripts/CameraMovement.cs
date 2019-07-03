using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] GameObject player;
    Vector3 targetPosition;
    [SerializeField] Vector3 offset;


    private void FixedUpdate()
    {
        targetPosition.z = player.transform.position.z;
        transform.position = targetPosition + offset;
    }

}
