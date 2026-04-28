using UnityEngine;
using ISUGameDev.SpearGame.Enemy;

public class LokiStateMachine
{
    private bool lockState = false;
    public LokiState CurrentLokiState { get; set; }
    public void Initialize(LokiState startingState)
    {
        CurrentLokiState = startingState;
        CurrentLokiState.EnterState();
    }

    public void ChangeState(LokiState newState)
    {
        if(!lockState)
        {
            CurrentLokiState.ExitState();
            CurrentLokiState = newState;
            CurrentLokiState.EnterState();
        }

    }

    public void LockState()
    {
        lockState = true;
    }

}
