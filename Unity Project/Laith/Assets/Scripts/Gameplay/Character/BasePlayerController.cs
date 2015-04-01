using UnityEngine;
using System.Collections;

/// <summary>
/// Base player is the base for player. Extend this class for different player mechanics
/// Author: Simon B
/// </summary>

public class BasePlayerController :  MonoBehaviour {
	
	public float speed = 10;
	public float jumpAcceleration  = 30.0f;
	
	// the amount of jumps
	private int JumpCount { get; set;}
	
	protected int MaxJumps { get; set;}

	public bool HasKey = false;
	
	public virtual void Update() {
		UpdateInput ();
		
		Debug.Log (rigidbody.velocity);
	}
	
	void UpdateInput ()
	{
		
		if (Input.GetKey (KeyCode.D))
			rigidbody.velocity += (Vector3.right * speed * Time.deltaTime);
		//rigidbody.MovePosition(rigidbody.position + Vector3.right * speed * Time.deltaTime);
		
		if (Input.GetKey(KeyCode.A))
			rigidbody.velocity += (Vector3.left * speed * Time.deltaTime);
		//rigidbody.MovePosition(rigidbody.position - Vector3.right * speed * Time.deltaTime);
		
		// Clamp to max velocity
		rigidbody.velocity = new Vector3(Mathf.Clamp(rigidbody.velocity.x, -5f, 5f), rigidbody.velocity.y, 0);
		
		if (IsGrounded()) {
			JumpCount = 0;
		}
		
		if (Input.GetKeyDown ("space") && JumpCount + 1 <= MaxJumps) {
			//rigidbody.velocity = new Vector3(rigidbody.velocity.x, jumpSpeed, rigidbody.velocity.z);
			rigidbody.AddForce(0, jumpAcceleration * rigidbody.mass, 0);
			JumpCount++;
		}
		/*
		CharacterController controller = GetComponent<CharacterController>();
		// reset jump count at landing
		if (controller.isGrounded) {
			jumpCount = 0;
		}
		float prevSpeed = moveDirection.y;
		moveDirection = new Vector3 (Input.GetAxis ("Horizontal"), 0, 0);
		//Input.GetAxis("Vertical"));
		moveDirection = transform.TransformDirection (moveDirection);
		moveDirection *= speed;
		moveDirection.y = prevSpeed;
		// Allow double jumping
		if (Input.GetKeyDown ("space") && jumpCount < 2) {
			moveDirection.y = jumpSpeed;
			jumpCount++;
		}
		// Apply gravity
		moveDirection.y -= gravity * Time.deltaTime;
		// Move the controller
		controller.Move (moveDirection * Time.deltaTime);
		// Set z to 0
		controller.transform.position = new Vector3 (controller.transform.position.x, controller.transform.position.y, 0);
*/
	}
	
	private bool IsGrounded() {
		return Physics.Raycast(transform.position, -Vector3.up,  collider.bounds.extents.y + 0.05f);
	}
	
	
}