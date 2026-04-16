using UnityEngine;
using System.Collections;

public class LokiClone : MonoBehaviour
{
    [SerializeField] private float timeUntilFire;
    [SerializeField] private float timeUntilDespawn;
    [SerializeField] private Vector3 spawnOffset;
    [SerializeField] GameObject projectile;
    private GameObject player;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        StartCoroutine(Attack());
    }

    private IEnumerator Attack()
    {
        yield return new WaitForSeconds(timeUntilFire);
        Debug.Log("Spawn peojectile");
        Instantiate(projectile, transform.position + spawnOffset, Quaternion.Euler(0, 0, 0));
        yield return new WaitForSeconds(timeUntilDespawn);
        Destroy(gameObject);
    }

    private void flipTest()
    {
        if (player.transform.position.x - transform.position.x < 0)
            transform.localScale = new Vector3(-1, 1, 1);
        
        else
            transform.localScale = new Vector3(1, 1, 1);

    }
}
