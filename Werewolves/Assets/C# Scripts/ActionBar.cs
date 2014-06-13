using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class ActionBar : MonoBehaviour {

	List<GameObject> actionBarSlots = new List<GameObject>();
	// Use this for initialization
	void Start () {
		for(int i = 1; i<=4; i++){
			actionBarSlots.Add(GameObject.Find("Slot" + i.ToString()));
		}
	}
	
	// Update is called once per frame
	void Update () {
//		if(Input.GetKeyDown(KeyCode.O)){
//		}
	}


	void OnMouseDown(){
		if(name.Contains("Slot")){
			if(this.GetComponent<SpriteRenderer>().sprite.name.Contains("wolfsbane")){
				this.GetComponent<SpriteRenderer>().color = Color.red;
			}
		}
	}

	public void addItemToBar(GameObject itemToAdd){
		Debug.Log(itemToAdd.name.ToString());
		foreach(GameObject slot in actionBarSlots){
			if(slot.GetComponent<SpriteRenderer>().sprite !=null){
				if(slot.GetComponent<SpriteRenderer>().sprite.name==itemToAdd.name){
					//increase count if stackable item, else repair/replace?
				}
			} else if (slot.GetComponent<SpriteRenderer>().sprite ==null){
				slot.GetComponent<SpriteRenderer>().sprite = Resources.Load("Sprites/Equipment/" + itemToAdd.name, typeof(Sprite)) as Sprite;
				break;
			}
		}
	}
}
