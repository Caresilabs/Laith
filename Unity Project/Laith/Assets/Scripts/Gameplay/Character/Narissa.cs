using UnityEngine;
using System.Collections;

public class Narissa : BasePlayerController {
	
	//NOTE: Climbing requires a trigger collider component attached.

	public float hookRange = 12f;
	public float hookSpeed = 30f;
	public float maxHookLength = 12f;
	public float hookChangeSpeed = 7f;
	public float spring = 1000f;
	
	public float maxSwingSpeed = 12f;
	public float swingAcceleration = 200f;

	public float climbSpeed = 5;

	private GameObject prefabHook = Resources.Load ("Hook") as GameObject;

	private GameObject hook;
	private Bow bow;

	public bool hooked, climbing, upgraded;

	public SpringJoint joint;

	public override void Start() {

		AirJumps = 1;

		bow = transform.FindChild ("Bow").GetComponent<Bow>();
		bow.enabled = true;
//		bow = Bow.Create (this);

		base.Start ();
	}

	public override void Update () {
		if (!dead) {
			if (Input.GetKey (KeyCode.Mouse0)) {
				bow.DrawBow();
			} else if (Input.GetKeyUp (KeyCode.Mouse0)) {
				bow.Release();
			}
			
			if (Input.GetKeyDown (KeyCode.Mouse1) && !hooked && hook == null) {
				FireHook ();
			}
			
			if (hooked) {
				HangingOnHook ();
			} else if (climbing) {
				Climbing ();
			} else if (!dead){
				Movement();
			}
			
			if (CheckForDead() && hook != null) {
				DestroyHook();
			}
			
			UpdateState ();
		}
	}

	private void FireHook(){
		hook = PhotonNetwork.Instantiate (prefabHook.name, transform.position, Quaternion.identity, 0) as GameObject;
		//hook.GetComponent<HookProjectile> ().enabled = true;
		
		HookProjectile hp = hook.GetComponent<HookProjectile> ();
		hp.shooter = gameObject;
		
		Rigidbody rb = hook.GetComponent<Rigidbody> ();
		rb.velocity = mouseDirection * hookSpeed;
		
		joint = gameObject.AddComponent<SpringJoint> ();
		joint.connectedBody = rb;
		
		joint.maxDistance = 100f;
		joint.minDistance = 0f;
		joint.spring = spring;
		joint.damper = 100f;
	}

	private void HangingOnHook(){
		Vector3 pullDirection = joint.connectedBody.transform.position - transform.position;
		pullDirection.Normalize ();

		//Extend hook and pull in hook
		if(Input.GetKey (KeyCode.S)){
			if(joint.maxDistance >= maxHookLength)
				return;
			joint.maxDistance += hookChangeSpeed * Time.deltaTime;
		}
		if(Input.GetKey (KeyCode.W)){
			if(joint.maxDistance <= 0)
				return;
			joint.maxDistance -= hookChangeSpeed * Time.deltaTime;
		}
		joint.maxDistance = Mathf.Clamp (joint.maxDistance, 0, maxHookLength);
		joint.minDistance = joint.maxDistance;

		//Swing controls
		Vector3 swingDirectionCC = Vector3.Cross (pullDirection, Vector3.forward);
		if (Input.GetKey (KeyCode.D))
			rigidbody.AddForce(swingDirectionCC * swingAcceleration * rigidbody.mass * Time.deltaTime);
		if (Input.GetKey(KeyCode.A))
			rigidbody.AddForce(-swingDirectionCC * swingAcceleration * rigidbody.mass * Time.deltaTime);
		if (rigidbody.velocity.magnitude > maxSwingSpeed) {
			Vector3 direction = rigidbody.velocity.normalized;
			rigidbody.velocity = direction * maxSwingSpeed;
		}

		if (Input.GetKeyDown ("space")) {
			DestroyHook ();
		}
		
		if (isGrounded) {
			UpdateState ();
		}
		climbing = false;
		rigidbody.useGravity = true;
	}

	private void Climbing(){
		rigidbody.velocity = Vector3.zero;

		Vector3 movePosition = Vector3.zero;
		if(Input.GetKey (KeyCode.S)){
			movePosition += new Vector3(0,-climbSpeed,0);
		}
		if(Input.GetKey (KeyCode.W)){
			movePosition += new Vector3(0,climbSpeed,0);
		}
		if(Input.GetKey (KeyCode.D)){
			movePosition += new Vector3(climbSpeed,0,0);
		}
		if(Input.GetKey (KeyCode.A)){
			movePosition += new Vector3(-climbSpeed,0,0);
		}
		rigidbody.MovePosition(transform.position + movePosition * Time.deltaTime);

		if (Input.GetKeyDown ("space")) {
			Jump ();
			climbing = false;
			rigidbody.useGravity = true;
		}
	}
	
	public void DestroyHook(){
		hooked = false;

		if (joint != null) {
			Destroy (joint);
			PhotonNetwork.Destroy (hook);
		}
	}

	void OnTriggerExit(){
		climbing = false;
		rigidbody.useGravity = true;
	}

	protected override void OnTriggerStay(Collider other){
		if (other.gameObject.tag == "Climbable") {
			if (Input.GetKey (KeyCode.W) || Input.GetKey (KeyCode.S)) {
				climbing = true;
				rigidbody.useGravity = false;
			}
		}
		base.OnTriggerStay (other);
	}

}
