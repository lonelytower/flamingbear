using UnityEngine;
using System.Collections;

public class Pickup : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D collisionObject){
		if(collisionObject.gameObject.tag=="Pickups"){
			GameObject.FindGameObjectWithTag("GameController").GetComponent<Manager>().actionBarEntity.addItemToBar(collisionObject.gameObject);
		}
		Destroy(collisionObject.gameObject);
	}
}
