using UnityEngine;
using ISUGameDev.SpearGame.Enemy;

public class LokiDeathState : LokiState
{
    public LokiDeathState(LokiBase loki, LokiStateMachine lokiStateMachine) : base(loki, lokiStateMachine)
    {

    }

    public override void EnterState()
    {
        base.EnterState();
        lokiStateMachine.LockState();
    }

    public override void ExitState()
    {
        base.ExitState();
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();
    }
}
