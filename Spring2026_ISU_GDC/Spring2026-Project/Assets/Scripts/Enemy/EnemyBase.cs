using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace ISUGameDev.SpearGame.Enemy
{
    public class EnemyBase : EntityBase
    {
       [SerializeField]
       private EnemyMovement movement;

       private bool showHealthBar = false;
       [SerializeField] private Slider healthSlider;
       [SerializeField] private Slider ghostSlider;
       [SerializeField] private float ghostLerpSpeed = 2f;
       [SerializeField] private float ghostDelay = 0.6f;

       [SerializeField] private Renderer enemyRenderer;
       [SerializeField] private Material hitFlashMaterial;
       [SerializeField] private float hitFlashDuration = 0.1f;
       
       [SerializeField] private FMODUnity.EventReference spearHitSFX;
       [SerializeField] private FMODUnity.EventReference enemyGruntSFX;
       [SerializeField] private FMODUnity.EventReference enemyDeathSFX;

       private Material originalMaterial;
       private float ghostMoveTimer = 0f;

       public override void TakeDamage(int damageValue, float knockback, float stunDuration, GameObject attacker)
       {
          showHealthBar = true;
          ghostMoveTimer = ghostDelay;
          base.TakeDamage(damageValue, knockback, stunDuration, attacker);
          Debug.Log(damageValue + " damage taken");
          FMODUnity.RuntimeManager.PlayOneShot(spearHitSFX);
          FMODUnity.RuntimeManager.PlayOneShot(enemyGruntSFX);

          StartCoroutine(HitFlash());

          if (health <= 0.0f)
          {
             FMODUnity.RuntimeManager.PlayOneShot(enemyDeathSFX);
             Destroy(gameObject, 0.2f);
          }
       }

       private IEnumerator HitFlash()
       {
          enemyRenderer.material = hitFlashMaterial;
          yield return new WaitForSeconds(hitFlashDuration);
          enemyRenderer.material = originalMaterial;
       }

       private void Start()
       {
          originalMaterial = enemyRenderer.material;

          healthSlider.gameObject.SetActive(false);
          healthSlider.maxValue = maxHealth;

          ghostSlider.gameObject.SetActive(false);
          ghostSlider.maxValue = maxHealth;
          ghostSlider.value = maxHealth;
       }

       private void Update()
       {
          if (showHealthBar)
          {
             healthSlider.gameObject.SetActive(true);
             ghostSlider.gameObject.SetActive(true);
             
             healthSlider.value = health;
             
             if (ghostMoveTimer > 0f)
             {
                ghostMoveTimer -= Time.deltaTime;
             }
             else
             {
                float easeOffset = 0.35f;
                ghostSlider.value = Mathf.MoveTowards(ghostSlider.value, health - easeOffset, Time.deltaTime * ghostLerpSpeed);
             }
          }
       }

       protected override void Awake()
       {
          base.Awake();
       }
    }
}