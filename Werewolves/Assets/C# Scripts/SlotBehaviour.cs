using UnityEngine;
using System.Collections;

public class SlotBehaviour : MonoBehaviour {

	Vector3 originalPosition;
	int itemDurability;
	public int itemQuantity = 1;

	// Use this for initialization
	void Start () {
		originalPosition = this.transform.localPosition;
	}
	
	// Update is called once per frame
	void Update () {
		if(this.GetComponent<SpriteRenderer>().sprite == null){
			this.GetComponent<SpriteRenderer>().sprite = Resources.Load("Sprites/UI/SlotEmpty",typeof(Sprite)) as Sprite;
		}
		if(this.GetComponent<SpriteRenderer>().sprite.name != "SlotEmpty"){
			this.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().enabled = true;
		} else {
			this.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().enabled = false;
		}
		if(itemQuantity<=0){
			   if(this.GetComponent<SpriteRenderer>().sprite.name != "SlotEmpty"){
				GameObject tempItem = Resources.Load("Items/"+this.GetComponent<SpriteRenderer>().sprite.name) as GameObject;
				if(tempItem.GetComponent<ItemBehaviour>().weapon == true){
					GameObject.Instantiate(Resources.Load("Items/Scrap Metal"),GameObject.FindGameObjectWithTag("Player").transform.position,Quaternion.identity);
				}
				this.GetComponent<SpriteRenderer>().sprite = Resources.Load("Sprites/UI/SlotEmpty",typeof(Sprite)) as Sprite;
			}
		}
	}

	void OnMouseDrag(){
		if(name.Contains("Slot")){
			Vector3 screenMouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			Vector3 mouseOffset = screenMouse - transform.parent.position;
			this.transform.localPosition = new Vector3(mouseOffset.x,mouseOffset.y,-0.3f);
		}
	}

	void OnMouseUpAsButton(){
		Sprite tempSprite;
		Vector2 tempDurability;
		GameObject droppedItem;
		bool switched = false;
		if(this.GetComponent<SpriteRenderer>().sprite.name != "SlotEmpty"){ //If this slot isn't empty
			if(!this.name.Contains("Equip")){ //If this slot is not an equip slot
				foreach(GameObject slot in GameObject.FindGameObjectWithTag("GameController").GetComponent<Manager>().actionBarEntity.returnActionBarList(false)){ //Check each slot in non equip slots
					if(slot.GetComponent<SpriteRenderer>().bounds.Intersects(this.GetComponent<SpriteRenderer>().bounds)&&slot.name!=this.name){ //If it intersects the bounds of the current slot and isn't the same slot
						tempSprite = slot.GetComponent<SpriteRenderer>().sprite;
						tempDurability = slot.transform.GetChild(0).GetComponent<DurabilityDisplay>().returnDurability();
						slot.transform.GetChild(0).GetComponent<DurabilityDisplay>().newDurability(this.transform.GetChild(0).GetComponent<DurabilityDisplay>().returnDurability());
						this.transform.GetChild(0).GetComponent<DurabilityDisplay>().newDurability(tempDurability);
						slot.GetComponent<SpriteRenderer>().sprite = this.GetComponent<SpriteRenderer>().sprite;
						this.GetComponent<SpriteRenderer>().sprite = tempSprite;
						this.transform.localPosition = originalPosition;
						switched = true; //Switch them ^^ and return the slots to their rightful place
					}
				}
			}

			GameObject tempItem2 = Resources.Load("Items/"+this.GetComponent<SpriteRenderer>().sprite.name) as GameObject;// the current gameobjects sprite
			if(tempItem2!=null)
			if(tempItem2.GetComponent<ItemBehaviour>().weapon == true){ //if the current item is a weapon
				foreach(GameObject slot in GameObject.FindGameObjectWithTag("GameController").GetComponent<Manager>().actionBarEntity.returnActionBarList(true)){ //Check the equip slots
					bool twoHanded2 = tempItem2.GetComponent<WeaponStats>().twoHanded; // Is the current weapon two handed?
					if(slot.GetComponent<SpriteRenderer>().bounds.Intersects(this.GetComponent<SpriteRenderer>().bounds)&&slot.name!=this.name){ //If the slot intersects an equip slot
						if(slot.GetComponent<SpriteRenderer>().sprite.name == "SlotEmpty"){ //If the slot you are moving the sprite to is empty
							if(slot == GameObject.FindGameObjectWithTag("GameController").GetComponent<Manager>().actionBarEntity.returnActionBarList(true)[0]){ //if the slot is the first equip slot
								if(twoHanded2){ //And the weapon is two handed
									if(GameObject.FindGameObjectWithTag("GameController").GetComponent<Manager>().actionBarEntity.returnActionBarList(true)[1].GetComponent<SpriteRenderer>().sprite.name == "SlotEmpty"){ //If the second equip slot is empty
										slot.GetComponent<SpriteRenderer>().sprite = this.GetComponent<SpriteRenderer>().sprite; //Set the equip slot to the current sprite
										GameObject.FindGameObjectWithTag("GameController").GetComponent<Manager>().actionBarEntity.returnActionBarList(true)[1].GetComponent<SpriteRenderer>().sprite = this.GetComponent<SpriteRenderer>().sprite; // And set the other equip slot to the current sprite as well
										this.GetComponent<SpriteRenderer>().sprite = Resources.Load("Sprites/UI/SlotEmpty",typeof(Sprite)) as Sprite; //Empty the current slot
									}
								} else {
									slot.GetComponent<SpriteRenderer>().sprite = this.GetComponent<SpriteRenderer>().sprite; //Set the equip slot sprite to the current sprite
									this.GetComponent<SpriteRenderer>().sprite = Resources.Load("Sprites/UI/SlotEmpty",typeof(Sprite)) as Sprite; //Empty the current slot
								}
								tempDurability = slot.transform.GetChild(0).GetComponent<DurabilityDisplay>().returnDurability();
								slot.transform.GetChild(0).GetComponent<DurabilityDisplay>().newDurability(this.transform.GetChild(0).GetComponent<DurabilityDisplay>().returnDurability());
								this.transform.GetChild(0).GetComponent<DurabilityDisplay>().newDurability(tempDurability);
							} else { //Moving it on to the second equip slot
								if(twoHanded2){
									if(GameObject.FindGameObjectWithTag("GameController").GetComponent<Manager>().actionBarEntity.returnActionBarList(true)[0].GetComponent<SpriteRenderer>().sprite.name == "SlotEmpty"){
										slot.GetComponent<SpriteRenderer>().sprite = this.GetComponent<SpriteRenderer>().sprite;
										GameObject.FindGameObjectWithTag("GameController").GetComponent<Manager>().actionBarEntity.returnActionBarList(true)[0].GetComponent<SpriteRenderer>().sprite = this.GetComponent<SpriteRenderer>().sprite;
										this.GetComponent<SpriteRenderer>().sprite = Resources.Load("Sprites/UI/SlotEmpty",typeof(Sprite)) as Sprite;
									}
								} else {
									slot.GetComponent<SpriteRenderer>().sprite = this.GetComponent<SpriteRenderer>().sprite;
									this.GetComponent<SpriteRenderer>().sprite = Resources.Load("Sprites/UI/SlotEmpty",typeof(Sprite)) as Sprite;
								}
								tempDurability = slot.transform.GetChild(0).GetComponent<DurabilityDisplay>().returnDurability();
								slot.transform.GetChild(0).GetComponent<DurabilityDisplay>().newDurability(this.transform.GetChild(0).GetComponent<DurabilityDisplay>().returnDurability());
								this.transform.GetChild(0).GetComponent<DurabilityDisplay>().newDurability(tempDurability);
							}
						switched = true;
						this.transform.localPosition = originalPosition;
						break;
						} else { //The slot isn't empty
							GameObject tempItem = Resources.Load("Items/"+slot.GetComponent<SpriteRenderer>().sprite.name) as GameObject; // the sprite of the slot you are transferring to
							bool twoHanded = tempItem.GetComponent<WeaponStats>().twoHanded; //Is the item of the slot being transferred to two handed?

							if(slot.GetComponent<SpriteRenderer>().bounds.Intersects(this.GetComponent<SpriteRenderer>().bounds)&&slot.name!=this.name){
								tempSprite = slot.GetComponent<SpriteRenderer>().sprite;
								if(twoHanded&&slot == GameObject.FindGameObjectWithTag("GameController").GetComponent<Manager>().actionBarEntity.returnActionBarList(true)[0]){
									GameObject.FindGameObjectWithTag("GameController").GetComponent<Manager>().actionBarEntity.returnActionBarList(true)[0].GetComponent<SpriteRenderer>().sprite = this.GetComponent<SpriteRenderer>().sprite;
									if(twoHanded2){
										GameObject.FindGameObjectWithTag("GameController").GetComponent<Manager>().actionBarEntity.returnActionBarList(true)[1].GetComponent<SpriteRenderer>().sprite = this.GetComponent<SpriteRenderer>().sprite;
									} else {
										GameObject.FindGameObjectWithTag("GameController").GetComponent<Manager>().actionBarEntity.returnActionBarList(true)[1].GetComponent<SpriteRenderer>().sprite = Resources.Load("Sprites/UI/SlotEmpty") as Sprite;
									}


								} else if (twoHanded&&slot == GameObject.FindGameObjectWithTag("GameController").GetComponent<Manager>().actionBarEntity.returnActionBarList(true)[1]){
									GameObject.FindGameObjectWithTag("GameController").GetComponent<Manager>().actionBarEntity.returnActionBarList(true)[1].GetComponent<SpriteRenderer>().sprite = this.GetComponent<SpriteRenderer>().sprite;
									if(twoHanded2){
										GameObject.FindGameObjectWithTag("GameController").GetComponent<Manager>().actionBarEntity.returnActionBarList(true)[0].GetComponent<SpriteRenderer>().sprite = this.GetComponent<SpriteRenderer>().sprite;
									} else {
										GameObject.FindGameObjectWithTag("GameController").GetComponent<Manager>().actionBarEntity.returnActionBarList(true)[0].GetComponent<SpriteRenderer>().sprite = Resources.Load("Sprites/UI/SlotEmpty") as Sprite;
									}
								}
								tempDurability = slot.transform.GetChild(0).GetComponent<DurabilityDisplay>().returnDurability();
								slot.transform.GetChild(0).GetComponent<DurabilityDisplay>().newDurability(this.transform.GetChild(0).GetComponent<DurabilityDisplay>().returnDurability());
								this.transform.GetChild(0).GetComponent<DurabilityDisplay>().newDurability(tempDurability);
								slot.GetComponent<SpriteRenderer>().sprite = this.GetComponent<SpriteRenderer>().sprite;
								this.GetComponent<SpriteRenderer>().sprite = tempSprite;
								this.transform.localPosition = originalPosition;
								switched = true;
								break;
							}
						}
					}
				}
			}
			if(!switched){
				if(Vector3.Distance(this.transform.localPosition,originalPosition)>0.3f){
					this.transform.localPosition = originalPosition;
					GameObject slotObject = null;
					if(name.Contains("Equip")){
						slotObject = GameObject.FindGameObjectWithTag("GameController").GetComponent<Manager>().actionBarEntity.addItemToBar(Resources.Load("Items/" + this.GetComponent<SpriteRenderer>().sprite.name) as GameObject);
							droppedItem = Resources.Load("Items/" + this.GetComponent<SpriteRenderer>().sprite.name) as GameObject;
							if(droppedItem.GetComponent<WeaponStats>().twoHanded == true){
							GameObject.FindGameObjectWithTag("GameController").GetComponent<Manager>().actionBarEntity.returnActionBarList(true)[1].GetComponent<SpriteRenderer>().sprite = Resources.Load("Sprites/UI/SlotEmpty") as Sprite;
							}
							if(droppedItem.GetComponent<ItemBehaviour>().weapon == true){
								slotObject.transform.GetChild(0).GetComponent<DurabilityDisplay>().newDurability(this.transform.GetChild(0).GetComponent<DurabilityDisplay>().returnDurability());
							}
						this.GetComponent<SpriteRenderer>().sprite = Resources.Load("Sprites/UI/SlotEmpty",typeof(Sprite)) as Sprite;
					} else {
						if(this.GetComponent<SpriteRenderer>().sprite.name != "SlotEmpty"){
							droppedItem = GameObject.Instantiate(Resources.Load("Items/" + this.GetComponent<SpriteRenderer>().sprite.name),GameObject.FindGameObjectWithTag("Player").transform.position, this.transform.rotation) as GameObject;
							droppedItem.name = this.GetComponent<SpriteRenderer>().sprite.name;
							if(droppedItem.GetComponent<ItemBehaviour>().weapon == true){
								droppedItem.GetComponent<WeaponStats>().durability = (int)(this.transform.GetChild(0).GetComponent<DurabilityDisplay>().returnDurability().x);
							}
							this.GetComponent<SpriteRenderer>().sprite = Resources.Load("Sprites/UI/SlotEmpty",typeof(Sprite)) as Sprite;
						}
					}
				} else {
					this.transform.localPosition = originalPosition;
				}
			}
		} else {
			this.transform.localPosition = originalPosition;
		}
	}
}
