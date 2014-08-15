using UnityEngine;
using System.Collections;

public class StaminaDisplay : MonoBehaviour {

	float playerStamina;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		if(GameObject.FindGameObjectWithTag("Player")!=null){
			playerStamina = GameObject.FindGameObjectWithTag("Player").GetComponent<Stats>().stamina;
			if(this.name.Contains("StaminaBar")){
				this.transform.localScale = new Vector3 (playerStamina/100,1,0);
			}
		}
	}
}
