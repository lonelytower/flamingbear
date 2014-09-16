using UnityEngine;
using System.Collections;

public class Nametags : MonoBehaviour {

	Rect aboveHead;
	Vector3 screenPointPosition;

	public string nameDisplayed;
	Ray mouseRay;
	RaycastHit2D mouseHit;
	public bool enabled;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		screenPointPosition = Camera.main.WorldToScreenPoint(this.transform.position);
		aboveHead  = new Rect(screenPointPosition.x-32,screenPointPosition.y-64,150,15);
		mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
		mouseHit = Physics2D.GetRayIntersection(mouseRay,Mathf.Infinity);
		if(mouseHit.collider!=null){
			if(mouseHit.collider.gameObject == this.gameObject){
				enabled = true;
			} else {
				enabled = false;
			}
		}
	}

	void OnGUI(){
		if(enabled==true){
			GUI.Label(new Rect(screenPointPosition.x-42,Screen.height-screenPointPosition.y-40,150,30),nameDisplayed);
		}

	}
	void tagFunction(int id){
		GUILayout.Label(nameDisplayed);
	}
}
