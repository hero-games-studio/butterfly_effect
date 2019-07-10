using System.Collections;
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
    float coolDownDefaultValue = 500;
    float distanceLimitValue = 0;
    float moveYDuration = 0.5f;
    float force;

    int currentLine;

    Rigidbody rigid;
    #endregion

    #region Fuctions

    private void Awake()
    {
        player = Player.getInstance();

        rigid = this.GetComponent<Rigidbody>();
    }
    private void Start()
    {
        coolDown = 500;
        currentLine = 1;
    }

    private void drawRays()
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

    public IEnumerator fly()
    {
        transform.SetParent(null);

        while (true)
        {
            drawRays();

            coolDown -= coolDownCountDownValue;

            Vector3 tempraryVelocity = rigid.velocity;

            float distanceZ = transform.localPosition.z - player.getPosition().z;
            Debug.Log("Distance = " + distanceZ);
            if (currentLine == player.getCurrentLine())
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
                tempraryVelocity.z = player.getVelocity().z +force;
                
            }

            rigid.velocity = tempraryVelocity;

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
    #endregion
}
