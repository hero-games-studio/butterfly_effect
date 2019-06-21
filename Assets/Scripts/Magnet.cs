using System.Collections;
using UnityEngine;
using UnityEngine.UI;


//Magnet
public class Magnet : MonoBehaviour
{
    #region Variables

    Rigidbody rigid;

    [Header("Values")]
    [SerializeField] float speedDefaultValue;
    [SerializeField] float sensitivity = 1.5f;//for mobile
    [SerializeField] float sizeUpValue = 0.00625f;
    [SerializeField] float fieldOfViewUpValue = 0.1f;
    [SerializeField] float netOffsetZ = -5.0f;
    [SerializeField] float camOffsetZ = -7.0f;
    [SerializeField] float netPosY = 0.4f;
    [SerializeField] float camPosY = 1.0f;

    [SerializeField] Vector2[] bound = { new Vector2(-0.5f, -0.2f), new Vector2(0.375f, 2.2f) };
    [SerializeField] Vector2[] boundViewportPoint = { new Vector2(0.0f, 0.0f), new Vector2(1f, 1f) };
    float speed;

    private int partCounter;//counting part

    [SerializeField] int point;

    [Header("Camera")]
    [SerializeField] GameObject camObject;//camera
    [SerializeField] Camera cam;


    private Vector3 velocity;
    private Vector3 lastPosition;

    [Header("Managers")]
    [SerializeField] UIManager uiManager;
    [SerializeField] SceneManagementManager sceneManager;

    #endregion

    #region Functions

    private void Start()
    {
        speed = speedDefaultValue;
        Time.timeScale = 1f;
        rigid = this.GetComponent<Rigidbody>();

        lastPosition = transform.position;

        partCounter = 0;
        point = 0;
    }

    private void FixedUpdate()
    {
        moveComputer();

        if (Input.touchCount > 0 && speed!=0)
        {
            move();
            
        }
        rigid.AddForce(Vector3.forward * speed, ForceMode.Impulse);
        //transform.Translate(Vector3.forward * speed * Time.smoothDeltaTime);
        camObject.transform.position = new Vector3(camObject.transform.position.x, camObject.transform.position.y, transform.position.z - 2);

        velocity = (transform.position - lastPosition) / Time.deltaTime;
        lastPosition = transform.position;

        if (rigid.velocity.z > 2)
        {
            rigid.velocity = new Vector3(rigid.velocity.x, rigid.velocity.y, 2);
        }

        controlBoundry();

        if (Input.GetKeyDown(KeyCode.R))
        {
            sceneManager.resetActiveScene();
        }

    }

    void moveComputer()
    {

         if(speed!=0)
            transform.Translate(Input.GetAxisRaw("Horizontal") / 40, Input.GetAxisRaw("Vertical") / 40, 0,Space.World);

    }

    IEnumerator sizeUp()
    {//Size up effect

        uiManager.toggleSizeUpText(true);

        for (int i = 0; i < 20; i++)
        {

            transform.localScale += new Vector3(sizeUpValue, sizeUpValue, sizeUpValue);
            cam.fieldOfView += fieldOfViewUpValue;
            yield return null;
        }

        uiManager.toggleSizeUpText(false);
    }

    IEnumerator clearNet()//rotate net to clear the net end of the each part
    {
        for (int i = 15; i > 0; i--)
        {

            transform.localEulerAngles = new Vector3(i * 6, 0, 180);

            yield return null;
        }
        transform.localEulerAngles = new Vector3(0, 0, 180);
        yield return new WaitForSeconds(0.5f);
        for (int i = 0; i < 15; i++)
        {

            transform.localEulerAngles = new Vector3(i * 6, 0, 180);

            yield return null;
        }
        transform.localEulerAngles = new Vector3(90, 0, 180);

      
    }

    private void OnTriggerEnter(Collider coll)
    {
        if (coll.gameObject.tag == "Finish")
        {
            StartCoroutine(clearNet());
            wait();
        }
    }

    public void countPart()
    {
        partCounter++;
    }

    public Vector3 getVelocity()
    {
        return velocity;
    }


    void controlBoundry()//check boundries
    {
        Vector3 targetPosition = cam.WorldToViewportPoint(transform.position);
        float offset = 0.003747553f;

        if (targetPosition.x > boundViewportPoint[1].x)
        {
            this.transform.position = cam.ViewportToWorldPoint(new Vector3(boundViewportPoint[1].x,targetPosition.y,targetPosition.z));
        }
        else if (targetPosition.x < boundViewportPoint[0].x)
        {
            this.transform.position = cam.ViewportToWorldPoint(new Vector3(boundViewportPoint[0].x, targetPosition.y, targetPosition.z));
        }

        if (targetPosition.y < boundViewportPoint[0].y)
        {
            float a = transform.position.z;
            this.transform.position = cam.ViewportToWorldPoint(new Vector3(targetPosition.x, boundViewportPoint[0].y, targetPosition.z- offset));
        }
        else if (targetPosition.y > boundViewportPoint[1].y)
        {
            this.transform.position = cam.ViewportToWorldPoint(new Vector3(targetPosition.x, boundViewportPoint[1].y, targetPosition.z+ offset));
        }
        /*
        if (this.transform.position.x > bound[1].x)
        {
            this.transform.position = new Vector3(0.375f, transform.position.y, transform.position.z);
        }
        else if (this.transform.position.x < bound[0].x)
        {
            this.transform.position = new Vector3(-0.5f, transform.position.y, transform.position.z);
        }

        if (this.transform.position.y < bound[0].y)
        {
            this.transform.position = new Vector3(transform.position.x, -0.2f, transform.position.z);
        }
        else if (this.transform.position.y > bound[1].y)
        {
            this.transform.position = new Vector3(transform.position.x, 2.2f, transform.position.z);
        }
        */
    }

    void move()//Mobile control
    {
        Touch touch = Input.GetTouch(0);

        Vector3 localPosition = transform.position;
        localPosition.x += touch.deltaPosition.x * sensitivity;
        localPosition.y += touch.deltaPosition.y * sensitivity;
        transform.position = localPosition;

    }

    void wait()//wait for the gate
    {
        rigid.velocity = Vector3.zero;
        speed = 0;
    }

    public void goOn()//go on to the next part
    {
        speed = speedDefaultValue;
        StartCoroutine(sizeUp());
    }

    public void getPoint()
    {
        point++;
    }

    public void restartPart()//to reset the part
    {
        transform.position = new Vector3(0, netPosY, (partCounter * 14) + netOffsetZ);
        camObject.transform.position = new Vector3(0, camPosY, (partCounter * 14) + camOffsetZ);
        speed = speedDefaultValue;
    }

    public void restartScale()
    {
        transform.localScale = new Vector3(0.0875f, 0.0875f, 0.0875f);
        cam.fieldOfView = 60;
    }
    #endregion
}