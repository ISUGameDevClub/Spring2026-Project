using System;
using ISUGameDev.SpearGame.Player;
using UnityEngine;

public class ArrowFire : MonoBehaviour
{
    [SerializeField] GameObject arrow;
    [SerializeField] GameObject firingPoint;
    [SerializeField] private GameObject enemyCanvas;

    private GameObject playerReference;

    void Start()
    {
        playerReference = FindAnyObjectByType<PlayerController>().gameObject;
    }

    // Is used as an animation event during the archer attack
    public void ShootArrow()
    {
        GameObject newArrow = Instantiate(arrow,firingPoint.transform.position,Quaternion.identity);
        if (firingPoint.transform.position.x > transform.position.x)
        {
            newArrow.GetComponent<ArrowMovement>().direction = 1;
        }
    }

    // Animation event to make them change their aim at the appropriate time
    public void FlipAfterFiring()
    {
        if (playerReference != null)
        {
            if (transform.position.x < playerReference.transform.position.x)
            {
                transform.rotation = Quaternion.Euler(new Vector3(0, 180 ,0));
                enemyCanvas.transform.rotation = Quaternion.Euler(new Vector3(0, 0 ,0));
            }
            else
            {
                transform.rotation = Quaternion.Euler(new Vector3(0, 0 ,0));
                enemyCanvas.transform.rotation = Quaternion.Euler(new Vector3(0, 0 ,0));
            }
        }
    }

}
