using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {
	
	
	//Inherited from weapon upon initialization
	public float velocity;
	public float lifetime;
	public int damage; 

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		this.transform.Translate(Vector3.up*velocity*Time.deltaTime);
		lifetime=lifetime-Time.deltaTime;
		if(lifetime<=0){
			GameObject.DestroyImmediate(this.gameObject); //And play expire animation once we get it.
		}
	}
}
