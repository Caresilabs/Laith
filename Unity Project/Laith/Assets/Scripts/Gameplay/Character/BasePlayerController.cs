using UnityEngine;
using System.Collections;

/// <summary>
/// Base player is the base for player. Extend this class for different player mechanics
/// Author: Simon B
/// </summary>

public class BasePlayerController :  Actor {
	
	// the amount of jumps
	public int JumpCount { get; set;}
	
	protected int MaxJumps { get; set;}

	public bool HasKey = false;
	public bool use;

	public virtual void Start(){
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

			use = false;
	}
	
	protected virtual void OnTriggerStay(Collider other){
		if (Input.GetKeyDown (KeyCode.E) && use == false) {
			Use (other.gameObject);
			use = true;
		}
	}

	void Use(GameObject trigger){
		trigger.SendMessage("Trigger");
	}

	protected void UpdateDirection(){
		faceDirection = (Direction)(MouseDirection ().x/Mathf.Abs (MouseDirection ().x));
	}

	protected Vector3 MouseDirection() {
		Vector3 mouseDirection = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
		mouseDirection.Normalize ();
		return mouseDirection;
	}
	
}