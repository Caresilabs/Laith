using UnityEngine;
using System.Collections;

public class Projectile : Weapon {

	/// <summary>
	/// Rotates body to look at the velocity direction. Knockback scales depending on velocity.
	/// Author: Henrik P.
	/// </summary>

	public float maxLifeTime = 20;

	void Update () {
		maxLifeTime -= Time.deltaTime;
		if (maxLifeTime <= 0)
			PhotonNetwork.Destroy (this.gameObject);

		rigidbody.transform.rotation = Quaternion.LookRotation (rigidbody.velocity);
		rigidbody.transform.Rotate (90, 0, 0);
	}

	public override void OnTriggerEnter(Collider other){
		if (other.isTrigger == true) {
			return;
		} else {
			maxLifeTime = 0;
		}

		base.OnTriggerEnter (other);
	}

	public override void DealDamage(Actor a){
		a.TakeDamage (damage, knockbackForce * rigidbody.velocity.normalized);
	}
}
