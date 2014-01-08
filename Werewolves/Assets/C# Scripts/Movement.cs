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
			this.transform.Translate(transform.InverseTransformDirection(new Vector3(-1,0,0)*speed));
		}
		else if(Input.GetAxis("Horizontal")>0){ //Right
			this.transform.Translate(transform.InverseTransformDirection(new Vector3(1,0,0)*speed));
		}
		else {
			
		}
		if(Input.GetAxis("Vertical")<0){ //Down
			this.transform.Translate(transform.InverseTransformDirection(new Vector3(0,-1,0)*speed));
		}
		else if(Input.GetAxis("Vertical")>0){ //Up
			this.transform.Translate(transform.InverseTransformDirection((new Vector3(0,1,0))*speed));
		}
		else {
			
		}
		
		this.transform.rotation = Quaternion.Euler(new Vector3(0,0,Mathf.Atan2(Input.mousePosition.y - Camera.main.WorldToScreenPoint(this.transform.position).y,Input.mousePosition.x - Camera.main.WorldToScreenPoint(this.transform.position).x)*Mathf.Rad2Deg-90));
	}
}
