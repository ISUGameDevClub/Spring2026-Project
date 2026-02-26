using UnityEngine;

public class LavaScript : MonoBehaviour
{
    [SerializeReference] bool lavaRise;
    [SerializeReference] float lavaRiseSpeed;
    private Rigidbody2D body;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        
    }

    public void StartLavaRise()
    {
        lavaRise = true;
    }
    public void StopLavaRise()
    {
        lavaRise = false;
    }

    // Update is called once per frame
    void Update()
    {
        lavaControl();

    }

    private void lavaControl()
    {
        if (lavaRise)
        {
            body.linearVelocityY = lavaRiseSpeed;
        }
        else
        {
            body.linearVelocityY = 0;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            //To do kill player
        }
    }
}
