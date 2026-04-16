using UnityEngine;
using System.Collections;

public class LokiCloneProj : MonoBehaviour
{
    private GameObject player;
    private Vector3 direction;
    [SerializeField] private float speed;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        direction = (player.transform.position- transform.position).normalized;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += direction * speed * Time.deltaTime;
    }
}
