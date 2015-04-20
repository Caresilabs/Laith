using UnityEngine;
using System.Collections;

public class RangedEnemy : BaseEnemy {

	public float projectileSpeed = 5f;
	public float attacksPerSecond = 0.5f;
	float attackCooldown;
	float attackTimer;

	// Use this for initialization
	public override void Start () {
		attackDamage = 1;
		attackCooldown = 1f / attacksPerSecond;

		acceleration = 20f;
		maxSpeed = 5f;
		jumpSpeed = 7f;
		base.Start ();
	}
	
	// Update is called once per frame
	public override void Update () {
		attackTimer -= Time.deltaTime;

		if (attackTimer <= 0) {
			RaycastHit rch;
			if (Physics.Raycast (transform.position, new Vector3 ((int)faceDirection, 0, 0), out rch, 20f)) {
				if (rch.collider.gameObject.tag == "Player") {
					FireProjectile (new Vector3 ((int)faceDirection, 0, 0));
				}
			}
		}

		base.Update ();
	}

	public void FireProjectile(Vector3 direction){
		GameObject projectile = Instantiate (Resources.Load ("Arrow"), transform.position, Quaternion.LookRotation(direction)) as GameObject;
		projectile.layer = 1;
		Physics.IgnoreCollision (collider, projectile.collider);
		
		projectile.rigidbody.velocity = direction * projectileSpeed;
		projectile.rigidbody.useGravity = false;
		
		Weapon p = projectile.GetComponent<Weapon> ();
		p.damage = attackDamage;
		p.wielder = this;

		attackTimer = attackCooldown;
	}
}
