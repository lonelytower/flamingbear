﻿using UnityEngine;
using System.Collections;

public class AllyRadius : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(this.transform.parent.gameObject.GetComponent<AllyAI>().target==null){
			this.transform.parent.gameObject.GetComponent<AllyAI>().setPriority(1);
		}

	}

	void OnTriggerEnter2D(Collider2D collider){
		if(collider.gameObject.tag=="Enemy"){
			this.transform.parent.gameObject.GetComponent<AllyAI>().setTarget(collider.gameObject);
			this.transform.parent.gameObject.GetComponent<AllyAI>().setPriority(2);
			collider.gameObject.GetComponent<EnemyAI>().setTarget(this.transform.parent.gameObject);
		}
		if(collider.gameObject.tag == "Player"){
			if(this.name.Contains("Follower")){
				this.transform.parent.gameObject.GetComponent<AllyAI>().setPriority(1);
			}
		}
	}
	void OnTriggerExit2D(Collider2D collider){
		if(collider.gameObject.tag=="Enemy"){
			this.transform.parent.gameObject.GetComponent<AllyAI>().setPriority(2);
			collider.gameObject.GetComponent<EnemyAI>().setTarget(this.transform.parent.gameObject);
		}
		if(collider.gameObject.tag == "Player"){
			if(this.name.Contains("Follower")){
//			this.transform.parent.gameObject.GetComponent<AllyAI>().leader = collider.gameObject.transform.parent.gameObject;
				this.transform.parent.gameObject.GetComponent<AllyAI>().setPriority(1);
			}
		}
	}
	void OnTriggerStay2D(Collider2D collider){
//		if(Vector3.Distance(this.transform.position,this.transform.parent.gameObject.GetComponent<AllyAI>().leader.gameObject.transform.position)>5){
//				this.transform.parent.gameObject.GetComponent<AllyAI>().setPriority(1);
//			} else {
				if(collider.gameObject.tag=="Enemy"){
			this.transform.parent.gameObject.GetComponent<AllyAI>().setTarget(collider.gameObject);
					this.transform.parent.gameObject.GetComponent<AllyAI>().setPriority(2);
				}
			}
//		}
}
