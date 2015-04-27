using UnityEngine;
using System.Collections;

public abstract class Actor : MonoBehaviour {

	/// <summary>
	/// Includes basic info such as health, acceleration and methods for taking damage and jumping.
	/// Author: Henrik P.
	/// </summary>

	public float maxHealth;
	public float currentHealth;

	public float attackDamage;

	public float acceleration;
	public float maxSpeed;
	public float jumpSpeed;

	public float invulnerabilityFrames;
	private float invulnerabilityTimer = 0;
	public bool invulnerable;

	public enum Direction{left = -1, none, right}
	public Direction faceDirection = Direction.right;


	public virtual void Update(){
		Invulnerability ();
	}

	protected void Invulnerability(){
		if (invulnerable) {
			++invulnerabilityTimer;
			if(invulnerabilityTimer >= invulnerabilityFrames){
				invulnerable = false;
				invulnerabilityTimer = 0;
			}
		}
	}

	public void TakeDamage(float damage, Vector3 knockback){
		if (invulnerable)
			return;
		invulnerable = true;
		currentHealth -= damage;
		rigidbody.AddForce (knockback + new Vector3(0,1,0));
	}

	protected void Jump(){
		rigidbody.velocity = new Vector3(rigidbody.velocity.x, jumpSpeed, rigidbody.velocity.z);
		//rigidbody.AddForce(0, jumpAcceleration * rigidbody.mass, 0);
	}

	protected bool IsGrounded() {
		return Physics.Raycast (transform.position, -Vector3.up, collider.bounds.extents.y + 0.05f);
	}
}
