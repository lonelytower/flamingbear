using UnityEngine;
using System.Collections;

public class Breakable : MonoBehaviour {

	bool broken = false;
	public Sprite initial;
	public Sprite brokenSprite;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(broken){
			this.GetComponent<SpriteRenderer>().sprite = brokenSprite;
			this.GetComponent<BoxCollider2D>().enabled = false;
		}
	}

	void OnCollisionEnter2D(Collision2D collider){
		if(collider.gameObject.tag == "Projectile"){
			broken = true;
			if(this.GetComponent<ItemDrops>()!=false){
				this.GetComponent<ItemDrops>().triggerDrop();
			}
			//change to breaking animation
			//After animation delete and spawn any items from drop table
			//Destroy(this.gameObject,0.2f);
		}
	}
	public bool returnBroken(){
		return broken;
	}
}
