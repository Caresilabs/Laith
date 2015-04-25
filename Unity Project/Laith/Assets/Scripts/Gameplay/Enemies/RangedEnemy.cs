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

		BasePlayerController[] bpc = GameObject.FindObjectsOfType<BasePlayerController> ();
		float closestDistance = 1000;
		BasePlayerController closest = null;
		for (int i = 0; i < bpc.GetLength(0); i++) {
			if(closest == null){
				closest = bpc[i];
				closestDistance = (bpc[i].transform.position - transform.position).magnitude;
				continue;
			}
			float distance = (bpc[i].transform.position - transform.position).magnitude;
			if(distance < closestDistance){
				closest = bpc[i];
				closestDistance = (bpc[i].transform.position - transform.position).magnitude;
			}
		}
		target = closest;

		if (attackTimer <= 0 && closestDistance < 20) {
			FireProjectile (target.transform.position - transform.position);
			
//			RaycastHit rch;
//			int layerMask = 1 << 8;
//			if (Physics.Raycast (transform.position, new Vector3 ((int)faceDirection, 0, 0), out rch, 20f, layerMask)) {
//				if (rch.collider.gameObject.tag == "Player") {
//					FireProjectile (new Vector3 ((int)faceDirection, 0, 0));
//				}
//			}
		}

		base.Update ();
	}

	public void FireProjectile(Vector3 direction){
		direction.Normalize ();
		GameObject projectile = PhotonNetwork.Instantiate ("Arrow", transform.position, Quaternion.LookRotation(direction), 0) as GameObject;
		projectile.layer = 1;
		projectile.GetComponent<Projectile> ().enabled = true;
		Physics.IgnoreCollision (collider, projectile.collider);

		projectile.rigidbody.velocity = direction * projectileSpeed;
		projectile.rigidbody.useGravity = false;
		
		Weapon p = projectile.GetComponent<Weapon> ();
		p.damage = attackDamage;
		p.wielder = this as Actor;

		attackTimer = attackCooldown;
	}
}
