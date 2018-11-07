using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
	public class BacteriaController : MonoBehaviour
	{
		private BaseBacteria[,] BacPos;
		private List<BaseBacteria> BacteriaList;
		private float TickTime;

		public int GridSize = 50;

		// Use this for initialization
		void Start()
		{
			BacPos = new BaseBacteria[GridSize, GridSize];
			BacteriaList = new List<BaseBacteria>();
			InititializePositions(BacPos);
			TickTime = 0f;
		}

		// Update is called once per frame
		void Update()
		{
			
		}

		private void FixedUpdate()
		{
			TickTime += Time.deltaTime;
			if (TickTime >= 0.25f)
			{

			}
		}


		private void InititializePositions(BaseBacteria[,] _bacPos)
		{
			_bacPos[0, 25] = new BaseBacteria();
			_bacPos[25, 0] = new BaseBacteria();
			_bacPos[50, 25] = new BaseBacteria();
			_bacPos[25, 50] = new BaseBacteria();
		}

	}
}
