using UnityEngine;
using System.Collections;

public class LokiAttack2 : MonoBehaviour
{
    [SerializeField] private Vector3[] lokiClonePositions;
    [SerializeField] private GameObject lokiClonePrefab;
    [SerializeField] private bool debugTest;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
        if (debugTest)
        {
            StartCoroutine(Debug());
        }
    }


    public void StartAttack()
    {
        for (int i = 0; i < lokiClonePositions.Length; i++)
        {
            GameObject lokiClone = Instantiate(lokiClonePrefab, lokiClonePositions[i], Quaternion.Euler(0,0,0));
        }
    }

    private IEnumerator Debug()
    {
        yield return new WaitForSeconds(1);
        StartAttack();
    }
}
