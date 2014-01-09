using UnityEngine;
using System.Collections;

public class WeaponSystem : MonoBehaviour {
	
	//This is where we do all the weapon switching and stuff.
	int weaponSlot = 1; //2 slots right now, set to 1 for melee and 2 for ranged
	bool melee = true; //Whether or not the equipped weapon is melee, will be inherited from the weapon itself later
	GameObject equippedWeapon; //The weapon you have equipped, selected from the Resources/weapons folder after we make them all
	float delay = 2; //all the inheritence!
	float delayCount = 2; //So we can set it back to the initial delay
	bool switchPressed = false;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetAxis("Fire3")>0&&switchPressed == false){ //Left shift and middle mouse button
			switchPressed = true;
			//Switch weapons
			switch(weaponSlot){
			case(1):
				Debug.Log("Ranged");
				weaponSlot=2;
				delay = 2;
				//this.renderer.material.color = Color.blue;
				melee = false;
				break;
			case(2):
				Debug.Log("Melee");
				delay = 0.3f;
				weaponSlot=1;
				//this.renderer.material.color = Color.red;
				melee = true;
				break;
			default:
				break;
			}
		}
		if(Input.GetAxis("Fire3")<=0){
			switchPressed = false;
		}
		if(Input.GetAxis("Fire1")>0){ //Left Control and left mouse button, remap latter
			if(delayCount<=0){
				delayCount = delay;
				if(melee){
					//Swing, thrust, bash, etc
					GameObject projectileObj;
					projectileObj = GameObject.Instantiate(Resources.Load("Weapons/ProjectileBase"),this.transform.position,this.transform.rotation) as GameObject;
					projectileObj.GetComponent<Projectile>().velocity = 5; //Set it to the weapon value.
					projectileObj.GetComponent<Projectile>().damage = 5;
					projectileObj.GetComponent<Projectile>().lifetime = 0.1f;
				} else {
					//Fire projectile! Projecticle sprite inherited from weapon later
					GameObject projectileObj;
					projectileObj = GameObject.Instantiate(Resources.Load("Weapons/ProjectileBase"),this.transform.position+Vector3.up,this.transform.rotation) as GameObject;
					projectileObj.GetComponent<Projectile>().velocity = 10f; //Set it to the weapon value.
					projectileObj.GetComponent<Projectile>().damage = 5;
				}
			}
		}
		delayCount-=Time.deltaTime;
	}
}
