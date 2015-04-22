using UnityEngine;
using System.Collections;

public class Projectile : Weapon {

	private float maxLifeTime = 20;

	void Update () {
		maxLifeTime -= Time.deltaTime;
		if (maxLifeTime <= 0)
			PhotonNetwork.Destroy (this.gameObject);

		rigidbody.transform.rotation = Quaternion.LookRotation (rigidbody.velocity);
		rigidbody.transform.Rotate (90, 0, 0);
	}

	public override void OnTriggerEnter(Collider other){
		if (other.isTrigger == true) {
			return;
		} else {
			PhotonNetwork.Destroy (this.gameObject);
		}
//		if (wielder.gameObject.tag == "Player" && other.gameObject.tag == "Enemy") {
//			BaseEnemy e = other.GetComponent<BaseEnemy>();
//			e.TakeDamage (damage);
//		} else if (wielder.gameObject.tag == "Enemy" && other.gameObject.tag == "Player") {
//			BasePlayerController p = other.GetComponent<BasePlayerController>();
//			p.TakeDamage (damage);
//		}
		base.OnTriggerEnter (other);
	}
}
