using UnityEngine;
using System.Collections;

public class Swap : MonoBehaviour {
	/// <summary>
	/// Script for the Swap ability. Switches position, velocity and rotation between the attached object and the target object.
	/// Author: Andreas Karlsson.
	/// </summary>

	public float cooldown;
	public bool isOnCooldown;
	private float tempCooldown;

	// Use this for initialization
	void Start () {
		tempCooldown = cooldown;
	}

	// Method that switches positions
	void SwitchPos(){
		GameObject target = GameObject.Find("Gareth(Clone)");

		Vector3 tempPosition = transform.position;   			// Stores this objects position
		Vector3 tempVelocity = rigidbody.velocity; 				// Stores this objects velocity
		Quaternion tempRotation = transform.rotation;			// Stores this objects rotation

		transform.position = target.transform.position;   		// Sets this objects position to be the targets position
		rigidbody.velocity = target.rigidbody.velocity;			// Sets this objects velocity to be the targets velocity
		transform.rotation = target.transform.rotation;			// Sets this objects rotation to be the targets rotation

		target.transform.position = tempPosition;   			// Sets the targets position to be the old position of this object
		target.rigidbody.velocity = tempVelocity;				// Sets the targets velocity to be the old velocity of this object
		target.transform.rotation = tempRotation;				// Sets the targets rotation to be the old rotiation of this object

		isOnCooldown = true;
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Q) && !isOnCooldown){
			SwitchPos();
		}

		if (isOnCooldown) {
			cooldown -= Time.deltaTime;
			if (cooldown <= 0){
				isOnCooldown = false;
				cooldown = tempCooldown;
			}
		}

	}
}
