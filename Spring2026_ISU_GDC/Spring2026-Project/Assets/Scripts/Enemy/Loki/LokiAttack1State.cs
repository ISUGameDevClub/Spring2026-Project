using UnityEngine;
using ISUGameDev.SpearGame.Enemy;

public class LokiAttack1State : LokiState
{
    public LokiAttack1State(LokiBase loki, LokiStateMachine lokiStateMachine) : base(loki, lokiStateMachine) { }


    public override void EnterState()
    {
        base.EnterState();
        loki.GetComponent<SlashAttack>().TriggerSlashAttack();
    }

    public override void ExitState()
    {
        loki.ResetPosition();
        base.ExitState();

    }

    public override void FrameUpdate()
    {
 
    }
}
