﻿using UnityEngine;
using System.Collections;

public class Stats : MonoBehaviour {
	
	public int health; //Damage is subtracted straight from health, if it reaches 0 the unit plays death animation and stops existing
	public float stamina;
	public int damage; //Every unit does flat damage, set in inspector to make this easier on us
	public bool cursed = false;
	public int threat;
	float engagementDamage; //Total damage done in the current engagement
	float engagementTimer;
	int previousHealth;
	float previousEngagementDamage;
	
	// Use this for initialization
	void Start () {
		previousHealth = health;
		previousEngagementDamage = engagementDamage;
	}
	
	// Update is called once per frame
	void Update () {
		if(previousEngagementDamage < engagementDamage){
			engagementTimer = 5;
		}
		if(health<previousHealth){
			engagementTimer = 5;
		}
		if(engagementTimer <= 0){
			engagementDamage = 0;
		}
		previousHealth = health;
		previousEngagementDamage = engagementDamage;
		engagementTimer -= Time.deltaTime;
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
			foreach(GameObject Ally in GameObject.FindGameObjectsWithTag("Ally")){
				if(Ally.GetComponent<AllyAI>().returnNearbyTargetsContains(this.gameObject)){
					Ally.GetComponent<AllyAI>().removeTarget(this.gameObject);
				}
			}
			foreach(GameObject CampArea in GameObject.FindGameObjectsWithTag("Camp")){
				CampArea.GetComponent<Camp>().removeEnemyFromList(this.gameObject);
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
				GameObject.FindGameObjectWithTag("Player").GetComponent<WeaponSystem>().lowerEquippedDurability(1);
				GameObject.FindGameObjectWithTag("GameController").GetComponent<Manager>().actionBarEntity.returnActionBarList(true)[0].transform.GetChild(0).GetComponent<DurabilityDisplay>().lowerDurability(1);
				if(GameObject.FindGameObjectWithTag("GameController").GetComponent<Manager>().actionBarEntity.returnEquippedItem(1).GetComponent<SpriteRenderer>().sprite.name!= "SlotEmpty"){
					GameObject.FindGameObjectWithTag("GameController").GetComponent<Manager>().actionBarEntity.returnActionBarList(true)[1].transform.GetChild(0).GetComponent<DurabilityDisplay>().lowerDurability(1);
				}
				if(GameObject.FindGameObjectWithTag("GameController").GetComponent<Manager>().actionBarEntity.returnActionBarList(true)[0].transform.GetChild(0).GetComponent<DurabilityDisplay>().returnDurability().x<=0){
					GameObject.FindGameObjectWithTag("GameController").GetComponent<Manager>().actionBarEntity.returnActionBarList(true)[0].GetComponent<SlotBehaviour>().itemQuantity-=1;
				}
				if(GameObject.FindGameObjectWithTag("GameController").GetComponent<Manager>().actionBarEntity.returnActionBarList(true)[1].transform.GetChild(0).GetComponent<DurabilityDisplay>().returnDurability().x<=0){
					GameObject.FindGameObjectWithTag("GameController").GetComponent<Manager>().actionBarEntity.returnActionBarList(true)[1].GetComponent<SlotBehaviour>().itemQuantity-=1;
				}
				collision.gameObject.GetComponent<Projectile>().owner.GetComponent<Stats>().increaseEngagementDamage(damage);
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
				collision.gameObject.GetComponent<Projectile>().owner.GetComponent<Stats>().increaseEngagementDamage(damage);
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
		yield return new WaitForSeconds(0.05f);
		if(this!=null){
			this.renderer.material.color = originalColor;
		}
	}
	public void increaseEngagementDamage(int amount){
		engagementDamage += amount;
	}
	public float returnThreat(){
		return engagementDamage;
	}
}
