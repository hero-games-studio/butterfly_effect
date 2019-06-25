using UnityEngine;

//Object on the ground
public class Objects : MonoBehaviour {
    #region Variables

    bool finish;
    bool magnetField;

    Vector3 firstPosition;
    [Header("Values")]
    [SerializeField] float speed=1;
    [SerializeField] float randomSpeedUpperLimit = 0.8f;
    [SerializeField] float randomSpeedLowerLimit = 1.2f;
    [SerializeField] float magnetEffectPower = 2.0f;

    Material material;

    [SerializeField] GameObject magnet;
    [SerializeField] GameObject magnetFieldObject;
    [SerializeField] Magnet magnetScript;

    [SerializeField] GameObject pointPrefab;

    Rigidbody rigid;

    Animator anim;

    ParticleSystem particle;

    string parentName;

    private bool inPart;

    #endregion

    #region Functions

    public void OnEnable()//first settings
    {
        inPart = false;

        anim = GetComponentInChildren<Animator>();
        rigid = this.GetComponent<Rigidbody>();
        particle = this.GetComponent<ParticleSystem>();
        material = this.GetComponent<Material>();

        finish = false;
        magnetField = false;

        setFlySpeed();

        rigid.velocity = Vector3.zero;

        parentName = transform.parent.transform.parent.gameObject.name;

        firstPosition = transform.position;

        rigid.isKinematic = true;
    }

    void Update()
    {
        if (finish && magnetScript.getSuccess() == true)//going to counter
        {
            rigid.velocity = Vector3.zero;
            transform.Translate(0, Time.deltaTime * speed, Time.deltaTime * speed*1.2f, Space.World);
        }
    }


    private void OnCollisionStay(Collision coll) {
        //...to fix the pass through problem
        if (coll.gameObject.tag == "Player" && magnetField)
        {
            rigid.velocity = magnet.GetComponent<Magnet>().getVelocity();
        }
    }

    private void OnTriggerEnter(Collider coll)
    {
        if (coll.gameObject.tag == "Finish")
        {
            rigid.velocity = Vector3.zero;
            Invoke("setFinish", 0.5f);
        }
        if (coll.gameObject.tag == "stop")
        {
            Debug.Log("Stop");
            finish = false;
            rigid.isKinematic = true;
        }
        if (coll.gameObject.tag == "CatchArea")//+1 effect
        {

            Instantiate(pointPrefab,transform.position,Quaternion.identity);
            particle.Play();
        }
    }

    private void OnTriggerStay(Collider coll)
    {
        //magnet effect
        if (coll.gameObject.tag == "MagnetField")
        {
            magnetEffect();
            rigid.isKinematic = false;
        }
        if (coll.gameObject.tag == "CatchArea" && magnetField)
        {
           // rigid.velocity = magnet.GetComponent<Magnet>().getVelocity();
        }
    }
    
    public bool getInPart()
    {
        return inPart;
    }

    public void setInPart(bool value)
    {
        inPart = value;
    }

    public string getParentName()
    {
        return parentName;
    }

    public void setParentName()
    {
        parentName= transform.parent.transform.parent.gameObject.name;
    }

    void setFinish()
    {
        finish = true;
    }

    void setFlySpeed()//set animation speed random
    {
        float speed= Random.Range(randomSpeedLowerLimit, randomSpeedUpperLimit);
        anim.SetFloat("animSpeed", speed);
    }

    public void setFirstPosition(Vector3 pos)
    {
        firstPosition = pos;
    }

    public void reSetPosition()//to reset the part
    {
        rigid.velocity = Vector3.zero;
        this.transform.localEulerAngles = Vector3.zero;
        transform.position = firstPosition;
        rigid.isKinematic = true;
        magnetField = false;
        anim.SetBool("catched", false);

        finish = false;
    }



    void magnetEffect()
    {
        Vector3 targetPosition;
        magnetField = true;
        if (this.transform.position.z > magnetFieldObject.transform.position.z)
        {
            targetPosition = new Vector3(magnetFieldObject.transform.position.x,
                                           magnetFieldObject.transform.position.y,
                                           this.transform.position.z);
            magnetEffectPower = 2;
        }
        else
        {
            targetPosition = magnetFieldObject.transform.position;
            magnetEffectPower = 5;
        }
      
        transform.position = Vector3.Lerp(this.transform.position, targetPosition, magnetEffectPower * Time.deltaTime);
        anim.SetBool("catched", true);

    }
    #endregion
}
