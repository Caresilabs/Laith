using UnityEngine;
using System.Collections;

/// <summary>
/// Base player is the base for player. Extend this class for different player mechanics
/// Author: Simon B
/// </summary>

public class BasePlayerController :  Actor {
	
	// the amount of jumps
	//public int JumpCount { get; set;}
	public int JumpCount;
	protected int MaxJumps { get; set;}

	private float brakeModifier = 3.6f;

	public bool HasKey = false;
	public bool use;
	public bool dead;

	public virtual void Start(){
		Physics.IgnoreLayerCollision (8, 9);
	}

	public override void Update() {
		if (!dead) {
			UpdateInput();
			CheckForDead();
		}
		base.Update ();
	}
	protected bool CheckForDead(){
		if(currentHealth <= 0){
			GetComponent<PhotonView>().RPC ("SendDeadInfo", PhotonTargets.All, null); 
			//dead = true;
			return true;
		}
		return false;
	}
	public void Respawn(Vector3 lastCheckpoint){

		transform.position = lastCheckpoint;
		rigidbody.velocity = Vector3.zero;
		GetComponent<PhotonView> ().RPC ("SendRespawnInfo", PhotonTargets.All, null);
	}
	[RPC]
	public void SendDeadInfo(){

		dead = true;
		GetComponentInChildren<MeshRenderer>().enabled = false;
	}
	[RPC]
	public void SendRespawnInfo (){

		dead = false;
		GetComponentInChildren<MeshRenderer>().enabled = true;
		currentHealth = maxHealth;
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
				float modifier = 1 - brakeModifier * Time.deltaTime;
				if(modifier < 0)
					modifier = 0;
				rigidbody.velocity *= modifier;
			}
		}


		// TODO fix bug where you can power jump by spamming space

		// Clamp to max velocity
		//rigidbody.velocity = new Vector3(Mathf.Clamp(rigidbody.velocity.x, -maxSpeed, maxSpeed), rigidbody.velocity.y, 0);


		if (Input.GetKeyDown ("space") && JumpCount + 1 <= MaxJumps) {
			Jump();
			transform.position = new Vector3(transform.position.x, transform.position.y + 0.15f, transform.position.z);
			JumpCount++;
		}

			use = false;
	}
	
	protected virtual void OnTriggerStay(Collider other){
		if (Input.GetKeyDown (KeyCode.E) && use == false) {
			Use (other.gameObject);
		}
	}

	private void Use(GameObject trigger){
		Triggerable t = trigger.gameObject.GetComponent<Triggerable>();
		if (t == null)
			return;
		if (t.playerActivated)
			t.Trigger ();
		use = true;
	}

	protected void UpdateDirection(){
		faceDirection = (Direction)(MouseDirection ().x/Mathf.Abs (MouseDirection ().x));
	}

	public Vector3 MouseDirection() {
		Vector3 mouseDirection = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
		mouseDirection.Normalize ();
		return mouseDirection;
	}
	
}