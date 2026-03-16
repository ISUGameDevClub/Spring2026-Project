using System;
using UnityEngine;
using UnityEngine.InputSystem;
using PlayerStateType = ISUGameDev.SpearGame.BasePlayerState.PlayerStateType;

public class PlayerPause : MonoBehaviour
{
    [SerializeField] private GameObject pauseUIPrefab;
    [SerializeField] private PlayerStateType playerPauseState;
    [SerializeField] private InputActionReference pauseAction;
    
    private PlayerStateMachine playerStateMachine;
    private PlayerStateType previousState;
    private PauseMenu curPauseMenuRef;

    private void Awake()
    {
        playerStateMachine = GetComponent<PlayerStateMachine>();
        pauseAction.action.started += OnPauseActionTriggered;
    }

    private void OnPauseActionTriggered(InputAction.CallbackContext context)
    {
        //if pause menu doesnt exist, create it and send player to 'pause' state
        if (curPauseMenuRef == null)
        {
            curPauseMenuRef = Instantiate(pauseUIPrefab).GetComponent<PauseMenu>();
            curPauseMenuRef.OnPauseMenuClosed += OnPauseMenuClosed;
            previousState = playerStateMachine.currentState.playerStateType;
            playerStateMachine.ChangeState(playerPauseState);
        }
        else //close pause menu and restore previous player state
        {
            curPauseMenuRef.CloseMenu();
        }
    }

    private void OnPauseMenuClosed()
    {
        playerStateMachine.ChangeState(previousState);
        curPauseMenuRef = null;
    }
}
