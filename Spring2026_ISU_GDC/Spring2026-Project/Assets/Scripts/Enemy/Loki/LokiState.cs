using UnityEngine;
using ISUGameDev.SpearGame.Enemy;

public class LokiState 
{
    protected LokiBase loki;
    protected LokiStateMachine lokiStateMachine;

    public LokiState(LokiBase loki, LokiStateMachine lokiStateMachine)
    {
        this.loki = loki;
        this.lokiStateMachine = lokiStateMachine;
    }

    public virtual void EnterState() { }
    public virtual void ExitState() { }
    public virtual void FrameUpdate() { }
}
