using UnityEngine;
using Nomad.Core.Events;
using System.Collections;


public class RainBullet : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float angleOffset;
    [SerializeField] private bool destroyOnGround;
    [SerializeField] private float lifeTime;
    [SerializeField] private GameObject indicatorObj;
    [SerializeField] private float startTime;

    private Rigidbody2D rb;
    private IGameEvent<int> _takeDamage;
    private Vector3 indicatorInitPos;
    private GameObject indicator;

    private float velX;
    private float velY;

    private float yDespawnPoint = -10f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        _takeDamage = GameEventRegistry.GetEvent<int>("PlayerTakeDamage", "Player");

        float angleInRads = (270f + angleOffset) * Mathf.Deg2Rad;

        velX = speed * Mathf.Cos(angleInRads);
        velY = speed * Mathf.Sin(angleInRads);

        StartCoroutine(LifeCycle());

        //Spawn Indicator Shit
        indicator = Instantiate(indicatorObj, transform);
        indicator.transform.position = transform.position;
        indicator.transform.rotation = Quaternion.Euler(0f, 0f, angleOffset);

        indicatorInitPos = indicator.transform.position;

    }

    // Update is called once per frame
    void Update()
    {
        rb.linearVelocity = new Vector2(velX, velY);
        indicator.transform.position = indicatorInitPos;
        if (transform.position.y <= yDespawnPoint)
            Destroy(gameObject);
    }

    public void Initialize(float angleOffset, float yDespawnPoint)
    {
        this.angleOffset = angleOffset;
        this.yDespawnPoint = yDespawnPoint;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _takeDamage.Publish(1);
            Destroy(gameObject);
        }

        else if (collision.CompareTag("Ground") && destroyOnGround)
            Destroy(gameObject);

    }

    private IEnumerator LifeCycle()
    {
        yield return new WaitForSeconds(lifeTime);
        Destroy(gameObject);
    }

}
