using UnityEngine;
using System.Collections;

public class Weapon : Photon.MonoBehaviour {

	public float damage;
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
		if (wielder.gameObject.layer == 8 && other.gameObject.layer == 10) {
			BaseEnemy e = other.GetComponent<BaseEnemy>();
			e.TakeDamage (damage);
		} else if (wielder.gameObject.layer == 10 && other.gameObject.layer == 8) {
			BasePlayerController p = other.GetComponent<BasePlayerController>();
			p.TakeDamage (damage);
		}
	}
}
