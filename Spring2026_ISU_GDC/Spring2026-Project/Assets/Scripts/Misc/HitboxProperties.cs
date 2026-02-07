using UnityEngine;
using System.Collections.Generic;


public class HitboxProperties : MonoBehaviour
{

    // Type the tag of the target you want to hurt here.
    [SerializeField] string attackTarget;
    [SerializeField] bool attackActive = false;
    [SerializeField] int damage = 1;

    [SerializeField] float knockbackX = 0;
    [SerializeField] float knockbackY = 0;

    //This the amount of time the victim is stunned for when hit. By default it's 1/3rd of a second
    [SerializeField] float hitStun = .33f;
  
    // If we damage an enemy, we add them to this list to not damage them every frame the attack is active.
    private List<GameObject> hurtEnemies = new List<GameObject>();
    private List<GameObject> inRange = new List<GameObject>();

    void Update()
    {
        //If our attack is out and at a point where it's supposed to damage someone, damage them.
        if (attackActive == true)
        {
            deal_damage();
        }
        else
        {
            hurtEnemies.Clear();
        }
    }


    public void deal_damage()
    {
        foreach (GameObject enemy in inRange)
        {
            if (hurtEnemies.IndexOf(enemy) == -1) // Negative 1 means they are not found in the list
            {
                // get person being hit's health script. Run a null check too.
               // Person being hit take damage function here. Will add it once we have a health script.
               hurtEnemies.Add(enemy);
            }
                
            }
        }
        //attack_landed = false;
    void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.gameObject.tag == attackTarget)
        {
            inRange.Add(collision.gameObject);
        }
        
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == attackTarget)
        {
            inRange.Remove(collision.gameObject);
        }
    }
    
    
}
    


   
    



