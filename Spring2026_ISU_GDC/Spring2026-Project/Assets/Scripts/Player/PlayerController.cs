using Nomad.Core.Events;
using Nomad.Events.Globals;
using UnityEngine;

namespace ISUGameDev.SpearGame.Player
{
    /// <summary>
    /// TODO: this needs a centralized input configuration class, handling over the HUD, etc.
    /// This is a composite/aggregate, not a place to put business logic
    /// </summary>
    public class PlayerController : EntityBase
    {
        public IGameEvent<EmptyEventArgs> PlayerDie => _playerDie;
        private IGameEvent<EmptyEventArgs> _playerDie;
        
        [SerializeField]
        private HeadsUpDisplay _userInterface;
        private PlayerHealthController healthControlRef;

        public override void TakeDamage(int damageValue, float knockback, float stunDuration, GameObject attacker)
        {
            base.TakeDamage(damageValue, knockback, stunDuration, attacker);
            healthControlRef.TakeOneDamage();
            if (health <= 0)
            {
                // TODO: incorporate an actual checkpoint respawn system
                _playerDie.Publish(default);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        protected override void Awake()
        {
            base.Awake();
            healthControlRef = GetComponent<PlayerHealthController>();
            _playerDie = GameEventRegistry.GetEvent<EmptyEventArgs>(nameof(PlayerDie), nameof(PlayerController));
        }

		protected override void OnDestroy()
		{
			base.OnDestroy();

            _playerDie?.Dispose();
		}
    }
}