using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour {

	public float speed;
	public bool moveable = true;
	public int direction = 0; //1 = up 2 = down 3 = left 4 = right
	float initialDelay = 0.1f;
	float delay = 0.1f;
	float hori;
	float vert;
	
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		hori = Input.GetAxis("Horizontal");
		vert = Input.GetAxis("Vertical");
		if(moveable){
			if(((hori>=0&&hori<1)||(hori<=0&&hori>-1))&&((vert>=0&&vert<1)||(vert<=0&&vert>-1))){
				rigidbody2D.velocity = Vector2.zero;
				delay = initialDelay;
			}
			else if(hori==1&&(vert!=1&&vert!=-1)){
				if(delay <= 0){
					rigidbody2D.AddForce(new Vector2(speed,0));
				} else {
					delay-=Time.deltaTime;
				}
				direction = 4;
				this.GetComponent<Animator>().Play("WalkRight");
			}
			else if(hori==-1&&(vert!=1&&vert!=-1)){
				if(delay <= 0){
					rigidbody2D.AddForce(new Vector2(-speed,0));
				} else {
					delay-=Time.deltaTime;
				}
				direction = 3;
				this.GetComponent<Animator>().Play("WalkLeft");
			}
			else if(hori==1&&vert==1){
				if(delay <= 0){
					rigidbody2D.AddForce(new Vector2(speed/2,speed/2));
				} else {
					delay-=Time.deltaTime;
				}
				direction = 4;
				this.GetComponent<Animator>().Play("WalkRight");
			}
			else if(hori==1&&vert==-1){
				if(delay <= 0){
					rigidbody2D.AddForce(new Vector2(speed/2,-speed/2));
				} else {
					delay-=Time.deltaTime;
				}
				direction = 4;
				this.GetComponent<Animator>().Play("WalkRight");
			}
			else if(hori==-1&&vert==1){
				if(delay <= 0){
					rigidbody2D.AddForce(new Vector2(-speed/2,speed/2));
				} else {
					delay-=Time.deltaTime;
				}
				direction = 3;
				this.GetComponent<Animator>().Play("WalkLeft");
			}
			else if(hori==-1&&vert==-1){
				if(delay <= 0){
					rigidbody2D.AddForce(new Vector2(-speed/2,-speed/2));
				} else {
					delay-=Time.deltaTime;
				}
				direction = 3;
				this.GetComponent<Animator>().Play("WalkLeft");
			}
			else if(vert==1&&(hori!=1&&hori!=-1)){
				if(delay <= 0){
					rigidbody2D.AddForce(new Vector2(0,speed));
				} else {
					delay-=Time.deltaTime;
				}
				direction = 1;
				this.GetComponent<Animator>().Play("WalkUp");
			}
			else if(vert==-1&&(hori!=1&&hori!=-1)){
				if(delay <= 0){
					
					rigidbody2D.AddForce(new Vector2 (0,-speed));
				} else {
					delay-=Time.deltaTime;
				}
				direction = 2;
				this.GetComponent<Animator>().Play("WalkDown");
			}
		}
//			if((Input.GetKey(KeyCode.A)||Input.GetKey(KeyCode.LeftArrow))&&(Input.GetKey(KeyCode.S)||Input.GetKey(KeyCode.DownArrow))){
//				this.transform.Translate(transform.InverseTransformDirection(new Vector3(-1,-1,0)*speed));
//				this.GetComponent<Animator>().Play("WalkLeft");
//				direction = 3;
//			} 
//			else if( (Input.GetKey(KeyCode.A)||Input.GetKey(KeyCode.LeftArrow)) && (Input.GetKey(KeyCode.W)||Input.GetKey(KeyCode.UpArrow)) ){
//				this.transform.Translate(transform.InverseTransformDirection(new Vector3(-1,1,0)*speed));
//				this.GetComponent<Animator>().Play("WalkLeft");
//				direction = 3;
//			}
//			else if((Input.GetKey(KeyCode.D)||Input.GetKey(KeyCode.RightArrow))&&(Input.GetKey(KeyCode.S)||Input.GetKey(KeyCode.DownArrow))){
//				this.transform.Translate(transform.InverseTransformDirection(new Vector3(1,-1,0)*speed));
//				this.GetComponent<Animator>().Play("WalkRight");
//				direction = 4;
//			} 
//			else if((Input.GetKey(KeyCode.D)||Input.GetKey(KeyCode.RightArrow))&&(Input.GetKey(KeyCode.W)||Input.GetKey(KeyCode.UpArrow))){
//				this.transform.Translate(transform.InverseTransformDirection(new Vector3(1,1,0)*speed));
//				this.GetComponent<Animator>().Play("WalkRight");
//				direction = 4;
//			}
//			else if(Input.GetKey(KeyCode.A)||Input.GetKey(KeyCode.LeftArrow)){ //Left
//				//Need collision checking in here, pre check preferably but we need other things for that
//				this.transform.Translate(transform.InverseTransformDirection(new Vector3(-1,0,0)*speed));
//				this.GetComponent<Animator>().Play("WalkLeft");
//				direction = 3;
//			}
//			else if(Input.GetKey(KeyCode.D)||Input.GetKey(KeyCode.RightArrow)){ //Right
//				this.transform.Translate(transform.InverseTransformDirection(new Vector3(1,0,0)*speed));
//				this.GetComponent<Animator>().Play("WalkRight");
//				direction = 4;
//			}
//			else if(Input.GetKey(KeyCode.S)||Input.GetKey(KeyCode.DownArrow)){ //Down
//				this.transform.Translate(transform.InverseTransformDirection(new Vector3(0,-1,0)*speed));
//				this.GetComponent<Animator>().Play("WalkDown");
//				direction = 2;
//			}
//			else if(Input.GetKey(KeyCode.W)||Input.GetKey(KeyCode.UpArrow)){ //Up
//				this.transform.Translate(transform.InverseTransformDirection((new Vector3(0,1,0))*speed));
//				this.GetComponent<Animator>().Play("WalkUp");
//				direction = 1;
//			}
//		}
		//this.transform.rotation = Quaternion.Euler(new Vector3(0,0,Mathf.Atan2(Input.mousePosition.y - Camera.main.WorldToScreenPoint(this.transform.position).y,Input.mousePosition.x - Camera.main.WorldToScreenPoint(this.transform.position).x)*Mathf.Rad2Deg-90));
	}
}
