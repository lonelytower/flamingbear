using UnityEngine;
using System.Collections;

public class ActionBar : MonoBehaviour {


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	}


	void OnMouseDown(){
		if(name.Contains("Slot")){
			Debug.Log(this.GetComponent<SpriteRenderer>().sprite.ToString());
			if(this.GetComponent<SpriteRenderer>().sprite.name.Contains("wolfsbane")){
				this.GetComponent<SpriteRenderer>().color = Color.red;
			}
		}
	}
	void OnMouseDrag(){
		if(name.Contains("Slot")){
			Vector3 screenMouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			Vector3 mouseOffset = screenMouse - transform.parent.position;
			this.transform.localPosition = new Vector3(mouseOffset.x,mouseOffset.y,0);
		}
	}
}
