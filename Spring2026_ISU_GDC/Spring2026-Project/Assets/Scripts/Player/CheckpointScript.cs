using UnityEngine;

public class CheckpointScript : MonoBehaviour
{
    Vector3 playerRespawn;
   
    void Start()
    {
        
    }

    
    void Update()
    {
        if(Input.GetKeyDown("r"))
        {
            CheckpointDebug();
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.tag.Equals("Checkpoint"))
        {
            playerRespawn = collider.gameObject.transform.position;
        }
        
    }

    private void CheckpointDebug()
    {
        transform.position = playerRespawn;
        print(playerRespawn);
    }


}
