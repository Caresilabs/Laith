using UnityEngine;
using System.Collections;

public class HookProjectile : MonoBehaviour {

	public GameObject shooter;
	private Narissa n;
	private float maxLifeTime;
	private float lifeTime = 0;
	private bool hooked;

	void OnTriggerEnter(){
		if (shooter == null)
			return;

		n.hooked = true;

		n.joint.maxDistance = (n.transform.position - transform.position).magnitude-2;

		rigidbody.isKinematic = true;
		hooked = true;
	}

	void Start(){
		n = shooter.GetComponent<Narissa> ();
		maxLifeTime = n.hookRange/rigidbody.velocity.magnitude;
	}

	void Update(){
		if (hooked)
			return;
		if (lifeTime >= maxLifeTime) {
			n.DestroyHook();
		}
		lifeTime += Time.deltaTime;
	}

}
