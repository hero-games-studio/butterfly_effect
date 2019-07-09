using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour
{

    Vector3 stepVector = new Vector3(0, 0, 600);
    bool wait;

    void Start()
    {
        wait = false;
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player" && !wait)
        {
            StartCoroutine("NextStep");
            wait=true;
        }
    }

    IEnumerator NextStep()
    {

        yield return new WaitForSeconds(0.5f);
        transform.localPosition = transform.localPosition + stepVector;
        wait=false;
    }

}
