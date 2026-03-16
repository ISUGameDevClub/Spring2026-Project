using UnityEngine;

public abstract class HealthBC : MonoBehaviour
{
    [SerializeField] protected int maxHealth;
    protected int currentHealth;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        currentHealth = maxHealth;
    }

    public virtual void TakeDamage(int damageValue, float knockback, float stunDuration, GameObject attacker){}

    public virtual void Heal(int value)
    {
        currentHealth += value;
        if (currentHealth > maxHealth)
            currentHealth = maxHealth;
    }

    public void FullHeal()
    {
        currentHealth = maxHealth;
    }

    public int GetCurrentHealth()
    {
        return currentHealth;
    }

    public int GetMaxHealth()
    {
        return maxHealth;
    }

    
}
