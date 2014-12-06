using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Cutscenes : MonoBehaviour {

	GameObject mainCamera;
	public bool triggered = false;
	bool lastFrameTriggered;
	int numberOfLines;
	public GUIStyle centerAlignStyle;
	public int currentLine = 0;
	public List<string> linesOfDialogue = new List<string>();
	public List<string> lineSpeakers = new List<string>();

	// Use this for initialization
	void Start () {
		mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
	}
	
	// Update is called once per frame
	void Update () {
		if(triggered){
			if(Input.GetKeyDown(KeyCode.Space)){
				if(currentLine<linesOfDialogue.Count-1){
				currentLine+=1;
				} else {
					triggered = false;
					currentLine = 0;
				}
			}
			if(Input.GetKeyDown(KeyCode.Escape)){
				triggered = false;
				currentLine = 0;
			}
		}
		if(Input.GetKeyDown(KeyCode.Keypad4)){
			if(triggered == false){
				triggered = true;
			} else {
				triggered = false;
			}
		}
		numberOfLines = linesOfDialogue.Count;
		if(lastFrameTriggered != triggered){
			if(triggered == true){
				for(int i = 0; i < mainCamera.transform.childCount; i++){
					mainCamera.transform.GetChild(i).gameObject.SetActive(false);
				}
				foreach(GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy")){
					enemy.GetComponent<EnemyAI>().enabled = false;
				}
				foreach(GameObject ally in GameObject.FindGameObjectsWithTag("Ally")){
					ally.GetComponent<AllyAI>().enabled = false;
				}
				StartCoroutine(letterboxFade(0.25f));
				GameObject.FindGameObjectWithTag("Player").GetComponent<Movement>().enabled = false;
				GameObject.FindGameObjectWithTag("MainCamera").GetComponent<UI>().enabled = false;
				//mainCamera.camera.rect = new Rect(0,0.16f,1,0.66f);
			} else {
				for(int i = 0; i < mainCamera.transform.childCount; i++){
					mainCamera.transform.GetChild(i).gameObject.SetActive(true);
				}
				foreach(GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy")){
					enemy.GetComponent<EnemyAI>().enabled = true;
				}
				foreach(GameObject ally in GameObject.FindGameObjectsWithTag("Ally")){
					ally.GetComponent<AllyAI>().enabled = true;
				}
				StartCoroutine(letterboxFade(0.25f));
				GameObject.FindGameObjectWithTag("Player").GetComponent<Movement>().enabled = true;
				GameObject.FindGameObjectWithTag("MainCamera").GetComponent<UI>().enabled = true;
					//mainCamera.camera.rect = new Rect(0,0,1,1);
			}
		}
		lastFrameTriggered = triggered;
	}

	void OnGUI (){

		//centerAlignStyle = GUI.skin.GetStyle("Label");
		//centerAlignStyle.alignment = TextAnchor.UpperCenter;
		if(triggered){
			//GUI.Label(new Rect(Screen.width/12,Screen.height/1.135f,Screen.width,Screen.height/5),linesOfDialogue[currentLine]);
			GUI.Label(new Rect(0,Screen.height/1.1f,Screen.width,Screen.height/5),"<size=16>" + lineSpeakers[currentLine] + ":  " +  linesOfDialogue[currentLine] + "</size>");
			GUI.Label(new Rect(0,Screen.height/1.05f,Screen.width,Screen.height/5),"<size=8> Press Space to continue or Esc to skip</size>");
		}
	}

	IEnumerator letterboxFade(float timeToTake){
		if(triggered){
			mainCamera.camera.rect = new Rect(0,0.016f,1,0.966f);
			yield return new WaitForSeconds(timeToTake/10);
			mainCamera.camera.rect = new Rect(0,0.032f,1,0.932f);
			yield return new WaitForSeconds(timeToTake/10);
			mainCamera.camera.rect = new Rect(0,0.048f,1,0.898f);
			yield return new WaitForSeconds(timeToTake/10);
			mainCamera.camera.rect = new Rect(0,0.064f,1,0.864f);
			yield return new WaitForSeconds(timeToTake/10);
			mainCamera.camera.rect = new Rect(0,0.080f,1,0.830f);
			yield return new WaitForSeconds(timeToTake/10);
			mainCamera.camera.rect = new Rect(0,0.096f,1,0.796f);
			yield return new WaitForSeconds(timeToTake/10);
			mainCamera.camera.rect = new Rect(0,0.112f,1,0.762f);
			yield return new WaitForSeconds(timeToTake/10);
			mainCamera.camera.rect = new Rect(0,0.128f,1,0.728f);
			yield return new WaitForSeconds(timeToTake/10);
			mainCamera.camera.rect = new Rect(0,0.144f,1,0.694f);
			yield return new WaitForSeconds(timeToTake/10);
			mainCamera.camera.rect = new Rect(0,0.16f,1,0.66f);
			yield return new WaitForSeconds(timeToTake/10);
		} else {
			mainCamera.camera.rect = new Rect(0,0.144f,1,0.694f);
			yield return new WaitForSeconds(timeToTake/10);
			mainCamera.camera.rect = new Rect(0,0.128f,1,0.728f);
			yield return new WaitForSeconds(timeToTake/10);
			mainCamera.camera.rect = new Rect(0,0.112f,1,0.762f);
			yield return new WaitForSeconds(timeToTake/10);
			mainCamera.camera.rect = new Rect(0,0.096f,1,0.796f);
			yield return new WaitForSeconds(timeToTake/10);
			mainCamera.camera.rect = new Rect(0,0.080f,1,0.830f);
			yield return new WaitForSeconds(timeToTake/10);
			mainCamera.camera.rect = new Rect(0,0.064f,1,0.864f);
			yield return new WaitForSeconds(timeToTake/10);
			mainCamera.camera.rect = new Rect(0,0.048f,1,0.898f);
			yield return new WaitForSeconds(timeToTake/10);
			mainCamera.camera.rect = new Rect(0,0.032f,1,0.932f);
			yield return new WaitForSeconds(timeToTake/10);
			mainCamera.camera.rect = new Rect(0,0.016f,1,0.966f);
			yield return new WaitForSeconds(timeToTake/10);
			mainCamera.camera.rect = new Rect(0,0,1,1);
			yield return new WaitForSeconds(timeToTake/10);
		}
	}
}
