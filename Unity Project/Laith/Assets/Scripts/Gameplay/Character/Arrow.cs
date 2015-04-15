using UnityEngine;
using System.Collections;

public class Arrow : MonoBehaviour {

	float damage;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		rigidbody.rotation = Quaternion.LookRotation (rigidbody.velocity);
	}

	void OnTriggerEnter(Collider other){
		if (other.gameObject.collider.isTrigger) {
			return;
		} else if (other.gameObject.tag != "Enemy") {
			Destroy (this.gameObject);
			return;
		}

		//Enemy e = other.GetComponent<Enemy>();
		//e.TakeDamage(damage);
	}

}
