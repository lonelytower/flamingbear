﻿using UnityEngine;
using System.Collections;

public class ItemBehaviour : MonoBehaviour {

	public float delay = 2.0f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(delay > 0){
			delay = delay - Time.deltaTime;
		}
	}
}