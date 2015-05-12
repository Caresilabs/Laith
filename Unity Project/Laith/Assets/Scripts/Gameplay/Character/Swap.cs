using UnityEngine;
using System.Collections;

public class Swap : Photon.MonoBehaviour {

	/// <summary>
	/// Script for the Swap ability. Switches position, velocity and rotation between the attached object and the target object.
	/// Author: Andreas Karlsson.
	/// </summary>

	public string targetTag;
	public float cooldown;
	public bool isOnCooldown;
	private float tempCooldown;

	void Start () {
		tempCooldown = cooldown;
	}

	[RPC]
	void SwitchPos(){
		// Finds a game-object with the desired name
		GameObject target = GameObject.FindWithTag(targetTag);

		// Stores the position, velocity and rotation of this object as temporary variables
		Vector3 tempPosition = transform.position;
		Vector3 tempVelocity = rigidbody.velocity;
		Quaternion tempRotation = transform.rotation;

		// Sets this objects position, velocity and rotation to be the targets position, velocity and rotation
		transform.position = target.transform.position;
		rigidbody.velocity = target.rigidbody.velocity;
		transform.rotation = target.transform.rotation;

		// Sets the targets position, velocity and rotation to be the old position, velocity and rotation of this object
		target.transform.position = tempPosition;
		target.rigidbody.velocity = tempVelocity;
		target.transform.rotation = tempRotation;

		isOnCooldown = true;
	}

	void Update () {
		if (Input.GetKeyDown (KeyCode.Q) && !isOnCooldown){
			//SwitchPos();
			GameObject.FindWithTag("Narissa").GetComponent<Narissa>().DestroyHook();
			photonView.RPC("SwitchPos", PhotonTargets.All);
		}

		// Timer for the cooldown
		if (isOnCooldown) {
			cooldown -= Time.deltaTime;
			if (cooldown <= 0){
				isOnCooldown = false;
				cooldown = tempCooldown;
			}
		}

	}
}
