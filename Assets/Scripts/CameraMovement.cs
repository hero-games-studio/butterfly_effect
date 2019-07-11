using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

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
        targetPosition.x = transform.position.x;
        transform.position = targetPosition + offset;
    }

    public void goRight()
    {
        transform.DOMoveX(2, 0.5f, false);
    }

    public void goLeft()
    {
        transform.DOMoveX(-2, 0.5f, false);
    }

    public void goMiddle()
    {
        transform.DOMoveX(0, 0.5f, false);
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Ground")
        {
            if (currentGround == 3)
            {
                currentGround = 0;
            }
            StartCoroutine(groundManager.moveGround(currentGround));
            currentGround++;
        }
    }

}
