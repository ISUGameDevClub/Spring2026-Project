using ISUGameDev.SpearGame.Player;
using Unity.VisualScripting;
using UnityEngine;

public class ArrowMovement : MonoBehaviour
{
   [SerializeField] float arrowSpeed = 5;
   public float direction = 1;

   [SerializeField] float lifetime = 5;

    void Start()
    {
        Destroy(gameObject,lifetime);
    }
    void Update()
    {
        transform.position = new Vector2(transform.position.x + arrowSpeed * direction * Time.deltaTime ,transform.position.y);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.GetComponentInParent<PlayerController>().TakeDamage(1,0,0,gameObject);
            Destroy(gameObject);
        }
        if (collision.tag == "Ground")
            Destroy(gameObject);
        
    }
}
