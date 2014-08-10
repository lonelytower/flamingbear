using UnityEngine;
using System.Collections;

public class Detection : MonoBehaviour {

	float baseRadius = 3.0f;
	float maxRadius = 5.0f;
	CircleCollider2D circColl;

	// Use this for initialization
	void Start () {
		circColl = this.GetComponent<CircleCollider2D>();
	}
	
	// Update is called once per frame
	void Update () {
		if(circColl.radius>maxRadius){
			circColl.radius = maxRadius;
		}
		if(circColl.radius<baseRadius){
			circColl.radius = baseRadius;
		}
		if(circColl.radius>baseRadius){
			circColl.radius -= Time.deltaTime;
		}
	}

	void OnCollisionEnter2D(Collision2D collision){
		if(collision.gameObject.tag=="Enemy"){
			collision.gameObject.GetComponent<EnemyAI>().setTarget(this.transform.parent.gameObject);
		}
		if(collision.gameObject.tag == "Ally"){
			collision.gameObject.GetComponent<AllyAI>().leader = this.gameObject.transform.parent.gameObject;
			collision.gameObject.GetComponent<AllyAI>().setPriority(1);
		}
	}
	void OnCollisionExit2D(Collision2D collision){
		if(collision.gameObject.tag == "Ally"){
			collision.gameObject.GetComponent<AllyAI>().leader = this.gameObject.transform.parent.gameObject;
			collision.gameObject.GetComponent<AllyAI>().setPriority(1);
		}
	}

	void OnTriggerEnter2D(Collider2D collider){
		if(collider.gameObject.tag=="Enemy"){
			collider.gameObject.GetComponent<EnemyAI>().setTarget(this.transform.parent.gameObject);
		}
		if(collider.gameObject.tag == "Ally"&&collider.gameObject.name.Contains("Follower")){
			collider.gameObject.GetComponent<AllyAI>().leader = this.gameObject.transform.parent.gameObject;
			collider.gameObject.GetComponent<AllyAI>().setPriority(1);
		}
	}
	void OnTriggerExit2D(Collider2D collider){
		if(collider.gameObject.tag == "Ally"&&collider.gameObject.name.Contains("Follower")){
			collider.gameObject.GetComponent<AllyAI>().leader = this.gameObject.transform.parent.gameObject;
			collider.gameObject.GetComponent<AllyAI>().setPriority(1);
		}
	}

	public void increaseRadius(float amount){
		circColl.radius+= amount;
	}
}
