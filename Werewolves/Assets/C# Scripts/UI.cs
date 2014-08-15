using UnityEngine;
using System.Collections;

public class UI : MonoBehaviour {

	Rect WeaponWindow = new Rect(Screen.width/6-Screen.width/12,Screen.height - Screen.height/4,Screen.width/6,Screen.width/8);
	Rect ItemWindow = new Rect(Screen.width-Screen.width/3,Screen.height - Screen.height/4,Screen.width/6,Screen.width/8);
	Rect DialogueWindow = new Rect(Screen.width*0.1f, Screen.height*0.7f,Screen.width*0.8f,Screen.height*0.3f);
	public bool dialogueOpen = false;
	public string speakerName;
	public string dialogue;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnGUI (){
		if(dialogueOpen == false){
			Time.timeScale = 1;
		} else {
			DialogueWindow = GUILayout.Window(1,DialogueWindow, DialogueFunction,"");
			Time.timeScale = 0;
		}
	}

	void DialogueFunction(int id){
		GUILayout.BeginHorizontal();
		GUILayout.BeginVertical();
		GUILayout.Label("<b><size=20>"+speakerName+"</size></b>");
		GUILayout.Label(dialogue);

		GUILayout.EndVertical();
		if(GUILayout.Button("<b><size=20>Continue</size></b>")){
			dialogueOpen = false;
		}
		GUILayout.EndHorizontal();
	}
}
