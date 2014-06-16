using UnityEngine;
using System.Collections;

public class Manager : MonoBehaviour {


	public ActionBar actionBarEntity;
	public WeaponSystem weaponSystemEntity;
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
