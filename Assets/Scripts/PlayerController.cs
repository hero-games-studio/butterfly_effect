using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerController : MonoBehaviour
{
    #region Variables

    [Header("Physics Components")]
    [SerializeField] private Rigidbody rigid;

    [Header("Movement Variables")]
    [Range(1, 50)] [SerializeField] private float speed = 2.5f;
    [SerializeField] private float positionRightLimit = 3.0f;
    [SerializeField] private float positionLeftLimit = -3.0f;
    [SerializeField] private float horizontalMoveDuration = 0.3f;
    [SerializeField] private Vector3[] line;
    [SerializeField] private int currentLine;
    [Header("Jump Variables")]
    [SerializeField] private float jumpDuration = 0.3f;
    [SerializeField] private float jumpTime = 0.3f;
    [Header("Duck Variables")]
    [SerializeField] private float duckDuration = 0.3f;
    [SerializeField] private float duckTime = 0.3f;


    [Header("Velocity Variables")]
    [SerializeField] private float velocityUpperLimit = 50;
    [SerializeField] private float velocityLowerLimit = 30;
    [Range(0.1f, 2.0f)] [SerializeField] private float rateofVelocityChange = 0.1f;

    [Header("Swipe Variables")]
    [SerializeField] private float minimumSwipeDistanceY;
    [SerializeField] private float minimumSwipeDistanceX;
    [SerializeField] private float timeDifferenceLimit = 0.5f;
    private float startTime;

    [Header("Commands")]
    [SerializeField] private string jumpCommand = "jump";
    [SerializeField] private string duckCommand = "duck";
    [SerializeField] private string rightCommand = "right";
    [SerializeField] private string leftCommand = "left";

    private Touch touch = default(Touch);
    private Vector2 startPosition = Vector2.zero;

    Player player;
    StageManager stageManager;

    Vector3 defaultPosition;


    #endregion

    #region Functions

    private void Awake()
    {
        player = Player.getInstance();
        stageManager = StageManager.Instance;

        defaultPosition = transform.position;

        currentLine = 0;
    }

    private void Start()
    {
    }

    private void FixedUpdate()
    {
        checkPositionLimits();
        controlVelocity();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            command(jumpCommand);
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            command(duckCommand);
        }
        else if (Input.GetKeyUp(KeyCode.A))
        {
            command(leftCommand);
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            command(rightCommand);
        }

        run();
        movement();
    }


    void movement()
    {
        if (Input.touches.Length > 0)
        {
            touch = Input.touches[0];

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    startPosition = touch.position;
                    startTime = Time.time;
                    break;
                case TouchPhase.Moved:
                    Vector3 positionDelta = (Vector2)touch.position - startPosition;

                    float timeDifference = Time.time - startTime;
                    bool timeOut = timeDifference > timeDifferenceLimit;

                    if (Mathf.Abs(positionDelta.y) > Mathf.Abs(positionDelta.x))
                    {
                        if (!timeOut)
                        {
                            if (positionDelta.y > 0 && Mathf.Abs(positionDelta.y) > minimumSwipeDistanceY)//Up
                            {
                                command(jumpCommand);
                            }
                            else
                            {
                                command(duckCommand);
                            }
                        }
                    }
                    else
                    {
                        if (!timeOut)
                        {
                            if (positionDelta.x > 0 && Mathf.Abs(positionDelta.x) > minimumSwipeDistanceX && !timeOut)//Right
                            {
                                command(rightCommand);
                            }
                            else//left
                            {
                                command(leftCommand);
                            }
                           
                        }
                    }
                    startTime = 0;
                    break;
            }
        }
    }

    IEnumerator jump()
    {
        transform.DOMoveY(line[currentLine].y, jumpDuration, false);
        yield return new WaitForSeconds(jumpTime);
        transform.DOMoveY(defaultPosition.y, jumpDuration, false);
    }

    IEnumerator duck()
    {
        transform.DOScaleY(0.25f, duckDuration);
        yield return new WaitForSeconds(duckTime);
        transform.DOScaleY(1f, duckDuration);
    }

    void run()
    {
        rigid.AddForce(Vector3.forward * speed);
        player.setVelocity(rigid.velocity);
    }

    void controlVelocity()
    {
        Vector3 velocity = rigid.velocity;

        if (velocity.z > velocityUpperLimit)
        {
            velocity.z -= rateofVelocityChange;
        }
        else if (velocity.z < velocityLowerLimit)
        {
            velocity.z += rateofVelocityChange;
        }

        rigid.velocity = velocity;
    }


    void checkPositionLimits()
    {
        Vector3 pos = transform.position;

        if (pos.x < positionLeftLimit)
        {
            pos.x = positionLeftLimit;
        }
        else if (pos.x > positionRightLimit)
        {
            pos.x = positionRightLimit;
        }

        transform.position = pos;
    }


    void command(string direction)
    {
        switch (direction)
        {
            case "right":
                if (currentLine != 2)
                    currentLine++;
                transform.DOMoveX(line[currentLine].x, horizontalMoveDuration, false);
                 player.setPosition(transform.position);
                break;
            case "left":
                if (currentLine != 0)
                    currentLine--;
                transform.DOMoveX(line[currentLine].x, horizontalMoveDuration, false);
                 player.setPosition(transform.position);
                break;
            case "jump":
                StartCoroutine(jump());
                break;
            case "duck":
                StartCoroutine(duck());
                break;
        }

    }

    public Vector3 getVelocity(){
        return rigid.velocity;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Golden")
        {
            stageManager.touchGoldenGround();
        }
        if(other.gameObject.tag== "GoldenButterfly"){
            stageManager.stopRoutines();
        }
    }

    #endregion
}

