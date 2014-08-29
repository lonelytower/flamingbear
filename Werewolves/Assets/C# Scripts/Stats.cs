using UnityEngine;
using System.Collections;

public class Stats : MonoBehaviour {
	
	public int health; //Damage is subtracted straight from health, if it reaches 0 the unit plays death animation and stops existing
	public float stamina;
	public int damage; //Every unit does flat damage, set in inspector to make this easier on us
	public bool cursed = false;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(health<=0){
			if(this.tag == "Player"){
				
				GameObject enemy = null;
				if(cursed){
					enemy = GameObject.Instantiate(Resources.Load("Characters/TestEnemy"),this.transform.position,this.transform.rotation) as GameObject;
				}
				GameObject.FindGameObjectWithTag("GameController").GetComponent<Manager>().cursedDeath(enemy);
			}
			foreach(GameObject spawner in GameObject.FindGameObjectsWithTag("Respawner")){
				spawner.GetComponent<RespawnEnemies>().removeEnemy(this.gameObject);
			}
			DestroyImmediate(this.gameObject);
		}
		if(stamina<100){
			stamina += 0.15f;
		}
	
	}

	void OnCollisionEnter2D(Collision2D collision) {
		if(tag == "Enemy"){
			if(collision.gameObject.tag=="Projectile"){
				if(collision.gameObject.GetComponent<Projectile>().ally == true){
					if(collision.gameObject.GetComponent<Projectile>().damageType == 0){
						StartCoroutine(onHit(collision.gameObject.GetComponent<Projectile>().damage,true));
						health -= collision.gameObject.GetComponent<Projectile>().damage;
					} else {
						StartCoroutine(onHit(collision.gameObject.GetComponent<Projectile>().damage,false));
						health -= collision.gameObject.GetComponent<Projectile>().damage;
					}
					Destroy(collision.gameObject);
				}
			}
		} else if(tag == "Player"){
			if(collision.gameObject.tag=="Projectile"){
				if(collision.gameObject.GetComponent<Projectile>().ally == false){
					if(collision.gameObject.GetComponent<Projectile>().damageType == 0){
						StartCoroutine(onHit(collision.gameObject.GetComponent<Projectile>().damage,true));
						health -= collision.gameObject.GetComponent<Projectile>().damage;
					} else {
						StartCoroutine(onHit(collision.gameObject.GetComponent<Projectile>().damage,false));
						health -= collision.gameObject.GetComponent<Projectile>().damage;
					}
					this.GetComponent<Movement>().TakeDamage (collision.gameObject.transform.position);
					Destroy(collision.gameObject);
				}
			}
		}
	}

	void OnTriggerEnter2D(Collider2D collision){
		if(tag == "Enemy"){
			if(collision.gameObject.tag=="Projectile"){
				if(collision.gameObject.GetComponent<Projectile>().ally == true){
					if(collision.gameObject.GetComponent<Projectile>().damageType == 0){
						StartCoroutine(onHit(collision.gameObject.GetComponent<Projectile>().damage,true));
						health -= collision.gameObject.GetComponent<Projectile>().damage;
					} else {
						StartCoroutine(onHit(collision.gameObject.GetComponent<Projectile>().damage,false));
						health -= collision.gameObject.GetComponent<Projectile>().damage;
					}
					Destroy(collision.gameObject);
				}
			}
		} else if(tag == "Player"){
			if(collision.gameObject.tag=="Projectile"){
				if(collision.gameObject.GetComponent<Projectile>().ally == false){
					if(collision.gameObject.GetComponent<Projectile>().damageType == 0){
						StartCoroutine(onHit(collision.gameObject.GetComponent<Projectile>().damage,true));
						health -= collision.gameObject.GetComponent<Projectile>().damage;
					} else {
						StartCoroutine(onHit(collision.gameObject.GetComponent<Projectile>().damage,false));
						health -= collision.gameObject.GetComponent<Projectile>().damage;
					}
					this.GetComponent<Movement>().TakeDamage (collision.gameObject.transform.position);
					Destroy(collision.gameObject);
				}
			}
		}
	}

	public IEnumerator onHit (int damage, bool doubleDamage){
		GameObject hitNumber = Resources.Load("Sprites/Effects/number") as GameObject;
		hitNumber.GetComponent<SpriteRenderer>().sprite = Resources.Load("Sprites/Effects/"+damage.ToString(), typeof(Sprite)) as Sprite;
		hitNumber.GetComponent<SpriteRenderer>().color = Color.white;
		GameObject.Instantiate(hitNumber,this.transform.position,Quaternion.identity);
		Color originalColor = this.renderer.material.color;
		this.renderer.material.color = Color.red;
		yield return new WaitForSeconds(0.2f);
		if(this!=null){
			this.renderer.material.color = originalColor;
		}
	}
}
