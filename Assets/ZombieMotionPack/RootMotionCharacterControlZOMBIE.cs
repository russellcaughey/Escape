		
using UnityEngine;
using System.Collections;


[AddComponentMenu("Mixamo/Demo/Root Motion Character")]
public class RootMotionCharacterControlZOMBIE: MonoBehaviour
{
	public float turningSpeed = 90f;
	public RootMotionComputer computer;
	public CharacterController character;
	
	void Start()
	{
		// validate component references
		if (computer == null) computer = GetComponent(typeof(RootMotionComputer)) as RootMotionComputer;
		if (character == null) character = GetComponent(typeof(CharacterController)) as CharacterController;
		
		// tell the computer to just output values but not apply motion
		computer.applyMotion = false;
		// tell the computer that this script will manage its execution
		computer.isManagedExternally = true;
		// since we are using a character controller, we only want the z translation output
		computer.computationMode = RootMotionComputationMode.ZTranslation;
		// initialize the computer
		computer.Initialize();
		
		// set up properties for the animations
		GetComponent<Animation>()["idle"].layer = 0; GetComponent<Animation>()["idle"].wrapMode = WrapMode.Loop;
		GetComponent<Animation>()["walk01"].layer = 1; GetComponent<Animation>()["walk01"].wrapMode = WrapMode.Loop;
		GetComponent<Animation>()["run"].layer = 1; GetComponent<Animation>()["run"].wrapMode = WrapMode.Loop;
		GetComponent<Animation>()["attack"].layer = 3; GetComponent<Animation>()["attack"].wrapMode = WrapMode.Once;
		GetComponent<Animation>()["headbutt"].layer = 3; GetComponent<Animation>()["headbutt"].wrapMode = WrapMode.Once;
		GetComponent<Animation>()["scratchidle"].layer = 3; GetComponent<Animation>()["scratchidle"].wrapMode = WrapMode.Once;
		GetComponent<Animation>()["walk02"].layer = 3; GetComponent<Animation>()["walk02"].wrapMode = WrapMode.Once;
		GetComponent<Animation>()["standup"].layer = 3; GetComponent<Animation>()["standup"].wrapMode = WrapMode.Once;
		
		GetComponent<Animation>().Play("idle");
		
	}
	
	void Update()
	{
		float targetMovementWeight = 0f;
		float throttle = 0f;
		
		// turning keys
		if (Input.GetKey(KeyCode.A)) transform.Rotate(Vector3.down, turningSpeed*Time.deltaTime);
		if (Input.GetKey(KeyCode.D)) transform.Rotate(Vector3.up, turningSpeed*Time.deltaTime);
		
		// forward movement keys
		// ensure that the locomotion animations always blend from idle to moving at the beginning of their cycles
		if (Input.GetKeyDown(KeyCode.W) && 
			(GetComponent<Animation>()["walk01"].weight == 0f || GetComponent<Animation>()["run"].weight == 0f))
		{
			GetComponent<Animation>()["walk01"].normalizedTime = 0f;
			GetComponent<Animation>()["run"].normalizedTime = 0f;
		}
		if (Input.GetKey(KeyCode.W))
		{
			targetMovementWeight = 1f;
		}
		if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)) throttle = 1f;
				
		// blend in the movement

		GetComponent<Animation>().Blend("run", targetMovementWeight*throttle, 0.5f);
		GetComponent<Animation>().Blend("walk01", targetMovementWeight*(1f-throttle), 0.5f);
		// synchronize timing of the footsteps
		GetComponent<Animation>().SyncLayer(1);
		
		// all the other animations, such as punch, kick, attach, reaction, etc. go here
		if (Input.GetKeyDown(KeyCode.Alpha1)) GetComponent<Animation>().CrossFade("attack", 0.2f);
		if (Input.GetKeyDown(KeyCode.Alpha2)) GetComponent<Animation>().CrossFade("headbutt", 0.2f);
		if (Input.GetKeyDown(KeyCode.Alpha3)) GetComponent<Animation>().CrossFade("scratchidle", 0.2f);
		if (Input.GetKeyDown(KeyCode.Alpha4)) GetComponent<Animation>().CrossFade("walk02", 0.2f);
		if (Input.GetKeyDown(KeyCode.Alpha5)) GetComponent<Animation>().CrossFade("standup", 0.2f);
		//if (Input.GetKeyDown(KeyCode.Alpha6)) animation.CrossFade("spinningkick", 0.2f);
		//if (Input.GetKeyDown(KeyCode.Alpha7)) animation.CrossFade("swordslash01", 0.2f);
		//if (Input.GetKeyDown(KeyCode.Alpha8)) animation.CrossFade("swordslash02", 0.2f);

	}
	
	void LateUpdate()
	{
		computer.ComputeRootMotion();
		
		// move the character using the computer's output
		character.SimpleMove(transform.TransformDirection(computer.deltaPosition)/Time.deltaTime);
	}
}