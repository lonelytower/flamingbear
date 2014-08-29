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

			Vector2 mouseVector = Camera.main.ScreenToWorldPoint(Input.mousePosition); //Where the mouse is in world position
			if(actionBarEquipSlots[0].collider2D.OverlapPoint(mouseVector)){ //Right click on equipSlot1
				bool twoHanded = false;
				if(actionBarEquipSlots[0].GetComponent<SpriteRenderer>().sprite!=null){// If there is a sprite
					tempItem = Resources.Load("Items/"+actionBarEquipSlots[0].GetComponent<SpriteRenderer>().sprite.name) as GameObject;//Get the item instance of that sprite
					twoHanded = tempItem.GetComponent<WeaponStats>().twoHanded;//Determine if it is two handed
				}
				Sprite tempSprite;
				foreach(GameObject slot in returnActionBarList(false)){//Look through all the normal action bar slots
					if(slot.name!=actionBarEquipSlots[0].name&&slot.GetComponent<SpriteRenderer>().sprite == null){ // If a slot is empty
						if(twoHanded){
							actionBarEquipSlots[1].GetComponent<SpriteRenderer>().sprite = null;//Unequip the second equip slot
						}
						slot.GetComponent<SpriteRenderer>().sprite = actionBarEquipSlots[0].GetComponent<SpriteRenderer>().sprite; //Set the found empty slots sprite to the first equip slots sprite
						actionBarEquipSlots[0].GetComponent<SpriteRenderer>().sprite = null; //Unequip the first equip slot

						slot.transform.GetChild(0).GetComponent<DurabilityDisplay>().newDurability(actionBarEquipSlots[0].transform.GetChild(0).GetComponent<DurabilityDisplay>().returnDurability());
						actionBarEquipSlots[0].transform.GetChild(0).GetComponent<DurabilityDisplay>().newDurability(0,1);
						actionBarEquipSlots[1].transform.GetChild(0).GetComponent<DurabilityDisplay>().newDurability(0,1);

					}
				}
				GameObject.FindGameObjectWithTag("GameController").GetComponent<Manager>().weaponSystemEntity.newEquippedItem();
			}
			if(actionBarEquipSlots[1].collider2D.OverlapPoint(mouseVector)){ //Right click on equipSlot2
				bool twoHanded = false;
				if(actionBarEquipSlots[1].GetComponent<SpriteRenderer>().sprite!=null){ // If there is a sprite
					tempItem = Resources.Load("Items/"+actionBarEquipSlots[0].GetComponent<SpriteRenderer>().sprite.name) as GameObject; //Get the item instance of that sprite
					twoHanded = tempItem.GetComponent<WeaponStats>().twoHanded; //Determine if it is two handed
				}
				Sprite tempSprite;
				foreach(GameObject slot in returnActionBarList(false)){ //Look through all the normal action bar slots
					if(slot.name!=actionBarEquipSlots[1].name&&slot.GetComponent<SpriteRenderer>().sprite == null){ // If a slot is empty
						if(twoHanded){ 
							actionBarEquipSlots[0].GetComponent<SpriteRenderer>().sprite = null; //Unequip the first equip slot
						}
						slot.GetComponent<SpriteRenderer>().sprite = actionBarEquipSlots[1].GetComponent<SpriteRenderer>().sprite; //Set the found empty slots sprite to the second equip slots sprite
						actionBarEquipSlots[1].GetComponent<SpriteRenderer>().sprite = null; // Empty the second equip slot

						slot.transform.GetChild(0).GetComponent<DurabilityDisplay>().newDurability(actionBarEquipSlots[0].transform.GetChild(0).GetComponent<DurabilityDisplay>().returnDurability());
						actionBarEquipSlots[0].transform.GetChild(0).GetComponent<DurabilityDisplay>().newDurability(0,1);
						actionBarEquipSlots[1].transform.GetChild(0).GetComponent<DurabilityDisplay>().newDurability(0,1);


					}
				}
				GameObject.FindGameObjectWithTag("GameController").GetComponent<Manager>().weaponSystemEntity.newEquippedItem();
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

	}
	public GameObject returnEquippedItem(int slot){
		if(actionBarEquipSlots[slot] != null){
			return actionBarEquipSlots[slot];
		}
		return null;
	}
	public GameObject addItemToBar(GameObject itemToAdd){
		GameObject slotAddedTo = null;
		foreach(GameObject slot in actionBarSlots){
			if(slot.GetComponent<SpriteRenderer>().sprite !=null){
				if(slot.GetComponent<SpriteRenderer>().sprite.name==itemToAdd.name){
					//increase count if stackable item, else repair/replace?
					slot.GetComponent<SlotBehaviour>().itemQuantity += 1;
					slotAddedTo = slot;
					break;
				}
			} else {
				if (slot.GetComponent<SpriteRenderer>().sprite ==null){
					slot.GetComponent<SpriteRenderer>().sprite = Resources.Load("Sprites/Equipment/" + itemToAdd.name, typeof(Sprite)) as Sprite;
					slotAddedTo = slot;
					break;
				}
			}
		}
		return slotAddedTo;
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
		Vector2 tempDurability = actionBarSlots[slot-1].transform.GetChild(0).GetComponent<DurabilityDisplay>().returnDurability();
		Vector2 tempDurability2 = actionBarEquipSlots[0].transform.GetChild(0).GetComponent<DurabilityDisplay>().returnDurability();
		if(actionBarSlots[slot-1].GetComponent<SpriteRenderer>().sprite!=null){
			tempItem = Resources.Load("Items/"+actionBarSlots[slot-1].GetComponent<SpriteRenderer>().sprite.name) as GameObject; // The activated slots item
			if(tempItem.GetComponent<ItemBehaviour>().weapon==true){
				if(actionBarEquipSlots[0].GetComponent<SpriteRenderer>().sprite!=null){
					tempItem2 = Resources.Load("Items/"+actionBarEquipSlots[0].GetComponent<SpriteRenderer>().sprite.name) as GameObject; //the item in the first equip slot
				}
				if(actionBarEquipSlots[0].GetComponent<SpriteRenderer>().sprite!=null){ //There is an item in the first equip slot
					if(tempItem2.GetComponent<WeaponStats>().twoHanded == true){ //And the item is two handed
						actionBarEquipSlots[1].GetComponent<SpriteRenderer>().sprite = null; //Remove the item from the second equip slot
					} else {

					}
					tempSprite = actionBarEquipSlots[0].GetComponent<SpriteRenderer>().sprite; //Set tempsprite to the sprite in the first equip slot
					actionBarEquipSlots[0].GetComponent<SpriteRenderer>().sprite=actionBarSlots[slot-1].GetComponent<SpriteRenderer>().sprite; //Change the first equip slot sprite to the activated slot sprite
					if(tempItem.GetComponent<WeaponStats>().twoHanded == true){ //Item being equipped is two handed
						actionBarEquipSlots[1].GetComponent<SpriteRenderer>().sprite=actionBarSlots[slot-1].GetComponent<SpriteRenderer>().sprite; // Also set the second equip slot sprite to the activated slot sprite
						actionBarSlots[slot-1].GetComponent<SpriteRenderer>().sprite=tempSprite; // Put the old weapon sprite in the activated slot

						actionBarSlots[slot-1].transform.GetChild(0).GetComponent<DurabilityDisplay>().newDurability((int)tempDurability2.x,(int)tempDurability2.y);
						actionBarEquipSlots[0].transform.GetChild(0).GetComponent<DurabilityDisplay>().newDurability((int)tempDurability.x,(int)tempDurability.y);
						actionBarEquipSlots[1].transform.GetChild(0).GetComponent<DurabilityDisplay>().newDurability((int)tempDurability.x,(int)tempDurability.y);

					} else { //Item being equipped is one handed
						if(actionBarEquipSlots[0].GetComponent<SpriteRenderer>().sprite!=null&&tempItem2.GetComponent<WeaponStats>().twoHanded == false){ //If the item in the first equip slot is one handed
							tempSprite = actionBarEquipSlots[1].GetComponent<SpriteRenderer>().sprite; //Set tempsprite to the second equip slot
							actionBarEquipSlots[1].GetComponent<SpriteRenderer>().sprite = actionBarSlots[slot-1].GetComponent<SpriteRenderer>().sprite; //Put the new sprite in equip slot 2
							actionBarSlots[slot-1].GetComponent<SpriteRenderer>().sprite = tempSprite; //Put the old sprite in the activated slot

							actionBarSlots[slot-1].transform.GetChild(0).GetComponent<DurabilityDisplay>().newDurability((int)tempDurability2.x,(int)tempDurability2.y);
							actionBarEquipSlots[0].transform.GetChild(0).GetComponent<DurabilityDisplay>().newDurability((int)tempDurability.x,(int)tempDurability.y);
							actionBarEquipSlots[1].transform.GetChild(0).GetComponent<DurabilityDisplay>().newDurability(0,1);


						} else { //If the item in the first equip slot is two handed
							actionBarSlots[slot-1].GetComponent<SpriteRenderer>().sprite = tempSprite; //put the old sprite in the activated slot

							actionBarSlots[slot-1].transform.GetChild(0).GetComponent<DurabilityDisplay>().newDurability((int)tempDurability2.x,(int)tempDurability2.y);
							actionBarEquipSlots[0].transform.GetChild(0).GetComponent<DurabilityDisplay>().newDurability((int)tempDurability.x,(int)tempDurability.y);

						}
					}
				} else { // There is no item in the first equip slot
					if(tempItem.GetComponent<WeaponStats>().twoHanded == true){ //The item being equipped is two handed
						actionBarEquipSlots[0].GetComponent<SpriteRenderer>().sprite=actionBarSlots[slot-1].GetComponent<SpriteRenderer>().sprite;
						actionBarEquipSlots[1].GetComponent<SpriteRenderer>().sprite=actionBarSlots[slot-1].GetComponent<SpriteRenderer>().sprite;
						actionBarSlots[slot-1].GetComponent<SpriteRenderer>().sprite = null;

						actionBarSlots[slot-1].transform.GetChild(0).GetComponent<DurabilityDisplay>().newDurability(0,1);
						actionBarEquipSlots[0].transform.GetChild(0).GetComponent<DurabilityDisplay>().newDurability((int)tempDurability.x,(int)tempDurability.y);
						actionBarEquipSlots[1].transform.GetChild(0).GetComponent<DurabilityDisplay>().newDurability((int)tempDurability.x,(int)tempDurability.y);

					} else if (tempItem.GetComponent<WeaponStats>().twoHanded == false){ // the item being equipped is one handed
						if(actionBarEquipSlots[0].GetComponent<SpriteRenderer>().sprite!=null&&tempItem2.GetComponent<WeaponStats>().twoHanded == false){
							tempSprite = actionBarEquipSlots[1].GetComponent<SpriteRenderer>().sprite;
							actionBarEquipSlots[1].GetComponent<SpriteRenderer>().sprite=actionBarSlots[slot-1].GetComponent<SpriteRenderer>().sprite;
							actionBarSlots[slot-1].GetComponent<SpriteRenderer>().sprite = tempSprite;

							actionBarSlots[slot-1].transform.GetChild(0).GetComponent<DurabilityDisplay>().newDurability(0,1);
							actionBarEquipSlots[0].transform.GetChild(0).GetComponent<DurabilityDisplay>().newDurability((int)tempDurability.x,(int)tempDurability.y);
							actionBarEquipSlots[1].transform.GetChild(0).GetComponent<DurabilityDisplay>().newDurability((int)tempDurability.x,(int)tempDurability.y);

						} else {
							actionBarEquipSlots[0].GetComponent<SpriteRenderer>().sprite=actionBarSlots[slot-1].GetComponent<SpriteRenderer>().sprite;
							actionBarSlots[slot-1].GetComponent<SpriteRenderer>().sprite = null;
							
							actionBarSlots[slot-1].transform.GetChild(0).GetComponent<DurabilityDisplay>().newDurability(0,1);
							actionBarEquipSlots[0].transform.GetChild(0).GetComponent<DurabilityDisplay>().newDurability((int)tempDurability.x,(int)tempDurability.y);
						}
					}
				}
				GameObject.FindGameObjectWithTag("GameController").GetComponent<Manager>().weaponSystemEntity.newEquippedItem();
			} else { //If the activated item is a perishable
				//Do the use stuff
				if(tempItem.name == "Wolfsbane"){
					if(GameObject.FindGameObjectWithTag("Player").GetComponent<Stats>().cursed == true){
						GameObject.FindGameObjectWithTag("Player").GetComponent<Stats>().cursed = false;
						actionBarSlots[slot-1].GetComponent<SlotBehaviour>().itemQuantity -= 1; 
					}
				}
			}
		}
	}
}
