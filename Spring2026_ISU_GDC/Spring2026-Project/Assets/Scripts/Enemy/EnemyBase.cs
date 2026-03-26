using UnityEngine;

namespace ISUGameDev.SpearGame.Enemy
{
	public class EnemyBase : EntityBase
	{
		[SerializeField]
		private EnemyMovement movement;

		public override void TakeDamage(int damageValue, float knockback, float stunDuration, GameObject attacker)
		{
			base.TakeDamage(damageValue, knockback, stunDuration, attacker);
			Debug.Log(damageValue + " damage taken");
			//Deal knockback
			//Deal stun
			//Play visual effects
			if (health <= 0.0f)
			{
				Destroy(gameObject, 0.2f);
			}
			//Added a delay to allow for visual effects like hitflash, animations, ect to play first

		}

		protected override void Awake()
		{
			base.Awake();
		}
	}
}