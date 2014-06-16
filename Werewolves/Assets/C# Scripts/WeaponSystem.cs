using UnityEngine;
using System.Collections;

public class WeaponSystem : MonoBehaviour {
	
	//This is where we do all the weapon switching and stuff.
	int weaponSlot = 1; //2 slots right now, set to 1 for melee and 2 for ranged
	bool melee = true; //Whether or not the equipped weapon is melee, will be inherited from the weapon itself later
	GameObject equippedWeapon; //The weapon you have equipped, selected from the Resources/weapons folder after we make them all
	public float delay = 1; //all the inheritence!
	float delayCount = 2; //So we can set it back to the initial delay
	bool switchPressed = false;
	GameObject equippedItem;
	GameObject equippedItem2;
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
				this.renderer.material.color = Color.grey;
				melee = false;
				break;
			case(2):
				Debug.Log("Melee");
				delay = 1f;
				weaponSlot=1;
				this.renderer.material.color = Color.white;
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
					StartCoroutine(launchMeleeAttack(this.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length));
					//Swing, thrust, bash, etc
					GameObject projectileObj = null;
					
					switch(this.GetComponent<Movement>().direction){
					case(1):
						projectileObj = GameObject.Instantiate(Resources.Load("Weapons/Swipe"),this.transform.position+(new Vector3(0,0.3f,0)),this.transform.rotation) as GameObject;
						break;
					case(2):
						projectileObj = GameObject.Instantiate(Resources.Load("Weapons/Swipe"),this.transform.position+(new Vector3(0,-0.3f,0)),this.transform.rotation) as GameObject;
						break;
					case(3):
						projectileObj = GameObject.Instantiate(Resources.Load("Weapons/Swipe"),this.transform.position+(new Vector3(-0.3f,0,0)),this.transform.rotation) as GameObject;
						projectileObj.transform.Rotate(new Vector3(0,0,90));
						break;
					case(4):
						projectileObj = GameObject.Instantiate(Resources.Load("Weapons/Swipe"),this.transform.position+(new Vector3(0.3f,0,0)),this.transform.rotation) as GameObject;
						projectileObj.transform.Rotate(new Vector3(0,0,90));
						break;
					default:
						break;
					}
					newEquippedItem();
				//	projectileObj.GetComponent<Projectile>().velocity = 5; //Set it to the weapon value.
					if(equippedItem != null){
						projectileObj.GetComponent<Projectile>().damage = equippedItem.GetComponent<WeaponStats>().enemyDamage;
					} else {
						projectileObj.GetComponent<Projectile>().damage = 1;
					}
				//	projectileObj.GetComponent<Projectile>().lifetime = 0.1f;
				} else {
					this.GetComponentInChildren<Detection>().increaseRadius(5);
					//Fire projectile! Projecticle sprite inherited from weapon later
					GameObject projectileObj;
					switch(this.GetComponent<Movement>().direction){
					case(1):
						projectileObj = GameObject.Instantiate(Resources.Load("Weapons/ProjectileBase"),this.transform.position+(new Vector3(0,0.3f,0)),this.transform.rotation) as GameObject;
						projectileObj.GetComponent<Projectile>().lifetime = 10;
						projectileObj.GetComponent<Projectile>().direction=this.GetComponent<Movement>().direction;
						projectileObj.transform.Rotate(new Vector3(0,0,90));
						break;
					case(2):
						projectileObj = GameObject.Instantiate(Resources.Load("Weapons/ProjectileBase"),this.transform.position+(new Vector3(0,-0.3f,0)),this.transform.rotation) as GameObject;
						projectileObj.GetComponent<Projectile>().lifetime = 10;
						projectileObj.GetComponent<Projectile>().direction=this.GetComponent<Movement>().direction;
						projectileObj.transform.Rotate(new Vector3(0,0,-90));
						break;
					case(3):
						projectileObj = GameObject.Instantiate(Resources.Load("Weapons/ProjectileBase"),this.transform.position+(new Vector3(-0.3f,0,0)),this.transform.rotation) as GameObject;
						projectileObj.GetComponent<Projectile>().lifetime = 10;
						projectileObj.GetComponent<Projectile>().direction=this.GetComponent<Movement>().direction;
						break;
					case(4):
						projectileObj = GameObject.Instantiate(Resources.Load("Weapons/ProjectileBase"),this.transform.position+(new Vector3(0.3f,0,0)),this.transform.rotation) as GameObject;
						projectileObj.GetComponent<Projectile>().lifetime = 10;
						projectileObj.GetComponent<Projectile>().direction=this.GetComponent<Movement>().direction;
						break;
					default:
						break;
					}
					//projectileObj = GameObject.Instantiate(Resources.Load("Weapons/ProjectileBase"),this.transform.position+Vector3.up,this.transform.rotation) as GameObject;
					//projectileObj.GetComponent<Projectile>().velocity = 10f; //Set it to the weapon value.
					//projectileObj.GetComponent<Projectile>().damage = 5;
				}
			}
		}
		delayCount-=Time.deltaTime;
	}
	IEnumerator launchMeleeAttack(float delay){
		int direction = 0; //1 = up 2 = down 3 = left 4 = right
		AnimatorStateInfo currentAnim;
		currentAnim = this.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0);
		if(currentAnim.IsName("WalkUp") == true){
			direction = 1;
			this.GetComponent<Animator>().Play("AttackUp");
		} else if (currentAnim.IsName("WalkDown") == true){
			direction = 2;
			this.GetComponent<Animator>().Play("AttackDown");

		} else if (currentAnim.IsName("WalkLeft") == true){
			direction = 3;
			this.GetComponent<Animator>().Play("AttackLeft");

		} else if (currentAnim.IsName("WalkRight") == true){
			direction = 4;
			this.GetComponent<Animator>().Play("AttackRight");

		}
		this.GetComponent<Movement>().moveable = false;
		yield return new WaitForSeconds(delay);
		switch(direction){
		case(1):
			this.GetComponent<Animator>().Play("WalkUp");
			break;
		case(2):
			this.GetComponent<Animator>().Play("WalkDown");
			break;
		case(3):
			this.GetComponent<Animator>().Play("WalkLeft");
			break;
		case(4):
			this.GetComponent<Animator>().Play("WalkRight");
			break;
		default:
			break;
		}
		this.GetComponent<Movement>().moveable = true;
	}

	public void newEquippedItem(){
		GameObject equippedItem = Resources.Load("Items/" + GameObject.FindGameObjectWithTag("GameController").GetComponent<Manager>().actionBarEntity.returnEquippedItem(0).name) as GameObject;
		GameObject equippedItem2 = Resources.Load("Items/" + GameObject.FindGameObjectWithTag("GameController").GetComponent<Manager>().actionBarEntity.returnEquippedItem(1).name) as GameObject;
		if(equippedItem!=null){
			delayCount = equippedItem.GetComponent<WeaponStats>().delay;
		}
	}
}
