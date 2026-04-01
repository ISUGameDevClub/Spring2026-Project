using UnityEngine;
using System.Collections.Generic;
using ISUGameDev.SpearGame.Enemy;


public class HitboxProperties : MonoBehaviour
{
    // Type the tag of the target you want to hurt here.
    [SerializeField] string attackTarget;
    [SerializeField] bool attackActive = false;
    private int damage = 1;

    [SerializeField] float knockbackX = 0.0f;
    [SerializeField] float knockbackY = 0.0f;

    //This the amount of time the victim is stunned for when hit. By default it's 1/3rd of a second
    [SerializeField] float hitStun = 0.33f;

    // If we damage an enemy, we add them to this list to not damage them every frame the attack is active.
    private List<GameObject> hurtEnemies = new List<GameObject>();
    private List<GameObject> enemiesInRange = new List<GameObject>();

    private void Update()
    {
        //If our attack is out and at a point where it's supposed to damage someone, damage them.
        if (attackActive)
        {
            if (hurtEnemies.Count < enemiesInRange.Count) // if we have yet to damage all enemies in range, damage them
            {
                DealDamage();
            }
        }
        else
        {
            hurtEnemies.Clear();
        }
    }

    public void SetDamageForHitbox(int damage)
    {
        this.damage = damage;
    }

    public void DealDamage()
    {
        foreach (var enemy in enemiesInRange)
        {
            if (hurtEnemies.IndexOf(enemy) == -1) // Negative 1 means they are not found in the list
            {
                if (enemy.TryGetComponent<EnemyBase>(out var component))
                {
                    component.TakeDamage(damage, hitStun, knockbackX, gameObject);
                    hurtEnemies.Add(enemy);
                }
            }
        }
    }

    //attack_landed = false;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == attackTarget)
        {
            enemiesInRange.Add(collision.gameObject);
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == attackTarget)
        {
            enemiesInRange.Remove(collision.gameObject);
        }
    }
}








