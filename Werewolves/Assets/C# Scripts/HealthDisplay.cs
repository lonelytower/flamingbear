using UnityEngine;
using System.Collections;

public class HealthDisplay : MonoBehaviour {

	float playerHealth;

	// Use this for initialization
	void Start () {
	}
	// Update is called once per frame
	void Update () {
		if(GameObject.FindGameObjectWithTag("Player")!=null){
			playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<Stats>().health;
		}
		if(this.name == "Health Bar Green"){
			this.transform.localScale = new Vector3 (playerHealth/100,1,0);
		}
	}
}
