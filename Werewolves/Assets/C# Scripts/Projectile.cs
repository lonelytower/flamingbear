using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {
	
	
	//Inherited from weapon upon initialization
	public float velocity;
	public float lifetime;
	public float damage; 

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Debug.Log(transform.rotation.eulerAngles.ToString());
		this.transform.Translate(new Vector3(0,(transform.rotation.eulerAngles.z*velocity*Time.deltaTime),0));
		lifetime=lifetime-Time.deltaTime;
		if(lifetime<=0){
			GameObject.DestroyImmediate(this.gameObject); //And play expire animation once we get it.
		}
	}
}
