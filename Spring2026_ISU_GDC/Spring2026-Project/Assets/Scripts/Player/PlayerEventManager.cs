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
    // These are just some starter ideas. This will change heavily depending on the game design document in the coming week.
}
