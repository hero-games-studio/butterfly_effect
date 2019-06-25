using System.Collections;
using UnityEngine;

//Ground
public class Ground : MonoBehaviour
{
    [SerializeField]
    Counter counter;
    private int objectCount;
    float time;
    [SerializeField] Animator anim;
    private void Start()
    {
        objectCount = 0;
        time = 0;
        StartCoroutine(move());

    }

    private void Update()
    {
        if (time < 3)
            time += Time.deltaTime;
    }

    private void OnTriggerEnter(Collider coll)//count the objects inside the ground object
    {
        if (coll.gameObject.tag == "Butterfly" && time <= 3)
        {
            counter.setObjectCount(++objectCount);
        }
        if (coll.gameObject.tag == "Magnet")
        {
            anim.SetBool("InTrap", true);
        }
    }

    IEnumerator move()
    {//move ground to use OnTriggerEnter() function 

        for (int i = 0; i > 0; i--)
        {
            transform.localPosition = new Vector3(i * 10, 0, 0);
            yield return null;
        }
        transform.localPosition = new Vector3(0, 0, 0);

    }

}
