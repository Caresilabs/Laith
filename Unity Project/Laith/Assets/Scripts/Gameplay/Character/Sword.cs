using UnityEngine;
using System.Collections;

public class Sword : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider other){
		if (other.gameObject.tag != "Enemy" || other.gameObject.collider.isTrigger) {
			return;
		} else {
			//Destroy (other.gameObject);
			//Enemy e = other.GetComponent<Enemy>();
			//e.TakeDamage(damage);
		}
		
		
	}
}
