using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;

namespace Assets.Scripts
{
	public class BacteriaController : MonoBehaviour
	{
		private BaseBacteria[,] BacPos;
		private List<GameObject> BacteriaList;
		private bool InitDone = false;
		public float TickTime;

		public int GridSize = 30;

		// Use this for initialization
		void Start()
		{
			BacPos = new BaseBacteria[GridSize, GridSize];
			BacteriaList = new List<GameObject>();
			InititializePositions();
			TickTime = 0f;
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
				if (TickTime >= 0.5f)
				{
					Tick();
					TickTime = 0;
				}
			}
			
		}

		private void Tick()
		{
			var bacList = BacteriaList;
			Co1();
		}

		IEnumerator Co1()
		{
			foreach (var bac in BacteriaList)
			{
				List<GameObject> baseBacterias = bac.GetComponent<BaseBacteria>().CheckAdjecentPos();
				Co2(bac, baseBacterias);
				//Check for empty
				List<Vector3> emptyPos = bac.GetComponent<BaseBacteria>().CheckEmptyAdjecentPos();
				Co3(bac, emptyPos);
			}
			return null;
		}

		IEnumerator Co3(GameObject bac, List<Vector3> emptyPos)
		{
			foreach (var posToGrow in emptyPos)
			{
				if (bac.GetComponent<BaseBacteria>().Grow())
					CreateBacteria(posToGrow);
			}
			return null;
		}

		IEnumerator Co2(GameObject bac, List<GameObject> baseBacterias)
		{
			foreach (var posToAttack in baseBacterias)
			{
				if (posToAttack != null)
					bac.GetComponent<BaseBacteria>().AttackOther(posToAttack.gameObject);
			}
			return null;
		}

		private void InititializePositions()
		{
			Vector3[] vector3s = new Vector3[] {new Vector3(0,0,15), new Vector3(15, 0, 0), new Vector3(15, 0, 30), new Vector3(30, 0, 15), };

			foreach (var v3 in vector3s)
			{
				CreateBacteria(v3);
			}
			InitDone = true;
		}

		private void CreateBacteria(Vector3 v3)
		{
			GameObject BacteriaObj = GameObject.CreatePrimitive(PrimitiveType.Cube);
			BacteriaObj.AddComponent<BaseBacteria>().Position = v3;
			BacteriaObj.GetComponent<BaseBacteria>().SetCheckArr();
			BacteriaObj.transform.position = v3;
			BacteriaList.Add(BacteriaObj);
		}
	}
}
