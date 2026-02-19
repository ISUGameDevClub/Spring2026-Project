using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;

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
            playerStateMachine.ChangeState(ISUGameDev.SpearGame.BasePlayerState.PlayerStateType.RoamingWithSpear);
        }
    }

    public void InflictStun(float stunTime)
    {
        playerStateMachine.ChangeState(ISUGameDev.SpearGame.BasePlayerState.PlayerStateType.Stunned);
        stunTimer = Time.time + stunTime;
    }
}
