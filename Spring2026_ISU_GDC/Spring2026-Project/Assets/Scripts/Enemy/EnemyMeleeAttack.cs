using UnityEngine;
using System.Collections;
using System.Linq;

public class EnemyMeleeAttack : MonoBehaviour
{
    [SerializeField] int damage = 1;
    [SerializeField] float attackCooldown = 1f;
    [SerializeField] SpriteRenderer GFX;
    [SerializeField] Sprite attackingImage;

    private float lastAttackTime;
    private Sprite cacheSprite;

    bool isAttacking = false;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            TryAttack(collider);
        }
    }

    private void OnTriggerStay2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            TryAttack(collider);
        }
    }

    void TryAttack(Collider2D player)
    {
        if (Time.time >= lastAttackTime + attackCooldown)
        {
            GFX.sprite = cacheSprite;

            if (!isAttacking)
            {
                StartCoroutine(PlayAttackAnimation());
            }

            PlayerHealthScript playerHealth = player.GetComponent<PlayerHealthScript>();

            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage);
            }

            lastAttackTime = Time.time;
        }
    }

    void Start()
    {
        cacheSprite = GFX.sprite;
    }

    IEnumerator PlayAttackAnimation()
    {
        isAttacking = true;

        GFX.sprite = attackingImage;
        yield return new WaitForSeconds(1f);

        GFX.sprite = cacheSprite;

        isAttacking = false;
    }
}
