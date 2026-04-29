using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace ISUGameDev.SpearGame.Enemy
{
    public class LokiBase : EntityBase
    {
       [SerializeField]
       private EnemyMovement movement;

       private bool showHealthBar = true;
       [SerializeField] private Slider healthSlider;
       [SerializeField] private Slider ghostSlider;
       [SerializeField] private float ghostLerpSpeed = 2f;
       [SerializeField] private float ghostDelay = 0.6f;

       [SerializeField] private Renderer enemyRenderer;
       [SerializeField] private Material hitFlashMaterial;
       [SerializeField] private float hitFlashDuration = 0.1f;
        public Animator animator;

       [Header("FMOD Events")]
       [SerializeField] private FMODUnity.EventReference spearHitSFX;
       [SerializeField] private FMODUnity.EventReference enemyGruntSFX;
       [SerializeField] private FMODUnity.EventReference enemyDeathSFX;
       public FMODUnity.EventReference lokiLaughSFX;

       private Material originalMaterial;
       private float ghostMoveTimer = 0f;
        private GameObject player;

        [Header("Loki Stuff")]
        [SerializeField] private Vector3[] TelePositions;
     
        public LokiStateMachine StateMachine { get; set; }
        public LokiIdleState IdleState { get; set; }
        public LokiAttack1State Attack1State { get; set; }
        public LokiAttack2State Attack2State { get; set; }
        public LokiDeathState DeathState { get; set; }

        private int att1Weight = 1;
        private int att2Weight = 1;

        public bool halfHealth = false;
        
        [SerializeField] private string sceneToLoad;
    
        [Header("Leave this blank if you don't want to display a level name")]
        [SerializeField] private string levelNameToDisplay;

        [SerializeField] private GameObject sceneTransitionPrefab;
        
        
        

        public override void TakeDamage(int damageValue, float knockback, float stunDuration, GameObject attacker)
        {
            ghostMoveTimer = ghostDelay;
            base.TakeDamage(damageValue, knockback, stunDuration, attacker);
            Debug.Log(damageValue + " damage taken");
            FMODUnity.RuntimeManager.PlayOneShot(spearHitSFX);
            FMODUnity.RuntimeManager.PlayOneShot(enemyGruntSFX);

            StartCoroutine(HitFlash());

            // Trigger Phase 2
            if (health < maxHealth / 2)
            {
                halfHealth = true;
            }

            //Dead
            if (health <= 0.0f)
            {
                FMODUnity.RuntimeManager.PlayOneShot(enemyDeathSFX);
                StateMachine.ChangeState(DeathState);
                
                SceneTransition sceneTransition = Instantiate(sceneTransitionPrefab).GetComponent<SceneTransition>();
                sceneTransition?.TriggerTransition(sceneToLoad, levelNameToDisplay);
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

            StateMachine.Initialize(IdleState);
       }

       private void Update()
       {
            StateMachine.CurrentLokiState.FrameUpdate();
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

            StateMachine = new LokiStateMachine();

            IdleState = new LokiIdleState(this, StateMachine);
            Attack1State = new LokiAttack1State(this, StateMachine);
            Attack2State = new LokiAttack2State(this, StateMachine);
            DeathState = new LokiDeathState(this, StateMachine);

            animator = transform.GetChild(0).GetComponent<Animator>();

            player = GameObject.FindWithTag("Player");
       }

        
        public void ChooseAttack()
        {
            int totalWeight = att1Weight + att2Weight;
            int roll = UnityEngine.Random.Range(0, totalWeight);
            if (roll < att1Weight)
            {
                //Attack 1
                StateMachine.ChangeState(Attack1State);
                att1Weight = 1;
                att2Weight++;
            }
            else
            {
                //Attack 2
                StateMachine.ChangeState(Attack2State);
                att2Weight = 1;
                att1Weight++;
            }

        }

        public void SwapToIdleState()
        {
            StateMachine.ChangeState(IdleState);
        }

        public void ResetPosition()
        {
            int pos = UnityEngine.Random.Range(0, TelePositions.Length);
            transform.position = TelePositions[pos];
            if (transform.position.x - player.transform.position.x > 0)
            {
                enemyRenderer.transform.localScale = new Vector3(-2, enemyRenderer.transform.localScale.y, enemyRenderer.transform.localScale.z);
            }
            else
            {
                enemyRenderer.transform.localScale = new Vector3(2, enemyRenderer.transform.localScale.y, enemyRenderer.transform.localScale.z);
            }
        }

    }

}