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

    void Start()
    {
        PA = GetComponent<PlayerAttacks>();
        PM = GetComponent<PlayerMovement>();
        aimingSpear = false;
        aimIndicatorObject.SetActive(false);
        isSpearInHand = true;
    }

    // Update is called once per frame
    
    void Update()
    {
        if (isSpearInHand)
        {
            giveSpear();
            AimSpear();
        }
    }
    
       private void AimSpear()
    {
        if (Input.GetMouseButton(0)) // if left click is held, update the arrow indicator
        {
            PA.enabled = false;
            PM.enabled = false;
            aimingSpear =true;
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

    private void ThrowSpear()
    {
        GameObject spawnedSpear = Instantiate(Spear);
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
}
