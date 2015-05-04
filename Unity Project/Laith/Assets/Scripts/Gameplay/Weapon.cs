using UnityEngine;
using System.Collections;

public class Weapon : Photon.MonoBehaviour {

	/// <summary>
	/// Checks for collision and deals damage if appropriate. Extend this class for anything that deals damage upon contact. 
	/// Author: Henrik P.
	/// </summary>

	public float damage;
	public Vector3 knockbackDirection = Vector3.zero;
	public float knockbackForce;
	public Actor wielder;

	public virtual void OnTriggerEnter(Collider other){
		if (other.isTrigger == true) {
			return;
		}
		Actor a;
		a = other.GetComponent<Actor>();
		if (a == null)
			return;

		if (wielder == null || 
		    wielder.gameObject.layer != other.gameObject.layer) {
			DealDamage (a);
		}
	}

	public virtual void DealDamage(Actor a){
		if (wielder == null || knockbackDirection != Vector3.zero) {
			a.TakeDamage (damage, knockbackForce * knockbackDirection);
		} else {
			a.TakeDamage (damage, knockbackForce * new Vector3 ((int)wielder.faceDirection, 0, 0));
		}
	}
}
