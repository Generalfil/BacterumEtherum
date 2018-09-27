using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BacteriaController : MonoBehaviour {

	private Vector3[,] BacPos;

	public int GridSize = 50;

	// Use this for initialization
	void Start () {
		BacPos = new Vector3[GridSize, GridSize];
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
