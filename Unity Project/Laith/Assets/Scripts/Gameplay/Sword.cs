using UnityEngine;
using System.Collections;

public class Sword : Weapon {

	/// <summary>
	/// Script bound to the sword prefab. Controls attacking with the sword.
	/// Author: Henrik P.
	/// </summary>

	public static Object prefab = Resources.Load ("Sword");
	public Actor actor;

	private GameObject pivot;
	private Vector3 swordOffset = new Vector3 (0.6f, 0, 0);
	private int attackDirection;
	public float attackTime = 0.6f;
	public float rotationSpeed = 3;
	private float currentAttackTime = 0f;
	private bool attacking;

	//Basically a constructor.
	public static Sword Create(Actor actor){
		GameObject newSword = PhotonNetwork.Instantiate (prefab.name,  Vector3.zero, Quaternion.identity, 0) as GameObject;
		newSword.layer = actor.gameObject.layer;
		Sword sword = newSword.GetComponent<Sword> ();
		sword.actor = actor;
		sword.damage = actor.attackDamage;
		sword.knockbackForce = 5000;
		sword.enabled = true;
		
		//Pivot sets origin point so that the sword rotates around this point instead of around its center.
		sword.pivot = new GameObject ("SwordPivot");
		sword.pivot.transform.parent = actor.transform;
		sword.pivot.transform.rotation = Quaternion.Euler(0,90,0);
		
		sword.transform.parent = sword.pivot.transform;
		sword.transform.localPosition = new Vector3 (0.1f, 1, 0);
		sword.collider.enabled = false;
		
		return sword;
	}

	void Start(){
		pivot = transform.parent.gameObject;
		actor = pivot.transform.parent.gameObject.GetComponent<Actor> ();
		damage = actor.attackDamage;
		knockbackForce = 5000;
	}

	void Update () {
		if (attacking) {
			currentAttackTime += Time.deltaTime;
			pivot.transform.Rotate (new Vector3 (rotationSpeed * attackDirection, 0, 0));

			if (currentAttackTime >= attackTime) {
				attacking = false;
				collider.enabled = false;
				pivot.transform.rotation = Quaternion.Euler (0, 90, 0);
			}
		}
		
		if(!attacking) {
			pivot.transform.localPosition = new Vector3(
				(int)actor.faceDirection * swordOffset.x,
				swordOffset.y,
				swordOffset.z);
		}
	}

	public void Attack(){
		if (!attacking) {
			attacking = true;
			attackDirection = (int)actor.faceDirection;
			collider.enabled = true;
			currentAttackTime = 0;
		}
	}

}
