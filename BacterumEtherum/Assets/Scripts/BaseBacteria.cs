using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts
{
	public class BaseBacteria : MonoBehaviour
	{
		public Vector3 Position { get; set; }
		public Guid Guid { get; set; }

		protected float Health;
		protected float Growchance;
		protected float Attack;
		

		private System.Random random;
		private List<Vector3> CheckArr;

		public BaseBacteria(): base()
		{
			random = new System.Random();
			float healthMod = CalculateHealthModifier(3,10);

			this.Health = 10f + healthMod;
			this.Growchance = 0.1f;
			this.Attack = 1f;
		}

		public BaseBacteria(float health, float growChance, float attack, Guid guid)
		{
			random = new System.Random();
			float healthMod = CalculateHealthModifier(3, 10);

			this.Health = health + healthMod;
			this.Growchance = growChance;
			this.Attack = attack;
			this.Guid = guid;
		}

		public List<GameObject> CheckAdjecentPos()
		{
			var AdjacentBac = new List<GameObject>();
			
			foreach (var chPos in CheckArr)
			{
				var Bac = FindAt(chPos);
				AdjacentBac.Add(Bac);
			}
			AdjacentBac.OrderBy(a => Guid.NewGuid()).ToList();
			return AdjacentBac;
		}
		public Vector3 CheckEmptyAdjecentPos()
		{
			var AdjacentBac = new Vector3();
			List<Vector3> eligblePos = CheckArr.Where(pos => pos.x <= 30 && pos.x >= 0 && pos.z <= 30 && pos.z >= 0).ToList();

			if (eligblePos != null)
			{
				AdjacentBac = eligblePos.ElementAt(random.Next(0, eligblePos.Count - 1));
				if (FindAt(AdjacentBac) == null)
					return AdjacentBac;
				else
					return new Vector3(-1, -1, -1);
			}
			else
			{
				return new Vector3(-1,-1,-1);
			}

			/*foreach (var chPos in CheckArr.Where(pos => pos.x <= 30 && pos.x >= 0 && pos.z <= 30 && pos.z >= 0))
			{
				var Bac = FindAt(chPos);
				if(Bac == null)
					AdjacentBac.Add(chPos);
			}
			AdjacentBac.OrderBy(a => Guid.NewGuid()).ToList();*/
		}

		public void AttackOther(GameObject m_gameObject)
		{ }

		public bool Grow()
		{
			if (random.Next(1, 100) <= 100*Growchance)
			{
				Debug.Log("Growing");
				return true;
			}
			else
			{
				return false;
			}	
		}

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

		private GameObject FindAt(Vector3 pos)
		{
			pos.y += 1; // shift the position 1 unit above
			RaycastHit hit;

			// cast a ray downwards with range = 2
			if (Physics.Raycast(pos, Vector3.down, out hit, 2))
				return hit.collider.gameObject;
			else
				return null;
		}

		public void SetCheckArr()
		{
			CheckArr = new List<Vector3>()
			{
				new Vector3(Position.x, Position.y, Position.z + 1),
				new Vector3(Position.x, Position.y, Position.z - 1),
				new Vector3(Position.x + 1, Position.y, Position.z),
				new Vector3(Position.x - 1, Position.y, Position.z),
				new Vector3(Position.x + 1, Position.y, Position.z + 1),
				new Vector3(Position.x - 1, Position.y, Position.z + 1),
				new Vector3(Position.x + 1, Position.y, Position.z - 1),
				new Vector3(Position.x - 1, Position.y, Position.z - 1)
			};
		}
		public void DeclareAlive()
		{
			Debug.Log("I was born: " + Guid);
		}
	}
}
