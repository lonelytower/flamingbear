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

	public bool moveable = true;
	public float walkspeed;
	public float attackDelay;
	private float untilLastAttack = 0f;
	private GameObject targetPlayer;
	private AIState state = new AIState();

	float AttackRange = 0.8f;

	// Use this for initialization
	void Start () {
		state = AIState.Idle;
		targetPlayer = GameObject.FindGameObjectWithTag ("Player");
	}

	private void WalkTowards(GameObject entity)
	{
		if(moveable){
			Vector3 direction = new Vector3(0,0,0);
			direction = entity.transform.position - this.transform.position;
			if(Mathf.Abs(direction.x)>Mathf.Abs(direction.y)){
				switch(direction.x>0){
				case(true):
					this.GetComponent<Animator>().Play("WalkRight");
					break;
				case(false):
					this.GetComponent<Animator>().Play("WalkLeft");
					break;
				default:
					break;
				}
			} else {
				switch(direction.y>0){
				case(true):
					this.GetComponent<Animator>().Play("WalkUp");
					break;
				case(false):
					this.GetComponent<Animator>().Play("WalkDown");
					break;
				default:
					break;
				}
			}
			if(Vector3.Distance(this.transform.position,entity.transform.position)>AttackRange){
				this.transform.position = Vector3.MoveTowards (this.transform.position, entity.transform.position, walkspeed * Time.deltaTime);
			}
			//this.rigidbody2D.velocity = new Vector3(direction.x,direction.y,direction.z);
		}
        // When we have a floor, I'll make it follow the floor. For now, the sky is the limit.


        //if (!this.animation.IsPlaying("walking"))
        //{
        //    this.animation.Play("walking");
        //}

        // Let's pretend we have a walking animation or something. Probably won't use animations if we're using sprites anyway.
        
	}

	private float GetDistanceFromEntity(GameObject entity)
	{
		if(entity!=null){
			return Vector3.Distance (this.transform.position, entity.transform.position);
		}
		return 0.0f;
	}

	IEnumerator AttackEntity(GameObject entity, float delay)
	{
		int direction = 0; //1 = up 2 = down 3 = left 4 = right
		AnimatorStateInfo currentAnim;
		currentAnim = this.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0);
		if(currentAnim.IsName("WalkUp") == true){
			direction = 1;
			this.GetComponent<Animator>().Play("AttackUp");
		} else if (currentAnim.IsName("WalkDown") == true){
			direction = 2;
			this.GetComponent<Animator>().Play("AttackDown");
			
		} else if (currentAnim.IsName("WalkLeft") == true){
			direction = 3;
			this.GetComponent<Animator>().Play("AttackLeft");
			
		} else if (currentAnim.IsName("WalkRight") == true){
			direction = 4;
			this.GetComponent<Animator>().Play("AttackRight");
		}
		moveable = false;
		entity.GetComponent<Stats>().health -= this.GetComponent<Stats>().damage;
		if (entity.GetComponent<Movement> () != null) {
						entity.GetComponent<Movement> ().TakeDamage (this.transform.position);
				}
		yield return new WaitForSeconds(delay);
		moveable = true;
		switch(direction){
		case(1):
			this.GetComponent<Animator>().Play("WalkUp");
			break;
		case(2):
			this.GetComponent<Animator>().Play("WalkDown");
			break;
		case(3):
			this.GetComponent<Animator>().Play("WalkLeft");
			break;
		case(4):
			this.GetComponent<Animator>().Play("WalkRight");
			break;
		default:
			break;
		}
	}
	// Update is called once per frame
	void Update () {
		untilLastAttack -= Time.deltaTime;

		if (GetDistanceFromEntity(targetPlayer) < AttackRange)
		{
            if (untilLastAttack <= 0)
			{
				StartCoroutine(AttackEntity(targetPlayer,this.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length));
				untilLastAttack = attackDelay;
			}
		}
		else 
		{
			WalkTowards (targetPlayer);
		}
	}

	public void setTarget(GameObject newTarget){
		targetPlayer = newTarget;
	}
}
