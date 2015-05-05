using UnityEngine;
using System.Collections;

public class Projectile : Weapon {

	/// <summary>
	/// Rotates body to look at the velocity direction. Knockback scales depending on velocity.
	/// Author: Henrik P.
	/// </summary>
	
	public static Projectile Create(string name, Vector3 position, Vector3 velocity, float damage, float lifeTime, GameObject wielder, bool useGravity){
		GameObject projectile = PhotonNetwork.Instantiate (name,  position, Quaternion.LookRotation(velocity), 0) as GameObject;
		projectile.transform.Rotate (90, 0, 0);
		projectile.rigidbody.velocity = velocity;
		projectile.layer = wielder.layer;

		Projectile p = projectile.GetComponent<Projectile> ();
		p.enabled = true;
		p.damage = damage;
		if (lifeTime != 0)
			p.maxLifeTime = lifeTime;
		if(wielder != null)
			p.wielder = wielder;
		p.rigidbody.useGravity = useGravity;

		try{
		Physics.IgnoreCollision (p.wielder.collider, p.collider);
		} catch (MissingComponentException){
			Debug.Log ("Object does not have collider.");
		}

		return p;
	}

	public float maxLifeTime = 20;
	public bool deflectable = true;
	
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
		a.TakeDamage (damage, knockbackForce * rigidbody.velocity/20f);
	}
}
