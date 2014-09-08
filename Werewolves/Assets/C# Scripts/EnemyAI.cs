using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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
	public bool awake = false;
	bool engaged = false;
	public float walkSpeed;
	public float attackDelay;
	private float untilLastAttack = 0f;
	private GameObject targetPlayer;
	private AIState state = new AIState();
	Vector3 newPoint;
	float switchDirectionTimer = 2;
	List<GameObject> nearbyTargets = new List<GameObject>();
	public float currentThreatLevel;

	float AttackRange = 0.8f;

	// Use this for initialization
	void Start () {
		if(this.name.Contains("Curse")){
			this.renderer.material.color = Color.green;
		}
		state = AIState.Idle;
		newPoint = new Vector3(this.transform.position.x+Random.Range(-1f,1f),this.transform.position.y+Random.Range(-1f,1f),this.transform.position.z);
		//targetPlayer = GameObject.FindGameObjectWithTag ("Player");
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
				this.transform.position = Vector3.MoveTowards(this.transform.position, entity.transform.position, walkSpeed * Time.deltaTime);
			} else {
				this.rigidbody2D.velocity = Vector3.zero;
			}

		} else {
			this.rigidbody2D.velocity = Vector3.zero;
		}
        // When we have a floor, I'll make it follow the floor. For now, the sky is the limit.


        //if (!this.animation.IsPlaying("walking"))
        //{
        //    this.animation.Play("walking");
        //}

        // Let's pretend we have a walking animation or something. Probably won't use animations if we're using sprites anyway.
        
	}
	void WalkTowardsRandomPoint(){
		switchDirectionTimer -= Time.deltaTime;
		if(switchDirectionTimer <= 0){
			switchDirectionTimer = 1.5f;
			newPoint = new Vector3(this.transform.position.x+Random.Range(-1f,1f),this.transform.position.y+Random.Range(-1f,1f),this.transform.position.z);
		}
		this.transform.position = Vector3.MoveTowards(this.transform.position,newPoint,walkSpeed*Time.deltaTime);
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
		StartCoroutine(entity.GetComponent<Stats>().onHit(this.GetComponent<Stats>().damage,false));
		entity.GetComponent<Stats>().health -= this.GetComponent<Stats>().damage;
		this.GetComponent<Stats>().increaseEngagementDamage(this.GetComponent<Stats>().damage);
		if(this.name.Contains("Curse")){
			if(Random.Range(1,100)<60){
				entity.GetComponent<Stats>().cursed = true;
			}
		}
		 else if(Random.Range(1,100)<5){
			entity.GetComponent<Stats>().cursed = true;
		}
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
		if(targetPlayer != null){
			engaged = true;
			if(nearbyTargets.Count!=0){
				foreach(GameObject target in nearbyTargets){
					if(target.GetComponent<Stats>().returnThreat() >= currentThreatLevel+currentThreatLevel*0.1f){
						targetPlayer = target;
						currentThreatLevel = target.GetComponent<Stats>().returnThreat();
					}
				}
			}
			if (GetDistanceFromEntity(targetPlayer) < AttackRange)
			{
				this.rigidbody2D.velocity = Vector3.zero;
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
		} else {
			engaged = false;
			WalkTowardsRandomPoint();
		}
	}

	public void setTarget(GameObject newTarget){
		awake = true;
		if(!nearbyTargets.Contains(newTarget)){
			nearbyTargets.Add(newTarget);
		}
		if(nearbyTargets.Count<=1){
			targetPlayer = newTarget;
			currentThreatLevel = newTarget.GetComponent<Stats>().returnThreat();
		} else {

		}
	}
}
