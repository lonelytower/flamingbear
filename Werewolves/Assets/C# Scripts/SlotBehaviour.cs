﻿using UnityEngine;
using System.Collections;

public class SlotBehaviour : MonoBehaviour {

	Vector3 originalPosition;
	Bounds slotBounds;

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
			this.transform.localPosition = new Vector3(mouseOffset.x,mouseOffset.y,-3.1f);
		}
	}

	void OnMouseUpAsButton(){
		Sprite tempSprite;
		GameObject droppedItem;
		bool switched = false;
		foreach(GameObject slot in GameObject.FindGameObjectWithTag("GameController").GetComponent<Manager>().actionBarEntity.returnActionBarList(false)){
			if(slot.renderer.bounds.Intersects(this.renderer.bounds)&&slot.name!=this.name){
				tempSprite = slot.GetComponent<SpriteRenderer>().sprite;
				slot.GetComponent<SpriteRenderer>().sprite = this.GetComponent<SpriteRenderer>().sprite;
				this.GetComponent<SpriteRenderer>().sprite = tempSprite;
				this.transform.localPosition = originalPosition;
				switched = true;
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
		public Bounds returnBounds(){
			return slotBounds;
		}
}
