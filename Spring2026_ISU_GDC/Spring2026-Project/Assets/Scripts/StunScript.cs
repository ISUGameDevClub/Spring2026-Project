using ISUGameDev.SpearGame.Player;
using ISUGameDev.SpearGame.Player.PlayerState;
using UnityEngine;

// TODO: refactor...
public class StunScript : MonoBehaviour
{

    public PlayerStateMachine playerStateMachine;
    float stunTimer = 0f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (stunTimer <= Time.time)
        {
            //playerStateMachine.ChangeState(PlayerStateType.RoamingWithSpear);
        }
    }

    public void InflictStun(float stunTime)
    {
        playerStateMachine.ChangeState(PlayerStateType.Stunned);
        stunTimer = Time.time + stunTime;
    }
}
