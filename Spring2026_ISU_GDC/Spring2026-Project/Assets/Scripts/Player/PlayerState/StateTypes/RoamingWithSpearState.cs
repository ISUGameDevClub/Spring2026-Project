using System.Linq;
using ISUGameDev.SpearGame;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

/// <summary>
/// Class to represent what is allowed while in RoamingWithSpearState. Inherits from BasePlayerState. Only Primary and Secondary Attacks are overridden, 
/// as default movement from base class(no changes) is intended
/// </summary>
public class RoamingWithSpearState : BasePlayerState
{
    /// <summary>
    /// The primary attack for this state is the basic combo attack (spear jab)
    /// </summary>
    [SerializeField] private BasicComboAttack basicComboAttack;

    /// <summary>
    /// The secondary attack for this state is the spear throw attack
    /// </summary>
    [SerializeField] private SpearThrowAttack spearThrowAttack;

    private void Awake()
    {
        this.playerStateType = PlayerStateType.RoamingWithSpear;
    }

    /// <summary>
    /// Returns the basic combo attack (spear stab)
    /// </summary>
    public override AttackMechanic GetPrimaryAttackMechanicForState()
    {
        return basicComboAttack.GetAttackImplementation();
    }

    public override AttackMechanic GetSecondaryAttackMechanicForState()
    {
        return spearThrowAttack.GetAttackImplementation();
    }

}
