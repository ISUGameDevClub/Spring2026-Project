using ISUGameDev.SpearGame.Player.PlayerState;
using UnityEngine;

namespace ISUGameDev.SpearGame.Player.PlayerAttacks
{
    /// <summary>
    /// Class to represent the attack for a player dashing to an already thrown spear. Must validate the spear is thrown before executing dash
    /// </summary>
    public class SpearDashAttack : MonoBehaviour, IAttack
    {
        private GameObject spearObjCache;
        private Vector3 playerRotCache;
        private bool spearStuckInWall;
        private bool isDashing;
        private Vector2 dashDirection;
        private Animator playerAnimator;
        [SerializeField] private AnimationClip playerIdleWithSpearClip;
        [SerializeField] private AnimationClip playerDashTowardsSpearClip;
        [SerializeField] private LayerMask wallLayer;
        public GameObject SpearInHand;

        [SerializeField] private float travelSpeed = 10f;
        [SerializeField] private float pickUpDistance = 0.3f;
        
        [SerializeField] private FMODUnity.EventReference dashSFX;
        [SerializeField] private FMODUnity.EventReference spearPickupSFX;

        [SerializeField] private HitboxProperties dashHitbox;

        private void Awake()
        {
            playerAnimator = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
        }

        public AttackMechanic GetAttackImplementation()
        {
            return DashTowardsSpear;
        }

        private void Start()
        {
            FindFirstObjectByType<PlayerEventManager>().OnPlayerSpearStuckInWall.AddListener(OnSpearStuckInWall);
        }

        private void OnSpearStuckInWall(GameObject spearObj)
        {
            //cache the stuck spears location
            spearObjCache = spearObj;

            //record that spear was stuck
            spearStuckInWall = true;
        }

        private void Update()
        {
            if (!isDashing) return;

            Rigidbody2D rb = transform.root.gameObject.GetComponent<Rigidbody2D>();

            //stop dashing when close enough to spear
            if (Vector3.Distance(transform.root.position, spearObjCache.transform.position) < pickUpDistance)
            {
                PickUpSpear();
                return;
            }

            //recalculate direction each frame so player tracks the spear's actual position
            Vector3 dir = (spearObjCache.transform.position - transform.root.position).normalized * travelSpeed;
            rb.linearVelocity = new Vector2(dir.x, dir.y);
        }

        private void DashTowardsSpear()
        {
            //validate that spear is stuck in wall before executing attack
            if (!spearStuckInWall) { return; }
            if (isDashing) { return; }

            transform.root.gameObject.GetComponent<PlayerHealthController>().cannotTakeDamage = true;
            
            dashHitbox.gameObject.SetActive(true);
            
            //sfx
            FMODUnity.RuntimeManager.PlayOneShot(dashSFX);
            
            //cache players rotation
            var playerObj = playerAnimator.gameObject;
            playerRotCache = playerObj.transform.rotation.eulerAngles;

            playerAnimator.Play(playerDashTowardsSpearClip.name);
            FindFirstObjectByType<PlayerStateMachine>().ChangeState(PlayerStateType.DashingTowardsSpear);

            //get dash direction
            Vector3 dir = (spearObjCache.transform.position - transform.root.position).normalized * travelSpeed;
            dashDirection = new Vector2(dir.x, dir.y);

            //rotate player to point towards spear
            float zAngle = Mathf.Atan2(spearObjCache.transform.position.y - transform.root.position.y,
                spearObjCache.transform.position.x - transform.root.position.x) * Mathf.Rad2Deg - 90f;
            float yAngle = 0;
            if (spearObjCache.transform.position.x < playerObj.transform.position.x)
            {
                yAngle = 180;
                zAngle = -zAngle;
            }
            playerObj.transform.rotation = Quaternion.Euler(0, yAngle, zAngle);

            //temporarily disable player collision with walls
            Physics2D.IgnoreLayerCollision(playerObj.layer, Mathf.RoundToInt(Mathf.Log(wallLayer.value, 2)), true);

            //set update logic in motion for dashing
            isDashing = true;
        }

        public void PickUpSpear()
        {
            FMODUnity.RuntimeManager.PlayOneShot(spearPickupSFX);
            
            Rigidbody2D rb = transform.root.gameObject.GetComponent<Rigidbody2D>();
            transform.root.gameObject.transform.rotation = Quaternion.Euler(playerRotCache);
            
            transform.root.gameObject.GetComponent<PlayerHealthController>().cannotTakeDamage = false;
            
            dashHitbox.gameObject.SetActive(false);

            playerAnimator.SetBool("HoldingSpear", true);
            playerAnimator.Play(playerIdleWithSpearClip.name);

            isDashing = false;
            spearStuckInWall = false;
            rb.linearVelocity = Vector2.zero;
            FindFirstObjectByType<PlayerStateMachine>().ChangeState(PlayerStateType.RoamingWithSpear);
            Destroy(spearObjCache?.gameObject);
            Physics2D.IgnoreLayerCollision(transform.root.gameObject.layer, Mathf.RoundToInt(Mathf.Log(wallLayer.value, 2)), false);

            giveSpear();
        }

        public void travelToSpear(Vector2 playerDirection)
        {
            Rigidbody2D rb = transform.root.gameObject.GetComponent<Rigidbody2D>();
            rb.linearVelocity = playerDirection;
        }

        public void giveSpear()
        {
            SpearInHand.SetActive(true);
        }
    }
}