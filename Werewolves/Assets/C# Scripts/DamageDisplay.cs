using UnityEngine;
using System.Collections;

public class DamageDisplay : MonoBehaviour {

	float lifetime = 0.5f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		this.transform.position = new Vector3(this.transform.position.x,this.transform.position.y+0.025f,this.transform.position.z);
		lifetime -= Time.deltaTime;
		if(lifetime<=0){
			DestroyImmediate(this.gameObject);
		}
	}
}
