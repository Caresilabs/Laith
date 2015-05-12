using UnityEngine;
using System.Collections;

public class MeleeEnemy : BaseEnemy {

	/// <summary>
	/// Uses same code as for Gareth's sword. Attacks player if within range.
	/// Author: Henrik P.
	/// </summary>

	private Sword weapon;

	public override void Start () {
		attackDamage = 1;
		acceleration = 20f;
		maxSpeed = 5f;
		jumpSpeed = 7f;

		weapon = Sword.Create (this as Actor);

		base.Start ();
	}
	
	public override void Update () {
		AttackPlayerInFront ();
		base.Update ();
	}

	private void AttackPlayerInFront(){
		RaycastHit rch;
		int layerMask = 1 << 8;
		if(Physics.Raycast(transform.position, new Vector3((int)faceDirection, 0, 0), out rch, 2f, layerMask)){
			if(Layer.IsPlayer(rch.collider.gameObject)){
				weapon.Attack ();
			}
		}
	}

}
