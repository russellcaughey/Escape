using UnityEngine;
using UnityEngine.Audio;
using System.Collections;

public class AudioTrigger : MonoBehaviour {
	
	public AudioSource audioToTrigger;
	public bool playOnce;
	public float delayBetweenPlays = 10f;

	private bool hasPlayed;
	private float timer;

	void Start(){

	}

	void OnTriggerEnter(){
		if(playOnce){
			if(!hasPlayed){
				audioToTrigger.Play();
				hasPlayed = true;
			}
		} else {
			if(!hasPlayed){
				audioToTrigger.Play();
				hasPlayed = true;
			}
		}
	}

	void Update(){
		// If audio can be played multiple times, start timer countdown
		if(!playOnce){
			if(hasPlayed){
				timer -= Time.deltaTime;
			}

			if (timer <= 0){
				timer = delayBetweenPlays;
				hasPlayed = false;
			}
		}
	}
}
