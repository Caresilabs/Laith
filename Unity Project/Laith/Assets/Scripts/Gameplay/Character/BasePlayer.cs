using UnityEngine;
using System.Collections;

public class BasePlayer : MonoBehaviour {
	
	float speed  = 6.0f;
	float jumpSpeed  = 11.0f;
	float gravity  = 20.0f;
	
	private Vector3 moveDirection = Vector3.zero;
	private int jumpCount;

	public void Update() {
		CharacterController controller = GetComponent<CharacterController>();

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
	}
}
