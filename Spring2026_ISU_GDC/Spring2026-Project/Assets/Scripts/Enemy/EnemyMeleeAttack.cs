using UnityEngine;

public class EnemyMeleeAttack : MonoBehaviour
{
    public int damage = 1;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag.Equals("Enemy"))
        {
            playerHealth--;
            print("Player Hit!\nHealth = " + playerHealth);
        }
    }
}