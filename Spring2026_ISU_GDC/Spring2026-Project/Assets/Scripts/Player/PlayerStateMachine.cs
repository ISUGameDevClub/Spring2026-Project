using UnityEngine;

public class PlayerStateMachine : MonoBehaviour
{
    [SerializeField] PlayerMovement movement;
    [SerializeField] PlayerAttacks attacks;

    public enum PlayerStates { active, dormant, attacking, stunned};
    private PlayerStates currentState;

    private PlayerEventManager playerEventManager;
    
    void Awake()
    {
        ChangeState(PlayerStates.active);
    }

    public void ChangeState(PlayerStates newState)
    {
        currentState = newState;
        switch (currentState)
        {
            case PlayerStates.active:
                movement.enabled = true;
                attacks.enabled = true;
            break;

            case PlayerStates.dormant:
                movement.enabled = false;
                attacks.enabled = false;
            break;

            case PlayerStates.attacking:
                movement.enabled = false;
                attacks.enabled = true;
            break;
        }
    }
    
}
