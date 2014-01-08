using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour {
	
	public float speed;
	
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetAxis("Horizontal")<0){ //Left
			//Need collision checking in here, pre check preferably but we need other things for that
			this.transform.Translate(Vector3.left*speed);
		}
		else if(Input.GetAxis("Horizontal")>0){ //Right
			this.transform.Translate(Vector3.right*speed);
		}
		else {
			
		}
		if(Input.GetAxis("Vertical")<0){ //Down
			this.transform.Translate(Vector3.down*speed);
		}
		else if(Input.GetAxis("Vertical")>0){ //Up
			this.transform.Translate(Vector3.up*speed);
		}
		else {
			
		}
		this.transform.LookAt(new Vector3(this.transform.position.x,this.transform.position.y,Input.mousePosition.x));
	}
}
