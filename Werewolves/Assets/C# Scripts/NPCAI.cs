using UnityEngine;
using System.Collections;

public class NPCAI : MonoBehaviour {

	public float itemDelay = 600;
	float delay = 600;
	public string NPCName;
	public string dialogue;
	public bool merchant = false;
	int scrapCount = 0;

	// Use this for initialization
	void Start () {
		GameObject.FindGameObjectWithTag("GameController").GetComponent<Manager>().NPCS.Add(this.gameObject);
		delay = 0;
	}
	
	// Update is called once per frame
	void Update () {
		if(delay>0){
			delay-=Time.deltaTime;
		}
	}

	void OnMouseUpAsButton(){
		if(merchant){
			GameObject scrapSlot = null;
			foreach(GameObject slot in GameObject.FindGameObjectWithTag("GameController").GetComponent<Manager>().actionBarEntity.returnActionBarList(false)){
				if(slot.GetComponent<SpriteRenderer>().sprite.name.Contains("Scrap")){
					scrapSlot = slot;
				}
			}
			if(scrapSlot != null){
				scrapCount += scrapSlot.GetComponent<SlotBehaviour>().itemQuantity;
				scrapSlot.GetComponent<SpriteRenderer>().sprite = null;
				scrapSlot.GetComponent<SlotBehaviour>().itemQuantity = 0;
			}
			if(scrapCount>=20){
				dialogue = "Hey, I've gathered enough scrap to make you a sword! Here you go, take good care of it.";
				GameObject swordObject;
				swordObject = Resources.Load("Items/Sword") as GameObject;
				if(GameObject.FindGameObjectWithTag("GameController").GetComponent<Manager>().actionBarEntity.addItemToBar(swordObject) == true){

				} else {
					GameObject.Instantiate(swordObject,GameObject.FindGameObjectWithTag("Player").transform.position,Quaternion.identity);
				}
			} else {
				dialogue = "You need " + (20-scrapCount).ToString() + " more scrap metal in order for me to make you a sword. Keep it coming!";
			}
			GameObject.FindGameObjectWithTag("MainCamera").GetComponent<UI>().dialogueOpen = true;
			GameObject.FindGameObjectWithTag("MainCamera").GetComponent<UI>().speakerName = NPCName;
		} else {
			GameObject.FindGameObjectWithTag("MainCamera").GetComponent<UI>().dialogueOpen = true;
			GameObject.FindGameObjectWithTag("MainCamera").GetComponent<UI>().speakerName = NPCName;
			GameObject.FindGameObjectWithTag("MainCamera").GetComponent<UI>().dialogue = dialogue;
		}
	}
}
