using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {
	
	
	//Inherited from weapon upon initialization
	public Vector2 flightDirection;
	public float velocity;
	public float lifetime;
	public int damage;
	public int damageType;
	public bool ally = true;
	public int direction = 0; //1 = up 2 = down 3 = left 4 = right Inherit from character direction

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
//		switch(direction){
//		case(1):
//			this.rigidbody2D.velocity = new Vector2(0,velocity);
//			break;
//		case(2):
//			this.rigidbody2D.velocity = new Vector2(0,-velocity);
//			break;
//		case(3):
//			this.rigidbody2D.velocity = new Vector2(-velocity,0);
//			break;
//		case(4):
//			this.rigidbody2D.velocity = new Vector2(velocity,0);
//			break;
//		default:
//			break;
//		}
		this.rigidbody2D.velocity = flightDirection.normalized*velocity;

		//this.transform.Translate(Vector3.forward*velocity*Time.deltaTime);
		lifetime=lifetime-Time.deltaTime;
		if(lifetime<=0){
			GameObject.DestroyImmediate(this.gameObject); //And play expire animation once we get it.
		}
	}
}
