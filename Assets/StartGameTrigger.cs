using UnityEngine;
using System.Collections;

public class StartGameTrigger : MonoBehaviour {
	
	public string startLevel;
	public Door startDoor;

	void OnTriggerEnter(){
		StartCoroutine("StartGame");
	}

	IEnumerator StartGame(){
		startDoor.Close();
		yield return new WaitForSeconds(1);
		Application.LoadLevel(startLevel);
	}
}
