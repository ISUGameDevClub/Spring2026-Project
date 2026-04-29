using System;
using System.Collections;
using System.Collections.Generic;
using ISUGameDev.SpearGame.Player;
using ISUGameDev.SpearGame.Player.Movement;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealthController : MonoBehaviour
{
    [SerializeField] private GameObject layoutGroup;
    [SerializeField] private GameObject featherPrefab;
    [SerializeField] private int maxHealthFeathers;
    private List<Feather> featherList;
    private int curFeatherIndex = -1;
    public bool cannotTakeDamage = false;

    [SerializeField] private FMODUnity.EventReference playerHitSfx;
    [SerializeField] private FMODUnity.EventReference playerMoanSfx;
    
    [SerializeField] private Renderer playerRenderer;
    [SerializeField] private Material hitFlashMaterial;
    [SerializeField] private float hitFlashDuration = 0.1f;

    [SerializeField] private GameObject playerGhostPrefab;
    [SerializeField] private FMODUnity.EventReference playerDeathSFX;
    
    [SerializeField] private float timeTillReloadSceneAfterDeath = 4f;
    [SerializeField] SceneTransition sceneTransitionPrefab;
    
    private Material originalMaterial;

    private void Start()
    {
        originalMaterial = playerRenderer.material;
        featherList = new List<Feather>();
        InitializeHealthIcons();
    }
   
    
    private void InitializeHealthIcons()
    {
        for (int i = 0; i < maxHealthFeathers; i++)
        { 
            Debug.Log("Creating feather");
           GameObject featherObj = Instantiate(featherPrefab, layoutGroup.transform);
           Feather featherComponent = featherObj.GetComponent<Feather>();
           featherList.Add(featherComponent);
           curFeatherIndex++;
        }
    }
    
    public void TakeOneDamage()
    {
        if (curFeatherIndex < 0) { return;}
        
        if (cannotTakeDamage == false)
        {
            featherList[curFeatherIndex].isActive = false;
            curFeatherIndex--;
            
            FMODUnity.RuntimeManager.PlayOneShot(playerHitSfx);
            FMODUnity.RuntimeManager.PlayOneShot(playerMoanSfx);
            
            StartCoroutine(HitFlash());
            
            if (curFeatherIndex < 0)
            {
                //player death
                PlayerDeath();
            }
        }
    }
    
    private IEnumerator HitFlash()
    {
        playerRenderer.material = hitFlashMaterial;
        yield return new WaitForSeconds(hitFlashDuration);
        playerRenderer.material = originalMaterial;
    }

    private void PlayerDeath()
    {
        Debug.Log("PLAYER DIED");
        
        playerRenderer.enabled = false;
        GetComponent<PlayerMovement>().enabled = false;
        GetComponent<PlayerAttackController>().enabled = false;
        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
        Instantiate(playerGhostPrefab, transform.position, Quaternion.identity);
        FMODUnity.RuntimeManager.PlayOneShot(playerDeathSFX);
        
        Invoke("ReloadSceneAfterDeath", timeTillReloadSceneAfterDeath);
    }

    private void ReloadSceneAfterDeath()
    {
        Vector3 playerPos = GetComponent<CheckpointScript>().playerRespawn;
        
        SceneTransition sceneTransition = Instantiate(sceneTransitionPrefab).GetComponent<SceneTransition>();
        sceneTransition?.TriggerTransition(SceneManager.GetActiveScene().name, "", playerPos);
    }
}
