using UnityEngine;
using static ISUGameDev.SpearGame.BasePlayerState;

/// <summary>
/// Class to represent the attack for a player dashing to an already thrown spear. Must validate the spear is thrown before executing dash
/// </summary>
public class SpearDashAttack : MonoBehaviour, IAttack
{
    private GameObject spearObjCache;
    private bool spearStuckInWall;
    private bool isDashing;
    private Vector2 dashDirection;
    public GameObject SpearInHand;

    [SerializeField] private float travelSpeed = 10f;
    [SerializeField] private float pickUpDistance = 0.3f;

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
            isDashing = false;
            spearStuckInWall = false;
            rb.linearVelocity = Vector2.zero;
            FindFirstObjectByType<PlayerStateMachine>().ChangeState(PlayerStateType.RoamingWithSpear);
            Destroy(spearObjCache.gameObject);
            giveSpear();
            return;
        }

        rb.linearVelocity = dashDirection;
    }

    private void DashTowardsSpear()
    {
        //validate that spear is stuck in wall before executing attack
        if (!spearStuckInWall) { return; }
        if (isDashing) { return; }

        FindFirstObjectByType<PlayerStateMachine>().ChangeState(PlayerStateType.DashingTowardsSpear);

        Vector3 dir = (spearObjCache.transform.position - transform.root.position).normalized * travelSpeed;
        dashDirection = new Vector2(dir.x, dir.y);
        //set update logic in motion for dashing
        isDashing = true;
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
