using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] float startEnemyHealth = 100;
    private float currentEnemyHealth;

    void Awake()
    {
        currentEnemyHealth = startEnemyHealth;
    }

    public void DamageEnemy(float damageValue, float knockback, float stunDuration, GameObject attacker)
    {
        currentEnemyHealth -= damageValue;
        if (currentEnemyHealth <= 0f)
        {
            Destroy(gameObject);
        }
        Debug.Log(damageValue + " taken");




    }
}
