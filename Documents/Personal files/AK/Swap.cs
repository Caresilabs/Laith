using UnityEngine;
using System.Collections;

public class Swap : MonoBehaviour {
	//Code by Andreas

	public float cooldown;
	public bool isOnCooldown;
	private float tempCooldown;

	// Use this for initialization
	void Start () {
		tempCooldown = cooldown;
	}

	// Method that switches positions
	void SwitchPos() {
		var target = GameObject.FindWithTag("Gareth");   	// Finds a GameObject with the correct tag and sets it as a variable
		Vector3 tempPos = transform.position;   			// Stores this objects position as a temporary variable
		transform.position = target.transform.position;   	// Sets this objects position to be the targets position
		target.transform.position = tempPos;   				// Sets the targets position as the old position of this object
		isOnCooldown = true;
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown ("space") && !isOnCooldown){
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
