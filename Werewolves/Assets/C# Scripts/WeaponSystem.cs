using UnityEngine;
using System.Collections;

public class WeaponSystem : MonoBehaviour {
	
	//This is where we do all the weapon switching and stuff.
	int weaponSlot = 1; //2 slots right now, set to 1 for melee and 2 for ranged
	bool melee = true; //Whether or not the equipped weapon is melee, will be inherited from the weapon itself later
	GameObject equippedWeapon; //The weapon you have equipped, selected from the Resources/weapons folder after we make them all
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetAxis("Fire3")>0){ //Left shift and middle mouse button
			//Switch weapons
			switch(weaponSlot){
			case(1):
				weaponSlot=2;
				melee = false;
				break;
			case(2):
				weaponSlot=1;
				melee = true;
				break;
			default:
				break;
			}
		}
		if(Input.GetAxis("Fire1")>0){ //Left Control and left mouse button, remap latter
			if(melee){
				//Swing, thrust, bash, etc
			} else {
				//Fire projectile! Projecticle sprite inherited from weapon later
				GameObject projectileObj;
				projectileObj = GameObject.Instantiate(Resources.Load("Weapons/ProjectileBase"),Vector3.forward/2,this.transform.rotation) as GameObject;
				projectileObj.GetComponent<Projectile>().velocity = 1; //Set it to the weapon value.
				projectileObj.transform.rotation = new Quaternion(0,0,this.transform.rotation.z);
			}
		}
	}
}
