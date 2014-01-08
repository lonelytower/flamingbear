using UnityEngine;
using System.Collections;

public class EnemyAI : MonoBehaviour {

	private enum AIState {
		Tracking,
		Attacking,
		Found,
		Following,
		Running,
		Hiding,
		Idle,
	};

	public float walkspeed = 0.1f;
	public float attackDelay = 2.5f;
	private GameObject targetPlayer;
	private AIState state = new AIState();

	// Use this for initialization
	void Start () {
		state = AIState.Idle;
		targetPlayer = GameObject.FindGameObjectWithTag ("Player");
	}

	private void WalkTowards(GameObject entity)
	{
		this.transform.position = Vector3.MoveTowards (this.transform.position, entity.transform.position, walkspeed);
        // When we have a floor, I'll make it follow the floor. For now, the sky is the limit.


        //if (!this.animation.IsPlaying("walking"))
        //{
        //    this.animation.Play("walking");
        //}

        // Let's pretend we have a walking animation or something. Probably won't use animations if we're using sprites anyway.
        
	}

	private float GetDistanceFromEntity(GameObject entity)
	{
		return (this.transform.position - entity.transform.position).magnitude;
	}

	private void AttackEntity(GameObject entity)
	{
        entity.GetComponent<Stats>().health -= this.GetComponent<Stats>().damage;
	}
	
	// Update is called once per frame
	void Update () {
        
		if (GetDistanceFromEntity(targetPlayer) < 1)
		{
            if (Time.deltaTime < attackDelay)
            	AttackEntity(targetPlayer);
		}
		else 
		{
			WalkTowards (targetPlayer);
		}


	}
}
