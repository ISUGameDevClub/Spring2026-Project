using ISUGameDev.SpearGame;
using UnityEngine;
using static ISUGameDev.SpearGame.BasePlayerState;

public class RoamingWithoutSpearState : BasePlayerState
{
    [SerializeField] private SpearDashAttack spearDashAttack;

    private void Awake()
    {
        this.playerStateType = PlayerStateType.RoamingWithoutSpear;
    }

    public override AttackMechanic GetSecondaryAttackMechanicForState()
    {
        return spearDashAttack.GetAttackImplementation();
    }
}
