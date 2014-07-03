using UnityEngine;
using System.Collections;

public class HealthDisplay : MonoBehaviour {

	float playerHealth;
	bool playerCursed;
	float curseDelay = 15;
	public GameObject cursedBar;
	public GameObject healthBar;

	// Use this for initialization
	void Start () {
	}
	// Update is called once per frame
	void Update () {
		if(GameObject.FindGameObjectWithTag("Player")!=null){
			playerCursed = GameObject.FindGameObjectWithTag("Player").GetComponent<Stats>().cursed;
			playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<Stats>().health;
		}
		if(playerCursed){
			curseDelay -= Time.deltaTime;
			if(curseDelay <=0){
				curseDelay = 15;
				GameObject.FindGameObjectWithTag("Player").GetComponent<Stats>().health -= 10;
			}
			if(cursedBar.activeSelf==false){
				cursedBar.SetActive(true);
				healthBar.SetActive(false);
			}
		} else {
			if(cursedBar.activeSelf==true){
				cursedBar.SetActive(false);
				healthBar.SetActive(true);
			}
		}
		if(this.name.Contains("HealthBar")||this.name.Contains("CurseBar")){
			this.transform.localScale = new Vector3 (playerHealth/100,1,0);
		}
	}
}
