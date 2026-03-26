using Nomad.Core.Events;
using Nomad.Events.Globals;
using UnityEngine;

namespace ISUGameDev.SpearGame
{
	public abstract class EntityBase : MonoBehaviour
	{
		[SerializeField]
		protected float maxHealth;
		[SerializeField]
		protected float moveSpeed;

		public float MaxHeath => maxHealth;
		public float MoveSpeed => moveSpeed;

		public float Health => health;
		protected float health;

		public IGameEvent<float> DamageTaken => _damageTaken;
		private IGameEvent<float> _damageTaken;

		/// <summary>
		/// 
		/// </summary>
		/// <param name="damage"></param>
		public virtual void TakeDamage(int damageValue, float knockback, float stunDuration, GameObject attacker)
		{
			health -= damageValue;
			_damageTaken.Publish(damageValue);
		}

		/// <summary>
		/// 
		/// </summary>
		protected virtual void Awake()
		{
			// we use GetHashCode() because the event isn't specific to any one entity, but the hash code of this
			// class is specific to this entity allocation.
			_damageTaken = GameEventRegistry.GetEvent<float>($"{GetHashCode()}:DamageTaken", nameof(EntityBase));

			health = maxHealth;
		}

		/// <summary>
		/// 
		/// </summary>
		protected virtual void OnDestroy()
		{
			_damageTaken?.Dispose();
		}
	}
}