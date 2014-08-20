using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class ActionBar : MonoBehaviour {

	List<GameObject> actionBarSlots = new List<GameObject>();
	List<GameObject> actionBarEquipSlots = new List<GameObject>();
	GameObject tempItem;
	Sprite tempSprite;
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
		if(Input.GetKeyDown(KeyCode.Mouse1)){

			Vector2 mouseVector = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			if(actionBarEquipSlots[0].collider2D.OverlapPoint(mouseVector)){
				bool twoHanded = false;
				if(actionBarEquipSlots[0].GetComponent<SpriteRenderer>().sprite!=null){
					tempItem = Resources.Load("Items/"+actionBarEquipSlots[0].GetComponent<SpriteRenderer>().sprite.name) as GameObject;
					twoHanded = tempItem.GetComponent<WeaponStats>().twoHanded;
				}
				Sprite tempSprite;
				foreach(GameObject slot in returnActionBarList(false)){
					if(slot.name!=actionBarEquipSlots[0].name&&slot.GetComponent<SpriteRenderer>().sprite == null){
						if(twoHanded){
							actionBarEquipSlots[1].GetComponent<SpriteRenderer>().sprite = null;
						}
						slot.GetComponent<SpriteRenderer>().sprite = actionBarEquipSlots[0].GetComponent<SpriteRenderer>().sprite;
						actionBarEquipSlots[0].GetComponent<SpriteRenderer>().sprite = null;
					}
				}
				//Right click on equipSlot1
			}
			if(actionBarEquipSlots[1].collider2D.OverlapPoint(mouseVector)){
				bool twoHanded = false;
				if(actionBarEquipSlots[1].GetComponent<SpriteRenderer>().sprite!=null){
					tempItem = Resources.Load("Items/"+actionBarEquipSlots[0].GetComponent<SpriteRenderer>().sprite.name) as GameObject;
					twoHanded = tempItem.GetComponent<WeaponStats>().twoHanded;
				}
				Sprite tempSprite;
				foreach(GameObject slot in returnActionBarList(false)){
					if(slot.name!=actionBarEquipSlots[1].name&&slot.GetComponent<SpriteRenderer>().sprite == null){
						if(twoHanded){
							actionBarEquipSlots[0].GetComponent<SpriteRenderer>().sprite = null;
						}
						slot.GetComponent<SpriteRenderer>().sprite = actionBarEquipSlots[1].GetComponent<SpriteRenderer>().sprite;
						actionBarEquipSlots[1].GetComponent<SpriteRenderer>().sprite = null;
					}
				}
				//Right click on equipSlot2
			}
			if(actionBarSlots[0].collider2D.OverlapPoint(mouseVector)){
				actionBarTrigger(1);
				//Right click on Slot1
			}
			if(actionBarSlots[1].collider2D.OverlapPoint(mouseVector)){
				actionBarTrigger(2);
				//Right click on Slot2
			}
			if(actionBarSlots[2].collider2D.OverlapPoint(mouseVector)){
				actionBarTrigger(3);
				//Right click on Slot3
			}
			if(actionBarSlots[3].collider2D.OverlapPoint(mouseVector)){
				actionBarTrigger(4);
				//Right click on Slot4
			}
		}
		if(Input.GetKeyDown(KeyCode.Alpha1)){
			actionBarTrigger(1);
		}
		if(Input.GetKeyDown(KeyCode.Alpha2)){
			actionBarTrigger(2);
		}
		if(Input.GetKeyDown(KeyCode.Alpha3)){
			actionBarTrigger(3);
		}
		if(Input.GetKeyDown(KeyCode.Alpha4)){
			actionBarTrigger(4);
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
//			if(slot.GetComponent<SpriteRenderer>().sprite !=null){
//				if(slot.GetComponent<SpriteRenderer>().sprite.name==itemToAdd.name){
//					//increase count if stackable item, else repair/replace?
//					addedToBar = true;
//					break;
//				}
//			} else 
		if (slot.GetComponent<SpriteRenderer>().sprite ==null){
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

	void actionBarTrigger(int slot){
		GameObject tempItem2 = null;
		if(actionBarSlots[slot-1].GetComponent<SpriteRenderer>().sprite!=null){
			tempItem = Resources.Load("Items/"+actionBarSlots[slot-1].GetComponent<SpriteRenderer>().sprite.name) as GameObject;
			if(tempItem.GetComponent<ItemBehaviour>().weapon==true){
				if(actionBarEquipSlots[0].GetComponent<SpriteRenderer>().sprite!=null){
					tempItem2 = Resources.Load("Items/"+actionBarEquipSlots[0].GetComponent<SpriteRenderer>().sprite.name) as GameObject;
				}
				if(actionBarEquipSlots[0].GetComponent<SpriteRenderer>().sprite!=null){ //There is an item in the first equip slot
					if(tempItem2.GetComponent<WeaponStats>().twoHanded == true){ //And the item is two handed
						actionBarEquipSlots[1].GetComponent<SpriteRenderer>().sprite = null;
					} else {

					}
					tempSprite = actionBarEquipSlots[0].GetComponent<SpriteRenderer>().sprite;
					actionBarEquipSlots[0].GetComponent<SpriteRenderer>().sprite=actionBarSlots[slot-1].GetComponent<SpriteRenderer>().sprite;
					if(tempItem.GetComponent<WeaponStats>().twoHanded == true){ //Item being equipped is two handed
						actionBarEquipSlots[1].GetComponent<SpriteRenderer>().sprite=actionBarSlots[slot-1].GetComponent<SpriteRenderer>().sprite;
						actionBarSlots[slot-1].GetComponent<SpriteRenderer>().sprite=tempSprite;
					} else { //Item being equipped is one handed
						if(actionBarEquipSlots[0].GetComponent<SpriteRenderer>().sprite!=null&&tempItem2.GetComponent<WeaponStats>().twoHanded == false){
							tempSprite = actionBarEquipSlots[1].GetComponent<SpriteRenderer>().sprite;
							actionBarEquipSlots[1].GetComponent<SpriteRenderer>().sprite = actionBarSlots[slot-1].GetComponent<SpriteRenderer>().sprite;
							actionBarSlots[slot-1].GetComponent<SpriteRenderer>().sprite = tempSprite;
						} else {
							actionBarSlots[slot-1].GetComponent<SpriteRenderer>().sprite = tempSprite;
						}
					}
				} else {
					if(tempItem.GetComponent<WeaponStats>().twoHanded == true){
						actionBarEquipSlots[0].GetComponent<SpriteRenderer>().sprite=actionBarSlots[slot-1].GetComponent<SpriteRenderer>().sprite;
						actionBarEquipSlots[1].GetComponent<SpriteRenderer>().sprite=actionBarSlots[slot-1].GetComponent<SpriteRenderer>().sprite;
						actionBarSlots[slot-1].GetComponent<SpriteRenderer>().sprite = null;
					} else if (tempItem.GetComponent<WeaponStats>().twoHanded == false){
						if(actionBarEquipSlots[0].GetComponent<SpriteRenderer>().sprite!=null&&tempItem2.GetComponent<WeaponStats>().twoHanded == false){
							tempSprite = actionBarEquipSlots[1].GetComponent<SpriteRenderer>().sprite;
							actionBarEquipSlots[1].GetComponent<SpriteRenderer>().sprite=actionBarSlots[slot-1].GetComponent<SpriteRenderer>().sprite;
							actionBarSlots[slot-1].GetComponent<SpriteRenderer>().sprite = tempSprite;
						} else {
							actionBarEquipSlots[0].GetComponent<SpriteRenderer>().sprite=actionBarSlots[slot-1].GetComponent<SpriteRenderer>().sprite;
							actionBarSlots[slot-1].GetComponent<SpriteRenderer>().sprite = null;
						}
					}
				}
				GameObject.FindGameObjectWithTag("GameController").GetComponent<Manager>().weaponSystemEntity.newEquippedItem();
			} else { //If the activated item is a perishable
				//Do the use stuff
				if(tempItem.name == "Wolfsbane"){
					if(GameObject.FindGameObjectWithTag("Player").GetComponent<Stats>().cursed == true){
						GameObject.FindGameObjectWithTag("Player").GetComponent<Stats>().cursed = false;
						actionBarSlots[slot-1].GetComponent<ItemBehaviour>().quantity -= 1;
					}
				}
			}
		}
	}
}
