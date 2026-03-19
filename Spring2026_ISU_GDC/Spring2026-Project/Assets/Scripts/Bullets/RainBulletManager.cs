using UnityEngine;
using System.Collections;

public class RainBulletManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject smallBulletPrefab;
    [SerializeField] private GameObject bigBulletPrefab;

    [Header("General Settings")]
    [SerializeField] private float minXSpawnPos;
    [SerializeField] private float maxXSpawnPos;
    [SerializeField] private float ySpawnPos;
    [SerializeField] private float yDespawnPos;

    [Header("Small Bullet Settings")]
    [SerializeField] private int numOfSmallBullets;
    [SerializeField] private float timeBetweenSmallBullets;
    [SerializeField] private float initialSmallSpawnDelay;
    [SerializeField] private float minSmallAngleOffset;
    [SerializeField] private float maxSmallAngleOffset;

    [Header("Big Bullet Settings")]
    [SerializeField] private int numOfBigBullets;
    [SerializeField] private float timeBetweenBigBullets;
    [SerializeField] private float initialBigSpawnDelay;
    [SerializeField] private float minBigSpawnAngle;
    [SerializeField] private float maxBigSpawnAngle;

    private int spawnedSmall = 0;
    private int spawnedBig = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // TEMPORARY
        StartSpawning();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartSpawning()
    {
        StartCoroutine(SpawnSmallBullets());
        StartCoroutine(SpawnBigBullets());
    }

 

    private IEnumerator SpawnSmallBullets()
    {
        if (spawnedSmall == 0)
            yield return new WaitForSeconds(initialSmallSpawnDelay);
        Vector2 spawnLocation = new Vector2(Random.Range(minXSpawnPos, maxXSpawnPos), ySpawnPos);
        GameObject spawnedObject = Instantiate(smallBulletPrefab, spawnLocation, Quaternion.Euler(0f, 0f, 0f));
        spawnedObject.GetComponent<RainBullet>().Initialize(Random.Range(minSmallAngleOffset, maxSmallAngleOffset), yDespawnPos);
        spawnedSmall++;
        yield return new WaitForSeconds(timeBetweenSmallBullets);
        if (spawnedSmall < numOfSmallBullets)
        {
            StartCoroutine(SpawnSmallBullets());
        }

    }

  

    private IEnumerator SpawnBigBullets()
    {
        if (spawnedBig == 0)
            yield return new WaitForSeconds(initialBigSpawnDelay);
        Vector2 spawnLocation = new Vector2(Random.Range(minXSpawnPos, maxXSpawnPos), ySpawnPos);
        GameObject spawnedObject = Instantiate(bigBulletPrefab, spawnLocation, Quaternion.Euler(0f, 0f, 0f));
        spawnedObject.GetComponent<RainBullet>().Initialize(Random.Range(minBigSpawnAngle, maxBigSpawnAngle), yDespawnPos);
        spawnedBig++;
        yield return new WaitForSeconds(timeBetweenBigBullets);
        if (spawnedBig < numOfBigBullets)
        {
            StartCoroutine(SpawnBigBullets());
        }

    }
}
