using UnityEngine;

public class EnemyHealth : HealthBC
{
    public override void TakeDamage(int damageValue, float knockback, float stunDuration, GameObject attacker)
    {
         Debug.Log(damageValue + " damage taken");
        currentHealth -= damageValue;
        //Deal knockback
        //Deal stun
        //Play visual effects
        if (currentHealth <= 0)
            Destroy(gameObject,.2f);
        //Added a delay to allow for visual effects like hitflash, animations, ect to play first

    }

}
