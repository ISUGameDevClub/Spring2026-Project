using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using static ISUGameDev.SpearGame.BasePlayerState;

public class ActivateSpear : MonoBehaviour
{
    public GameObject SpearInHand;
    public GameObject dashObject;

    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {

            
            GameObject thrownSpear = GameObject.Find("Spear(Clone)");

            if (thrownSpear == null)
            {
                SpearInHand.SetActive(true);
                collision.transform.root.gameObject.GetComponent<PlayerStateMachine>().ChangeState(PlayerStateType.RoamingWithSpear);
            }



        }
    }
}
