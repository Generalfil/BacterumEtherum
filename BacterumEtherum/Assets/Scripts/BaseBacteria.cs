using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts
{
	public class BaseBacteria : MonoBehaviour
	{
		System.Random random;
		protected float Health;
		protected float Growchance;
		protected float Attack;
		public Vector3 Position;

		
		public BaseBacteria(): base()
		{
			random = new System.Random();
			float healthMod = CalculateHealthModifier(3,10);

			this.Health = 10f + healthMod;
			this.Growchance = 0.1f;
			this.Attack = 1f;
		}

		public BaseBacteria(float health, float growChance, float attack)
		{
			random = new System.Random();
			float healthMod = CalculateHealthModifier(3, 10);

			this.Health = health + healthMod;
			this.Growchance = growChance;
			this.Attack = attack;
		}

		protected void CheckAdjecentPos()
		{ }

		protected void AttackOther()
		{ }

		protected void Grow()
		{ }

		protected void Mutate()
		{ }

		protected void Die()
		{ }

		private float CalculateHealthModifier(int loops, int numberToRandom)
		{
			float healthMod = 0;
			for (int i = 0; i < loops; i++)
			{
				healthMod += random.Next(numberToRandom);
			}

			healthMod /= 3;
			return healthMod;
		}

	}
}
