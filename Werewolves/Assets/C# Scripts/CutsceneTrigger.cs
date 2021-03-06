﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CutsceneTrigger : MonoBehaviour {
	
	bool enabled = true;
	public List<string> newLinesOfDialogue = new List<string>();
	public List<string> newLineSpeakers = new List<string>();

	public List<cutsceneControl> newCutsceneSequence = new List<cutsceneControl>();
	public List<GameObject> newAffectedGameObjects = new List<GameObject>();

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D collider){
		if(enabled == true){
			if(collider.gameObject.tag=="Player"){
				GameObject.FindGameObjectWithTag("GameController").GetComponent<Cutscenes>().linesOfDialogue = newLinesOfDialogue;
				GameObject.FindGameObjectWithTag("GameController").GetComponent<Cutscenes>().lineSpeakers = newLineSpeakers;
				GameObject.FindGameObjectWithTag("GameController").GetComponent<Cutscenes>().cutsceneSequence = newCutsceneSequence;
				GameObject.FindGameObjectWithTag("GameController").GetComponent<Cutscenes>().affectedGameObjects = newAffectedGameObjects;
				GameObject.FindGameObjectWithTag("GameController").GetComponent<Cutscenes>().triggered = true;
				
				enabled = false;
			}
		}
	}
}
