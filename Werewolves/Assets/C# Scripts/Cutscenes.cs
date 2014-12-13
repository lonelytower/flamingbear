using UnityEngine;
using System.Collections;
using System.Collections.Generic;


[System.Serializable]
public class cutsceneControls{
	public Vector3 newPosition;
	public float moveSpeed;
	public string animationName;
	public bool loopAnimation;
}

[System.Serializable]
public class cutsceneControl{
	public List<cutsceneControls> stepActions = new List<cutsceneControls>();
	public bool followPlayer;
	public bool changeCameraPosition;
	public Vector3 newCameraLocation;
	public bool pan;
	public float panSpeed;
	public bool autoPlay;
	public float autoPlayDelay;
	[HideInInspector]
	public bool timerSet;
}

public class Cutscenes : MonoBehaviour {

	GameObject mainCamera;
	public bool triggered = false;
	bool lastFrameTriggered;
	int numberOfLines;
	public GUISkin centerAlignSkin;
	//public GUIStyle centerAlignStyle;
	public int currentLine = 0;
	float timer;
	
	public List<string> linesOfDialogue = new List<string>();
	public List<string> lineSpeakers = new List<string>();


	
	public List<cutsceneControl> cutsceneSequence = new List<cutsceneControl>();
	public List<GameObject> affectedGameObjects = new List<GameObject>();

	// Use this for initialization
	void Start () {
		mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
	}
	
	// Update is called once per frame
	void Update () {
		if(triggered){
			lineActions(currentLine);
			timer -= Time.deltaTime;
			if(Input.GetKeyDown(KeyCode.Space)||timer<=0){
				if(currentLine<linesOfDialogue.Count-1){
				currentLine+=1;
				} else {
					triggered = false;
					currentLine = 0;
				}
				timer = Mathf.Infinity;
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
				currentLine =0;
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
				foreach(GameObject pickup in GameObject.FindGameObjectsWithTag("Pickups")){
					pickup.collider2D.enabled = false;
				}
				StartCoroutine(letterboxFade(0.25f));
				mainCamera.GetComponent<CameraFollow>().follow = false;
				GameObject.FindGameObjectWithTag("Player").GetComponent<Movement>().enabled = false;
				GameObject.FindGameObjectWithTag("Player").GetComponent<WeaponSystem>().enabled = false;
				GameObject.FindGameObjectWithTag("MainCamera").GetComponent<UI>().enabled = false;
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
				foreach(GameObject pickup in GameObject.FindGameObjectsWithTag("Pickups")){
					pickup.collider2D.enabled = true;
				}
				StartCoroutine(letterboxFade(0.25f));
				mainCamera.GetComponent<CameraFollow>().follow = true;
				GameObject.FindGameObjectWithTag("Player").GetComponent<Movement>().enabled = true;
				GameObject.FindGameObjectWithTag("Player").GetComponent<WeaponSystem>().enabled = true;
				GameObject.FindGameObjectWithTag("MainCamera").GetComponent<UI>().enabled = true;
			}
		}
		lastFrameTriggered = triggered;
	}

	void OnGUI (){
		centerAlignSkin.label.alignment = TextAnchor.UpperCenter;
		if(triggered){
			GUI.Label(new Rect(0,Screen.height/1.1f,Screen.width,Screen.height/5),"<size=16>" + lineSpeakers[currentLine] + ":  " +  linesOfDialogue[currentLine] + "</size>",centerAlignSkin.GetStyle("Label"));
			GUI.Label(new Rect(0,0,Screen.width,Screen.height/5),"<size=8> Press Space to continue or Esc to skip</size>",centerAlignSkin.GetStyle("Label"));
		}
	}

	void lineActions(int lineIndex){
		if(cutsceneSequence[lineIndex].autoPlay == true&&cutsceneSequence[lineIndex].timerSet == false){
			timer = cutsceneSequence[lineIndex].autoPlayDelay;
			cutsceneSequence[lineIndex].timerSet = true;
		}
		if(cutsceneSequence[lineIndex].followPlayer== false && cutsceneSequence[lineIndex].changeCameraPosition == true){
			mainCamera.GetComponent<CameraFollow>().follow = false;
			if(cutsceneSequence[lineIndex].pan == true){
				mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position,cutsceneSequence[lineIndex].newCameraLocation,(1/(cutsceneSequence[lineIndex].newCameraLocation - mainCamera.transform.position).magnitude)*cutsceneSequence[lineIndex].panSpeed);
			} else {
				mainCamera.transform.position = cutsceneSequence[lineIndex].newCameraLocation;
			}
		} else if(cutsceneSequence[lineIndex].followPlayer == true){
			mainCamera.GetComponent<CameraFollow>().follow = true;
		}
		for(int i = 0; i<cutsceneSequence[lineIndex].stepActions.Count; i++){
			if(affectedGameObjects[i].transform.position!=cutsceneSequence[lineIndex].stepActions[i].newPosition){
				affectedGameObjects[i].transform.position = Vector3.Lerp(affectedGameObjects[i].transform.position,cutsceneSequence[lineIndex].stepActions[i].newPosition,(1/(cutsceneSequence[lineIndex].stepActions[i].newPosition - affectedGameObjects[i].transform.position).magnitude)*cutsceneSequence[lineIndex].stepActions[i].moveSpeed);
			}
			affectedGameObjects[i].GetComponent<Animator>().Play(cutsceneSequence[lineIndex].stepActions[i].animationName);
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
