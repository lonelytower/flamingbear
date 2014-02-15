using UnityEngine;
using System.Collections;

public class Breakable : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter2D(Collision2D collider){
		if(collider.gameObject.tag == "Projectile"){
			//change to breaking animation
			//After animation delete and spawn any items from drop table
			Destroy(this.gameObject,0.2f);
		}
	}
}
