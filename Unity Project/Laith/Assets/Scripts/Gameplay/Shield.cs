using UnityEngine;
using System.Collections;

public class Shield : Weapon {

	/// <summary>
	/// Script bound to the shield prefab. Contains all shield logic.
	/// Author: Henrik P.
	/// </summary>

	public static Object prefab = Resources.Load ("Shield");
	private Vector3 shieldOffset = new Vector3(0, 0.5f, 0);
	private float shieldDistance = 1.5f;
	private Gareth gareth;

	public static Shield Create(Gareth gareth){
		GameObject newShield = PhotonNetwork.Instantiate (prefab.name,  Vector3.zero, Quaternion.identity, 0) as GameObject;
		newShield.layer = gareth.gameObject.layer;
		Shield shield = newShield.GetComponent<Shield> ();
		shield.transform.parent = gareth.transform;
		shield.gareth = gareth;
		shield.enabled = true;
		shield.wielder = gareth.gameObject;

		shield.damage = 50;
		shield.knockbackForce = 100000;

		return shield;
	}

	void Start(){
		gareth = transform.parent.gameObject.GetComponent<Gareth> ();
		wielder = gareth.gameObject;

		damage = 50;
		knockbackForce = 100000;
	}

	public void ShieldUp(Vector3 direction){
		Vector3 shieldDirection = direction;

		transform.LookAt(transform.position + shieldDirection * 10);
		transform.Rotate(90,0,0);
		transform.position = transform.parent.position + shieldOffset + shieldDirection * shieldDistance;
	}

	public void ShieldDown(){
		transform.rotation = Quaternion.Euler (90, 0, 0);
		transform.position = transform.parent.position + new Vector3(0, 0, 1) * shieldDistance;
	}

	void DeflectProjectile(Projectile p){

			if(!p.deflectable){
				p.maxLifeTime = 0;
				return;
			}
			p.rigidbody.velocity = p.rigidbody.velocity.magnitude * gareth.mouseDirection;
			p.damage = 20;
			p.gameObject.layer = 8;
			p.maxLifeTime = 20;
			try{
				Physics.IgnoreCollision (p.wielder.collider, p.collider, false);
			} catch(MissingComponentException){
				Debug.Log ("Object does not have collider.");
			}
			p.wielder = gareth.gameObject;
			return;
	}

	public override void OnTriggerEnter(Collider other){
		Projectile p = other.gameObject.GetComponent<Projectile> ();
		if (p != null && p.wielder.gameObject.layer != 8) {
			DeflectProjectile (p);
		}

		if (gareth.sprint) {
			Actor a = other.gameObject.GetComponent<Actor> ();
			if (a == null)
				return;
			
			if (gameObject.layer != other.gameObject.layer){
				DealDamage (a);
				gareth.currentSprintTime = gareth.sprintTime;
			}
		}
	}

}
