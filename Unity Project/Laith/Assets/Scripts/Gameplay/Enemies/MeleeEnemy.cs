using UnityEngine;
using System.Collections;

public class MeleeEnemy : BaseEnemy {

	private GameObject weaponPivot;
	public GameObject weapon;
	public Vector3 weaponOffset = new Vector3 (0.6f, 0, 0);

	private int attackDirection;
	private float attackTime = 1f;
	private float currentAttackTime = 0f;
	private bool attacking;
	private float angle = 0;

	public override void Start () {
		attackDamage = 1;
		acceleration = 20f;
		maxSpeed = 5f;
		jumpSpeed = 7f;

		weaponPivot = new GameObject ("weaponPivot");
		weaponPivot.transform.parent = transform;
		weaponPivot.transform.localPosition = new Vector3((int)faceDirection* weaponOffset.x,weaponOffset.y,weaponOffset.z);
		weaponPivot.transform.rotation = Quaternion.Euler(0,90,0);
		
		//weapon = PhotonNetwork.Instantiate (Resources.Load("Sword").name,  Vector3.zero, Quaternion.identity, 0) as GameObject;
		weapon.transform.parent = weaponPivot.transform;
		weapon.transform.localPosition = new Vector3 (0.1f, 1, 0);
		weapon.collider.enabled = false;
		weapon.layer = 1;
		Weapon w = weapon.GetComponent<Weapon> ();
		w.damage = 1f;
		w.wielder = this as Actor;
		base.Start ();
	}
	
	public override void Update () {
		AttackPlayerInFront ();
		Attacking ();
		base.Update ();
	}

	private void AttackPlayerInFront(){
		if (attacking)
			return;
		RaycastHit rch;
		int layerMask = 1 << 8;
		if(Physics.Raycast(transform.position, new Vector3((int)faceDirection, 0, 0), out rch, 2f, layerMask)){
			if(rch.collider.gameObject.tag == "Player"){
				Attack ();
			}
		}
	}

	public void Attack(){
		attacking = true;
		attackDirection = (int)faceDirection;
		weapon.collider.enabled = true;
		currentAttackTime = 0;
	}

	private void Attacking(){
		if (attacking) {
			currentAttackTime += Time.deltaTime;
			angle += 3;
			weaponPivot.transform.Rotate (new Vector3 (3 * attackDirection, 0, 0));
			if (currentAttackTime >= attackTime) {
				attacking = false;
				weapon.collider.enabled = false;
				weaponPivot.transform.rotation = Quaternion.Euler (0, 90, 0);
			}
		}
		if(!attacking) {
			weaponPivot.transform.localPosition = new Vector3((int)faceDirection* weaponOffset.x,weaponOffset.y,weaponOffset.z);
		}
	}
}
