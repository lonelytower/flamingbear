using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RespawnEnemies : MonoBehaviour {

	public List<GameObject> Enemies = new List<GameObject>();
	public int enemyNumbers;
	public float spawnDelay;
	float delay;
	GameObject newSpawn;
	public GameObject enemyToSpawn;
	public bool onScreen = false;

	// Use this for initialization
	void Start () {
		foreach(GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy")){
			if(!Enemies.Contains(enemy)){
				Enemies.Add(enemy);
			}
		}
		delay = spawnDelay;
	}
	
	// Update is called once per frame
	void Update () {
		if(!onScreen){
			if(Enemies.Count<enemyNumbers){
				if(delay<=0){
					newSpawn = GameObject.Instantiate(enemyToSpawn,this.transform.position,Quaternion.identity) as GameObject;
					newSpawn.name.Replace("(Clone)","");
					Enemies.Add(newSpawn);
					delay = spawnDelay;
				}
				delay -= Time.deltaTime;
			}
		}
	}
	public void removeEnemy(GameObject enemyToRemove){
		if(Enemies.Contains(enemyToRemove)){
			Enemies.Remove(enemyToRemove);
		}
	}

	public void OnTriggerEnter2D(Collider2D collider){
		if(collider.tag == "Player"){
			onScreen = true;
		}
	}
	public void OnTriggerExit2D(Collider2D collider){
		if(collider.tag == "Player"){
			onScreen = false;
		}
	}
}
