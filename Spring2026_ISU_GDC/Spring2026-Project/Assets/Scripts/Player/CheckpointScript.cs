using UnityEditor.Animations;
using UnityEngine;

public class CheckpointScript : MonoBehaviour
{
    
      public Vector3 playerRespawn;
      [SerializeField] private FMODUnity.EventReference checkpointSFX;
      private GameObject curCheckpointObj;
   
    void Start()
    {
        
    }

    
    void Update()
    {
        /*if(Input.GetKeyDown(KeyCode.R))
        {
            CheckpointDebug();
        }*/
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.CompareTag("Checkpoint") && collider.gameObject != curCheckpointObj)
        {
            playerRespawn = collider.gameObject.transform.position;
            Animator totemAnimator = collider.gameObject.GetComponent<Animator>();
            totemAnimator.Play("Glow");
            FMODUnity.RuntimeManager.PlayOneShot(checkpointSFX);
            curCheckpointObj = collider.gameObject;
        }
        
    }

    private void CheckpointDebug()
    {
        transform.position = playerRespawn;
        print(playerRespawn);
    }


}
