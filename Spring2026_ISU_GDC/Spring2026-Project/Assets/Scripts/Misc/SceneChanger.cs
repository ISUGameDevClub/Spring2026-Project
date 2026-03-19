using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneChanger : MonoBehaviour
{
    private Animator animator;
    private GameObject blackScreen;
    [SerializeField] private GameObject BlackScreen;

    
    public void changeScene(int sceneIndex)
    {
        StartCoroutine(sceneChange(sceneIndex));
    }
    public IEnumerator sceneChange(int sceneIndex)
    {
        blackScreen = Instantiate(BlackScreen, transform);
        animator = blackScreen.GetComponent<Animator>();
        animator.Play("FadeOut", 0, 0f);
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(sceneIndex);
        
    }


}
