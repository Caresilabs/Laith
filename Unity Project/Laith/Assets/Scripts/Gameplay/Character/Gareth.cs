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
	
	public void Start() {
		MaxJumps = 1;
		defaultMaxSpeed = maxSpeed;
		shield = Instantiate (shield) as GameObject;
	}
	public void Shield(){
		if (Input.GetKey (KeyCode.Mouse0)) {
			Vector3 mouseDirection = Input.mousePosition - Camera.main.WorldToScreenPoint (rigidbody.transform.position);
			mouseDirection.Normalize ();
			
			float angle = Mathf.Asin(mouseDirection.y / 1f) * 180f / Mathf.PI;
			shield.transform.rotation = Quaternion.Euler(0, 0, angle);
			shield.transform.position = transform.position + shieldOffset + mouseDirection * shieldDistance;
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
	private void Charge() {
		if (Input.GetKeyDown (KeyCode.Mouse1) && !cooldown) {
			if(base.IsGrounded() && rigidbody.velocity.x != 0) {
				cooldown = true;
				sprint = true;
				maxSpeed = sprintSpeed;
			}
		}
	}
	
	public override void Update () {
		
		Shield ();
		Charge ();
		Sprint ();
		Cooldown ();
		base.Update ();
	}
}
