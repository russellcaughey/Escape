using UnityEngine;
using System.Collections;

public class SFXController : MonoBehaviour {

	void Awake(){
		DontDestroyOnLoad(transform.gameObject);
	}
}
