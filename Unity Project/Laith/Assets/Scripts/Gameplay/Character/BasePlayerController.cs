using UnityEngine;
using System.Collections;

/// <summary>
/// Base player is the base for player. Extend this class for different player mechanics
/// Author: Simon B
/// </summary>

public class BasePlayerController :  MonoBehaviour {
	
	public float acceleration;
	public float maxSpeed;
	public float jumpSpeed;
	
	// the amount of jumps
	public int JumpCount { get; set;}
	
	protected int MaxJumps { get; set;}

	public bool HasKey = false;

	protected enum Direction{left = -1, none, right}
	protected Direction faceDirection = Direction.right;

	public void Start(){
		Physics.IgnoreLayerCollision (8, 9);
	}

	public virtual void Update() {
		UpdateInput ();
	}
	
	protected void UpdateInput ()
	{
		UpdateDirection();

		if (Input.GetKey (KeyCode.D) && rigidbody.velocity.x < maxSpeed) {
			rigidbody.velocity += (Vector3.right * acceleration * Time.deltaTime);
			//rigidbody.MovePosition (rigidbody.position + Vector3.right * speed/5f * Time.deltaTime);
		}
		if (Input.GetKey (KeyCode.A) && rigidbody.velocity.x > -maxSpeed) {
			rigidbody.velocity += (Vector3.left * acceleration * Time.deltaTime);
			//rigidbody.MovePosition (rigidbody.position - Vector3.right * speed/5f * Time.deltaTime);
		}

		if (IsGrounded ()) {
			JumpCount = 0;
			//Characters stop themselves when no input is given
			if (!Input.GetKey (KeyCode.A) && !Input.GetKey (KeyCode.D)) {
				rigidbody.velocity *= 0.95f;
			}
		}


		// TODO fix bug where you can power jump by spamming space

		// Clamp to max velocity
		//rigidbody.velocity = new Vector3(Mathf.Clamp(rigidbody.velocity.x, -maxSpeed, maxSpeed), rigidbody.velocity.y, 0);


		if (Input.GetKeyDown ("space") && JumpCount + 1 <= MaxJumps) {
			Jump();
			JumpCount++;
		}
	}

	protected void UpdateDirection(){
		faceDirection = (Direction)(MouseDirection ().x/Mathf.Abs (MouseDirection ().x));
	}

	protected void Jump(){
		rigidbody.velocity = new Vector3(rigidbody.velocity.x, jumpSpeed, rigidbody.velocity.z);
		//rigidbody.AddForce(0, jumpAcceleration * rigidbody.mass, 0);
	}

	protected bool IsGrounded() {
		return Physics.Raycast(transform.position, -Vector3.up,  collider.bounds.extents.y + 0.05f);
	}

	protected Vector3 MouseDirection() {
		Vector3 mouseDirection = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
		mouseDirection.Normalize ();
		return mouseDirection;
	}
	
}