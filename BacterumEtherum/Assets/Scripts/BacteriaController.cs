using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;
using System.Threading.Tasks;

namespace Assets.Scripts
{
	public class BacteriaController : MonoBehaviour
	{
		private BaseBacteria[,] BacPos;
		private List<GameObject> BacteriaList;
		private bool InitDone = false;
		public float TickTime;
		public bool tickDone;

		public int GridSize = 30;

		// Use this for initialization
		void Start()
		{
			BacPos = new BaseBacteria[GridSize, GridSize];
			BacteriaList = new List<GameObject>();
			InititializePositions();
			TickTime = 0f;
			tickDone = true;
		}

		// Update is called once per frame
		void Update()
		{
			
		}

		private void FixedUpdate()
		{
			if (InitDone)
			{
				TickTime += Time.deltaTime;
				if (TickTime >= 0.3f)
				{
					
					if (tickDone)
					{
						tickDone = false;
						Tick();					
						TickTime = 0;
					}
				}
			}
			
		}

		private void Tick()
		{
			//Debug.Log("Tick");
			Co1();
			tickDone = true;
		}

		IEnumerator Co1()
		{
			var grownBac = new List<GameObject>();
			foreach (var bac in BacteriaList)
			{
				if (bac.GetComponent<BaseBacteria>().Die())
				{
					Destroy(bac);
					continue;				
				}

				//Check for empty
				Vector3 emptyPos = bac.GetComponent<BaseBacteria>().CheckEmptyAdjecentPos();
				if (emptyPos.x != -1)
				{
					if (bac.GetComponent<BaseBacteria>().Grow(emptyPos))
							grownBac.Add(CreateBacteria(emptyPos, bac.GetComponent<BaseBacteria>().Guid, bac.GetComponent<Renderer>().material.color, false));
				}
				else if (emptyPos.x != -1 && emptyPos.x == -2)
				{
					Debug.Log("Attacktime");

					//Todo When not growing, Check once for surrondings, 
					var gObj = bac.GetComponent<BaseBacteria>().AttackOther(emptyPos);
					if (gObj != null)
					{
						bac.GetComponent<BaseBacteria>().AddCheckArr(gObj.transform.position);
					}
					else
					{
						bac.GetComponent<BaseBacteria>().AddCheckArr(emptyPos);
					}

					/*List<GameObject> baseBacterias = bac.GetComponent<BaseBacteria>().CheckAdjecentPos();
					Co2(bac, baseBacterias);*/
				}
			}
			BacteriaList.AddRange(grownBac);
			return null;
		}

		/*IEnumerator Co3(GameObject bac, List<Vector3> emptyPos)
		{
			foreach (var posToGrow in emptyPos)
			{
				if (bac.GetComponent<BaseBacteria>().Grow())
					CreateBacteria(posToGrow);
			}
			return null;
		}*/

		/*IEnumerator Co2(GameObject bac, List<GameObject> baseBacterias)
		{
			foreach (var posToAttack in baseBacterias)
			{
				if (posToAttack != null)
					bac.GetComponent<BaseBacteria>().AttackOther(posToAttack.gameObject);
			}
			return null;
		}*/

		private void InititializePositions()
		{
			Vector3[] vector3s = new Vector3[] {new Vector3(1,0,15), new Vector3(15, 0, 1), new Vector3(15, 0, 29), new Vector3(29, 0, 15), };

			foreach (var v3 in vector3s)
			{
				var guid = Guid.NewGuid();
				BacteriaList.Add(CreateBacteria(v3, guid, new Color(UnityEngine.Random.value, UnityEngine.Random.value, UnityEngine.Random.value, 1.0f), true));
			}
			InitDone = true;
		}

		private GameObject CreateBacteria(Vector3 v3, Guid guid, Color color, bool initialBac)
		{
			GameObject BacteriaObj = GameObject.CreatePrimitive(PrimitiveType.Cube);
			BacteriaObj.AddComponent<BaseBacteria>().Position = v3;
			BacteriaObj.transform.position = v3;
			BacteriaObj.GetComponent<BaseBacteria>().SetCheckArr();
			BacteriaObj.GetComponent<BaseBacteria>().SetAttackArr();
			BacteriaObj.GetComponent<BaseBacteria>().Guid = guid;
			if (!initialBac)
			{
				if (BacteriaObj.GetComponent<BaseBacteria>().Mutate())
					color = new Color(UnityEngine.Random.value, UnityEngine.Random.value, UnityEngine.Random.value, 1.0f);
			}
			//BacteriaObj.GetComponent<BaseBacteria>().DeclareAlive();
			BacteriaObj.GetComponent<Renderer>().material.color = color;

			return BacteriaObj;
		}
	}
}
