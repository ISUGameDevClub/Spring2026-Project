using UnityEngine;

public class EnemyAnimationEvents : MonoBehaviour 
{
    private ParticleSystem hitParticles;
    
    void Start()
    {
        hitParticles = GetComponentInChildren<ParticleSystem>();
    }
    
    public void PlayHitParticles()
    {
        if (hitParticles != null)
            hitParticles.Play();
    }
}