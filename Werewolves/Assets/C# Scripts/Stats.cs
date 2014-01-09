using UnityEngine;
using System.Collections;

public class Stats : MonoBehaviour {
	
	public int health; //Damage is subtracted straight from health, if it reaches 0 the unit plays death animation and stops existing
	public int damage; //Every unit does flat damage, set in inspector to make this easier on us
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(health<=0){
			Debug.Log("Owie");
		}
	
	}

	void OnCollisionEnter2D(Collision2D collision) {
		if(collision.gameObject.tag=="Projectile"){
			health -= collision.gameObject.GetComponent<Projectile>().damage;
			Destroy(collision.gameObject);
		}
	}
}
