using UnityEngine;
using System.Collections;

public class Shield : MonoBehaviour {

	/// <summary>
	/// Script bound to the shield prefab. Contains all shield logic.
	/// Author: Henrik P.
	/// </summary>

	public static Object prefab = Resources.Load ("Shield");
	private Vector3 shieldOffset = new Vector3(0, 0.5f, 0);
	private float shieldDistance = 1;
	private Gareth gareth;

	public static Shield Create(Gareth gareth){
		GameObject newShield = PhotonNetwork.Instantiate (prefab.name,  Vector3.zero, Quaternion.identity, 0) as GameObject;
		Shield shield = newShield.GetComponent<Shield> ();
		shield.transform.parent = gareth.transform;
		shield.gareth = gareth;
		shield.enabled = true;

		return shield;
	}

	public void ShieldUp(Vector3 direction){
		Vector3 shieldDirection = direction;
		if(Input.mousePosition.y <= Camera.main.WorldToScreenPoint(transform.position).y){
			shieldDirection = new Vector3((int)gareth.faceDirection,0,0);
		}

		transform.LookAt(transform.position + shieldDirection * 10);
		transform.Rotate(90,0,0);
		transform.position = transform.parent.position + shieldOffset + shieldDirection * shieldDistance;
	}

	public void ShieldDown(){
		transform.rotation = Quaternion.Euler (90, 0, 0);
		transform.position = transform.parent.position + new Vector3(0, 0, 1) * shieldDistance;
	}

	void OnTriggerEnter(Collider other){
		Projectile p = other.gameObject.GetComponent<Projectile> ();
		if (p != null && p.wielder.gameObject.layer == 10) {
			Physics.IgnoreCollision (p.wielder.collider, p.collider, false);
			p.rigidbody.velocity = p.rigidbody.velocity.magnitude * gareth.MouseDirection();
			p.wielder = gareth as Actor;
			p.gameObject.layer = 9;
			p.maxLifeTime = 20;
			return;
		}

	}
}
