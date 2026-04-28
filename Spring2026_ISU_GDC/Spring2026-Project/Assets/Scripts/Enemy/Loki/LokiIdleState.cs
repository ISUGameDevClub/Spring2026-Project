using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using ISUGameDev.SpearGame.Enemy;

public class LokiIdleState : LokiState
{
    private float laughChance = 0.5f;
    private float laughDuration = 6;
    private float idleDuration = 4;
    private int state = 0;
    private float timer = 0;
    private float idleTime = 0;
    public LokiIdleState(LokiBase loki, LokiStateMachine lokiStateMachine) : base(loki, lokiStateMachine) { }


    public override void EnterState()
    {
        base.EnterState();
        loki.animator.Play("Loki Idle");
        if (Random.value < laughChance)
        {
            loki.animator.Play("LokiLaugh");
            FMODUnity.RuntimeManager.PlayOneShot(loki.lokiLaughSFX);
            idleTime = laughDuration;
        }
        else if (!loki.halfHealth)
        {
            idleTime = idleDuration;
        }
        else
        {
            idleTime = idleDuration / 2;
        }
        state = 1;
        timer = 0;
    }

    public override void ExitState()
    {
        base.ExitState();
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();
        timer += Time.deltaTime;
        switch (state)
        {
            case 1:
                if (timer > idleTime)
                {
                    state++;
                    timer = 0;
                    loki.ChooseAttack();
                }
                break;
        }
    }

    

}
