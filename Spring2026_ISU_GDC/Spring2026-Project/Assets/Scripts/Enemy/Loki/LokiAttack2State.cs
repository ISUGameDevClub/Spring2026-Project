using UnityEngine;
using ISUGameDev.SpearGame.Enemy;

public class LokiAttack2State : LokiState
{
    private Animator animator;
    private float poofTime = 0.2f;
    private float attackTime = 5f;

    private int state = 0;
    private float timer = 0;
    public LokiAttack2State(LokiBase loki, LokiStateMachine lokiStateMachine) : base(loki, lokiStateMachine)
    {

    }

    public override void EnterState()
    {
        base.EnterState();
        animator = loki.transform.GetChild(0).GetComponent<Animator>();
        loki.GetComponent<LokiAttack2>().StartAttack();
        animator.Play("Poof");
        state = 0;
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
            case 0:
                if (timer >= poofTime)
                {
                    state++;
                    timer = 0;
                    loki.transform.position = new Vector3(100, 100, 0);
                }
                break;
            case 1:
                if(timer >= attackTime)
                {
                    state++;
                    timer = 0;
                    animator.Play("Poof");
                    loki.ResetPosition();
                }
                break;
            case 2:
                if(timer >= poofTime)
                {
                    state++;
                    timer = 0;
                    loki.SwapToIdleState();
                }
                break;
        }
    }
}
