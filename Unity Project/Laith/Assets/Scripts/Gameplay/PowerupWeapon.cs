using UnityEngine;
using System.Collections;

public class PowerupWeapon : MonoBehaviour {

	public float narissaDrawSpeed;
	public float narissaDamageModifier;
	public float garethDamageModifier;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider hit) {

		if (Layer.IsPlayer(hit.gameObject)){
			if (hit.gameObject.tag == "Gareth" && hit.GetComponent<Gareth>().upgraded != true){

				hit.GetComponent<Gareth>().upgraded = true;
			}

			else if (hit.gameObject.tag == "Narissa" && hit.GetComponent<Narissa>().upgraded != true){
				hit.GetComponentInChildren<Bow>().drawSpeed = narissaDrawSpeed;
				hit.GetComponentInChildren<Bow>().arrowDamageModifier = narissaDamageModifier;
				hit.GetComponent<Narissa>().upgraded = true;
			}
		}	
	}
}
