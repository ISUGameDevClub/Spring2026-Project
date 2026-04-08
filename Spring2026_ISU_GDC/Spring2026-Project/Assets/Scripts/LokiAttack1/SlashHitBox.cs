using UnityEngine;

public class SlashHitBox : MonoBehaviour
{
    [Header("Hitbox reference")]
    [SerializeField] private Collider2D hitboxCollider;

    [Header("Damage")]
    [SerializeField] private float damage = 20f;
    [SerializeField] private float knockback = 5f;


    private bool _isActive;

    private void Awake()
    {

        if (hitboxCollider != null)
            hitboxCollider.enabled = false;
    }

    /// <summary>Enable the hitbox for the duration of a slash.</summary>
    public void Activate()
    {
        _isActive = true;
        if (hitboxCollider != null) hitboxCollider.enabled = true;

    }

    /// <summary>Disable the hitbox after the slash swing completes.</summary>
    public void Deactivate()
    {
        _isActive = false;
        if (hitboxCollider != null) hitboxCollider.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!_isActive) return;
        if (!other.CompareTag("Player")) return;

        // Add in damage system logic


        Debug.Log($"[SlashHitbox] Hit {other.name} for {damage} damage.");
    }
}
