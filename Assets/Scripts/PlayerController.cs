using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerController : MonoBehaviour
{
    #region Variables

    [Header("Camera")]
    [SerializeField] CameraMovement camereMovement;

    [Header("Physics Components")]
    [SerializeField] private Rigidbody rigid;
    [Header("Animator Components")]
    [SerializeField] private Animator anim;

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
    [SerializeField] private float swipeY = 0.75f;


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

    [Header("Children")]
    [SerializeField] private GameObject body;

    private Touch touch = default(Touch);
    private Vector2 startPosition = Vector2.zero;
    private Vector3 firstPosition = new Vector3(0, 1.5f, -21);

    Player player;
    StageManager stageManager;
    GroundManager groundManager;
    UIManager uiManager;
    ParticleManager particleManager;
    private int currentGround;
    Vector3 defaultPosition;
    float coolDown;
    bool isDone;

    #endregion

    #region Functions

    private void Awake()
    {
        player = Player.getInstance();
        stageManager = StageManager.Instance;
        groundManager = GroundManager.Instance;
        uiManager = UIManager.Instance;
        particleManager = ParticleManager.Instance;

        defaultPosition = transform.position;

        currentLine = 1;

        player.setCurrentLine(currentLine);
    }

    private void Start()
    {
        currentGround = 0;
        coolDown = 0;
        isDone = false;

        uiManager.setActivePanel(false);
    }

    private void FixedUpdate()
    {
        checkPositionLimits();
        if (!isDone)
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

        coolDown -= Time.deltaTime;

        player.setCurrentLine(currentLine);
        player.setPosition(transform.position);

        if (!isDone)
        {
            run();
            movement();
        }

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
        if (!anim.GetBool("Jump"))
        {
            anim.SetBool("Jump", true);
            anim.SetBool("Duck", false);
            transform.DOMoveY(line[currentLine].y, jumpDuration, false);
            yield return new WaitForSeconds(jumpTime);
            transform.DOMoveY(defaultPosition.y, jumpDuration, false);
            anim.SetBool("Jump", false);
        }
    }

    IEnumerator duck()
    {
        if (!anim.GetBool("Duck"))
        {
            anim.SetBool("Duck", true);
            anim.SetBool("Jump", false);
            transform.DOMoveY(swipeY, duckDuration, false);
            body.transform.DOScaleY(0.25f, duckDuration);
            yield return new WaitForSeconds(duckTime);
            transform.DOMoveY(defaultPosition.y, duckDuration, false);
            body.transform.DOScaleY(1f, duckDuration);
            anim.SetBool("Duck", false);

        }
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
                if (currentLine == 2)
                {
                    camereMovement.goRight();
                }
                else if (currentLine == 1)
                {
                    camereMovement.goMiddle();
                }
                break;
            case "left":
                if (currentLine != 0)
                    currentLine--;
                transform.DOMoveX(line[currentLine].x, horizontalMoveDuration, false);
                player.setPosition(transform.position);
                if (currentLine == 1)
                {
                    camereMovement.goMiddle();
                }
                else if (currentLine == 0)
                {
                    camereMovement.goLeft();
                }
                break;
            case "jump":
                StartCoroutine(jump());
                break;
            case "duck":
                StartCoroutine(duck());
                break;

        }

    }

    void levelOver()
    {
        stageManager.stopRoutines();
        uiManager.setActivePanel(true);
        rigid.velocity = Vector3.zero;
        isDone = true;
        groundManager.setISDone(true);
        particleManager.finish();
    }

    public void restart()
    {
        transform.position = firstPosition;
        isDone = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Golden")
        {
            stageManager.touchGoldenGround();

        }
        if (other.gameObject.tag == "GoldenButterfly")
        {
            levelOver();
        }
        if (other.gameObject.tag == "Butterfly")
        {
            int count = player.getButterflyCount() + 1;
            player.setButterflyCount(count);
            Debug.Log(count);
        }
        if (other.gameObject.tag == "Last")
        {
            levelOver();
        }
    }

    void stumbleFalse()
    {
        anim.SetBool("Stumble", false);
         isDone = false;
    }

    private void OnCollisionEnter(Collision other)
    {

        if (other.gameObject.tag == "Obstacle")
        {
            Vector3 vel = rigid.velocity;
            vel.z=10;
            rigid.velocity=vel;

            particleManager.hitObstacle();
            anim.SetBool("Stumble", true);
            isDone = true;
            Invoke("stumbleFalse", 0.7f);
        }

    }
    #endregion
}

