using UnityEngine;

public class SpearThrow : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
     public GameObject aimIndicator;
     public GameObject aimIndicatorObject;
     public Camera playerCam;
     public bool aimingSpear;
    void Start()
    {
        aimingSpear = false;
        aimIndicatorObject.SetActive(false);
    }

    // Update is called once per frame
    
    void Update()
    {
        AimSpear();
    }
    public Vector2 direction;
    public GameObject Spear;
       private void AimSpear()
    {
        if(Input.GetMouseButton(0)) // if left click is held, update the arrow indicator
        {
            aimingSpear=true;
            aimIndicatorObject.SetActive(true);
            Vector3 mousePosition;
            mousePosition = Input.mousePosition;
			mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

            direction = new Vector2(mousePosition.x - aimIndicator.transform.position.x,mousePosition.y-aimIndicator.transform.position.y);
            aimIndicator.transform.up = direction;
        }
        else
        {
            if(aimingSpear)// if left click was just released throw spear
                {   
                    aimingSpear=false;
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
    }
}
