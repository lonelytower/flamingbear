using UnityEngine;
using System.Collections;

public class SlotBehaviour : MonoBehaviour {

	Vector3 originalPosition;

	// Use this for initialization
	void Start () {
		originalPosition = this.transform.localPosition;
	}
	
	// Update is called once per frame
	void Update () {

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
		GameObject droppedItem;
		bool switched = false;
		if(this.GetComponent<SpriteRenderer>().sprite != null){
			foreach(GameObject slot in GameObject.FindGameObjectWithTag("GameController").GetComponent<Manager>().actionBarEntity.returnActionBarList(false)){
				if(slot.renderer.bounds.Intersects(this.renderer.bounds)&&slot.name!=this.name){
					tempSprite = slot.GetComponent<SpriteRenderer>().sprite;
					slot.GetComponent<SpriteRenderer>().sprite = this.GetComponent<SpriteRenderer>().sprite;
					this.GetComponent<SpriteRenderer>().sprite = tempSprite;
					this.transform.localPosition = originalPosition;
					switched = true;
				}
			}
			foreach(GameObject slot in GameObject.FindGameObjectWithTag("GameController").GetComponent<Manager>().actionBarEntity.returnActionBarList(true)){
				GameObject tempItem2 = Resources.Load("Items/"+this.GetComponent<SpriteRenderer>().sprite.name) as GameObject;// the current gameobjects sprite
				bool twoHanded2 = tempItem2.GetComponent<WeaponStats>().twoHanded;
				if(slot.renderer.bounds.Intersects(this.renderer.bounds)&&slot.name!=this.name){
					if(slot.GetComponent<SpriteRenderer>().sprite == null){
						if(slot == GameObject.FindGameObjectWithTag("GameController").GetComponent<Manager>().actionBarEntity.returnActionBarList(true)[0]){
							if(twoHanded2){
								if(GameObject.FindGameObjectWithTag("GameController").GetComponent<Manager>().actionBarEntity.returnActionBarList(true)[1].GetComponent<SpriteRenderer>().sprite == null){
									slot.GetComponent<SpriteRenderer>().sprite = this.GetComponent<SpriteRenderer>().sprite;
									GameObject.FindGameObjectWithTag("GameController").GetComponent<Manager>().actionBarEntity.returnActionBarList(true)[1].GetComponent<SpriteRenderer>().sprite = this.GetComponent<SpriteRenderer>().sprite;
									this.GetComponent<SpriteRenderer>().sprite = null;
								}
							} else {
								slot.GetComponent<SpriteRenderer>().sprite = this.GetComponent<SpriteRenderer>().sprite;
								this.GetComponent<SpriteRenderer>().sprite = null;
							}
						} else {
							if(twoHanded2){
								if(GameObject.FindGameObjectWithTag("GameController").GetComponent<Manager>().actionBarEntity.returnActionBarList(true)[0].GetComponent<SpriteRenderer>().sprite == null){
									slot.GetComponent<SpriteRenderer>().sprite = this.GetComponent<SpriteRenderer>().sprite;
									GameObject.FindGameObjectWithTag("GameController").GetComponent<Manager>().actionBarEntity.returnActionBarList(true)[0].GetComponent<SpriteRenderer>().sprite = this.GetComponent<SpriteRenderer>().sprite;
									this.GetComponent<SpriteRenderer>().sprite = null;
								}
							} else {
								slot.GetComponent<SpriteRenderer>().sprite = this.GetComponent<SpriteRenderer>().sprite;
								this.GetComponent<SpriteRenderer>().sprite = null;
							}
						}
					switched = true;
					this.transform.localPosition = originalPosition;
					break;
					} else {
						GameObject tempItem = Resources.Load("Items/"+slot.GetComponent<SpriteRenderer>().sprite.name) as GameObject; // the sprite of the slot you are transferring to
						bool twoHanded = tempItem.GetComponent<WeaponStats>().twoHanded;

						if(slot.renderer.bounds.Intersects(this.renderer.bounds)&&slot.name!=this.name){
							tempSprite = slot.GetComponent<SpriteRenderer>().sprite;
							if(twoHanded&&slot == GameObject.FindGameObjectWithTag("GameController").GetComponent<Manager>().actionBarEntity.returnActionBarList(true)[0]){
								GameObject.FindGameObjectWithTag("GameController").GetComponent<Manager>().actionBarEntity.returnActionBarList(true)[0].GetComponent<SpriteRenderer>().sprite = this.GetComponent<SpriteRenderer>().sprite;
								if(twoHanded2){
									GameObject.FindGameObjectWithTag("GameController").GetComponent<Manager>().actionBarEntity.returnActionBarList(true)[1].GetComponent<SpriteRenderer>().sprite = this.GetComponent<SpriteRenderer>().sprite;
								} else {
									GameObject.FindGameObjectWithTag("GameController").GetComponent<Manager>().actionBarEntity.returnActionBarList(true)[1].GetComponent<SpriteRenderer>().sprite = null;
								}

							} else if (twoHanded&&slot == GameObject.FindGameObjectWithTag("GameController").GetComponent<Manager>().actionBarEntity.returnActionBarList(true)[1]){
								GameObject.FindGameObjectWithTag("GameController").GetComponent<Manager>().actionBarEntity.returnActionBarList(true)[1].GetComponent<SpriteRenderer>().sprite = this.GetComponent<SpriteRenderer>().sprite;
								if(twoHanded2){
									GameObject.FindGameObjectWithTag("GameController").GetComponent<Manager>().actionBarEntity.returnActionBarList(true)[0].GetComponent<SpriteRenderer>().sprite = this.GetComponent<SpriteRenderer>().sprite;
								} else {
									GameObject.FindGameObjectWithTag("GameController").GetComponent<Manager>().actionBarEntity.returnActionBarList(true)[0].GetComponent<SpriteRenderer>().sprite = null;
								}
							}
							slot.GetComponent<SpriteRenderer>().sprite = this.GetComponent<SpriteRenderer>().sprite;
							this.GetComponent<SpriteRenderer>().sprite = tempSprite;
							this.transform.localPosition = originalPosition;
							switched = true;
							break;
						}
					}
				}
			}
			if(!switched){
				if(Vector3.Distance(this.transform.localPosition,originalPosition)>0.3f){
					this.transform.localPosition = originalPosition;
					if(name.Contains("Equip")){
						if(GameObject.FindGameObjectWithTag("GameController").GetComponent<Manager>().actionBarEntity.addItemToBar(Resources.Load("Items/" + this.GetComponent<SpriteRenderer>().sprite.name) as GameObject)==true){
							droppedItem = Resources.Load("Items/" + this.GetComponent<SpriteRenderer>().sprite.name) as GameObject;
							if(droppedItem.GetComponent<WeaponStats>().twoHanded == true){
								GameObject.FindGameObjectWithTag("GameController").GetComponent<Manager>().actionBarEntity.returnActionBarList(true)[1].GetComponent<SpriteRenderer>().sprite = null;
							}
						} else {
							droppedItem = GameObject.Instantiate(Resources.Load("Items/" + this.GetComponent<SpriteRenderer>().sprite.name),GameObject.FindGameObjectWithTag("Player").transform.position, this.transform.rotation) as GameObject;
							droppedItem.name = this.GetComponent<SpriteRenderer>().sprite.name;
							this.GetComponent<SpriteRenderer>().sprite = null;
						}
						this.GetComponent<SpriteRenderer>().sprite = null;
					} else {
						if(this.GetComponent<SpriteRenderer>().sprite!=null){
							droppedItem = GameObject.Instantiate(Resources.Load("Items/" + this.GetComponent<SpriteRenderer>().sprite.name),GameObject.FindGameObjectWithTag("Player").transform.position, this.transform.rotation) as GameObject;
							droppedItem.name = this.GetComponent<SpriteRenderer>().sprite.name;
							this.GetComponent<SpriteRenderer>().sprite = null;
						}
					}
				} else {
					this.transform.localPosition = originalPosition;
				}
			}
		}
	}
}
