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
	
	//private float lastPos;

	public GameObject shield;
	public float shieldDistance = 1;
	public Vector3 shieldOffset = new Vector3(0, 0.5f, 0);

	private GameObject swordPivot;
	public GameObject sword;
	public Vector3 swordOffset = new Vector3 (0.6f, 0, 0);
	private int attackDirection;
	private float attackTime = 1f;
	private float currentAttackTime = 0f;
	private bool attacking;
	private float angle = 0;

	public void Start() {
		acceleration = 20f;
		maxSpeed = 5f;
		jumpSpeed = 7f;
		
		MaxJumps = 1;
		defaultMaxSpeed = maxSpeed;
		shield = Instantiate (shield) as GameObject;

		//Pivot sets origin point so that the sword rotates around this point instead of around its center.
		swordPivot = new GameObject ("SwordPivot");
		swordPivot.transform.parent = transform;
		swordPivot.transform.rotation = Quaternion.Euler(0,90,0);

		sword = Instantiate (sword) as GameObject;
		sword.transform.parent = swordPivot.transform;
		sword.transform.localPosition = new Vector3 (0.1f, 1, 0);
		sword.collider.enabled = false;
		base.Start ();
	}

	public override void Update () {
		Sword ();
		Shield ();
		Charge ();
		Sprint ();
		Cooldown ();
		base.Update ();
	}

	private void Sword(){
		if (Input.GetKeyDown (KeyCode.Mouse0) && !attacking) {
			attacking = true;
			attackDirection = (int)faceDirection;
			sword.collider.enabled = true;
			currentAttackTime = 0;
		}

		if (attacking) {
			currentAttackTime += Time.deltaTime;
			angle += 3;
			swordPivot.transform.Rotate (new Vector3 (3 * attackDirection, 0, 0));
			if (currentAttackTime >= attackTime) {
				attacking = false;
				sword.collider.enabled = false;
				swordPivot.transform.rotation = Quaternion.Euler (0, 90, 0);
			}
		}
		if(!attacking) {
			swordPivot.transform.localPosition = new Vector3((int)faceDirection* swordOffset.x,swordOffset.y,swordOffset.z);
		}
	}

	private void Shield(){
		if (Input.GetKey (KeyCode.Mouse1)) {

			if(Input.mousePosition.y < Camera.main.WorldToScreenPoint(transform.position).y){
				Vector3 shieldDirection = new Vector3((int)faceDirection,0,0);
				shield.transform.LookAt(transform.position + shieldDirection * 10);
				shield.transform.Rotate(90,0,0);
				shield.transform.position = transform.position + shieldOffset + shieldDirection * shieldDistance;
				return;
			}

			Vector3 mouseDirection = MouseDirection ();
			//float angle = Mathf.Asin (mouseDirection.y / 1f) * 180f / Mathf.PI;
			//shield.transform.rotation = Quaternion.Euler (0, 0, angle);
			shield.transform.LookAt(transform.position + mouseDirection * 10);
			shield.transform.Rotate(90,0,0);
			shield.transform.position = transform.position + shieldOffset + mouseDirection * shieldDistance;
		} else {
			shield.transform.rotation = Quaternion.Euler (90, 0, 0);
			shield.transform.position = transform.position + new Vector3(0, 0, 1) * shieldDistance;
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
