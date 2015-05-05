using UnityEngine;
using System.Collections;

public class RangedEnemy : BaseEnemy {

	/// <summary>
	/// Fires projectiles at target player.
	/// Author: Henrik P.
	/// </summary>

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

		target = FindTarget();

		if (target != null && attackTimer <= 0) {
			FireProjectile (target.transform.position - transform.position);
		}

		base.Update ();
	}

	private BasePlayerController FindTarget(){
		BasePlayerController[] bpc = GameObject.FindObjectsOfType<BasePlayerController> ();
		float closestDistance = 1000;
		BasePlayerController closest = null;

		for (int i = 0; i < bpc.GetLength(0); i++) {
			if (closest == null) {
				closest = bpc [i];
				closestDistance = (bpc [i].transform.position - transform.position).magnitude;
				continue;
			}

			float distance = (bpc [i].transform.position - transform.position).magnitude;

			if (distance < closestDistance) {
				closest = bpc [i];
				closestDistance = (bpc [i].transform.position - transform.position).magnitude;
			}
		}

		if (closestDistance > 20)
			return null;
		else
			return closest;
	}

	public void FireProjectile(Vector3 direction){
		direction.Normalize ();
		Projectile.Create (
			"Arrow",
			transform.position,
			direction * projectileSpeed,
			attackDamage,
			0,
			gameObject,
			false
			);

		attackTimer = attackCooldown;
	}
}
