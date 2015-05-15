using UnityEngine;
using System.Collections;

public class PowerupWeapon : MonoBehaviour {

	/// <summary>
	/// Script that provides each character with specific upgrades to their weapons.
	/// Narissa draws her bow faster and her arrows do more damage.
	/// Gareth swings his sword faster and does more damage.
	/// Author: Andreas Karlsson.
	/// </summary>

	public float narissaDrawSpeed;
	public float narissaDamageModifier;
	public float garethDamageModifier;
	
	void OnTriggerEnter(Collider hit) {

		if (Layer.IsPlayer(hit.gameObject)){

			if (hit.gameObject.tag == "Gareth" && hit.GetComponent<Gareth>().upgraded != true){
				hit.GetComponentInChildren<Sword>().damage *= garethDamageModifier;
				hit.GetComponentInChildren<Sword>().rotationSpeed = 5.0f;
				hit.GetComponentInChildren<Sword>().attackTime = 0.4f;
				hit.GetComponent<Gareth>().upgraded = true;
				GetComponent<PhotonView>().RPC ("DestroyOnNetwork", PhotonTargets.AllBuffered);
			}

			else if (hit.gameObject.tag == "Narissa" && hit.GetComponent<Narissa>().upgraded != true){
				hit.GetComponentInChildren<Bow>().drawSpeed = narissaDrawSpeed;
				hit.GetComponentInChildren<Bow>().arrowDamageModifier = narissaDamageModifier;
				hit.GetComponent<Narissa>().upgraded = true;
				GetComponent<PhotonView>().RPC ("RemovePowerup", PhotonTargets.AllBuffered);
			}
		}	
	}

	[RPC]
	public void RemovePowerup() {
		Destroy(this.gameObject);
	}

}
