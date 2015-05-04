using UnityEngine;
using System.Collections;

public class Boulder : MonoBehaviour {

	/// <summary>
	/// Kills colliding actor when relative velocity is big enough.
	/// Author: Henrik P.
	/// </summary>

	void OnCollisionEnter(Collision collision){
		if (collision.gameObject.tag == "Breakable") {
			Destroy(collision.gameObject);
			return;
		}
		Actor other = collision.gameObject.GetComponent<Actor> ();
		if (other == null)
			return;

		if (rigidbody.velocity.magnitude > 4 && 
			collision.relativeVelocity.magnitude > 4) {
			other.currentHealth = 0;
		}
	}
}
