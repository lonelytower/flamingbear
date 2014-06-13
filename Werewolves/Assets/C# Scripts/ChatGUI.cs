using UnityEngine;
using System.Collections;

public class ChatGUI : MonoBehaviour {
	Rect chatWindow= new Rect(400, 0, Screen.width - 400, Screen.height);
	Rect optionsWindow= new Rect(0,0,400,Screen.height/2);
	Rect serverWindow= new Rect(0,Screen.height/2,400,Screen.height/2);
	public GUISkin necroSkin; 
	Vector2 scrollPosition;

	string inputField= "";
	string username = "Player";
	string serverIP="";
	int serverPort= 0;

	ArrayList entries  = new ArrayList();
void  OnGUI (){
		GUI.skin = necroSkin;
		if(chatWindow.width != Screen.width-400){
			chatWindow.width =Screen.width-400;
		}
		if(optionsWindow.width != 400){
			optionsWindow.width =400;
		}
		chatWindow = GUILayout.Window(0, chatWindow, chatFunction, "");
		optionsWindow = GUILayout.Window(1, optionsWindow, optionsFunction, "");
		serverWindow = GUILayout.Window(2, serverWindow, serverFunction, "");
}



	void  chatFunction ( int id  ){
		GUILayout.Label("Chat");
		// make the window draggable
		//GUI.DragWindow();
	
		// handle scrolling capabilities of window
		scrollPosition = GUILayout.BeginScrollView(scrollPosition);
		
		// layout the gui elements here
		// display all the elements of the list using labels
		foreach(string entry in entries)
		{
			GUILayout.Label(entry);
		}
		
		// end scrolling view
		GUILayout.EndScrollView();
		
		
		// enter text here
		inputField = GUILayout.TextField(inputField,250,GUILayout.Width(chatWindow.width-100));
		
		
		// handle keyboard input
			char myChar ='\n';
		if(Event.current.type == EventType.keyDown && Event.current.character == myChar && inputField.Length > 0)
		{
			// add keyboard input to the list
			entries.Add(username + "\n\n" + inputField);
			networkView.RPC("HandleGlobalKeyboardInput", RPCMode.Others, inputField, username);
			// reset the input field to empty
			inputField = "";
			// ensure we can see the most recent entries
			scrollPosition.y = 1000000;
		}
		
	}
	void optionsFunction ( int id) { 
		//GUI.DragWindow();
		GUILayout.Label("Chat Options\n" + username);
		username = GUILayout.TextField(username,50,GUILayout.Width(optionsWindow.width-100));
	}
	void serverFunction (int id) {
		GUILayout.Label("Server Options");
		if (Network.peerType == NetworkPeerType.Disconnected){
			GUILayout.Label("You are disconnected");
		}
		else if (Network.peerType == NetworkPeerType.Client){
			GUILayout.Label("You are connected to a server");
		}
		else if (Network.peerType == NetworkPeerType.Server){
			GUILayout.Label("You are hosting a server");
		}
		GUILayout.Label("Your IP : " + Network.player.ipAddress);
		GUILayout.Label("Your Port : " + Network.player.port);
		GUILayout.BeginHorizontal();
		GUILayout.Label("IP");
		serverIP = GUILayout.TextField(serverIP,12,GUILayout.Width(serverWindow.width-200));
		GUILayout.EndHorizontal();
		GUILayout.BeginHorizontal();
		GUILayout.Label("Port");
		serverPort = int.Parse(GUILayout.TextField(serverPort.ToString(),10,GUILayout.Width(serverWindow.width-200)));
		GUILayout.EndHorizontal();
		if (Network.peerType != NetworkPeerType.Disconnected){
			if(GUILayout.Button("Disconnect")){
				Network.Disconnect();
			}
		} else {
		if(GUILayout.Button("Connect to server")){
			Network.Connect(serverIP, serverPort);
		}
		if(GUILayout.Button("Start Server")){
			Network.InitializeServer(4, serverPort);
		}
		}
	}
	[RPC]
	void  HandleGlobalKeyboardInput ( string text, NetworkMessageInfo msg, string user){
		// add keyboard input to the list
		entries.Add(user + "\n\n" + text);
			
		// ensure we can see the most recent entries
		scrollPosition.y = 1000000;
		
	}
}