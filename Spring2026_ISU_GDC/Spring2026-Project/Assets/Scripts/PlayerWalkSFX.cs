using UnityEngine;
using FMODUnity;
using ISUGameDev.SpearGame.Player.Movement;

public class PlayerWalkSFX : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    [SerializeField] private float timeBetweenSounds;
    private float timeCounter;
    private Rigidbody2D player;
   private PlayerMovement pm;
    private void Start()
    {
        player = GetComponent<Rigidbody2D>();
        pm = GetComponent<PlayerMovement>();
        timeCounter = timeBetweenSounds;
    }

    [SerializeField] private FMODUnity.EventReference walk1;
    [SerializeField] private FMODUnity.EventReference walk2;
    [SerializeField] private FMODUnity.EventReference walk3;
    [SerializeField] private FMODUnity.EventReference walk4;


    private void Update()
    {
        if ((timeCounter>=0))
        {
            if (Mathf.Abs(Input.GetAxisRaw("Horizontal")) > 0f && player.linearVelocity.y == 0 && pm.touchingWall == false)
            timeCounter -= Time.deltaTime;
            //Debug.Log(timeCounter);
        }
        else
        {
            timeCounter = timeBetweenSounds;
            Debug.Log("Sound Played");
            PlayMySound();
        }
    }
    public void PlayMySound()
    {
        int soundToPlay = Random.Range(1, 5);
        if(soundToPlay == 1)
        FMODUnity.RuntimeManager.PlayOneShot(walk1);
        else if (soundToPlay == 2)
        FMODUnity.RuntimeManager.PlayOneShot(walk2);
        else if (soundToPlay == 3)
            FMODUnity.RuntimeManager.PlayOneShot(walk3);
        else 
            FMODUnity.RuntimeManager.PlayOneShot(walk4);

    }
}
