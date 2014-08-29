using UnityEngine;
using System.Collections;

public class WeaponSystem : MonoBehaviour {
	
	//This is where we do all the weapon switching and stuff.
	int weaponSlot = 1; //2 slots right now, set to 1 for melee and 2 for ranged
	public bool melee = true; //Whether or not the equipped weapon is melee, will be inherited from the weapon itself later
	GameObject equippedWeapon; //The weapon you have equipped, selected from the Resources/weapons folder after we make them all
	public float delay = 1; //all the inheritence!
	float delayCount = 1f; //So we can set it back to the initial delay
	bool switchPressed = false;
	GameObject equippedItem;
	GameObject equippedItem2;
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {

//		if(Input.GetAxis("Fire3")>0&&switchPressed == false){ //Left shift and middle mouse button
//			switchPressed = true;
//			//Switch weapons
//			switch(weaponSlot){
//			case(1):
//				Debug.Log("Ranged");
//				weaponSlot=2;
//				delay = 2;
//				this.renderer.material.color = Color.grey;
//				melee = false;
//				break;
//			case(2):
//				Debug.Log("Melee");
//				delay = 1f;
//				weaponSlot=1;
//				this.renderer.material.color = Color.white;
//				melee = true;
//				break;
//			default:
//				break;
//			}
//		}
//		if(Input.GetAxis("Fire3")<=0){
//			switchPressed = false;
//		}
		if(Input.GetAxis("Fire1")>0){ //Left Control and left mouse button, remap latter
			Vector2 mouseVector = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit2D mouseHit = Physics2D.GetRayIntersection(mouseRay,Mathf.Infinity);
			bool attack = true;
			foreach(GameObject slot in GameObject.FindGameObjectWithTag("GameController").GetComponent<Manager>().actionBarEntity.returnActionBarList(false)){
				if(slot.collider2D.OverlapPoint(mouseVector)){
					attack = false;
				}
			}
			foreach(GameObject slot in GameObject.FindGameObjectWithTag("GameController").GetComponent<Manager>().actionBarEntity.returnActionBarList(true)){
				if(slot.collider2D.OverlapPoint(mouseVector)){
					attack = false;
				}
			}
			foreach(GameObject merchant in GameObject.FindGameObjectWithTag("GameController").GetComponent<Manager>().NPCS){
				if(mouseHit.collider!=null){
					if(mouseHit.collider.tag.Contains("Merch")){
						attack = false;
					}
				}
				if(merchant.tag.Contains("Merch")){
					if(merchant.collider2D.OverlapPoint(mouseVector)){
						attack = false;
					}
				}
			}
			if(attack==true){
				if(delayCount<=0){
					delayCount = delay;
					if(melee){
						StartCoroutine(launchMeleeAttack(this.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length));
						//Swing, thrust, bash, etc
						GameObject projectileObj = null;
						Vector2 spawnPosition;
						spawnPosition = mouseVector - new Vector2(this.transform.position.x,this.transform.position.y);
						spawnPosition = spawnPosition.normalized;
						projectileObj = GameObject.Instantiate(Resources.Load("Weapons/Swipe"),this.transform.position + new Vector3(spawnPosition.normalized.x,spawnPosition.normalized.y,0),this.transform.rotation) as GameObject;
						projectileObj.transform.LookAt(this.transform.position);
						projectileObj.transform.rotation = Quaternion.Euler(new Vector3(0,0,projectileObj.transform.eulerAngles.x));
//						switch(this.GetComponent<Movement>().direction){
//						case(1):
//							projectileObj = GameObject.Instantiate(Resources.Load("Weapons/Swipe"),this.transform.position+(new Vector3(0,0.3f,0)),this.transform.rotation) as GameObject;
//							break;
//						case(2):
//							projectileObj = GameObject.Instantiate(Resources.Load("Weapons/Swipe"),this.transform.position+(new Vector3(0,-0.3f,0)),this.transform.rotation) as GameObject;
//							break;
//						case(3):
//							projectileObj = GameObject.Instantiate(Resources.Load("Weapons/Swipe"),this.transform.position+(new Vector3(-0.3f,0,0)),this.transform.rotation) as GameObject;
//							projectileObj.transform.Rotate(new Vector3(0,0,90));
//							break;
//						case(4):
//							projectileObj = GameObject.Instantiate(Resources.Load("Weapons/Swipe"),this.transform.position+(new Vector3(0.3f,0,0)),this.transform.rotation) as GameObject;
//							projectileObj.transform.Rotate(new Vector3(0,0,90));
//							break;
//						default:
//							break;
//						}


						//newEquippedItem();
					//	projectileObj.GetComponent<Projectile>().velocity = 5; //Set it to the weapon value.
						if(equippedItem != null){
							projectileObj.GetComponent<Projectile>().damage = equippedItem.GetComponent<WeaponStats>().damage;
							projectileObj.GetComponent<Projectile>().damageType = equippedItem.GetComponent<WeaponStats>().damageType;
						} else {
							projectileObj.GetComponent<Projectile>().damage = 1;
						}
					//	projectileObj.GetComponent<Projectile>().lifetime = 0.1f;
					} else {
						this.GetComponentInChildren<Detection>().increaseRadius(5);
						//Fire projectile! Projecticle sprite inherited from weapon later
						GameObject projectileObj;
						Vector2 spawnPosition;
//						switch(this.GetComponent<Movement>().direction){
//						case(1):
//							projectileObj = GameObject.Instantiate(Resources.Load("Weapons/ProjectileBase"),this.transform.position+(new Vector3(0,0.3f,0)),this.transform.rotation) as GameObject;
//							projectileObj.GetComponent<Projectile>().lifetime = 10;
//							projectileObj.GetComponent<Projectile>().direction=this.GetComponent<Movement>().direction;
//							projectileObj.transform.Rotate(new Vector3(0,0,90));
//							break;
//						case(2):
//							projectileObj = GameObject.Instantiate(Resources.Load("Weapons/ProjectileBase"),this.transform.position+(new Vector3(0,-0.3f,0)),this.transform.rotation) as GameObject;
//							projectileObj.GetComponent<Projectile>().lifetime = 10;
//							projectileObj.GetComponent<Projectile>().direction=this.GetComponent<Movement>().direction;
//							projectileObj.transform.Rotate(new Vector3(0,0,-90));
//							break;
//						case(3):
//							projectileObj = GameObject.Instantiate(Resources.Load("Weapons/ProjectileBase"),this.transform.position+(new Vector3(-0.3f,0,0)),this.transform.rotation) as GameObject;
//							projectileObj.GetComponent<Projectile>().lifetime = 10;
//							projectileObj.GetComponent<Projectile>().direction=this.GetComponent<Movement>().direction;
//							break;
//						case(4):
//							projectileObj = GameObject.Instantiate(Resources.Load("Weapons/ProjectileBase"),this.transform.position+(new Vector3(0.3f,0,0)),this.transform.rotation) as GameObject;
//							projectileObj.GetComponent<Projectile>().lifetime = 10;
//							projectileObj.GetComponent<Projectile>().direction=this.GetComponent<Movement>().direction;
//							break;
//						default:
//							break;
//						}
						//projectileObj = GameObject.Instantiate(Resources.Load("Weapons/ProjectileBase"),this.transform.position+Vector3.up,this.transform.rotation) as GameObject;

						
						spawnPosition = mouseVector - new Vector2(this.transform.position.x,this.transform.position.y);
						spawnPosition = spawnPosition.normalized;
						projectileObj = GameObject.Instantiate(Resources.Load("Weapons/ProjectileBase"),this.transform.position + new Vector3(spawnPosition.normalized.x,spawnPosition.normalized.y,0),this.transform.rotation) as GameObject;
						projectileObj.GetComponent<Projectile>().flightDirection = spawnPosition;
						//projectileObj.GetComponent<Projectile>().direction=this.GetComponent<Movement>().direction;
						projectileObj.GetComponent<Projectile>().velocity = equippedItem.GetComponent<WeaponStats>().projectileVelocity; //Set it to the weapon value.
						//projectileObj.GetComponent<Projectile>().damage = 5;
					}
					if(equippedItem!=null){
						equippedItem.GetComponent<WeaponStats>().durability-=1;
						GameObject.FindGameObjectWithTag("GameController").GetComponent<Manager>().actionBarEntity.returnActionBarList(true)[0].transform.GetChild(0).GetComponent<DurabilityDisplay>().lowerDurability(1);
						if(GameObject.FindGameObjectWithTag("GameController").GetComponent<Manager>().actionBarEntity.returnEquippedItem(1).GetComponent<SpriteRenderer>().sprite!= null){
							GameObject.FindGameObjectWithTag("GameController").GetComponent<Manager>().actionBarEntity.returnActionBarList(true)[1].transform.GetChild(0).GetComponent<DurabilityDisplay>().lowerDurability(1);
						}
						if(GameObject.FindGameObjectWithTag("GameController").GetComponent<Manager>().actionBarEntity.returnActionBarList(true)[0].transform.GetChild(0).GetComponent<DurabilityDisplay>().returnDurability().x<=0){
							GameObject.FindGameObjectWithTag("GameController").GetComponent<Manager>().actionBarEntity.returnActionBarList(true)[0].GetComponent<SlotBehaviour>().itemQuantity-=1;
						}
						if(GameObject.FindGameObjectWithTag("GameController").GetComponent<Manager>().actionBarEntity.returnActionBarList(true)[1].transform.GetChild(0).GetComponent<DurabilityDisplay>().returnDurability().x<=0){
							GameObject.FindGameObjectWithTag("GameController").GetComponent<Manager>().actionBarEntity.returnActionBarList(true)[1].GetComponent<SlotBehaviour>().itemQuantity-=1;
						}
					}
				}
			}
		}
		delayCount-=Time.deltaTime;
	}
	IEnumerator launchMeleeAttack(float delay){
		int direction = 0; //1 = up 2 = down 3 = left 4 = right
		AnimatorStateInfo currentAnim;
		float mouseX =  Input.mousePosition.x/Screen.width;
		float mouseY =  Input.mousePosition.y/Screen.height;
		this.GetComponent<Movement>().moveable = false;
		if(Input.GetAxis("Fire1")>0){
			if(mouseX+mouseY >= 1&&mouseX>mouseY&&mouseX>0.5f){
				direction = 4;
				if(this.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("AttackRight")==false){
					this.GetComponent<Animator> ().Play ("AttackRight");
				}
			} 
			if(mouseX+mouseY<1&&mouseX<mouseY&&mouseX<0.5f) {
				direction = 3;
				if(this.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("AttackLeft")==false){
					this.GetComponent<Animator> ().Play ("AttackLeft");
				}
			}
			if(mouseX+mouseY>=1&&mouseY>mouseX&&mouseY>0.5f){
				direction = 1;
				if(this.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("AttackUp")==false){
					this.GetComponent<Animator> ().Play ("AttackUp");
				}
			}
			if(mouseX+mouseY<1&&mouseY<mouseX&&mouseY<0.5f){
				direction = 2;
				if(this.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("AttackDown")==false){
						this.GetComponent<Animator> ().Play ("AttackDown");
					}
			}
		}
		currentAnim = this.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0);
//		if(currentAnim.IsName("WalkUp") == true){
//			direction = 1;
//			this.GetComponent<Animator>().Play("AttackUp");
//		} else if (currentAnim.IsName("WalkDown") == true){
//			direction = 2;
//			this.GetComponent<Animator>().Play("AttackDown");
//
//		} else if (currentAnim.IsName("WalkLeft") == true){
//			direction = 3;
//			this.GetComponent<Animator>().Play("AttackLeft");
//
//		} else if (currentAnim.IsName("WalkRight") == true){
//			direction = 4;
//			this.GetComponent<Animator>().Play("AttackRight");
//
//		}
		this.GetComponent<Movement>().moveable = false;
		yield return new WaitForSeconds(this.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length);
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
		if(GameObject.FindGameObjectWithTag("GameController").GetComponent<Manager>().actionBarEntity.returnEquippedItem(0).GetComponent<SpriteRenderer>().sprite!=null){
			equippedItem = Resources.Load("Items/" + GameObject.FindGameObjectWithTag("GameController").GetComponent<Manager>().actionBarEntity.returnEquippedItem(0).GetComponent<SpriteRenderer>().sprite.name) as GameObject;
		} else {
			equippedItem = null;
		}
		if(GameObject.FindGameObjectWithTag("GameController").GetComponent<Manager>().actionBarEntity.returnEquippedItem(1).GetComponent<SpriteRenderer>().sprite!= null){
			equippedItem2 = Resources.Load("Items/" + GameObject.FindGameObjectWithTag("GameController").GetComponent<Manager>().actionBarEntity.returnEquippedItem(1).GetComponent<SpriteRenderer>().sprite.name) as GameObject;
		} else {
			equippedItem2 = null;
		}
		if(equippedItem!=null){
			if(equippedItem.GetComponent<WeaponStats>().ranged == true){
				melee = false;
			} else {
				melee = true;
			}
		} else {
			melee = true;
		}
		if(equippedItem!=null){
			delayCount = equippedItem.GetComponent<WeaponStats>().delay;
		}
	}
}
