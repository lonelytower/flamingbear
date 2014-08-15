using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class Manager : MonoBehaviour {


	public ActionBar actionBarEntity;
	public WeaponSystem weaponSystemEntity;
	public List<GameObject> NPCS = new List<GameObject>();
	public bool gameoverCursed = false;
	public float deathDelay = 5;
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
				deathDelay = 5;
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
	public void cursedDeath(){
		gameoverCursed = true;
	}
}
