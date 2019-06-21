using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Finish : MonoBehaviour
{

    [SerializeField] Counter counter;
    [SerializeField] UIManager uiManager;

    private void OnTriggerEnter(Collider coll)
    {
        if (coll.gameObject.tag == "Magnet")
        {
            counter.setTimeScale(1);//start timer to control the goal
        }
    }
}
