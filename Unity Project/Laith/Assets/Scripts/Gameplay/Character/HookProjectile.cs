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

//		n.hookPoint = transform.position;
		n.hooked = true;

		n.joint.maxDistance = 4;
		--n.JumpCount; 

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
		if (lifeTime >= maxLifeTime)
			Destroy (gameObject);
		lifeTime += Time.deltaTime;
	}

}
