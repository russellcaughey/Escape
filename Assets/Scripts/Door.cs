using UnityEngine;
using System.Collections;

public class Door : MonoBehaviour {

	public bool isOpen = false;
	public bool isLocked = false;
	public float openSpeed = 0.2f;
	public float closeSpeed = 1;
	public float openSmooth = 1;
	public AudioClip openAudio;
	public AudioClip closeAudio;
	public AudioClip slamAudio;

	private Animator anim;
	private AudioSource audioSource;

	void Awake(){
		anim = GetComponent<Animator>();
		audioSource = GetComponent<AudioSource>();
	}

	void Start(){
		if(isOpen){
			anim.SetBool("Open", true);
		}else{
			anim.SetBool("Open", false);
		}
	}

//	float timer = 3f;
	
	void Update () {

	}

	// Open door
	public void Open(){
		if(!isLocked && !anim.GetBool("Open")){
			anim.SetBool("Open", true);
			if(openAudio){
				audioSource.clip = openAudio;
				audioSource.Play();
			}
		}
	}

	// Close door
	public void Close(){
		if(anim.GetBool("Open")){
			anim.SetBool("Open", false);
			if(closeAudio){
				audioSource.clip = slamAudio;
				audioSource.Play();
			}
		}
	}

	// Play door audio
	// Handled in Animator for precision timing
	public void PlaySlam(){
		if(slamAudio){
			audioSource.clip = slamAudio;
			audioSource.Play();
		}
	}
}
