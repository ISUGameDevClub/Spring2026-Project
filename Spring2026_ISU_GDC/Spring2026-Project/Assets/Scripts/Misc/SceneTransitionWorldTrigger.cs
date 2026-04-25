using System;
using UnityEngine;

public class SceneTransitionWorldTrigger : MonoBehaviour
{
    [SerializeField] private string sceneToLoad;
    
    [Header("Leave this blank if you don't want to display a level name")]
    [SerializeField] private string levelNameToDisplay;

    [SerializeField] private GameObject sceneTransitionPrefab;
    
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            SceneTransition sceneTransition = Instantiate(sceneTransitionPrefab).GetComponent<SceneTransition>();
            sceneTransition?.TriggerTransition(sceneToLoad, levelNameToDisplay);
        }
    }
}
