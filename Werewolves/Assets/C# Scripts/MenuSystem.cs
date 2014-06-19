using UnityEngine;
using System.Collections;

public class MenuSystem : MonoBehaviour {
		
	Rect menuWindow = new Rect(0,Screen.height/2,Screen.width,Screen.height/2);
	Rect pauseWindow = new Rect(Screen.width/3,Screen.height/2.5f,Screen.width/2,Screen.height/0.5f);
	int currentMenu = 0; //0 = menu, 1 = options, 2 = pause, 3 = death, 4 = none

	// Use this for initialization
	void Start () {
		DontDestroyOnLoad(this.gameObject);
	}
	
	// Update is called once per frame
	void Update () {
		if(Application.loadedLevelName == "GameScene"){
			this.transform.parent = GameObject.FindGameObjectWithTag("MainCamera").transform;
			this.transform.localPosition = Vector3.zero;
		}
		if(Input.GetKeyDown(KeyCode.Escape)&&(currentMenu!=2||currentMenu!=3)){
			Time.timeScale = 0;
			currentMenu = 2;
		}
	}

	void OnGUI () {
		if(currentMenu==0){
			if(Application.loadedLevelName == "GameScene"){
				pauseWindow = GUILayout.Window(1,pauseWindow,menuFunctionGame,Resources.Load("Menu Textures/Options Window") as Texture,"");
			} else {
				menuWindow = GUILayout.Window(0,menuWindow,menuFunction,Resources.Load("Menu Textures/Menu Window") as Texture,"");
			}
		} else if (currentMenu==1){
			if(Application.loadedLevelName == "GameScene"){
				pauseWindow = GUILayout.Window(1,pauseWindow,optionsFunctionGame,Resources.Load("Menu Textures/Options Window") as Texture,"");
			} else {
				menuWindow = GUILayout.Window(1,menuWindow,optionsFunction,Resources.Load("Menu Textures/Options Window") as Texture,"");
			}
		} else if (currentMenu==2){
			pauseWindow = GUILayout.Window(2,pauseWindow,pauseFunction,Resources.Load("Menu Textures/Pause Window") as Texture,"");
		} else if (currentMenu==3){
			pauseWindow = GUILayout.Window(3,pauseWindow,deathFunction,Resources.Load("Menu Textures/Death Window") as Texture,"");
		}
	}

	void menuFunction ( int id ) {
		if(GUILayout.Button(Resources.Load("Menu Textures/Begin")  as Texture,"")){
			currentMenu = 4;
			Application.LoadLevel("GameScene");
		}
		if(GUILayout.Button(Resources.Load("Menu Textures/Options") as Texture ,"")){
			currentMenu = 1;
		}
		if(GUILayout.Button(Resources.Load("Menu Textures/Quit") as Texture,"")){
			Application.Quit();
		}
	}
	void menuFunctionGame ( int id ) {
		if(GUILayout.Button(Resources.Load("Menu Textures/Continue")  as Texture,"")){
			currentMenu = 4;
			Time.timeScale = 1;
		}
		if(GUILayout.Button(Resources.Load("Menu Textures/Options") as Texture ,"")){
			currentMenu = 1;
		}
		if(GUILayout.Button(Resources.Load("Menu Textures/Quit") as Texture,"")){
			Application.Quit();
		}
	}
	void optionsFunction ( int id ) {
		if(GUILayout.Button(Resources.Load("Menu Textures/Controls") as Texture,"")){
		}
		if(GUILayout.Button(Resources.Load("Menu Textures/Audio") as Texture,"")){
		}
		if(GUILayout.Button(Resources.Load("Menu Textures/Menu") as Texture,"")){
			currentMenu = 0;
		}
	}
	void optionsFunctionGame ( int id ) {
		if(GUILayout.Button(Resources.Load("Menu Textures/Controls") as Texture,"")){
		}
		if(GUILayout.Button(Resources.Load("Menu Textures/Audio") as Texture,"")){
		}
		if(GUILayout.Button(Resources.Load("Menu Textures/Continue") as Texture,"")){
			currentMenu = 4;
			Time.timeScale = 1;
		}
	}
	void pauseFunction ( int id ) {
		if(GUILayout.Button(Resources.Load("Menu Textures/Continue") as Texture,"")){
			currentMenu = 4;
			Time.timeScale = 1;
		}
		if(GUILayout.Button(Resources.Load("Menu Textures/Options") as Texture,"")){
			currentMenu = 1;
		}
		if(GUILayout.Button(Resources.Load("Menu Textures/Menu") as Texture,"")){
			currentMenu = 0;
		}
	}
	void deathFunction ( int id ) {
		if(GUILayout.Button(Resources.Load("Menu Textures/Continue") as Texture,"")){
			Application.LoadLevel("GameScene");
		}
		if(GUILayout.Button(Resources.Load("Menu Textures/Menu") as Texture,"")){
			currentMenu = 0;
		}
		if(GUILayout.Button(Resources.Load("Menu Textures/Quit") as Texture,"")){
			Application.Quit();
		}
	}
}
