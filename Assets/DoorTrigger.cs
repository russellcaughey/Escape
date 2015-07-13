using UnityEngine;
using System.Collections;

public class DoorTrigger : MonoBehaviour {

	public Door door;

	private int triggerCounter = 0;

	void OnTriggerEnter(){
		triggerCounter++;

		if(!door.isOpen && triggerCounter >= 2){
			door.Open();
		}

		if(door.isOpen){
			door.Close();
		}
	}
}
