using UnityEngine;
using System.Collections;

public class MerchantAI : MonoBehaviour {

	public float itemDelay = 600;
	float delay = 600;

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
		if(delay<=0){
			GameObject maceObject;
			maceObject = Resources.Load("Items/Mace") as GameObject;
			if(GameObject.FindGameObjectWithTag("GameController").GetComponent<Manager>().actionBarEntity.addItemToBar(maceObject) == true){

			} else {
				GameObject.Instantiate(maceObject,GameObject.FindGameObjectWithTag("Player").transform.position,Quaternion.identity);
			}
			delay = itemDelay;
		} else {
			GameObject.FindGameObjectWithTag("MainCamera").GetComponent<UI>().dialogueOpen = true;
			GameObject.FindGameObjectWithTag("MainCamera").GetComponent<UI>().speakerName = "Merchant Tim";
			GameObject.FindGameObjectWithTag("MainCamera").GetComponent<UI>().dialogue = "Hey, I'm out of weapons right now. Come back later!";
		}
	}
}
