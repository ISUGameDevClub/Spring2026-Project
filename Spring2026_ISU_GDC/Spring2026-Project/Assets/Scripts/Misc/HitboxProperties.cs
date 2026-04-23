using System;
using System.Collections;
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

    [SerializeField] private int defaultDamage;
    [SerializeField] private bool overrideGlobalGameData = false;
    [SerializeField] private bool debugMessagesOn = false;

    private void Start()
    {
        if (overrideGlobalGameData)
        {
            SetDamageForHitbox(defaultDamage);
        }
    }

    private void Update()
    {
        //If our attack is out and at a point where it's supposed to damage someone, damage them.
        if (attackActive == true)
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
                    
                    if (debugMessagesOn)
                    {
                        Debug.Log(component.gameObject.name  +" takes damage");
                    }
                }
            }
        }
    }

    //attack_landed = false;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (debugMessagesOn)
        {
            Debug.Log("Hitbox hit " + collision.gameObject.name);
        }
        
        if (collision.gameObject.CompareTag(attackTarget))
        {
            enemiesInRange.Add(collision.gameObject);
            
            if (debugMessagesOn)
            {
                Debug.Log("Added " + collision.gameObject.name +" to enemiesInRange");
            }
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(attackTarget))
        {
            //bandaid fix to make sure OnTriggerExit doesnt get called to early on fast moving projectiles
            StartCoroutine(RemoveEnemyFromRangeAfterDelay(collision.gameObject, enemiesInRange,0.2f));
            //enemiesInRange.Remove(collision.gameObject);
        }
    }
    
    //bandaid fix to make sure OnTriggerExit doesnt get called to early on fast moving projectiles
    IEnumerator RemoveEnemyFromRangeAfterDelay(GameObject enemyObj, List<GameObject> enemyList, float delay)
    {
        yield return new WaitForSeconds(delay);
        enemyList.Remove(enemyObj);
    }

    private void OnDisable()
    {
        hurtEnemies.Clear();
        enemiesInRange.Clear();
    }
}








