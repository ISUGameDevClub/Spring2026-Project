using ISUGameDev.SpearGame.Player.PlayerState;

namespace ISUGameDev.SpearGame.Player.PlayerAttacks
{
    /// <summary>
    /// Contract for an attack in game. Must have an implementation for carrying out an attack
    /// </summary>
    public interface IAttack
    {

        /// <summary>
        /// Gets attack implementation for this specific attack.
        /// </summary>
        /// <returns>function pointer delegate of type AttackMechanic</returns>
        AttackMechanic GetAttackImplementation();
    }
}