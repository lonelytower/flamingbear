using UnityEngine;
using System.Collections;

public class MerchantAI : MonoBehaviour {

	// Use this for initialization
	void Start () {
		GameObject.FindGameObjectWithTag("GameController").GetComponent<Manager>().NPCS.Add(this.gameObject);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnMouseUpAsButton(){
		GameObject maceObject;
		maceObject = Resources.Load("Items/Mace") as GameObject;
		if(GameObject.FindGameObjectWithTag("GameController").GetComponent<Manager>().actionBarEntity.addItemToBar(maceObject) == true){

		} else {
			GameObject.Instantiate(maceObject,GameObject.FindGameObjectWithTag("Player").transform.position,Quaternion.identity);
		}
	}
}
