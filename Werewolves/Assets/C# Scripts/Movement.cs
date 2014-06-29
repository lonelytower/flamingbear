using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour {

	public float speed;
	public bool moveable = true;
	public int direction = 0; //1 = up 2 = down 3 = left 4 = right
	GameObject staminaBar;
	float defaultSpeed;
	float initialDelay = 0.1f;
	float delay = 0.1f;
	float hori;
	float vert;
	float mouseX;
	float mouseY;
	float dashTime = 0.5f;
	bool dashing = false;

	float stunTime = 0;

	const float TIME_TO_STUN = 0.2f;

	Vector2 hitDirection = new Vector2(float.NaN, float.NaN);

	// Use this for initialization
	void Start () {
		defaultSpeed = speed;
		staminaBar = GameObject.Find("StaminaBar");
	}

	public void TakeDamage(Vector2 sourceposition)
	{
		moveable = false;
		Vector2 damageTaker = this.transform.position;
		hitDirection = damageTaker - sourceposition;
		hitDirection.Normalize();
		hitDirection *= 500;
		stunTime = TIME_TO_STUN;
	}

	// Update is called once per frame
	void Update () {
		if(dashing){
			speed = defaultSpeed*2;
			dashTime -= Time.deltaTime;
		}
		if(Input.GetKeyDown(KeyCode.LeftShift)&&dashing == false){
			if(GameObject.FindGameObjectWithTag("Player").GetComponent<Stats>().stamina>=33){
				GameObject.FindGameObjectWithTag("Player").GetComponent<Stats>().stamina -= 33;
				dashing = true;
			}
		} else if(dashTime<=0){
			dashing = false;
			dashTime = 0.5f;
			speed = defaultSpeed;
		}
		mouseX =  Input.mousePosition.x/Screen.width;
		mouseY =  Input.mousePosition.y/Screen.height;
		if(Input.GetAxis("Fire1")>0){
			if(mouseX+mouseY >= 1&&mouseX>mouseY&&mouseX>0.5f){
				direction = 4;
				if(this.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("WalkRight")==false&&this.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("AttackRight")==false){
					this.GetComponent<Animator> ().Play ("AttackRight");
				}
			} 
			if(mouseX+mouseY<1&&mouseX<mouseY&&mouseX<0.5f) {
				direction = 3;
				if(this.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("WalkLeft")==false&&this.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("AttackLeft")==false){
					this.GetComponent<Animator> ().Play ("AttackLeft");
				}
			}
			if(mouseX+mouseY>=1&&mouseY>mouseX&&mouseY>0.5f){
				direction = 1;
				if(this.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("WalkUp")==false&&this.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("AttackUp")==false){
					this.GetComponent<Animator> ().Play ("AttackUp");
				}
			}
			if(mouseX+mouseY<1&&mouseY<mouseX&&mouseY<0.5f){
				direction = 2;
				if(this.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("WalkDown")==false&&this.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("AttackDown")==false){
						this.GetComponent<Animator> ().Play ("AttackDown");
					}
			}
		}
//		if(this.tag=="Player"){
//			this.transform.LookAt(Input.mousePosition);
//		}
		hori = Input.GetAxis("Horizontal");
		vert = Input.GetAxis("Vertical");
		if (moveable) {
						if (((hori >= 0 && hori < 1) || (hori <= 0 && hori > -1)) && ((vert >= 0 && vert < 1) || (vert <= 0 && vert > -1))) {
								rigidbody2D.velocity = Vector2.zero;
								delay = initialDelay;
						} else if (hori == 1 && (vert != 1 && vert != -1)) {
								if (delay <= 0) {
										rigidbody2D.AddForce (new Vector2 (speed, 0));
								} else {
										delay -= Time.deltaTime;
								}
								//direction = 4;
								this.GetComponent<Animator> ().Play ("WalkRight");
						} else if (hori == -1 && (vert != 1 && vert != -1)) {
								if (delay <= 0) {
										rigidbody2D.AddForce (new Vector2 (-speed, 0));
								} else {
										delay -= Time.deltaTime;
								}
								//direction = 3;
								this.GetComponent<Animator> ().Play ("WalkLeft");
						} else if (hori == 1 && vert == 1) {
								if (delay <= 0) {
										rigidbody2D.AddForce (new Vector2 (speed / 2, speed / 2));
								} else {
										delay -= Time.deltaTime;
								}
								//direction = 4;
								this.GetComponent<Animator> ().Play ("WalkRight");
						} else if (hori == 1 && vert == -1) {
								if (delay <= 0) {
										rigidbody2D.AddForce (new Vector2 (speed / 2, -speed / 2));
								} else {
										delay -= Time.deltaTime;
								}
								//direction = 4;
								this.GetComponent<Animator> ().Play ("WalkRight");
						} else if (hori == -1 && vert == 1) {
								if (delay <= 0) {
										rigidbody2D.AddForce (new Vector2 (-speed / 2, speed / 2));
								} else {
										delay -= Time.deltaTime;
								}
								//direction = 3;
								this.GetComponent<Animator> ().Play ("WalkLeft");
						} else if (hori == -1 && vert == -1) {
								if (delay <= 0) {
										rigidbody2D.AddForce (new Vector2 (-speed / 2, -speed / 2));
								} else {
										delay -= Time.deltaTime;
								}
								//direction = 3;
								this.GetComponent<Animator> ().Play ("WalkLeft");
						} else if (vert == 1 && (hori != 1 && hori != -1)) {
								if (delay <= 0) {
										rigidbody2D.AddForce (new Vector2 (0, speed));
								} else {
										delay -= Time.deltaTime;
								}
								//direction = 1;
								this.GetComponent<Animator> ().Play ("WalkUp");
						} else if (vert == -1 && (hori != 1 && hori != -1)) {
								if (delay <= 0) {
					
										rigidbody2D.AddForce (new Vector2 (0, -speed));
								} else {
										delay -= Time.deltaTime;
								}
							//	direction = 2;
								this.GetComponent<Animator> ().Play ("WalkDown");
						}
			} else {
			// if we aren't moveable
			if (!float.IsNaN(hitDirection.x))
			{
				// and our hit direction is acceptable
				if (delay <= 0) {
					//this.GetComponent<Animator>().Play ("Hurt");
					this.GetComponent<SpriteRenderer>().color = new Color(1f, 0f, 0f);
					rigidbody2D.AddForce (new Vector2 (hitDirection.x, hitDirection.y));
					delay = initialDelay;
				} else {
					delay -= Time.deltaTime;
					stunTime -= Time.deltaTime;
				}
				if (stunTime <= 0)
				{
					moveable = true;
					this.GetComponent<SpriteRenderer>().color = new Color(1f,1f,1f);
					stunTime = TIME_TO_STUN;
					hitDirection = new Vector2(float.NaN, float.NaN);
				} // been flying for long enough.
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
