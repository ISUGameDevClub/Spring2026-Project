using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttacks : MonoBehaviour
{
    private Animator playerAnimator;
    private float comboTimer = 0;

    /*
    The window of time in seconds that the player has to use the second basic attack. If they use the first basic attack button and then
    wait longer than the comboWindow time, the next time they use the attack button it will be once again basic attack 1. */
    [SerializeField] float comboWindow = 1.5f;
    [SerializeField] AnimationClip[] basicAttacks;
    private int currentComboAttack = 0;

    private delegate void AttackMechanic();
    private AttackMechanic useAttack;

    public bool currentlyAttacking = false;

    [SerializeField] private InputActionReference attack;

    
    
    void Awake()
    {
        playerAnimator = GetComponent<Animator>();
        useAttack = UseComboAttacks;

    }

    void OnEnable()
    {
        attack.action.started += UseCurrentAttack;
    }
    void OnDisable()
    {
        attack.action.started -= UseCurrentAttack;
    }

    // Update is called once per frame
    void Update()
    {
        
        if(currentComboAttack > 0 && comboTimer < Time.time)
        {
            currentComboAttack = 0;
        }
    }

    //  Used for when the player attacks while equipped with the spear.
    private void UseComboAttacks()
    {
        playerAnimator.Play(basicAttacks[currentComboAttack].name);
        if (currentComboAttack < basicAttacks.Count() - 1)
        {
            currentComboAttack += 1;
            comboTimer = Time.time + comboWindow;
        }
        else
        {
            // Reset the attacks back to the start of the combo.
            currentComboAttack = 0;
        }
    }

    private void LungeToSpear()
    {
        // When we do not have a spear, we replace the useAttack() delegate with this.
    }
    
   
 


    public void UseCurrentAttack(InputAction.CallbackContext context)
    {
        if (!currentlyAttacking && playerAnimator != null)
        {
            useAttack();
        }
    }


}


