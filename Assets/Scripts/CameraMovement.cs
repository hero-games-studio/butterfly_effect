using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] ParticleSystem part1, part2;
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

    public void restart()
    {
        currentGround = 0;
        goMiddle();

    }

    public void goRight()
    {
        transform.DOMoveX(2.5f, 0.5f, false);
    }

    public void goLeft()
    {
        transform.DOMoveX(-2.5f, 0.5f, false);
    }

    public void goMiddle()
    {
        transform.DOMoveX(0, 0.5f, false);
    }

    public void setGround()
    {
        currentGround = 0;
    }
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Ground")
        {
            if (currentGround == 3)
            {
                currentGround = 0;
            }
            if (!groundManager.getISDone())
            {
                StartCoroutine(groundManager.moveGround(currentGround));
                currentGround++;
            }
        }
        if (other.gameObject.tag == "Last")
        {
            part1.Play();
            part2.Play();
        }
    }

}
