using UnityEngine;
using System.Collections;

public class Gareth : BasePlayerController {
	
	public float sprintSpeed = 10f;
	public float sprintTime = 2f;
	public float sprintCooldown = 4f;
	private float currentSprintTime = 0f;
	private float currentCooldownTime = 0f;
	private float defaultMaxSpeed;
	
	public bool sprint = false;
	public bool cooldown = false;

	private Sword sword;
	private Shield shield;
	public float shieldMoveSpeed = 3f;


	public override void Start() {
		attackDamage = 20;
		acceleration = 20f;
		maxSpeed = 5f;
		jumpSpeed = 10f;
		
		MaxJumps = 1;
		defaultMaxSpeed = maxSpeed;

		sword = Sword.Create (this as Actor);
		shield = Shield.Create (this);

		base.Start ();
	}

	public override void Update () {
		Attack ();
		Block ();
		Charge ();
		Sprint ();
		Cooldown ();
		base.Update ();
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
			shield.ShieldUp(MouseDirection());
		} else {
			if(!sprint){
			maxSpeed = defaultMaxSpeed;
			}
			shield.ShieldDown ();
		}

	}

	private void Charge() {
		if (Input.GetKeyDown (KeyCode.LeftShift) && !cooldown) {
			if(base.IsGrounded() && rigidbody.velocity.x != 0) {
				cooldown = true;
				sprint = true;
				maxSpeed = sprintSpeed;
			}
		}
	}

	private void Sprint() {
		if (sprint) {
			
			float dirr = rigidbody.velocity.x;
			if(dirr > 0) {
				rigidbody.velocity = new Vector3(sprintSpeed, rigidbody.velocity.y);
			}
			else if(dirr < 0) {
				rigidbody.velocity = new Vector3(-sprintSpeed, rigidbody.velocity.y);
			}
			
			currentSprintTime += Time.deltaTime;
			if(currentSprintTime > sprintTime && base.IsGrounded()) {
				currentSprintTime = 0;
				sprint = false;
				maxSpeed = defaultMaxSpeed;
			}
			//Debug.Log (lastPos + "  :  " + transform.position.x);
			//lastPos = transform.position.x;
		}
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
