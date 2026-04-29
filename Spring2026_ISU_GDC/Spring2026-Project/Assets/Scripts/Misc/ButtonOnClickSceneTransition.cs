using UnityEngine;

public class ButtonOnClickSceneTransition : MonoBehaviour
{
    [SerializeField] private string sceneToLoad;
    
    [Header("Leave this blank if you don't want to display a level name")]
    [SerializeField] private string levelNameToDisplay;

    [SerializeField] private GameObject sceneTransitionPrefab;
    

    public void LoadSceneOnButtonClick()
    {
        SceneTransition sceneTransition = Instantiate(sceneTransitionPrefab).GetComponent<SceneTransition>();
        sceneTransition?.TriggerTransition(sceneToLoad, levelNameToDisplay);
    }
}
