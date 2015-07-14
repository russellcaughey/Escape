using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {
	
	protected bool attacking;
	protected bool caughtPlayer = false;

	public bool PlayerCaught{
		get { return caughtPlayer; }
	}

	public virtual void Attack(){
		attacking = true;
	}

	public virtual bool CatchPlayer(){
		return false;
	}
}
