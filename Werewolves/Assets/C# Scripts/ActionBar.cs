using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class ActionBar : MonoBehaviour {

	List<GameObject> actionBarSlots = new List<GameObject>();
	List<GameObject> actionBarEquipSlots = new List<GameObject>();
	// Use this for initialization
	void Start () {
		for(int i = 1; i<=4; i++){
			actionBarSlots.Add(GameObject.Find("Slot" + i.ToString()));
		}
		for(int i = 1; i<=2; i++){
			actionBarEquipSlots.Add(GameObject.Find("EquipSlot" + i.ToString()));
		}
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.F)){
			actionBarEquipSlots[0].GetComponent<SpriteRenderer>().sprite=actionBarSlots[0].GetComponent<SpriteRenderer>().sprite;
		}
		if(Input.GetKeyDown(KeyCode.Mouse1)){
			GameObject tempItem;
			Vector2 mouseVector = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			if(actionBarEquipSlots[0].collider2D.OverlapPoint(mouseVector)){
				//Right click on equipSlot1
			}
			if(actionBarEquipSlots[1].collider2D.OverlapPoint(mouseVector)){
				//Right click on equipSlot2
			}
			if(actionBarSlots[0].collider2D.OverlapPoint(mouseVector)){
				tempItem = Resources.Load("Items/"+actionBarSlots[0].GetComponent<SpriteRenderer>().sprite.name) as GameObject;
				if(actionBarEquipSlots[0].GetComponent<SpriteRenderer>().sprite==null){
					actionBarEquipSlots[0].GetComponent<SpriteRenderer>().sprite=actionBarSlots[0].GetComponent<SpriteRenderer>().sprite;
					if(tempItem.GetComponent<WeaponStats>().twoHanded == true){
						actionBarEquipSlots[1].GetComponent<SpriteRenderer>().sprite=actionBarSlots[0].GetComponent<SpriteRenderer>().sprite;
					}
					actionBarSlots[0].GetComponent<SpriteRenderer>().sprite = null;
					GameObject.FindGameObjectWithTag("GameController").GetComponent<Manager>().weaponSystemEntity.newEquippedItem();
				} else if (actionBarEquipSlots[1].GetComponent<SpriteRenderer>().sprite==null&&tempItem.GetComponent<WeaponStats>().twoHanded == false){
					actionBarEquipSlots[1].GetComponent<SpriteRenderer>().sprite=actionBarSlots[0].GetComponent<SpriteRenderer>().sprite;
					actionBarSlots[0].GetComponent<SpriteRenderer>().sprite = null;
					GameObject.FindGameObjectWithTag("GameController").GetComponent<Manager>().weaponSystemEntity.newEquippedItem();
				}
				//Right click on Slot1
			}
			if(actionBarSlots[1].collider2D.OverlapPoint(mouseVector)){
				tempItem = Resources.Load("Items/"+actionBarSlots[1].GetComponent<SpriteRenderer>().sprite.name) as GameObject;
				if(actionBarEquipSlots[0].GetComponent<SpriteRenderer>().sprite==null){
					actionBarEquipSlots[0].GetComponent<SpriteRenderer>().sprite=actionBarSlots[1].GetComponent<SpriteRenderer>().sprite;
					if(tempItem.GetComponent<WeaponStats>().twoHanded == true){
						actionBarEquipSlots[1].GetComponent<SpriteRenderer>().sprite=actionBarSlots[1].GetComponent<SpriteRenderer>().sprite;
					}
					actionBarSlots[1].GetComponent<SpriteRenderer>().sprite = null;
					GameObject.FindGameObjectWithTag("GameController").GetComponent<Manager>().weaponSystemEntity.newEquippedItem();
				} else if (actionBarEquipSlots[1].GetComponent<SpriteRenderer>().sprite==null&&tempItem.GetComponent<WeaponStats>().twoHanded == false){
					actionBarEquipSlots[1].GetComponent<SpriteRenderer>().sprite=actionBarSlots[1].GetComponent<SpriteRenderer>().sprite;
					actionBarSlots[1].GetComponent<SpriteRenderer>().sprite = null;
					GameObject.FindGameObjectWithTag("GameController").GetComponent<Manager>().weaponSystemEntity.newEquippedItem();
				}
				//Right click on Slot2
			}
			if(actionBarSlots[2].collider2D.OverlapPoint(mouseVector)){
				tempItem = Resources.Load("Items/"+actionBarSlots[2].GetComponent<SpriteRenderer>().sprite.name) as GameObject;
				if(actionBarEquipSlots[0].GetComponent<SpriteRenderer>().sprite==null){
					actionBarEquipSlots[0].GetComponent<SpriteRenderer>().sprite=actionBarSlots[2].GetComponent<SpriteRenderer>().sprite;
					if(tempItem.GetComponent<WeaponStats>().twoHanded == true){
						actionBarEquipSlots[1].GetComponent<SpriteRenderer>().sprite=actionBarSlots[2].GetComponent<SpriteRenderer>().sprite;
					}
					actionBarSlots[2].GetComponent<SpriteRenderer>().sprite = null;
					GameObject.FindGameObjectWithTag("GameController").GetComponent<Manager>().weaponSystemEntity.newEquippedItem();
				} else if (actionBarEquipSlots[1].GetComponent<SpriteRenderer>().sprite==null&&tempItem.GetComponent<WeaponStats>().twoHanded == false){
					actionBarEquipSlots[1].GetComponent<SpriteRenderer>().sprite=actionBarSlots[2].GetComponent<SpriteRenderer>().sprite;
					actionBarSlots[2].GetComponent<SpriteRenderer>().sprite = null;
					GameObject.FindGameObjectWithTag("GameController").GetComponent<Manager>().weaponSystemEntity.newEquippedItem();
				}
				//Right click on Slot3
			}
			if(actionBarSlots[3].collider2D.OverlapPoint(mouseVector)){
				tempItem = Resources.Load("Items/"+actionBarSlots[3].GetComponent<SpriteRenderer>().sprite.name) as GameObject;
				if(actionBarEquipSlots[0].GetComponent<SpriteRenderer>().sprite==null){
					actionBarEquipSlots[0].GetComponent<SpriteRenderer>().sprite=actionBarSlots[3].GetComponent<SpriteRenderer>().sprite;
					if(tempItem.GetComponent<WeaponStats>().twoHanded == true){
						actionBarEquipSlots[1].GetComponent<SpriteRenderer>().sprite=actionBarSlots[3].GetComponent<SpriteRenderer>().sprite;
					}
					actionBarSlots[3].GetComponent<SpriteRenderer>().sprite = null;
					GameObject.FindGameObjectWithTag("GameController").GetComponent<Manager>().weaponSystemEntity.newEquippedItem();
				} else if (actionBarEquipSlots[1].GetComponent<SpriteRenderer>().sprite==null&&tempItem.GetComponent<WeaponStats>().twoHanded == false){
					actionBarEquipSlots[1].GetComponent<SpriteRenderer>().sprite=actionBarSlots[3].GetComponent<SpriteRenderer>().sprite;
					actionBarSlots[3].GetComponent<SpriteRenderer>().sprite = null;
					GameObject.FindGameObjectWithTag("GameController").GetComponent<Manager>().weaponSystemEntity.newEquippedItem();
				}
				//Right click on Slot4
			}
			}
		}


	void OnMouseDown(){
		if(name.Contains("Slot")){
			if(this.GetComponent<SpriteRenderer>().sprite.name.Contains("wolfsbane")){
				this.GetComponent<SpriteRenderer>().color = Color.red;
			}
		}
	}
	public GameObject returnEquippedItem(int slot){
		if(actionBarEquipSlots[slot] != null){
			return actionBarEquipSlots[slot];
		}
		return null;
	}
	public bool addItemToBar(GameObject itemToAdd){
		bool addedToBar = false;
		foreach(GameObject slot in actionBarSlots){
			if(slot.GetComponent<SpriteRenderer>().sprite !=null){
				if(slot.GetComponent<SpriteRenderer>().sprite.name==itemToAdd.name){
					//increase count if stackable item, else repair/replace?
					addedToBar = true;
					break;
				}
			} else if (slot.GetComponent<SpriteRenderer>().sprite ==null){
				slot.GetComponent<SpriteRenderer>().sprite = Resources.Load("Sprites/Equipment/" + itemToAdd.name, typeof(Sprite)) as Sprite;
				addedToBar = true;
				break;
			}
		}
		return addedToBar;
	}
	public List<GameObject> returnActionBarList(bool equipSlots){
		if(equipSlots){
			return actionBarEquipSlots;
		} else {
			return actionBarSlots;
		}
	}
}
