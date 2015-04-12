using UnityEngine;
using System.Collections;

public class Narissa : BasePlayerController {

	public float hookRange = 12f;
	public float hookSpeed = 30f;
	public float maxHookLength = 12f;
	public float maxSwingSpeed = 12f;
	public float swingAcceleration = 200f;
	public float hookChangeSpeed = 5f;
	public float spring = 1000f;
	public float climbSpeed = 5;

	public GameObject prefabHook;
	private GameObject hook;

	public bool hooked;
	public bool climbing;

	public SpringJoint joint;

	public void Start() {
		acceleration = 25f;
		maxSpeed = 7f;
		jumpSpeed = 7f;

		MaxJumps = 2;
		prefabHook = Resources.Load ("Hook") as GameObject;
	}

	public override void Update () {
		if (Input.GetKeyDown (KeyCode.Mouse0) && !hooked && hook == null) {
			FireHook ();
		}

		if (hooked) {
			HangingOnHook ();
		} else if (climbing) {
			Climbing ();
		} else {
			UpdateInput ();
		}
	}

	void HangingOnHook(){
		Vector3 pullDirection = joint.connectedBody.transform.position - transform.position;
		pullDirection.Normalize ();

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
		
		if (Input.GetKeyDown ("space")) {
			DestroyHook ();
			//Jump ();
		}

		Vector3 swingDirectionCC = Vector3.Cross (pullDirection, Vector3.forward);
		
		if (Input.GetKey (KeyCode.D))
			rigidbody.AddForce(swingDirectionCC * swingAcceleration * rigidbody.mass * Time.deltaTime);
		
		if (Input.GetKey(KeyCode.A))
			rigidbody.AddForce(-swingDirectionCC * swingAcceleration * rigidbody.mass * Time.deltaTime);

		if (rigidbody.velocity.magnitude > maxSwingSpeed) {
			Vector3 direction = rigidbody.velocity.normalized;
			rigidbody.velocity = direction * maxSwingSpeed;
		}

		climbing = false;
		rigidbody.useGravity = true;
	}

	void FireHook(){
		hook = PhotonNetwork.Instantiate (prefabHook.name, transform.position, Quaternion.identity, 0) as GameObject;
		//hook.transform.position = transform.position;
		hook.GetComponent<PhotonView> ().ObservedComponents.Add (hook.transform);

		Physics.IgnoreCollision (collider, hook.collider);

		HookProjectile hp = hook.GetComponent<HookProjectile> ();
		hp.shooter = gameObject;

		Vector3 mouseDirection = Input.mousePosition - Camera.main.WorldToScreenPoint(rigidbody.transform.position);
		mouseDirection.Normalize ();

		Rigidbody rb = hook.GetComponent<Rigidbody> ();
		rb.velocity = mouseDirection * hookSpeed;

		joint = gameObject.AddComponent<SpringJoint> ();
		joint.connectedBody = rb;

		joint.maxDistance = 100f;
		joint.minDistance = 0f;
		joint.spring = spring;
		joint.damper = 100f;
	}

	public void DestroyHook(){
		hooked = false;
		//PhotonNetwork.Destroy (joint as GameObject);
		PhotonNetwork.Destroy (hook);

	}

	void Climbing(){
		//rigidbody.useGravity = false;
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

	void OnTriggerExit(){
		climbing = false;
		rigidbody.useGravity = true;
	}

	void OnTriggerStay(Collider other){
		if (other.gameObject.tag == "Climbable") {
			if (Input.GetKey (KeyCode.W) || Input.GetKey (KeyCode.S)) {
				climbing = true;
				rigidbody.useGravity = false;
			}
		} 
	}

}
