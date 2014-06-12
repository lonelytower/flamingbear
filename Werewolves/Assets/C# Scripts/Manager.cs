using UnityEngine;
using System.Collections;

public class Manager : MonoBehaviour {


	public ActionBar actionBarEntity;
	// Use this for initialization
	void Start () {
		actionBarEntity = GameObject.Find("ActionBar").GetComponent<ActionBar>();
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.R)){
			Application.LoadLevel("GameScene");
		}
	}
}
