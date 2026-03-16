using UnityEngine;

public class PlayerHealthScript : HealthBC
{
    [SerializeField] CheckpointScript checkpointScript;
    
    public override void TakeDamage(int damageValue, float knockback, float stunDuration, GameObject attacker)
    {
        currentHealth -= damageValue;
        DeathHandler();
        
    }

    private void DeathHandler()
    {
       if(currentHealth<=0)
        {
            transform.position = checkpointScript.playerRespawn;
            currentHealth = maxHealth;
        } 
    }
}
