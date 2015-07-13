using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {
	
	protected bool attacking;

	public virtual void Attack(){
		attacking = true;
	}
}
