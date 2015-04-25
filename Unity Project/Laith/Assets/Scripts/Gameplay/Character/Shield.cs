using UnityEngine;
using System.Collections;

public class Shield : MonoBehaviour {

	Gareth gareth;

	void Start () {
		gareth = transform.parent.gameObject.GetComponent<Gareth> ();
	}
	
	void Update () {
	
	}

	void OnTriggerEnter(Collider other){
		Projectile p = other.gameObject.GetComponent<Projectile> ();
		if (p != null && p.wielder.gameObject.layer == 10) {
			Physics.IgnoreCollision (p.wielder.collider, p.collider, false);
			p.rigidbody.velocity = p.rigidbody.velocity.magnitude * gareth.MouseDirection();
			p.wielder = gareth as Actor;
			p.gameObject.layer = 9;
			p.maxLifeTime = 20;
		}

	}
}
