using UnityEngine;
using System.Collections;

public class Gareth : BasePlayerController {

	private Direction sprintDirection;
	public float sprintSpeed = 10f;
	public float sprintTime = 2f;
	public float sprintCooldown = 4f;
	public float currentSprintTime = 0f;
	private float currentCooldownTime = 0f;
	private float defaultMaxSpeed;
	
	public bool sprint = false;
	public bool cooldown = false;

	private Sword sword;
	private Shield shield;
	public float shieldMoveSpeed = 3f;


	public override void Start() {
		attackDamage = 20;
		acceleration = 25f;
		maxSpeed = 9f;
		jumpSpeed = 10f;
		
		AirJumps = 0;
		defaultMaxSpeed = maxSpeed;

		sword = transform.FindChild ("SwordPivot").FindChild ("Sword").GetComponent<Sword>();
		shield = transform.FindChild ("Shield").GetComponent<Shield>();
		sword.enabled = true;
		shield.enabled = true;

//		sword = Sword.Create (this as Actor);
//		shield = Shield.Create (this);

		base.Start ();
	}

	public override void Update () {
		Attack ();
		Block ();
		Charge ();
		Cooldown ();
		base.Update ();
		if (sprint) {
			faceDirection = sprintDirection;
		}
	}

	private void Attack(){
		if (Input.GetKeyDown (KeyCode.Mouse0)) {
			sword.Attack();
		}
	}

	private void Block(){
		if (Input.GetKey (KeyCode.Mouse1)) {
			if(!sprint){
			maxSpeed = shieldMoveSpeed;
			}
			shield.ShieldUp(mouseDirection);
		} else {
			if(!sprint){
			maxSpeed = defaultMaxSpeed;
			}
			shield.ShieldDown ();
		}
	}

	private void Charge() {
		if (Input.GetKeyDown (KeyCode.LeftShift) && !cooldown) {
			if(base.isGrounded) {
				sprintDirection = faceDirection;
				cooldown = true;
				sprint = true;
				maxSpeed = sprintSpeed;
			}
		}

		if (sprint) {
			rigidbody.velocity = new Vector3((int)sprintDirection * sprintSpeed, rigidbody.velocity.y);
			shield.ShieldUp (new Vector3((int)sprintDirection, 0));

			currentSprintTime += Time.deltaTime;
			if(currentSprintTime > sprintTime || CheckForWall()) {
				currentSprintTime = 0;
				sprint = false;
				maxSpeed = defaultMaxSpeed;
			}
			//Debug.Log (lastPos + "  :  " + transform.position.x);
			//lastPos = transform.position.x;
		}
	}
	protected bool CheckForWall(){

		RaycastHit hit;
		if (Physics.Raycast (transform.position + new Vector3(0,1,0), new Vector3 ((int)sprintDirection, 0, 0), out hit, collider.bounds.extents.x + 0.2f)) {
			Component test = hit.collider.gameObject.GetComponent<SoftWall>();
			if(test == null && hit.collider.gameObject.tag != "Gareth"){
				return true;
			}
		}
		return false;
	}
	
	private void Cooldown() {
		if (cooldown) {
			currentCooldownTime += Time.deltaTime;
			if(currentCooldownTime > sprintCooldown) {
				currentCooldownTime = 0;
				cooldown = false;
			}
		}
	}

}
