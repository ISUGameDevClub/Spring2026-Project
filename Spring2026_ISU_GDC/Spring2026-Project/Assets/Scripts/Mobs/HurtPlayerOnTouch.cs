using UnityEngine;

public class HurtPlayerOnTouch : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<PlayerHealthController>().TakeOneDamage();
        }
    }
}
