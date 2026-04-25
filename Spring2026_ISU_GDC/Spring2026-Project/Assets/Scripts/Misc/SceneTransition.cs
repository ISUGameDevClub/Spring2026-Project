using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using TMPro;

public class SceneTransition : MonoBehaviour
{
    [Header("Transition Settings")]
    [SerializeField] private Image fadeImage;
    [SerializeField] private float fadeDuration = 2f;
    [SerializeField] private float blackoutTime = 2f;
    [SerializeField] private float timeTillShowLevelName = 2f;
    [SerializeField] private GameObject loadingText;

    public static SceneTransition Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    /// <summary>
    /// Leave levelTitle null or "" if no title to display
    /// </summary>
    /// <param name="sceneToLoad"></param>
    /// <param name="levelTitle"></param>
    public void TriggerTransition(string sceneToLoad, string levelTitle)
    {
        StartCoroutine(TransitionRoutine(sceneToLoad, levelTitle));
    }

    private IEnumerator TransitionRoutine(string sceneToLoad, string levelTitle)
    {
        // Fade to black
        yield return StartCoroutine(Fade(0f, 1f));

        // Load the scene
        AsyncOperation loadOp = SceneManager.LoadSceneAsync(sceneToLoad);
        while (!loadOp.isDone)
            yield return null;

        yield return new WaitForSeconds(blackoutTime);
        
        // Fade back to clear
        yield return StartCoroutine(Fade(1f, 0f));
        
        yield return new WaitForSeconds(timeTillShowLevelName);
        
        // Optionally show loading text
        if (levelTitle != null || levelTitle != "")
        {
            loadingText.GetComponent<TextMeshProUGUI>().text = levelTitle;
            loadingText.SetActive(true);
        }
        
        //bandaid fix 10 second buffer
        yield return new WaitForSeconds(10f);
        
        //destroy this object after fade
        if (Instance == this)
        {
            Instance = null;
            Destroy(gameObject);
        }
    }

    private IEnumerator Fade(float fromAlpha, float toAlpha)
    {
        float elapsed = 0f;
        Color color = fadeImage.color;

        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            color.a = Mathf.Lerp(fromAlpha, toAlpha, elapsed / fadeDuration);
            fadeImage.color = color;
            yield return null;
        }

        color.a = toAlpha;
        fadeImage.color = color;
    }
}