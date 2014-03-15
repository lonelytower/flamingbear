using UnityEngine;
using System.Collections;

public class UI : MonoBehaviour {

	Rect WeaponWindow = new Rect(Screen.width/6-Screen.width/12,Screen.height - Screen.height/4,Screen.width/6,Screen.width/8);
	Rect ItemWindow = new Rect(Screen.width-Screen.width/3,Screen.height - Screen.height/4,Screen.width/6,Screen.width/8);
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnGUI (){
		WeaponWindow = GUILayout.Window(0,WeaponWindow,UIWeaponSystem,"");
		ItemWindow = GUILayout.Window(1,ItemWindow,UIItemSystem,"");
	}
	void UIWeaponSystem(int id){
		GUILayout.BeginHorizontal();
		GUILayout.Button("Left Weapon");
		GUILayout.Button("Right Weapon");
		GUILayout.EndHorizontal();
	}
	void UIItemSystem(int id){
		GUILayout.BeginHorizontal();
		GUILayout.Button("Previous Item");
		GUILayout.Button("Current Item");
		GUILayout.Button("Next Item");
		GUILayout.EndHorizontal();
	}
}
