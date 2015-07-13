using UnityEngine;
using System.Collections;

public class Zombie : Enemy {

	public float runningSpeed = 8f;
	public float strikingDistance = 5f;
	public AudioClip runningAudio;
	public AudioClip playerCatchAudio;

	private AudioSource audioSource;
	private Animator anim;
	private Vector3 trackingObject;
	private bool caughtPlayer = false;

	void Awake(){
		anim = GetComponent<Animator>();
		audioSource = GetComponent<AudioSource>();
	}

	void Update () {
		if (attacking){
			// Distace away from target
			float distanceToTarget = Mathf.Abs(transform.position.x - trackingObject.x);

			// If target is within striking range...
			if(distanceToTarget < strikingDistance){
				anim.SetInteger("State", EnemyStates.STRIKING);
				if(anim.GetCurrentAnimatorStateInfo(0).IsName("Striking"))
					anim.SetInteger("PreviousState", EnemyStates.STRIKING);
//				if(!caughtPlayer) CatchPlayer();


			// Otherwise keep running at target
			} else{
				if(anim.GetInteger("State") != EnemyStates.RUNNING)
					anim.SetInteger("State", EnemyStates.RUNNING);
					if(anim.GetCurrentAnimatorStateInfo(0).IsName("Running"))
						anim.SetInteger("PreviousState", EnemyStates.RUNNING);
			}

			// Track target
			trackingObject = TrackPlayer();

			// Turn towards the object that is being tracked
			gameObject.transform.LookAt(trackingObject);

			// Run at target
			Run ();

		// If not attacking set state to idle
		} else{
			if(anim.GetInteger("State") != EnemyStates.IDLE){
				anim.SetInteger("State", EnemyStates.IDLE);
				if(anim.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
					anim.SetInteger("PreviousState", EnemyStates.IDLE);
			}
		}

	}

	// Track player
	Vector3 TrackPlayer(){
		Transform player = GameObject.FindGameObjectWithTag("Player").transform;
		return new Vector3(player.position.x, this.transform.position.y, player.position.z);
	}

	// Run
	void Run(){
		transform.Translate(Vector3.forward * Time.deltaTime * runningSpeed);
		if(!caughtPlayer){
			if(audioSource.clip != runningAudio){
				Debug.Log("Running audio");
				audioSource.clip = runningAudio;
				audioSource.Play();
			}
		}
	}

	// Attack
	public override void Attack(){
		attacking = true;
	}

	void CatchPlayer(){
		audioSource.clip = playerCatchAudio;
		audioSource.Play();
		caughtPlayer = true;
	}

	void OnCollisionEnter(Collision other){
		if(other.transform.tag == "Player"){
			if(!caughtPlayer) CatchPlayer();
		}
	}
}
