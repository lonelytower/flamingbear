using UnityEngine;
using System.Collections;

public class AllyAI : MonoBehaviour {

	public float walkSpeed = 200;
	public int priority = 1; //1 = approach player, 2 = attack enemies
	public GameObject leader;
	public GameObject target;
	bool moveable = true;
	float attackDelay;
	public float startingDelay=2;

	// Use this for initialization
	void Start () {
		attackDelay = startingDelay;
		GameObject.FindGameObjectWithTag("GameController").GetComponent<Manager>().NPCS.Add(this.gameObject);
	}
	
	// Update is called once per frame
	void Update () {
		if(attackDelay > 0)
		attackDelay -= Time.deltaTime;
		switch(priority){
		case(1):
			if(leader!=null){
				if(Vector3.Distance(this.transform.position,leader.transform.position)>2){
					this.transform.position = Vector3.MoveTowards(this.transform.position, leader.transform.position, walkSpeed * Time.deltaTime);
				}
			}
			break;
		case(2):
			if(target!=null){
				if(Vector3.Distance(this.transform.position,target.transform.position)>1){
					this.transform.position = Vector3.MoveTowards(this.transform.position, target.transform.position, walkSpeed * Time.deltaTime);
				} else {
					if(attackDelay<=0){
						StartCoroutine(AttackEntity(target,this.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length));
						attackDelay = startingDelay;
					}
				}
			}
			break;
		default:
			break;
		}
	}
	void changeDirection(){
		if(moveable){
			Vector3 direction = new Vector3(0,0,0);
			if(priority == 1){
				direction = leader.transform.position - this.transform.position;
			} else if (priority == 2){
				direction = target.transform.position - this.transform.position;
			}
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
		}
	}
	public void setPriority(int newPriority){
		priority = newPriority;
	}

	IEnumerator AttackEntity(GameObject entity, float delay){
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
		//entity.GetComponent<Stats>().onHit(this.GetComponent<Stats>().damage,false);
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
}
