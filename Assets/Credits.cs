using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Credits : MonoBehaviour {

	public string[] messages;
	public float timeBetweenMessages = 3f;

	private Text currentMessage;
	private int counter;
	private float timer;

	void Awake(){
		currentMessage = GetComponent<Text>();
	}
		
	void Start(){
		timer = timeBetweenMessages;
		currentMessage.text = messages[0];
	}

	void Update(){
		if(timer >= 0){
			timer -= Time.deltaTime;
		}

		if(timer <= 0 && counter+1 < messages.Length){
			DisplayNextMessage();
		}
	}

	void DisplayNextMessage(){
		counter++;
		currentMessage.text = messages[counter];
		timer = timeBetweenMessages;
	}
}
