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
	protected int AirJumps { get; set;}

	private float brakeModifier = 3.6f;

	public bool HasKey = false;
	public bool use;
	public bool dead;

	public Vector3 mouseDirection;

	public virtual void Start(){

	}

	public override void Update() {
		if (!dead) {
			CheckForDead();
			Movement ();
			UpdateState();
		}
	}

	protected bool CheckForDead(){
		if(currentHealth <= 0){
			GetComponent<PhotonView>().RPC ("SendDeadInfo", PhotonTargets.All); 
			//dead = true;
			return true;
		}
		return false;
	}

	public void Respawn(Vector3 lastCheckpoint){
		GetComponent<PhotonView> ().RPC ("SendRespawnInfo", PhotonTargets.All, lastCheckpoint);
	}

	[RPC]
	public void SendDeadInfo(){
		dead = true;
		MeshRenderer[] test = GetComponentsInChildren<MeshRenderer>();
		for (int i = 0; i < test.Length; ++i) {
			test[i].enabled = false;
		}
		GetComponent<CapsuleCollider> ().isTrigger = true;
		currentHealth = 0;
	}

	[RPC]
	public void SendRespawnInfo (Vector3 lastCheckpoint){
		dead = false;
		MeshRenderer[] test = GetComponentsInChildren<MeshRenderer>();
		for (int i = 0; i < test.Length; ++i) {
			test[i].enabled = true;
		}
		GetComponent<CapsuleCollider> ().isTrigger = false;
		rigidbody.velocity = Vector3.zero;
		transform.position = lastCheckpoint;
		currentHealth = maxHealth;
	}

	protected void UpdateState ()
	{
		CheckIfGrounded ();
		UpdateMouseDirection ();
		UpdateDirection ();

		if (isGrounded) {
			JumpCount = 0;
			AutoBrake ();
		}
		use = false;
		base.Invulnerability ();
	}

	protected void Movement(){
		if (Input.GetKey (KeyCode.D) && rigidbody.velocity.x < maxSpeed) 
			rigidbody.velocity += (Vector3.right * acceleration * Time.deltaTime);
		if (Input.GetKey (KeyCode.A) && rigidbody.velocity.x > -maxSpeed) 
			rigidbody.velocity += (Vector3.left * acceleration * Time.deltaTime);

		if (Input.GetKeyDown ("space") && isGrounded) {
			Jump ();
		} else if (Input.GetKeyDown ("space") && JumpCount < AirJumps){
			Jump();
			JumpCount++;
		}
	}

	//Characters stop themselves when no input is given
	private void AutoBrake(){
		if (!Input.GetKey (KeyCode.A) && !Input.GetKey (KeyCode.D)) {
			float modifier = 1 - brakeModifier * Time.deltaTime;
			if(modifier < 0)
				modifier = 0;
			rigidbody.velocity *= modifier;
		}
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
		faceDirection = (Direction)(mouseDirection.x/Mathf.Abs (mouseDirection.x));
	}

	public void UpdateMouseDirection() {
		mouseDirection = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
		mouseDirection.Normalize ();
	}
	
}