using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour {

	public float speed;
	public bool moveable = true;
	public int direction = 0; //1 = up 2 = down 3 = left 4 = right
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(moveable){
			if((Input.GetKey(KeyCode.A)||Input.GetKey(KeyCode.LeftArrow))&&(Input.GetKey(KeyCode.S)||Input.GetKey(KeyCode.DownArrow))){
				this.transform.Translate(transform.InverseTransformDirection(new Vector3(-1,-1,0)*speed));
				this.GetComponent<Animator>().Play("WalkLeft");
				direction = 3;
			} 
			else if( (Input.GetKey(KeyCode.A)||Input.GetKey(KeyCode.LeftArrow)) && (Input.GetKey(KeyCode.W)||Input.GetKey(KeyCode.UpArrow)) ){
				this.transform.Translate(transform.InverseTransformDirection(new Vector3(-1,1,0)*speed));
				this.GetComponent<Animator>().Play("WalkLeft");
				direction = 3;
			}
			else if((Input.GetKey(KeyCode.D)||Input.GetKey(KeyCode.RightArrow))&&(Input.GetKey(KeyCode.S)||Input.GetKey(KeyCode.DownArrow))){
				this.transform.Translate(transform.InverseTransformDirection(new Vector3(1,-1,0)*speed));
				this.GetComponent<Animator>().Play("WalkRight");
				direction = 4;
			} 
			else if((Input.GetKey(KeyCode.D)||Input.GetKey(KeyCode.RightArrow))&&(Input.GetKey(KeyCode.W)||Input.GetKey(KeyCode.UpArrow))){
				this.transform.Translate(transform.InverseTransformDirection(new Vector3(1,1,0)*speed));
				this.GetComponent<Animator>().Play("WalkRight");
				direction = 4;
			}
			else if(Input.GetKey(KeyCode.A)||Input.GetKey(KeyCode.LeftArrow)){ //Left
				//Need collision checking in here, pre check preferably but we need other things for that
				this.transform.Translate(transform.InverseTransformDirection(new Vector3(-1,0,0)*speed));
				this.GetComponent<Animator>().Play("WalkLeft");
				direction = 3;
			}
			else if(Input.GetKey(KeyCode.D)||Input.GetKey(KeyCode.RightArrow)){ //Right
				this.transform.Translate(transform.InverseTransformDirection(new Vector3(1,0,0)*speed));
				this.GetComponent<Animator>().Play("WalkRight");
				direction = 4;
			}
			else if(Input.GetKey(KeyCode.S)||Input.GetKey(KeyCode.DownArrow)){ //Down
				this.transform.Translate(transform.InverseTransformDirection(new Vector3(0,-1,0)*speed));
				this.GetComponent<Animator>().Play("WalkDown");
				direction = 2;
			}
			else if(Input.GetKey(KeyCode.W)||Input.GetKey(KeyCode.UpArrow)){ //Up
				this.transform.Translate(transform.InverseTransformDirection((new Vector3(0,1,0))*speed));
				this.GetComponent<Animator>().Play("WalkUp");
				direction = 1;
			}
		}
		//this.transform.rotation = Quaternion.Euler(new Vector3(0,0,Mathf.Atan2(Input.mousePosition.y - Camera.main.WorldToScreenPoint(this.transform.position).y,Input.mousePosition.x - Camera.main.WorldToScreenPoint(this.transform.position).x)*Mathf.Rad2Deg-90));
	}
}
