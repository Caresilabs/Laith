using UnityEngine;
using System.Collections;

public class PowerupEffects : MonoBehaviour
{

	/// <summary>
	/// Controls movement of powerups
	/// Author: Andreas Karlsson.
	/// </summary>

	public float turnSpeed;
	public float moveSpeedModifier;
	private float moveTimer = 3.0f;
	private float currentMoveTimer;
	private Vector3 direction;

	void Start (){
		direction = new Vector3 (0, 1, 0);
		gameObject.rigidbody.velocity = direction * moveSpeedModifier;
	}

	void Update (){

		if (currentMoveTimer >= moveTimer){
			currentMoveTimer = 0.0f;
			gameObject.rigidbody.velocity *= -1;
		}
		transform.Rotate(Vector3.up, turnSpeed * Time.deltaTime);

		currentMoveTimer += Time.deltaTime;
	}
}

