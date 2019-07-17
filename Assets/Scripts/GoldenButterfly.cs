﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GoldenButterfly : Butterfly
{
    #region Variables

    Player player;

    float coolDown = 300;
    float upperPositionY = 5;
    float lowerPositionY = 1;
    float coolDownCountDownValue = 0.5f;
    float coolDownDefaultValue = 200;
    float distanceLimitValue = 0;
    float moveYDuration = 0.5f;
    float force;
    float lastGroundYTarget = 25;
    float lastGroundVerticalDuration = 200;

    bool slide;

    int currentLine;

    bool breakBool = false;

    GroundPoolManager groundPoolManager;

    Rigidbody rigid;
    #endregion

    #region Fuctions

    private void Awake()
    {
        player = Player.getInstance();
        groundPoolManager = GroundPoolManager.Instance;

        rigid = this.GetComponent<Rigidbody>();
    }
    private void Start()
    {
        coolDown = coolDownDefaultValue;
        currentLine = 1;
        breakBool = false;
        slide = false;
    }

    public void setSlide(bool slide)
    {
        this.slide = slide;
    }

    private void drawRays()
    {
        try
        {

            Vector3 origin = transform.position + Vector3.forward;
            Vector3 directionForward = Vector3.forward;
            Vector3 directionRight = Vector3.forward + Vector3.right;
            Vector3 directionLeft = Vector3.forward + Vector3.left;

            RaycastHit hit;

            bool hitForward = Physics.Raycast(origin, directionForward, out hit, 25);
            Debug.DrawLine(origin, directionForward, Color.red, 25);


            if (hitForward && (hit.collider.gameObject.tag == "Butterfly" || hit.collider.gameObject.tag == "Obstacle"))
            {
                if (transform.position.y == upperPositionY)
                {
                    StartCoroutine(goDown());
                }
                else if (transform.position.y == lowerPositionY)
                {
                    StartCoroutine(goUp());
                }
            }
            if (coolDown <= 0)
            {

                if (!hitForward)
                {
                    if (transform.position.x == 0)//middle
                    {
                        int randomValue = Random.Range(0, 3);
                        Debug.Log(randomValue);
                        if (randomValue == 0)
                        {
                            StartCoroutine(goLeft());
                        }
                        else if (randomValue == 1)
                        {
                            StartCoroutine(goRight());
                        }

                        coolDown = coolDownDefaultValue;
                    }
                    else
                    {
                        int randomValue = Random.Range(0, 2);
                        Debug.Log(randomValue);
                        if (randomValue == 0)
                        {
                            StartCoroutine(goMiddle());
                        }

                        coolDown = coolDownDefaultValue;
                    }
                }
            }
        }
        catch
        {
            Debug.Log("Hata!");
        }
    }

    public IEnumerator fly()
    {
        transform.SetParent(null);

        while (true)
        {
            try
            {
                drawRays();

                coolDown -= coolDownCountDownValue;

                Vector3 tempraryVelocity = rigid.velocity;

                float distanceZ = transform.localPosition.z - player.getPosition().z;

                if (currentLine == player.getCurrentLine() && !slide)
                {
                    if (distanceZ > 20)
                    {
                        force = 1.5f;
                    }
                    else
                    {
                        force += Time.deltaTime;
                    }

                    tempraryVelocity.z = player.getVelocity().z - force;
                }
                else
                {
                    force = 0.25f;
                    tempraryVelocity.z = player.getVelocity().z + force;

                }

                rigid.velocity = tempraryVelocity;



            }
            catch
            {

            }
            yield return new WaitForFixedUpdate();
        }
    }


    private IEnumerator goDown()
    {
        rigid.DOMoveY(lowerPositionY, moveYDuration, false);
        yield return new WaitForSeconds(1);
        rigid.DOMoveY(upperPositionY, moveYDuration, false);
    }

    private IEnumerator goUp()
    {
        rigid.DOMoveY(upperPositionY, moveYDuration, false);
        yield return null;
    }

    private IEnumerator goLeft()
    {
        currentLine = 0;
        distanceLimitValue = 0;
        rigid.DOMoveX(-2.75f, moveYDuration, false);
        yield return new WaitForSeconds(moveYDuration + 1);
        distanceLimitValue = 1.75f;
    }

    private IEnumerator goRight()
    {
        currentLine = 2;
        distanceLimitValue = 0;
        rigid.DOMoveX(2.75f, moveYDuration, false);
        yield return new WaitForSeconds(moveYDuration + 1);
        distanceLimitValue = 1.75f;

    }

    private IEnumerator goMiddle()
    {
        currentLine = 1;
        distanceLimitValue = 0;
        rigid.DOMoveX(0, moveYDuration, false);
        yield return new WaitForSeconds(moveYDuration + 1);
        distanceLimitValue = 1.75f;
    }

    void OnDestroy()
    {
        StopAllCoroutines();
    }

    void restart()
    {
        breakBool = true;
        coolDown = coolDownDefaultValue;
        currentLine = 1;
        print("REstart");
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Last")
        {
            transform.DOMoveY(lastGroundYTarget, lastGroundVerticalDuration, false);
        }
        if (other.gameObject.tag == "Player")
        {

        }
    }
    #endregion
}
