using UnityEngine;
using System.Collections;

/// <summary>
/// Base player is the base for player. Extend this class for different player mechanics
/// Author: Simon B
/// </summary>

public class BasePlayerController :  MonoBehaviour {
	
	public float speed = 100;
	public float jumpAcceleration  = 300.0f;
	
	// the amount of jumps
	public int JumpCount { get; set;}
	
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

		// TODO fix bug where you can power jump by spamming space
		if (Input.GetKeyDown ("space") && JumpCount + 1 <= MaxJumps) {
			//rigidbody.velocity = new Vector3(rigidbody.velocity.x, jumpSpeed, rigidbody.velocity.z);
			rigidbody.AddForce(0, jumpAcceleration * rigidbody.mass, 0);
			JumpCount++;
		}
	}
	
	private bool IsGrounded() {
		return Physics.Raycast(transform.position, -Vector3.up,  collider.bounds.extents.y + 0.05f);
	}
	
	
}