using UnityEngine;

public class Projectile : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] public float speed = 10f;
    [SerializeField] public float lifeline = 4f;

    private Vector3 direction;

    public void Initialize(Vector3 direction)
    {
        direction = direction.normalized;
        Destroy(gameObject, lifeline);
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += direction * speed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player hit");
            Destroy(gameObject);
        }
    }
}
