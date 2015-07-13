using UnityEngine;
using System.Collections;

public class EnemyTrigger : MonoBehaviour {
	
	public GameObject enemyToTrigger;

	private Enemy enemy;

	void Awake(){
		enemy = enemyToTrigger.GetComponentInChildren<Enemy>();
		if(enemy == null) Debug.Log("No enemy component found");
	}

	void OnTriggerEnter(){
		enemy.Attack();
	}
}
