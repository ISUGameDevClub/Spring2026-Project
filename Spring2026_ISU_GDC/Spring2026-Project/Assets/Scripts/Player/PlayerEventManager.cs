using ISUGameDev.SpearGame;
using UnityEngine;
using UnityEngine.Events;

public class PlayerEventManager : MonoBehaviour
{
    public UnityEvent gotHit;

    public UnityEvent died; //  ); RIP

    public UnityEvent spearEquipped;
    public UnityEvent spearRemoved;

    public UnityEvent landOnGround;

    public UnityEvent leaveGround;

    /// <summary>
    /// Invoked when the player's state is changed.
    /// Param: The new player state
    /// </summary>
    public UnityEvent<BasePlayerState> OnPlayerStateChanged;

    /// <summary>
    /// Invoked when the player's spear got stuck in a wall.
    /// Param: The spear's game object
    /// </summary>
    public UnityEvent<GameObject> OnPlayerSpearStuckInWall;

    /// <summary>
    /// Invoked when the player's spear travels off camera.
    /// </summary>
    public UnityEvent OnPlayerSpearTraveledOffCamera;

    // These are just some starter ideas. This will change heavily depending on the game design document in the coming week.
}


