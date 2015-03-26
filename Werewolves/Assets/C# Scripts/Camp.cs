using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Camp : MonoBehaviour {

	List<GameObject> enemiesInZone = new List<GameObject>();
	bool triggered = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D collider){
		if(collider.tag == "Enemy"){
			if(!enemiesInZone.Contains(collider.gameObject)){
				enemiesInZone.Add(collider.gameObject);
			}
		}
		if(!triggered){
			if(collider.tag=="Player"){
				if(enemiesInZone.Count ==0){
					
					Debug.Log("Camp Triggered");
					triggered = true;
					//Do camp stuff
				}
			}
		}
	}

	void OnTriggerExit2D(Collider2D collider){
		if(collider.tag == "Enemy"){
			if(enemiesInZone.Contains(collider.gameObject)){
				enemiesInZone.Remove(collider.gameObject);
			}
		}
	}
	void OnTriggerStay2D(Collider2D collider){
		if(collider.tag == "Enemy"){
			if(!enemiesInZone.Contains(collider.gameObject)){
				enemiesInZone.Add(collider.gameObject);
			}
		}
		if(!triggered){
			if(collider.tag=="Player"){
				if(enemiesInZone.Count ==0){

					Debug.Log("Camp Triggered");
					triggered = true;
					//Do camp stuff
				}
			}
		}
	}

	public void removeEnemyFromList(GameObject enemyToRemove){
		if(enemiesInZone.Contains(enemyToRemove)){
			enemiesInZone.Remove(enemyToRemove);
		}
	}

}
