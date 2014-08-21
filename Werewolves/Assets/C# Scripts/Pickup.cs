using UnityEngine;
using System.Collections;

public class Pickup : MonoBehaviour {



	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

	}

//	void OnTriggerEnter2D(Collider2D collisionObject){
//		if(delay<=0){
//			if(collisionObject.gameObject.tag=="Pickups"){
//				GameObject.FindGameObjectWithTag("GameController").GetComponent<Manager>().actionBarEntity.addItemToBar(collisionObject.gameObject);
//			}
//			Destroy(collisionObject.gameObject);
//		}
//	}

	void OnTriggerStay2D(Collider2D collisionObject){
		if(collisionObject.gameObject.tag=="Pickups"){
			if(collisionObject.GetComponent<ItemBehaviour>().delay<=0){
				if(collisionObject.gameObject.tag=="Pickups"){
					GameObject slotObject;
					slotObject = GameObject.FindGameObjectWithTag("GameController").GetComponent<Manager>().actionBarEntity.addItemToBar(collisionObject.gameObject);
					if(slotObject!=null){
						if(collisionObject.GetComponent<ItemBehaviour>().weapon == true){
							slotObject.gameObject.transform.GetChild(0).GetComponent<DurabilityDisplay>().newDurability(collisionObject.GetComponent<WeaponStats>().durability,collisionObject.GetComponent<WeaponStats>().maxDurability);
						}
						Destroy(collisionObject.gameObject);
					} else {

					}
				}

			}
		}
	}
}
