using System.Linq;
using ISUGameDev.SpearGame.Player.PlayerState;
using UnityEngine;

namespace ISUGameDev.SpearGame.Player.PlayerAttacks
{
    public class BasicComboAttack : MonoBehaviour, IAttack
    {
        /// <summary>
        /// The window of time in seconds that the player has to use the second basic attack. If they use the first basic attack button and then
        /// wait longer than the comboWindow time, the next time they use the attack button it will be once again basic attack 1.
        /// </summary>
        [SerializeField] float comboWindow = 1.5f;

        [SerializeField] AnimationClip[] basicAttacks;
        [SerializeField] private HitboxProperties hitboxForJab;
        private int currentComboAttack = 0;
        private Animator playerAnimator;
        private float comboTimer = 0;
        
        [SerializeField] private FMODUnity.EventReference attackSFX;
        
        [SerializeField] float attackCooldown = 0.5f;
        private float cooldownTimer = 0;

        public AttackMechanic GetAttackImplementation()
        {
            return UseComboAttacks;
        }

        private void Start()
        {
            //TODO: get rid of this hack
            playerAnimator = transform.parent.parent.GetComponent<Animator>();
        }

        private void Update()
        {

            if (currentComboAttack > 0 && comboTimer < Time.time)
            {
                currentComboAttack = 0;
            }
        }

        //  Used for when the player attacks while equipped with the spear.
        private void UseComboAttacks()
        {
            if (IsOnCooldown()) return;
            
            //Debug.Log((int)GlobalGameData.Data.basicJabDamage);
            hitboxForJab.SetDamageForHitbox((int)GlobalGameData.Data.basicJabDamage);

            FMODUnity.RuntimeManager.PlayOneShot(attackSFX);

            cooldownTimer = Time.time + attackCooldown;
            
            playerAnimator.Play(basicAttacks[currentComboAttack].name);
            if (currentComboAttack < basicAttacks.Count() - 1)
            {
                currentComboAttack += 1;
                comboTimer = Time.time + comboWindow;
            }
            else
            {
                // Reset the attacks back to the start of the combo.
                currentComboAttack = 0;
            }
        }
        
        public bool IsOnCooldown()
        {
            return Time.time < cooldownTimer;
        }
    }
}