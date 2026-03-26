using ISUGameDev.SpearGame.Player.PlayerAttacks;
using UnityEngine;

namespace ISUGameDev.SpearGame.Player.PlayerState.StateTypes
{
    public class RoamingWithoutSpearState : BasePlayerState
    {
        [SerializeField]
        private SpearDashAttack spearDashAttack;

        private void Awake()
        {
            playerStateType = PlayerStateType.RoamingWithoutSpear;
        }

        public override AttackMechanic GetSecondaryAttackMechanicForState()
        {
            return spearDashAttack.GetAttackImplementation();
        }
    }
}
