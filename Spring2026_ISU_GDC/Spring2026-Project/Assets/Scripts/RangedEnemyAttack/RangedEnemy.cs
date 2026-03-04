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
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
