using UnityEngine;
using System.Collections;

public class ItemBehaviour : MonoBehaviour {

	public float delay = 0.5f;
	public bool weapon = false;
	public int quantity = 1;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(delay > 0){
			delay = delay - Time.deltaTime;
		}
		if(quantity<=0){
			Destroy(this.gameObject);
		}
	}
}
