using UnityEngine;
using System.Collections;

public class RemoveTrigger : MonoBehaviour {

	/// <summary>
	/// Destroys any object upon collision.
	/// Author: Henrik P.
	/// </summary>

	void OnTriggerEnter(Collider other){
		Destroy (other.gameObject);
	}
}
