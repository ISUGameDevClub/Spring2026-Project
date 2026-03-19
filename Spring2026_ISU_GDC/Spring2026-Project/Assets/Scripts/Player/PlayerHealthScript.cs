using UnityEngine;
using Nomad.Core.Events;

public class PlayerHealthScript : MonoBehaviour
{

    [SerializeField] int playerHealth;
    [SerializeField] CheckpointScript checkpointScript;

    private IGameEvent<int> _takeDamage;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _takeDamage = GameEventRegistry.GetEvent<int>("PlayerTakeDamage", "Player");
        _takeDamage.Subscribe(this, OnTakeDamage);
    }

    private void OnTakeDamage(in int damage)
    {
        // TODO: Player take damage stuff
        Debug.Log("Player Hit");
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
