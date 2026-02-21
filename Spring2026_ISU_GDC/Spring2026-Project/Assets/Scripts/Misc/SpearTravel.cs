using Unity.VisualScripting;
using UnityEngine;

public class SpearTravel : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Rigidbody2D rb;
    public float moveSpeed;
    private float rot;
    public bool hitWall;
    private float xAngle;
    private float yAngle;

    public SpearThrow st;
    // Update is called once per frame
    void Start()
    {
        rot = gameObject.transform.rotation.eulerAngles.z;
        xAngle = Mathf.Cos((rot * Mathf.PI) / 180);
        yAngle = Mathf.Sin((rot * Mathf.PI) / 180);
    }
    void Update()
    {
        if (hitWall == false)
        {
            rb.linearVelocity = new Vector2(xAngle * moveSpeed, yAngle * moveSpeed);
        }
        else
        {
            rb.linearVelocity = new Vector2(0f, 0f);
        }   
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Ground")
        {
            hitWall = true;
            FindFirstObjectByType<PlayerEventManager>().OnPlayerSpearStuckInWall.Invoke(this.gameObject);
        }
        if(hitWall&&col.gameObject.tag=="Player")
        {
            /*Destroy(gameObject);
            SpearThrow.isSpearInHand = true;
            hitWall = false;
            SpearThrow.travellingToSpear = false;*/
        }
        /*
        if(col.gameObject.tag == "Enemy")
        {
            //deal damage & knockback to enemy
        }
        */
    }
    private void OnBecameInvisible()
    {
        Destroy(gameObject);
        SpearThrow.isSpearInHand = true;
    }
}
