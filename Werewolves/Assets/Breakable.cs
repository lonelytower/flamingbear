using UnityEngine;
using System.Collections;

public class Breakable : MonoBehaviour {


	public Sprite initial;
	public Sprite broken;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter2D(Collision2D collider){
		if(collider.gameObject.tag == "Projectile"){
			this.GetComponent<SpriteRenderer>().sprite = broken;
			//change to breaking animation
			//After animation delete and spawn any items from drop table
			//Destroy(this.gameObject,0.2f);
		}
	}
}
