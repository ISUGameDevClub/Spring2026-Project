using UnityEngine;
using PlayerStateType = ISUGameDev.SpearGame.BasePlayerState.PlayerStateType;

/// <summary>
/// Class to represent just the spear throwing attack. Must validate that the player has the spear before executing the attack.
/// </summary>
public class SpearThrowAttack : MonoBehaviour, IAttack
{



    public GameObject aimIndicatorHolder;//empty object ontop of the player
    public GameObject aimIndicatorObject;//physical arrow, child of the empty aim indicator holder
    private bool aimingSpear;
    private Vector2 direction;
    public GameObject Spear;

    public GameObject SpearInHand;
    public static bool isSpearInHand;

    /// <summary>
    /// Flag that controls whether we start aiming the spear each frame.
    /// </summary>
    private bool currentlyAimingSpear;


    public AttackMechanic GetAttackImplementation()
    {
        return SpearThrowImplementation;
    }

    private void SpearThrowImplementation()
    {
        //Change Player State to Aiming State
        this.transform.root.gameObject.GetComponent<PlayerStateMachine>().ChangeState(PlayerStateType.AimingSpear);
        giveSpear();
        AimSpear();
    }

    void Start()
    {
        aimingSpear = false;
        aimIndicatorObject.SetActive(false);
        isSpearInHand = true;
    }

    void Update()
    {
        if (currentlyAimingSpear)
        {
            AimSpear();
        }
    }
    private void AimSpear()
    {

        //TODO: make this use Input System
        if (Input.GetMouseButton(1)) // if input is held, update the arrow indicator. replace with input systems
        {
            //playerAttacks.enabled = false;
            //playerMovement.enabled = false;//disable movement and attacks when aiming
            aimingSpear = true;
            aimIndicatorObject.SetActive(true);
            Vector3 mousePosition;
            mousePosition = Input.mousePosition;
            mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

            direction = new Vector2(mousePosition.x - aimIndicatorHolder.transform.position.x, mousePosition.y - aimIndicatorHolder.transform.position.y);
            aimIndicatorHolder.transform.up = direction;
        }
        else
        {
            if (aimingSpear)// if left click was just released throw spear
            {
                //playerAttacks.enabled = true;
                //playerMovement.enabled = true;
                aimingSpear = false;
                ThrowSpear();
            }
            aimIndicatorObject.SetActive(false);
        }
    }
    GameObject spawnedSpear;
    private void ThrowSpear()
    {
        spawnedSpear = Instantiate(Spear);
        spawnedSpear.transform.position = gameObject.transform.position;
        spawnedSpear.transform.up = direction;
        spawnedSpear.transform.Rotate(0f, 0f, 90f);
        removeSpear();

        //Change player state to RoamingWithoutSpear
        this.transform.root.gameObject.GetComponent<PlayerStateMachine>().ChangeState(PlayerStateType.RoamingWithoutSpear);

        currentlyAimingSpear = false;
    }

    public void removeSpear()
    {
        SpearInHand.SetActive(false);
        isSpearInHand = false;
        //playerAttacks.enabled = false;
    }
    public void giveSpear()
    {
        SpearInHand.SetActive(true);
        isSpearInHand = true;
        currentlyAimingSpear = true;
        //playerAttacks.enabled = true;
    }
    public void travelToSpear(Vector2 playerDirection)
    {
        Rigidbody2D rb = gameObject.GetComponent<Rigidbody2D>();
        rb.linearVelocity = playerDirection;
    }
    /*
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (travellingToSpear&&col.gameObject.tag=="Enemy")
        {
            //deal damage & knockback to any enemies in path
        }
    */
}
