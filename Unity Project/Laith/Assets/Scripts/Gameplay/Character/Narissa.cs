using UnityEngine;
using System.Collections;

public class Narissa : BasePlayerController {

	public float hookRange = 12f;
	public float hookSpeed = 50f;
	public float maxHookLength = 10f;

	public float spring = 500f;

	public GameObject prefabHook;
	private GameObject hook;
	public bool hooked;

	public SpringJoint joint;

	public void Start() {
		MaxJumps = 2;
		prefabHook = Resources.Load ("Hook") as GameObject;
	}

	public override void Update () {
		if (Input.GetKeyDown (KeyCode.Mouse0) && !hooked && hook == null) {
			FireHook ();
		}

		if (hooked) {
			HangingOnHook();
		} else {
			UpdateInput ();
		}
	}

	void HangingOnHook(){
		Vector3 pullDirection = joint.connectedBody.rigidbody.transform.position - rigidbody.transform.position;
		Vector3 rb = rigidbody.transform.position;
		pullDirection.Normalize ();

		if(Input.GetKey (KeyCode.S)){
			if(joint.maxDistance < 0)
				return;
			joint.maxDistance += 0.08f;
		}
		if(Input.GetKey (KeyCode.W)){
			if(joint.maxDistance > maxHookLength){
				return;
			}
			joint.maxDistance -= 0.08f;
		}
		joint.maxDistance = Mathf.Clamp (joint.maxDistance, 0, maxHookLength);
		
		if (Input.GetKeyDown ("space")) {
			DestroyHook ();
			rigidbody.AddForce(0, jumpAcceleration/2f * rigidbody.mass, 0);
		}

		Vector3 swingDirectionCC = Vector3.Cross (pullDirection, Vector3.forward);
		
		if (Input.GetKey (KeyCode.D))
			rigidbody.AddForce(swingDirectionCC * 200 * rigidbody.mass * Time.deltaTime);
		
		if (Input.GetKey(KeyCode.A))
			rigidbody.AddForce(-swingDirectionCC * 200 * rigidbody.mass * Time.deltaTime);
	}

	void FireHook(){
		hook = Instantiate (prefabHook) as GameObject;
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
		joint.spring = spring;
		joint.damper = 100f;
	}

	public void DestroyHook(){
		hooked = false;
		Destroy (joint);
		Destroy (hook);
	}

}
