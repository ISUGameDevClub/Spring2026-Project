using UnityEngine;

public class Spawner : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private bool isSpawning = true;
    public GameObject Enemy;
    public float spawnTime;
    private float timer;

    void Start()
    {
        timer = spawnTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (isSpawning)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                timer = spawnTime;
                Instantiate(Enemy, gameObject.transform.position,Quaternion.identity);
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            isSpawning = false;
        }
    }
}
