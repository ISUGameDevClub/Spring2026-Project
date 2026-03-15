using System.Diagnostics.Tracing;
using UnityEngine;

public class PlayerKnockback : MonoBehaviour
{
    [SerializeField] private float knockbackAmount = 1;
    
    Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        
    }
    
    public void applyKnockback(float knockbackAmount, GameObject attacker)
    {
        if(attacker.transform.localPosition.x < transform.localPosition.x)
        {
            rb.AddForce(new Vector2(knockbackAmount, knockbackAmount/2));
        }
        else
        {
            rb.AddForce(new Vector2(-knockbackAmount, knockbackAmount / 2));
        }
        
    }

}
