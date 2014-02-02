using UnityEngine;
using System.Collections;

public class LayerHide : MonoBehaviour {

	bool hide = false;
	RaycastHit rayInfo;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
//		if(Physics.Raycast(Camera.main.ScreenPointToRay(GameObject.FindGameObjectWithTag("Player").transform.position),out rayInfo)){
//			Debug.Log("Hitting : " + rayInfo.collider.name);
//			if(rayInfo.collider.gameObject.GetComponent<SpriteRenderer>().sortingLayerName == "Foreground"){
//				hide = true;
//			} else {
//				hide = false;
//			}
//		}
		if(hide){
			foreach(GameObject item in GameObject.FindGameObjectsWithTag("Foreground")){
				
				item.renderer.material.color = new Color(1,1,1,0.4f);
			}
		} else {
			foreach(GameObject item in GameObject.FindGameObjectsWithTag("Foreground")){
				
				item.renderer.material.color = new Color(1,1,1,1);
			}
		}
	}
	void OnTriggerEnter2D(Collider2D other){
		if(other.tag == "Foreground"){
			hide = true;
		}
	}
	void OnTriggerExit2D(Collider2D other){
		if(other.tag == "Foreground"){
			hide = false;
		}
	}
}
