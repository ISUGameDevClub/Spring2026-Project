using System.Linq;
using ISUGameDev.SpearGame;
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

    
    private AttackMechanic currentPrimaryAttack;
    private AttackMechanic currentSecondaryAttack;


    [SerializeField] private InputActionReference primaryAttack;
    [SerializeField] private InputActionReference secondaryAttack;

    /// <summary>
    /// The PlayerEventManager associated with this Player
    /// </summary>
    private PlayerEventManager playerEventManager;
    
    
    
    void Awake()
    {
        playerAnimator = GetComponent<Animator>();
        playerEventManager = GetComponent<PlayerEventManager>();


        //subscribe ChangeCurrentAttackSubscriptionFromState to OnPlayerStateChanged event
        playerEventManager.OnPlayerStateChanged.AddListener(ChangeCurrentAttackSubscriptionFromState);
    }

    // Jake TODO: Unify this with single source of truth input system later
    void OnEnable()
    {
        primaryAttack.action.started += UseCurrentPrimaryAttack;
        secondaryAttack.action.started += UseCurrentSecondaryAttack;
    }
    void OnDisable()
    {
        primaryAttack.action.started -= UseCurrentPrimaryAttack;
        secondaryAttack.action.started -= UseCurrentSecondaryAttack;
    }

    private void OnDestroy()
    {
        //unsubscribe OnDestroy
        playerEventManager.OnPlayerStateChanged.RemoveListener(ChangeCurrentAttackSubscriptionFromState);
    }

    /// <summary>
    /// Calls the currentPrimaryAttack function pointer.
    /// </summary>
    /// <param name="context"></param>
    public void UseCurrentPrimaryAttack(InputAction.CallbackContext context)
    {
        if (playerAnimator != null)
        {
            currentPrimaryAttack?.Invoke();
        }
    }

    /// <summary>
    /// Calls the currentSecondaryAttack function pointer.
    /// </summary>
    /// <param name="context"></param>
    public void UseCurrentSecondaryAttack(InputAction.CallbackContext context)
    {
        if (playerAnimator != null)
        {
            currentSecondaryAttack?.Invoke();
        }
    }


    /// <summary>
    /// Changes the planned attack the player will use when 'Attack' input action is triggered. 
    /// </summary>
    /// <param name="newAttackSubscription">The new attack delegate our currentAttack pointer will be subscribed to</param>
    public void ChangeCurrentAttackSubscriptionFromState(BasePlayerState newState)
    {
        currentPrimaryAttack = newState.GetPrimaryAttackMechanicForState();
        currentSecondaryAttack = newState.GetSecondaryAttackMechanicForState();
    }

}


