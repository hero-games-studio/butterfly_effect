using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    Animation anim;
    void Awake()
    {
        anim=GetComponent<Animation>();
    }
    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {
            anim.Play();
        }
    }

}
