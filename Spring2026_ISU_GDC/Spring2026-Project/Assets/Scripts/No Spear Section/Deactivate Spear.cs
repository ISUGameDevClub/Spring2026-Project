using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using static ISUGameDev.SpearGame.BasePlayerState;

public class DeactivateSpear : MonoBehaviour
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
        //GameObject thrownSpear = GameObject.Find("Spear(Clone)");
        
        if (collision.gameObject.tag == "Player")
        {
            SpearInHand.SetActive(false);
            collision.transform.root.gameObject.GetComponent<PlayerStateMachine>().ChangeState(PlayerStateType.RoamingWithoutSpear);
            

        }
    }
    
}
