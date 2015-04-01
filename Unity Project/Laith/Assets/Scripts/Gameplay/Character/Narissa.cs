using UnityEngine;
using System.Collections;

public class Narissa : BasePlayerController {

	public float hookRange = 12f;
	public float hookSpeed = 50f;
	public float maxHookLength = 10f;

	private GameObject prefab;
	private GameObject hook;
	//public Vector3 hookPoint;
	public bool hooked;
	public float pullForce = 100000f;

	public SpringJoint joint;

	public void Start() {
		MaxJumps = 2;
		prefab = Resources.Load ("Hook") as GameObject;
	}

	public override void Update () {
		if (Input.GetKeyDown (KeyCode.Mouse0) && !hooked && hook == null) {
			Hook ();
		}

		if (hooked) {
			if (Input.GetKeyDown ("space")) {
				hooked = false;
				Destroy (joint);
				Destroy (hook);
			}

			if(Input.GetKey (KeyCode.S)){
				joint.maxDistance += 0.05f;
				if(joint.maxDistance > maxHookLength)
					joint.maxDistance = maxHookLength;
			}
			if(Input.GetKey (KeyCode.W)){
				joint.maxDistance -= 0.05f;
				if(joint.maxDistance < 0)
					joint.maxDistance = 0;
			}

//			Vector3 hookDir = hookPoint - transform.position;
//			hookDir.Normalize ();
//			rigidbody.AddForce (hookDir * pullForce * Time.deltaTime);
		}

		base.Update ();
	}

	void Hook(){
		hook = Instantiate (prefab) as GameObject;
		hook.transform.position = transform.position;

		Physics.IgnoreCollision (collider, hook.collider);

		HookProjectile hp = hook.GetComponent<HookProjectile> ();
		hp.shooter = gameObject;

		Vector3 mouseDirection = Input.mousePosition - Camera.main.WorldToScreenPoint(rigidbody.transform.position);
		mouseDirection.Normalize ();

		Rigidbody rb = hook.GetComponent<Rigidbody> ();
		rb.velocity = mouseDirection * hookSpeed;

		joint = gameObject.AddComponent<SpringJoint> ();
		joint.connectedBody = rb;

		joint.maxDistance = 1000f;
		joint.minDistance = 0f;
		joint.spring = 200f;
		joint.damper = 100f;
	}

//	Drar Narissa mot en hookpoint istället för att skapa en spring joint.	
//	void Hook(){
//		GameObject hook = Instantiate (prefab) as GameObject;
//		Physics.IgnoreCollision (collider, hook.collider);
//		hook.transform.position = transform.position;
//		Rigidbody rb = hook.GetComponent<Rigidbody> ();
//
//		HookProjectile hp = hook.GetComponent<HookProjectile> ();
//		hp.shooter = gameObject;
//
//		Vector3 mouseDirection = Input.mousePosition - Camera.main.WorldToScreenPoint(rigidbody.transform.position);
//		mouseDirection.Normalize ();
//
//		rb.velocity = mouseDirection * hookSpeed;
//	}
}
