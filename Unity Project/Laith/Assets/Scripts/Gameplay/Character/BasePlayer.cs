using UnityEngine;
using System.Collections;

/// <summary>
/// Base player is the base for player. Extend this class for different player mechanics
/// Author: Simon B
/// </summary>

public class BasePlayer : MonoBehaviour {

	protected CharacterController controller;
	public float speed  = 6.0f;
	public float jumpSpeed  = 11.0f;
	public float gravity  = 20.0f;
	
	private Vector3 moveDirection = Vector3.zero;
	// the amount of jumps
	private int jumpCount;

	public virtual void Update() {

		CharacterController controller = GetComponent<CharacterController>();

		// reset jump count at landing
		if (controller.isGrounded) {
			jumpCount = 0;
		}

		float prevSpeed = moveDirection.y;
		moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, 0); //Input.GetAxis("Vertical"));
		moveDirection = transform.TransformDirection(moveDirection);
		moveDirection *= speed;
		moveDirection.y = prevSpeed;

		// Allow double jumping
		if (Input.GetKeyDown("space") && jumpCount < 2) {
			moveDirection.y = jumpSpeed;
			jumpCount++;
		}
		
		// Apply gravity
		moveDirection.y -= gravity * Time.deltaTime;	
		
		// Move the controller
		controller.Move(moveDirection * Time.deltaTime);

		// Set z to 0
		controller.transform.position = new Vector3 (controller.transform.position.x, controller.transform.position.y, 0);
	}

//	void MovePlayer(){
//		controller.Move(moveDirection * Time.deltaTime);
//	}

}
