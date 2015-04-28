using UnityEngine;
using System.Collections;

public class Weapon : Photon.MonoBehaviour {

	/// <summary>
	/// Checks for collision and deals damage if appropriate. Extend this class for anything that deals damage upon contact.
	/// Author: Henrik P.
	/// </summary>

	public float damage;
	public float knockbackForce;
	public Actor wielder;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	}

	public virtual void OnTriggerEnter(Collider other){
		if (other.isTrigger == true) {
			return;
		}

		if (wielder == null || 
		    wielder.gameObject.layer == 8 && other.gameObject.layer == 10 || 
		    wielder.gameObject.layer == 10 && other.gameObject.layer == 8) {
			Actor a;
			a = other.GetComponent<Actor>();
			if (a != null)
				DealDamage (a);
		}
	}

	public virtual void DealDamage(Actor a){
		a.TakeDamage (damage, knockbackForce * new Vector3((int)wielder.faceDirection,0,0));
	}
}
