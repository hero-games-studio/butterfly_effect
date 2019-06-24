using UnityEngine;
using TMPro;
//Counter in the final hole
public class Counter : MonoBehaviour
{
    #region Variables
    [Header("Values")]
    [SerializeField] private int goal, perfectGoal;
    [SerializeField] private int count;
    [SerializeField] private int objectCount;
    [SerializeField] private ParticleSystem sunshine;

    private bool perfectControl;

    [SerializeField] private TextMeshPro countText;

    [SerializeField] private float time, timeScale;

    [Header("Success Rates")]
    [SerializeField] private float rate = 0.6f, perfectRate = 0.8f;

    private Animator anim;
    [Header("Net")]
    [SerializeField] private Magnet magnet;

    [Header("Particles")]
    [SerializeField] private ParticleSystem[] successParticleEffect;
    [SerializeField] private ParticleSystem perfectParticleEffect;

    [Header("Managers")]
    [SerializeField] UIManager uiManager;
    [SerializeField] StageManager stageManager;

    [SerializeField] ButterflySpawner butterflySpawner;

    #endregion

    #region Functions

    private void Start()
    {
        anim = GetComponentInParent<Animator>();

        time = 0;
        timeScale = 0;
        count = 0;

        perfectControl = false;
    }

    private void Update()
    {

        time += Time.deltaTime * timeScale;
        //waiting to balls reach the finish hole
        if (time > 5 && count < goal)//control goal
        {
            time = timeScale = 0;
            restartPart();
        }

        countText.text = count + "/" + goal;
        if (count >= goal && !anim.GetBool("Gate") && goal > 0)//control goal
        {
            success();
        }
        if (perfectGoal <= count && !perfectControl)//control perfect
        {
            perfectControl = true;
            perfect();
        }

    }

    void success()//over successfully
    {
       // Handheld.Vibrate();

        uiManager.toggleOverLevelText(true);

        for (int i = 0; i < successParticleEffect.Length; i++)
        {

            successParticleEffect[i].Play();

        }

        anim.SetBool("Gate", true);

        magnet.countPart();

        Invoke("waitAllBalls",0.2f);

        Invoke("goOn", 2.0f);//call goOn() after 2 seconds


    }

    void waitAllBalls()
    {
        if (perfectControl)
        {
            uiManager.setOverLevelText("Perfect!");
        }
        else
        {
            uiManager.setOverLevelText("Successfully Completed!");
        }
    }

    void goOn()//reached the goal
    {
        nextPart();
        magnet.goOn();
        uiManager.toggleOverLevelText(false);
        uiManager.setOverLevelText("");

    }

    private void OnTriggerEnter(Collider coll)//count the object in the final hole
    {
        if (coll.gameObject.tag == "Butterfly")
        {
            count++;
        }
    }

    void perfect()//over perfect
    {
        sunshine.Play();
        perfectParticleEffect.Play();
    }

    public void setObjectCount(int objectCount)
    {
        this.objectCount = objectCount;
        setGoal();
    }

    public int getCount()
    {
        return count;
    }

    private void setGoal()//set the goal with number of object on the block
    {
        float temp = objectCount * rate;
        goal = Mathf.RoundToInt(temp);
        temp = objectCount * perfectRate;
        perfectGoal = Mathf.RoundToInt(temp);
    }

    public void setTimeScale(float timeScale)
    {
        this.timeScale = timeScale;
    }

    void nextPart()
    {
        int randomCount = Random.Range(3,10);
        setObjectCount(randomCount);
        stageManager.nextPart();
        count = 0;
        time = 0;
        timeScale = 0;
        anim.SetBool("Gate", false);
        perfectControl = false;
        butterflySpawner.setButterflies(randomCount);
        butterflySpawner.generateButterflyPositions(randomCount);
    }

    void restartPart()//to reset the part
    {
        string parentName = transform.root.gameObject.name;
        GameObject[] objects = GameObject.FindGameObjectsWithTag("Butterfly");
        for (int i = 0; i < objects.Length; i++)
        {
            string objectParentName = objects[i].GetComponent<Objects>().getParentName();
            if (parentName == objectParentName)
            {
                objects[i].GetComponent<Objects>().reSetPosition();
            }
        }
        magnet.restartPart();

        count = 0;
    }
    #endregion
}
