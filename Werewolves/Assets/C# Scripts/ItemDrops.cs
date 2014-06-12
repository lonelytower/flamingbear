using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class ItemDrops : MonoBehaviour {
	//knife 25 sword 20 trap 30 wolfsbane 40 revolver 10 shield 15 Mace 5 Musket 5 
	List<string> dropTable = new List<string>();
	List<float> dropTablePct = new List<float>();
	Breakable breakingScript;
	public int id = 0; //0 = item crate, 1 = weapon crate, 2 = chest, 3=dead bodies(npc), 4 = dead bodies(player)

	// Use this for initialization
	void Start () {
		breakingScript = this.GetComponent<Breakable>();
		dropTable.Add("Nothing");
		switch(id){
		case(0):
			dropTable.Add("Trap");
			dropTable.Add("Wolfsbane");
			dropTablePct.Add(30);
			dropTablePct.Add(30);
			dropTablePct.Add(40);
			break;
		case(1):
			dropTable.Add("Knife");
			dropTable.Add("Sword");
			dropTable.Add("Revolver");
			dropTable.Add("Shield");
			dropTable.Add("Mace");
			dropTable.Add("Musket");
			dropTablePct.Add(20);
			dropTablePct.Add(25);
			dropTablePct.Add(100);
			dropTablePct.Add(10);
			dropTablePct.Add(15);
			dropTablePct.Add(5);
			dropTablePct.Add(5);
			break;
		case(2):
			break;
		default:
			break;
		}

	
	}

	public void triggerDrop(){
		GameObject newDrop;
		for(int i = 1; i < dropTable.Count; i++){
			float value = Random.Range(0,100);
			if(value > (100-dropTablePct[i])){
				if(Resources.Load("Items/"+ dropTable[i])!=null){
					newDrop = GameObject.Instantiate(Resources.Load("Items/"+ dropTable[i]),this.transform.position,this.transform.rotation) as GameObject;
					newDrop.name = dropTable[i];
				}
				}
		}
//		float value = Random.Range(0,100);
//		Debug.Log(value.ToString());
//		switch(id){
//		case(0):
//			if(value>=30&&value<=60){
//				Debug.Log("Fire");
//				GameObject.Instantiate(Resources.Load("Items/Trap"),this.transform.position,this.transform.rotation);
//			}
//			if(value>60){
//				GameObject.Instantiate(Resources.Load("Items/Wolfsbane"),this.transform.position,this.transform.rotation);
//			}
//			break;
//		default:
//			Debug.Log("Default");
//			break;
//		}
	}
	// Update is called once per frame
	void Update () {
	
	}
}
