using Nomad.Core.Events;
using Nomad.Events.Globals;
using System.Drawing;
using UnityEngine;
using UnityEngine.UI;

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

        public override void TakeDamage(int damageValue, float knockback, float stunDuration, GameObject attacker)
        {
            base.TakeDamage(damageValue, knockback, stunDuration, attacker);
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

            _playerDie = GameEventRegistry.GetEvent<EmptyEventArgs>(nameof(PlayerDie), nameof(PlayerController));
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();

            _playerDie?.Dispose();
        }




        //Health UI
        
        public int numOfHearts;

        [SerializeField] private UnityEngine.UI.Image[] hearts;
        [SerializeField] private Sprite fullHeart;
        [SerializeField] private Sprite emptyHeart;
        [SerializeField] private float HP;

        void Start()
        {
            //HP = health;
        }
        private void Update()
        {
            Debug.Log("Test");
            health = HP;
            if (health < numOfHearts)
            {
                health = numOfHearts;
            }
            for (int i = 0; i < hearts.Length; i++)
            {
                if (i < health)
                {
                    hearts[i].sprite = fullHeart;
                }
                else
                {
                    hearts[i].sprite = emptyHeart;
                }
                    if (i < hearts.Length)
                {
                    hearts[i].enabled = true;
                }
                else
                {
                    hearts[i].enabled = false;
                }

            }
        }

    }
    

}