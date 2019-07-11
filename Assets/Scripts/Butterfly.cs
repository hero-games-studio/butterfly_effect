using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Butterfly : MonoBehaviour
{
    [SerializeField] Animation anim;
    [SerializeField] GameObject body;

    [SerializeField] ParticleSystem particle;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            //anim.Play();
            body.gameObject.SetActive(false);   
            particle.Play();
        }
    }
}
