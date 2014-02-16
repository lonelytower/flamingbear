using UnityEngine;
using System.Collections;

public class HealthDisplay : MonoBehaviour {

	public Sprite HeartMax;
	public Sprite HeartSecond;
	public Sprite HeartThird;
	public Sprite HeartEmpty;

	GameObject Heart1;
	GameObject Heart2;
	GameObject Heart3;

	float playerHealth;

	// Use this for initialization
	void Start () {
		Heart1 = GameObject.Find("Heart1");
		Heart2 = GameObject.Find("Heart2");
		Heart3 = GameObject.Find("Heart3");
	}
	// Update is called once per frame
	void Update () {
		playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<Stats>().health;
		if(playerHealth>88){
			Heart1.GetComponent<SpriteRenderer>().sprite = HeartMax;
		} else if(playerHealth>77){
			Heart1.GetComponent<SpriteRenderer>().sprite = HeartSecond;
		} else if(playerHealth>66){
			Heart1.GetComponent<SpriteRenderer>().sprite = HeartThird;
		} else if(playerHealth>55){
			Heart1.GetComponent<SpriteRenderer>().sprite = HeartEmpty;
			Heart2.GetComponent<SpriteRenderer>().sprite = HeartMax;
		} else if(playerHealth>44){
			Heart2.GetComponent<SpriteRenderer>().sprite = HeartSecond;
		} else if(playerHealth>33){
			Heart2.GetComponent<SpriteRenderer>().sprite = HeartThird;
		} else if(playerHealth>22){
			Heart2.GetComponent<SpriteRenderer>().sprite = HeartEmpty;
			Heart3.GetComponent<SpriteRenderer>().sprite = HeartMax;
		} else if(playerHealth>11){
			Heart3.GetComponent<SpriteRenderer>().sprite = HeartSecond;
		} else if(playerHealth>0){
			Heart3.GetComponent<SpriteRenderer>().sprite = HeartThird;
		} else if(playerHealth<=0||playerHealth==null){
			Heart3.GetComponent<SpriteRenderer>().sprite = HeartEmpty;
		}
	}
}
