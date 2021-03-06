﻿using UnityEngine;
using System.Collections;

public class DurabilityDisplay : MonoBehaviour {
	float durability = 1;
	float maxDurability = 1;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		this.transform.localScale = new Vector3 ((durability/maxDurability)*0.17f,0.25f,0);
	}

	public void newDurability(int newDurability, int newMaxDurability){
		durability = newDurability;
		maxDurability = newMaxDurability;
	}
	public void newDurability(Vector2 newValues){
		durability = (int)newValues.x;
		maxDurability = (int)newValues.y;
	}
	public void lowerDurability(int amount){
		if(maxDurability!=1){
			durability -= amount;
		}
	}
	public Vector2 returnDurability(){
		return new Vector2(durability,maxDurability);
	}
}
