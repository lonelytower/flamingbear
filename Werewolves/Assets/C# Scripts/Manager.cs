using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class Manager : MonoBehaviour {


	public ActionBar actionBarEntity;
	public WeaponSystem weaponSystemEntity;
	public List<GameObject> NPCS = new List<GameObject>();
	// Use this for initialization
	void Start () {
		actionBarEntity = GameObject.Find("ActionBar").GetComponent<ActionBar>();
		weaponSystemEntity = GameObject.FindGameObjectWithTag("Player").GetComponent<WeaponSystem>();
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.R)){
			Application.LoadLevel("GameScene");
		}
	}
}
