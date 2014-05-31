using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class SnapToGrid : MonoBehaviour {

	public float snapDistance = 3f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		float snapInverse = 1/snapDistance;
		
		float x, y, z;
		
		// if snapValue = .5, x = 1.45 -> snapInverse = 2 -> x*2 => 2.90 -> round 2.90 => 3 -> 3/2 => 1.5
		// so 1.45 to nearest .5 is 1.5
		x = Mathf.Round(transform.position.x * snapInverse)/snapInverse;
		y = Mathf.Round(transform.position.y * snapInverse)/snapInverse;
		this.transform.position = new Vector3(x,y,0);
	}
}
