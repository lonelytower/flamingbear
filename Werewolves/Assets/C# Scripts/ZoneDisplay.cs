using UnityEngine;
using System.Collections;

public class ZoneDisplay : MonoBehaviour {


	Ray mouseRay;
	RaycastHit2D mouseHit;
	Rect screenMiddle = new Rect(Screen.width/3,Screen.height/5,Screen.width/2,Screen.height/4);
	bool displayText = false;
	public string zoneName = "";
	float fadeOutTimer = 3;
	float fadeOutMax = 3;
	Color fadeColor;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Debug.Log(fadeColor.ToString());
		if(displayText==true){
			fadeOutTimer -= Time.deltaTime;
			fadeColor = Color.white;
			fadeColor.a = (fadeOutTimer/fadeOutMax)*255;
		}
		if(fadeOutTimer<=0){
			fadeOutTimer = fadeOutMax;
			displayText = false;
		}
	}

	void OnTriggerExit2D(Collider2D collider){
		displayText = true;
	}

	void OnGUI(){
		if(displayText==true){
			GUI.contentColor = fadeColor;
			GUI.Label(screenMiddle,"<Size=36>"+zoneName+"</size>");
		}
	}

}
