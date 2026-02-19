using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public StunScript stunScript;
    [SerializeField] float startEnemyHealth = 100;
    private float currentEnemyHealth;

    void Awake()
    {
        currentEnemyHealth = startEnemyHealth;
    }

    public void DamageEnemy(float damageValue, float knockback, float stunDuration, GameObject attacker)
    {

        //Damage
        currentEnemyHealth -= damageValue;
        Debug.Log(damageValue + "damage taken");
        if (currentEnemyHealth <= 0f)
        {
            Destroy(gameObject);
        }

        //Knockback




        //Stun
        stunScript.InflictStun(stunDuration);

    }

    public void HealEnemy(float healValue)
    {
        if (healValue == -1)
        {
            currentEnemyHealth = startEnemyHealth;
            Debug.Log("Full heal");
        }
        else
        {
            currentEnemyHealth += healValue;
            Debug.Log(healValue + " health gained");
        }
        

    }

}
