using UnityEngine;
using System.Collections;

public class Stats : MonoBehaviour {
	
	public int health; //Damage is subtracted straight from health, if it reaches 0 the unit plays death animation and stops existing
	public int damage; //Every unit does flat damage, set in inspector to make this easier on us
	public bool cursed = false;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(health<=0){
			if(this.tag == "Player"){
				Application.LoadLevel("InitialTestScene");
			}
			DestroyImmediate(this.gameObject);

		}
	
	}

	void OnCollisionEnter2D(Collision2D collision) {
		if(tag == "Enemy"){
			if(collision.gameObject.tag=="Projectile"){
				if(collision.gameObject.GetComponent<Projectile>().ally == true){
					if(collision.gameObject.GetComponent<Projectile>().damageType == 0){
						health -= collision.gameObject.GetComponent<Projectile>().damage*2;
					} else {
						health -= collision.gameObject.GetComponent<Projectile>().damage;
					}
					Destroy(collision.gameObject);
				}
			}
		} else if(tag == "Player"){
			if(collision.gameObject.tag=="Projectile"){
				if(collision.gameObject.GetComponent<Projectile>().ally == false){
					if(collision.gameObject.GetComponent<Projectile>().damageType == 0){
						health -= collision.gameObject.GetComponent<Projectile>().damage*2;
					} else {
						health -= collision.gameObject.GetComponent<Projectile>().damage;
					}
					this.GetComponent<Movement>().TakeDamage (collision.gameObject.transform.position);
					Destroy(collision.gameObject);
				}
			}
		}
	}
}
