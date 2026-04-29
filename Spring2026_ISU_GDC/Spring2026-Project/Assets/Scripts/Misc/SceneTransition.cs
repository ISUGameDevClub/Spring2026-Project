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
    [SerializeField] private FMODUnity.EventReference drumHitSFX;

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
    public void TriggerTransition(string sceneToLoad, string levelTitle, Vector3 playerSpawnPoint = default, bool movePlayer = true)
    {
        StartCoroutine(TransitionRoutine(sceneToLoad, levelTitle, playerSpawnPoint, movePlayer));
    }

    private IEnumerator TransitionRoutine(string sceneToLoad, string levelTitle, Vector3 playerSpawnPoint = default, bool movePlayer = true)
    {
        // Fade to black
        Debug.Log("[JAKE_MEM_ACCESS] fadeImage: " + (fadeImage == null ? "NULL" : "OK"));
        yield return StartCoroutine(Fade(0f, 1f));

        // Load the scene
        Debug.Log("[JAKE_MEM_ACCESS] sceneToLoad: " + (string.IsNullOrEmpty(sceneToLoad) ? "NULL/EMPTY" : sceneToLoad));
        
        //FIx?
        SceneManager.LoadScene(sceneToLoad);
        
        //AsyncOperation loadOp = SceneManager.LoadSceneAsync(sceneToLoad);
        //Debug.Log("[JAKE_MEM_ACCESS] loadOp: " + (loadOp == null ? "NULL - scene name likely invalid" : "OK"));
        /*while (!loadOp.isDone)
            yield return null;*/

        //if player spawn point is not default (0,0,0), then spawn player at that point
        if (playerSpawnPoint != Vector3.zero && movePlayer)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            Debug.Log("[JAKE_MEM_ACCESS] player (FindGameObjectWithTag): " + (player == null ? "NULL - no object tagged Player in scene" : "OK"));
            Debug.Log("[JAKE_MEM_ACCESS] player.transform: " + (player != null && player.transform == null ? "NULL" : (player != null ? "OK" : "SKIPPED - player is null")));
            player.transform.position = playerSpawnPoint;
        }
        
        yield return new WaitForSeconds(blackoutTime);
        
        // Fade back to clear
        Debug.Log("[JAKE_MEM_ACCESS] fadeImage (pre-fade-back): " + (fadeImage == null ? "NULL - may have been destroyed during scene load" : "OK"));
        yield return StartCoroutine(Fade(1f, 0f));
        
        yield return new WaitForSeconds(timeTillShowLevelName);
        
        // Optionally show loading text
        if (levelTitle != null && levelTitle != "")
        {
            Debug.Log("[JAKE_MEM_ACCESS] loadingText: " + (loadingText == null ? "NULL" : "OK"));
            Debug.Log("[JAKE_MEM_ACCESS] loadingText.GetComponent<TextMeshProUGUI>(): " + (loadingText != null && loadingText.GetComponent<TextMeshProUGUI>() == null ? "NULL - no TMP component on loadingText" : (loadingText != null ? "OK" : "SKIPPED - loadingText is null")));
            loadingText.GetComponent<TextMeshProUGUI>().text = levelTitle;
            loadingText.SetActive(true);
            yield return new WaitForSeconds(1f);
            Debug.Log("[JAKE_MEM_ACCESS] drumHitSFX.IsNull: " + (drumHitSFX.IsNull ? "NULL - FMOD event reference not set" : "OK"));
            FMODUnity.RuntimeManager.PlayOneShot(drumHitSFX);
        }
        
        //bandaid fix seconds buffer
        if (levelTitle != null && levelTitle != "")
        {
            yield return new WaitForSeconds(10f);
        }
        else
        {
            yield return new WaitForSeconds(0.5f);
        }
        
        Debug.Log("[JAKE_MEM_ACCESS] Instance at destroy check: " + (Instance == null ? "NULL" : "OK"));
        //destroy this object after fade
        if (Instance == this)
        {
            Instance = null;
            Destroy(gameObject);
        }
    }

    private IEnumerator Fade(float fromAlpha, float toAlpha)
    {
        Debug.Log("[JAKE_MEM_ACCESS] Fade() entered");
        Debug.Log("[JAKE_MEM_ACCESS] fadeImage component: " + (!fadeImage ? "DESTROYED/NULL" : "OK"));
    
        if (!fadeImage)
        {
            Debug.LogError("[JAKE_MEM_ACCESS] Fade() aborted - fadeImage is null or destroyed");
            yield break;
        }
        
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
        
        Debug.Log("[JAKE_MEM_ACCESS] Fade() completed");
    }
}