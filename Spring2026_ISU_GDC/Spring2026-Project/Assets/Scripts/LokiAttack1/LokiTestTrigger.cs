using UnityEngine;

public class LokiTestTrigger : MonoBehaviour
{
    [SerializeField] private SlashAttack lokiAttack;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            lokiAttack.TriggerSlashAttack();
            Debug.Log("Triggered slash attack");
        }
    }
}
