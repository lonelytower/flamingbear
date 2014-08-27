using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class Manager : MonoBehaviour {


	public ActionBar actionBarEntity;
	public WeaponSystem weaponSystemEntity;
	public List<GameObject> NPCS = new List<GameObject>();
	public bool gameoverCursed = false;
	public float deathDelay = 5;
	GameObject newEnemySpawned;
	// Use this for initialization
	void Start () {
		actionBarEntity = GameObject.Find("ActionBar").GetComponent<ActionBar>();
		weaponSystemEntity = GameObject.FindGameObjectWithTag("Player").GetComponent<WeaponSystem>();
	}
	
	// Update is called once per frame
	void Update () {
		if(gameoverCursed){

			deathDelay -= Time.deltaTime;
			if(deathDelay <=0){
				deathDelay = 2;
				foreach(GameObject slot in GameObject.FindGameObjectWithTag("GameController").GetComponent<Manager>().actionBarEntity.returnActionBarList(false)){
					GameObject droppedItem;
					droppedItem = GameObject.Instantiate(Resources.Load("Items/" + slot.GetComponent<SpriteRenderer>().sprite.name),newEnemySpawned.transform.position, this.transform.rotation) as GameObject;
					droppedItem.name = slot.GetComponent<SpriteRenderer>().sprite.name;
					slot.GetComponent<SpriteRenderer>().sprite = null;
				}
				gameoverCursed = false;
				GameObject newPlayer = Resources.Load("Characters/Player") as GameObject;
				GameObject.Instantiate(newPlayer);
				GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraFollow>().Reassign();
			}
		}
		if(Input.GetKeyDown(KeyCode.R)){
			Application.LoadLevel("GameScene");
		}
	}
	public void cursedDeath(GameObject newEnemy){
		newEnemySpawned = newEnemy;
		gameoverCursed = true;
	}
}
