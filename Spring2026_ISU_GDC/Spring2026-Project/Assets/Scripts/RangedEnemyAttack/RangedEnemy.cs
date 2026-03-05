using System.Diagnostics.Contracts;
using UnityEngine;

public class RangedEnemy : MonoBehaviour
{
    [Header("References")]
    [SerializeField] public GameObject projectilePrefab;
    [SerializeField] public Transform firePoint;
    [SerializeField] public Transform player;

    [Header("Settings")]
    [SerializeField] public float attackRange = 10f;
    public float spreadHalfAngle = 45f;
    [SerializeField] public float fireRate = 1f;

    private float nextFireTime = 0f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if(player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }

        if(firePoint == null)
        {
            firePoint = transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null) return;
        if(Time.time < nextFireTime) return;

        if(IsPlayerInRange() && IsPlayerInSpread())
        {
            Fire();
            nextFireTime = Time.time + fireRate;
        }
    }

    private bool IsPlayerInRange()
    {
        float distance = Vector3.Distance(transform.position, player.position);
        return distance <= attackRange;
    }

    private bool IsPlayerInSpread()
    {
        Vector3 directionToPlayer = (player.position - transform.position).normalized;
        float angle = Vector3.Angle(transform.forward, directionToPlayer);
        return angle <= spreadHalfAngle;
    }

    private void Fire()
    {
        if(projectilePrefab == null)
        {
            Debug.LogWarning("Projectile prefab is not assigned.");
            return;
        }

        Vector3 directionToPlayer = (player.position - firePoint.position).normalized;
        GameObject projectileobj = Instantiate(projectilePrefab, firePoint.position, Quaternion.LookRotation(directionToPlayer));

        Projectile projectile = projectileobj.GetComponent<Projectile>();
        if (projectile != null)
        {
            projectile.Initialize(directionToPlayer);
        }
    }
}
