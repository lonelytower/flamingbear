using UnityEngine;
using System.Collections;

public class WeaponStats : MonoBehaviour {

	public float delay;
	public int damage;
	public int damageType; //0 = sharp, 1 = blunt
	public bool ranged;
	public bool twoHanded;
	public float projectileVelocity;
	public int maxDurability;
	public int durability;

	// Use this for initialization
	void Start () {
		durability = maxDurability;
	}
	
	// Update is called once per frame
	void Update () {
		if(durability<=0){
			this.GetComponent<ItemBehaviour>().quantity -= 1;
			durability = maxDurability;
		}
	
	}
}
