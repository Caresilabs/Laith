using UnityEngine;
using System.Collections;

public abstract class Actor : MonoBehaviour {

	public float maxHealth;
	public float currentHealth;

	public float attackDamage;

	public float acceleration;
	public float maxSpeed;
	public float jumpSpeed;

	public enum Direction{left = -1, none, right}
	public Direction faceDirection = Direction.right;


	public void TakeDamage(float damage, Vector3 knockback){
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
