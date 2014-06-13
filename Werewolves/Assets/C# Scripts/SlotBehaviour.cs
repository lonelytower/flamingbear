using UnityEngine;
using System.Collections;

public class SlotBehaviour : MonoBehaviour {

	Vector3 originalPosition;

	// Use this for initialization
	void Start () {
		originalPosition = this.transform.localPosition;
		Debug.Log(originalPosition.ToString());
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
		Debug.Log("Occured");
		GameObject droppedItem;
		Debug.Log(Vector3.Distance(this.transform.localPosition,originalPosition).ToString());
		if(Vector3.Distance(this.transform.localPosition,originalPosition)>1){
			this.transform.localPosition = originalPosition;
			droppedItem = GameObject.Instantiate(Resources.Load("Items/" + this.GetComponent<SpriteRenderer>().sprite.name),GameObject.FindGameObjectWithTag("Player").transform.position, this.transform.rotation) as GameObject;
			droppedItem.name = this.GetComponent<SpriteRenderer>().sprite.name;
			this.GetComponent<SpriteRenderer>().sprite = null;
		} else {
			this.transform.localPosition = originalPosition;
		}
	}
}
