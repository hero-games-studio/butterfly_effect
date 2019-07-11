using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Butterfly : MonoBehaviour
{
    [SerializeField] Animation anim;
    [SerializeField] GameObject body;
    [SerializeField] Player player;

    [SerializeField] ParticleSystem particle;

    void Awake()
    {
        player=Player.getInstance();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            //anim.Play();
            body.gameObject.SetActive(false);
            particle.Play();
            if(this.gameObject.tag == "Butterfly")
                player.setButterflyCount(1+player.getButterflyCount());
        }
    }
}
