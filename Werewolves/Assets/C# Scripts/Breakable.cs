using UnityEngine;
using System.Collections;

public class Breakable : MonoBehaviour {

	bool broken = false;
	public int hp;
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
			if(collider.gameObject.GetComponent<Projectile>().damageType == 1){
				hp -= collider.gameObject.GetComponent<Projectile>().damage*2;
			} else {
				hp -= collider.gameObject.GetComponent<Projectile>().damage;
			}
			if(hp<=0){
				broken = true;
				if(this.GetComponent<ItemDrops>()!=false){
					this.GetComponent<ItemDrops>().triggerDrop();
				}
			}
			Destroy(collider.gameObject);
			//change to breaking animation
			//After animation delete and spawn any items from drop table
			//Destroy(this.gameObject,0.2f);
		}
	}

	void OnTriggerEnter2D(Collider2D collider){
		if(collider.gameObject.tag == "Projectile"){
			if(collider.gameObject.GetComponent<Projectile>().damageType == 1){
				hp -= collider.gameObject.GetComponent<Projectile>().damage*2;
			} else {
				hp -= collider.gameObject.GetComponent<Projectile>().damage;
			}
			if(hp<=0){
				broken = true;
				if(this.GetComponent<ItemDrops>()!=false){
					this.GetComponent<ItemDrops>().triggerDrop();
				}
			}
			Destroy(collider.gameObject);
			//change to breaking animation
			//After animation delete and spawn any items from drop table
			//Destroy(this.gameObject,0.2f);
		}
	}
	public bool returnBroken(){
		return broken;
	}
}
