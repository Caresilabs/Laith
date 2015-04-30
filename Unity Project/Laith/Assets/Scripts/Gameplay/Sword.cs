﻿using UnityEngine;
using System.Collections;

public class Sword : Weapon {

	/// <summary>
	/// Script bound to the sword prefab. Controls attacking with the sword.
	/// Author: Henrik P.
	/// </summary>

	public static Object prefab = Resources.Load ("Sword");

	private GameObject pivot;
	private Vector3 swordOffset = new Vector3 (0.6f, 0, 0);
	private int attackDirection;
	private float attackTime = 0.6f;
	private float rotationSpeed = 3;
	private float currentAttackTime = 0f;
	private bool attacking;

	//Basically a constructor.
	public static Sword Create(Actor wielder){
		GameObject newSword = PhotonNetwork.Instantiate (prefab.name,  Vector3.zero, Quaternion.identity, 0) as GameObject;
		Sword sword = newSword.GetComponent<Sword> ();
		sword.wielder = wielder;
		sword.damage = wielder.attackDamage;
		sword.knockbackForce = 100;
		sword.enabled = true;
		
		//Pivot sets origin point so that the sword rotates around this point instead of around its center.
		sword.pivot = new GameObject ("SwordPivot");
		sword.pivot.transform.parent = wielder.transform;
		sword.pivot.transform.rotation = Quaternion.Euler(0,90,0);
		
		sword.transform.parent = sword.pivot.transform;
		sword.transform.localPosition = new Vector3 (0.1f, 1, 0);
		sword.collider.enabled = false;
		
		return sword;
	}

	public void Attack(){
		if (!attacking) {
			attacking = true;
			attackDirection = (int)wielder.faceDirection;
			collider.enabled = true;
			currentAttackTime = 0;
		}
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
				(int)wielder.faceDirection * swordOffset.x,
				swordOffset.y,
				swordOffset.z);
		}
	}
}
