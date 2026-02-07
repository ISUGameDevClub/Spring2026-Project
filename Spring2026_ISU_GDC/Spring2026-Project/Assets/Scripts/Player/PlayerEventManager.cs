using UnityEngine;
using UnityEngine.Events;

public class PlayerEventManager : MonoBehaviour
{
    public UnityEvent gotHit;
    //Mainly see this as useful for when we need things to occur when the player takes stun.
    public UnityEvent spearEquipped;
    public UnityEvent spearRemoved;
    //Might make this just one event with a boolean.
    public UnityEvent<bool> onGround;

    public UnityEvent<PlayerStateMachine.PlayerStates> stateChanged;

    // These are just some starter ideas. This will change heavily depending on the game design document in the coming week.
}
