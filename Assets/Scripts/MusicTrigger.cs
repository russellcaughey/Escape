using UnityEngine;
using System.Collections;

public class MusicTrigger : MonoBehaviour {

	public AudioClip music;

	private AudioSource musicSource;

	void Awake(){
		musicSource = GameObject.FindGameObjectWithTag("MusicController").GetComponent<AudioSource>();
	}

	void OnTriggerEnter(){
		musicSource.clip = music;
		musicSource.Play ();
	}
}
