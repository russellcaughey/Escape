using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class GameController : MonoBehaviour {

	public CanvasGroup screenFader;

	private GameObject[] enemiesGO;
	private List<Enemy> enemies = new List<Enemy>();

	void Start () {
		enemiesGO = GameObject.FindGameObjectsWithTag("Enemy");
		if (enemiesGO.Length == 0){
			Debug.LogWarning("No enemies in the scene");
		}
		else{
			foreach (GameObject go in enemiesGO){
				enemies.Add(go.GetComponent<Enemy>());
			}
		}
	}

	void Update () {
		if (enemiesGO.Length > 0){
			foreach(Enemy e in enemies){
				if(e.PlayerCaught){
					StartCoroutine("EndGame");
				}
			}
		}
	}

	IEnumerator EndGame(){
		yield return new WaitForSeconds(0.5f);
		Application.LoadLevel("EndScene");
	}
}
