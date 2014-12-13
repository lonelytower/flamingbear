using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {
	GameObject player;
	public bool follow = true;

	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () {
		if(player!=null&&follow==true){
			this.transform.position = new Vector3(player.transform.position.x,player.transform.position.y,player.transform.position.z-1);
		}
	
	}
	public void Reassign(){
		player = GameObject.FindGameObjectWithTag("Player");
	}
}
