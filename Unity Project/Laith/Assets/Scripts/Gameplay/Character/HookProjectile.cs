﻿using UnityEngine;
using System.Collections;

public class HookProjectile : Photon.MonoBehaviour {

	public GameObject shooter;
	public LineRenderer line;
	private Narissa n;
	private float maxLifeTime;
	private float lifeTime = 0;
	private bool hooked;

	void OnTriggerEnter(Collider other){
		if (!photonView.isMine)
			return;

		if (other.gameObject.collider.isTrigger)
			return;
		else if (other.gameObject.tag != "Hookable" || shooter == null) {
			n.DestroyHook ();
			return;
		}

		n.hooked = true;

		n.joint.maxDistance = (n.transform.position - transform.position).magnitude-1;
		rigidbody.isKinematic = true;
		hooked = true;
	}

	void Start(){
		if (shooter == null)
			shooter = GameObject.Find ("Narissa(Clone)");

		n = shooter.GetComponent<Narissa> ();

		if (!photonView.isMine)
			return;

		maxLifeTime = n.hookRange/rigidbody.velocity.magnitude;
	}

	void Update(){
		line.SetPosition (0, shooter.transform.position);
		line.SetPosition (1, transform.position);

		if (!photonView.isMine)
			return;

		rigidbody.transform.rotation = Quaternion.LookRotation (rigidbody.velocity);
		rigidbody.transform.Rotate (90, 0, 0);


		if (hooked)
			return;
		if (lifeTime >= maxLifeTime) {
			n.DestroyHook();
		}
		lifeTime += Time.deltaTime;
	}

}
