using UnityEngine;

public class PlayerHealthScript : MonoBehaviour
{

    [SerializeField] int playerHealth;
    [SerializeField] CheckpointScript checkpointScript;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        DeathHandler();
    }

    private void DeathHandler()
    {
        
       if(playerHealth<=0)
        {
            transform.position = checkpointScript.playerRespawn;
            playerHealth = 3;
        } 
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.tag.Equals("Enemy"))
        {
            playerHealth--;
            print("Player Hit!\nHealth = "+ playerHealth);
        }
    }
}
