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

		public struct V3Empty
		{
			public Vector3 vector3;
			public bool isEmpty;
		}

		protected float Health;
		protected float GrowChance;
		protected float Attack;
		protected float MutateChance;
		
		private System.Random random;
		private List<Vector3> CheckArr;
		private List<Vector3> AttackArr;

		public BaseBacteria(): base()
		{
			random = new System.Random();
			float healthMod = CalculateModifier(3,10);

			this.Health = 10f + healthMod;
			this.GrowChance = 0.1f;
			this.Attack = 2f;
			this.MutateChance = 0.03f;
		}

		public BaseBacteria(float health, float growChance, float attack, Guid guid)
		{
			random = new System.Random();
			float healthMod = CalculateModifier(3, 10);

			this.Health = health + healthMod;
			this.GrowChance = growChance;
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

		public V3Empty CheckEmptyAdjecentPos()
		{
			var AdjacentBac = new Vector3();
			V3Empty localV3 = new V3Empty();
			List<Vector3> posWithinMapNotNull = CheckArr.Where(pos => pos.x <= 30 && pos.x >= 0 && pos.z <= 30 && pos.z >= 0).ToList();

			if (posWithinMapNotNull != null && posWithinMapNotNull.Count > 0)
			{
				AdjacentBac = posWithinMapNotNull.ElementAt(random.Next(0, posWithinMapNotNull.Count - 1));
				if (FindAt(AdjacentBac) == null)
				{
					localV3.vector3 = AdjacentBac;
					localV3.isEmpty = true;
					return localV3;
				}
				else
				{ 
					localV3.vector3 = AdjacentBac;
					localV3.isEmpty = false;
					return localV3;
				}
			}
			else
			{
				localV3.vector3 = new Vector3(-1, -1, -1);
				localV3.isEmpty = false;
				return localV3;
			}
		}

		public GameObject AttackOther(Vector3 v3)
		{
			var gObj = FindAt(v3);
			if (gObj.GetComponent<BaseBacteria>().Guid != Guid && gObj != null)
			{
				gObj.GetComponent<BaseBacteria>().Health -= this.Attack;
				Debug.Log("Attacking" + gObj.GetComponent<BaseBacteria>().Health);
				return gObj;
			}
			else
				return null;
		}

		public bool Grow(Vector3 growPos)
		{
			if (random.Next(1, 100) <= 100*GrowChance)
			{
				CheckArr.Remove(CheckArr.FirstOrDefault(x => x == growPos));
				Debug.Log("Growing");
				return true;
			}
			else
			{
				return false;
			}	
		}

		public bool Mutate()
		{
			random = new System.Random();
			if (random.Next(0, 100) <= 100 * MutateChance)
			{
				float healthMod = CalculateModifier(3, 15);
				float attackMod = CalculateModifier(3, 10);
				float growMod = CalculateModifier(3, 10);
				float mutateMod = CalculateModifier(5, 3);
				this.Health += healthMod;
				this.Attack += attackMod;
				if (GrowChance < 0.5f)
					this.GrowChance += growMod/10;
				if (MutateChance < 0.2f)
					this.MutateChance += mutateMod;
				else
					this.MutateChance -= mutateMod;
				this.Guid = Guid.NewGuid();
				return true;
			}
			return false;
		}

		public bool Die()
		{
			if (this.Health <= 0)
			{
				return true;
			}
			else
				return false;

		}

		private float CalculateModifier(int loops, int numberToRandom)
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

		public void SetAttackArr()
		{
			AttackArr = null;
			AttackArr = new List<Vector3>()
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

		public void SetCheckArr()
		{
			CheckArr = null;
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

		public void AddCheckArr(Vector3 v3)
		{
			CheckArr.Add(v3);
		}
		public void DeclareAlive()
		{
			Debug.Log("I was born: " + Guid);
		}

		public void Buff()
		{
			this.Attack += 1;
			this.Health += 1;
		}
	}
}
