using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GoldenButterfly : Butterfly
{
    #region Variables
    Vector3 sameLineVelocityOffset = new Vector3(0, 0, -3);
    Vector3 differentLineVelocityOffset = new Vector3(0, 0, 2);

    Player player;

    float coolDown = 300;
    float upperPositionY = 4;
    float lowerPositionY = 1;
    float coolDownCountDownValue = 0.5f;
    float coolDownDefaultValue = 500;
    float distanceLimitValue = 1.75f;
    float moveYDuration = 0.5f;

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


        if (hitForward && hit.collider.gameObject.tag == "Butterfly")
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
        if (hitForward && hit.collider.gameObject.tag == "Obstacle")
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
                if (transform.position.x == 0)
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
        while (true)
        {
            drawRays();
            Vector3 playerPosition = player.getPosition();

            coolDown -= coolDownCountDownValue;
            Debug.Log(coolDown);
            float distance = playerPosition.x - transform.localPosition.x;
            distance = Mathf.Abs(distance);

            if (distance > distanceLimitValue)
            {
                rigid.velocity = player.getVelocity() + sameLineVelocityOffset;
            }
            else
            {
                rigid.velocity = player.getVelocity() + differentLineVelocityOffset;
            }

            yield return new WaitForFixedUpdate();
        }
    }

    private IEnumerator goDown()
    {
        transform.DOLocalMoveY(lowerPositionY, moveYDuration, false);
        yield return null;
    }

    private IEnumerator goUp()
    {
        transform.DOLocalMoveY(upperPositionY, moveYDuration, false);
        yield return null;
    }

    private IEnumerator goLeft()
    {
        Debug.Log("left");
        distanceLimitValue = 0;
        transform.DOLocalMoveX(-2.75f, moveYDuration, false);
        yield return new WaitForSeconds(moveYDuration + 1);
        distanceLimitValue = 1.75f;
    }

    private IEnumerator goRight()
    {
        Debug.Log("right");
        distanceLimitValue = 0;
        transform.DOLocalMoveX(2.75f, moveYDuration, false);
        yield return new WaitForSeconds(moveYDuration + 1);
        distanceLimitValue = 1.75f;

    }

    private IEnumerator goMiddle()
    {
        distanceLimitValue = 0;
        transform.DOLocalMoveX(0, moveYDuration, false);
        yield return new WaitForSeconds(moveYDuration + 1);
        distanceLimitValue = 1.75f;
    }

    #endregion




}
