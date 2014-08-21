using UnityEngine;
using System.Collections;

public class UI : MonoBehaviour {

	Rect WeaponWindow = new Rect(Screen.width/6-Screen.width/12,Screen.height - Screen.height/4,Screen.width/6,Screen.width/8);
	Rect ItemWindow = new Rect(Screen.width-Screen.width/3,Screen.height - Screen.height/4,Screen.width/6,Screen.width/8);
	Rect DialogueWindow = new Rect(Screen.width*0.3f, Screen.height*0.7f,Screen.width*0.7f,Screen.height*0.3f);
	Rect ToolTipWindow = new Rect(Screen.width*0.0f, Screen.height*0.8f,Screen.width*0.3f,Screen.height*0.2f);
	public bool dialogueOpen = false;
	public bool toolTipAppear = false;
	public string speakerName;
	public string dialogue;
	public string itemName;
	public string itemDescription;
	Ray mouseRay;
	RaycastHit2D mouseHit;
	int itemDurability;
	// Use this for initialization
	void Start () {


	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Space)){
			dialogueOpen = false;
		}
		mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
		mouseHit = Physics2D.GetRayIntersection(mouseRay,Mathf.Infinity);
		if(mouseHit.collider!=null){
			if(mouseHit.collider.tag == "Pickups"){
				itemName = mouseHit.collider.name;
				if(mouseHit.collider.GetComponent<WeaponStats>()!=null){
					itemDurability = mouseHit.collider.GetComponent<WeaponStats>().durability;
				}
				toolTipAppear = true;
			} else if(mouseHit.collider.tag == "UISlots" ){
				if(mouseHit.collider.GetComponent<SpriteRenderer>().sprite!=null){
					itemName = mouseHit.collider.GetComponent<SpriteRenderer>().sprite.name;
					itemDurability =(int)mouseHit.collider.transform.GetChild(0).GetComponent<DurabilityDisplay>().returnDurability().x;
					toolTipAppear = true;
				}
			} else {
				toolTipAppear = false;
			}
		} else {
			toolTipAppear = false;
		}
	}

	void OnGUI (){
		if(dialogueOpen == false){
		//	Time.timeScale = 1;
		} else {
			DialogueWindow = GUILayout.Window(1,DialogueWindow, DialogueFunction,"<size=12>Press Space to continue</size>");
		//	Time.timeScale = 0;
		}
		if(toolTipAppear== false){

		} else {
			ToolTipWindow = GUILayout.Window(2,ToolTipWindow, ToolTipFunction, "<b>"+itemName+"</b>");
		}
	}

	void DialogueFunction(int id){
		GUILayout.BeginHorizontal();
		GUILayout.BeginVertical();
		GUILayout.Label("<b><size=20>"+speakerName+"</size></b>");
		GUILayout.Label("<size=16>"+dialogue+"</size>");

		GUILayout.EndVertical();
		GUILayout.EndHorizontal();
	}

	void ToolTipFunction(int id){
		GUILayout.BeginHorizontal();
	switch(itemName){
		case("Sword"):
			GUILayout.Label("Two-Handed Melee\nDamage : 6\nDurability : "+itemDurability.ToString()+ "/80");
			break;
		case("Mace"):
			GUILayout.Label("One-Handed Melee\nDamage : 4\nDurability : "+itemDurability.ToString()+ "/50");
			break;
		case("Rifle"):
			GUILayout.Label("Two-Handed Ranged\nDamage : 6\nDurability : "+itemDurability.ToString()+ "/45");
			break;
		case("Wolfsbane"):
			GUILayout.Label("Cleanses the infection");
			break;
		default:
		break;
	}
		GUILayout.EndHorizontal();
	}
}
