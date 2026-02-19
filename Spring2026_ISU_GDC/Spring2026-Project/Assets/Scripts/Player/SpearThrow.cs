using UnityEngine;

public class SpearThrow : MonoBehaviour
{
     public GameObject aimIndicatorHolder;//empty object ontop of the player
     public GameObject aimIndicatorObject;//physical arrow, child of the empty aim indicator holder
     public Camera playerCam;
     private bool aimingSpear;
    private Vector2 direction;
    public GameObject Spear;
    private PlayerAttacks PA;
    private PlayerMovement PM;

    public GameObject SpearInHand;
    public static bool isSpearInHand;

    public float TravelToSpearSpeed;
    public static bool travellingToSpear;

    void Start()
    {
        PA = GetComponent<PlayerAttacks>();
        PM = GetComponent<PlayerMovement>();
        aimingSpear = false;
        aimIndicatorObject.SetActive(false);
        isSpearInHand = true;
    }

    // Update is called once per frame
    Vector2 playerDirection;
    void Update()
    {
        
        CapsuleCollider2D playerHitbox = gameObject.GetComponent<CapsuleCollider2D>();
        
        if (!travellingToSpear)
        {
            playerHitbox.isTrigger = false;
            if (isSpearInHand)
            {
                giveSpear();
                AimSpear();
            }
            else if (Input.GetMouseButton(0) && SpearTravel.hitWall)//replace mouse0 with input systems
            {
                travellingToSpear = true;
                playerDirection = new Vector2((spawnedSpear.transform.position.x - gameObject.transform.position.x)*TravelToSpearSpeed, (spawnedSpear.transform.position.y - gameObject.transform.position.y)*TravelToSpearSpeed);
                travelToSpear(playerDirection);
            }
        }
        else
        {
            playerHitbox.isTrigger = true;//pass through walls when dashing to spear
            travelToSpear(playerDirection);
        }
    }
       private void AimSpear()
    {
        if (Input.GetMouseButton(0)) // if input is held, update the arrow indicator. replace with input systems
        {
            PA.enabled = false;
            PM.enabled = false;//disable movement and attacks when aiming
            aimingSpear = true;
            aimIndicatorObject.SetActive(true);
            Vector3 mousePosition;
            mousePosition = Input.mousePosition;
			mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
            
            direction = new Vector2(mousePosition.x - aimIndicatorHolder.transform.position.x,mousePosition.y-aimIndicatorHolder.transform.position.y);
            aimIndicatorHolder.transform.up = direction;
        }
        else
        {
            if(aimingSpear)// if left click was just released throw spear
                {
                PA.enabled = true;
                PM.enabled = true;
                aimingSpear =false;
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
        spawnedSpear.transform.Rotate(0f,0f,90f);
        removeSpear();
    }

    public void removeSpear()
    {
        SpearInHand.SetActive(false);
        isSpearInHand = false;
        PA.enabled = false;
    }
    public void giveSpear()
    {
        SpearInHand.SetActive(true);
        isSpearInHand = true;
        PA.enabled = true;
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
