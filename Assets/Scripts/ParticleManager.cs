using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleManager : MonoSingleton<ParticleManager>
{
    [SerializeField] ParticleSystem finishParticle1, finishParticle2;
    [SerializeField] ParticleSystem sadParticle;
    [SerializeField] ParticleSystem perfectParticle;
    public void finish()
    {
        finishParticle1.Play();
        finishParticle2.Play();
    }

    public void hitObstacle()
    {
        sadParticle.Play();
    }

    public void catchButterfly()
    {
        finish();
        perfectParticle.Play();
    }


}
