﻿using UnityEngine;
using System.Collections;

public abstract class Actor : Photon.MonoBehaviour {

	/// <summary>
	/// Includes basic info such as health, acceleration and methods for taking damage and jumping.
	/// Author: Henrik P.
	/// </summary>

	public float maxHealth;
	public float currentHealth;

	public float attackDamage;

	public float acceleration;
	public float maxSpeed;
	public float jumpSpeed;

	public float invulnerabilityFrames = 10;
	private float invulnerabilityTimer = 0;
	public bool invulnerable;

	protected bool isGrounded;

	public enum Direction{left = -1, none, right}
	public Direction faceDirection = Direction.right;

	public virtual void Update(){
		Invulnerability ();
	}

	protected void Invulnerability(){
		if (invulnerable) {
			++invulnerabilityTimer;
			if(invulnerabilityTimer >= invulnerabilityFrames){
				invulnerable = false;
				invulnerabilityTimer = 0;
			}
		}
	}

	public void TakeDamage(float damage, Vector3 knockback){
		if (invulnerable)
			return;
		
		photonView.RPC("Damage", PhotonTargets.All, damage, knockback);
		/*
		if (PhotonNetwork.isMasterClient) {
			photonView.RPC("Damage", PhotonTargets.All, damage, knockback);
			//Damage (damage, knockback);
		} else {
			photonView.RPC("Damage", PhotonTargets.MasterClient, damage, knockback);
		}
		*/
	}

	[RPC]
	public void Damage (float damage, Vector3 knockback)
	{
		//invulnerable = true;
		currentHealth -= damage;
		rigidbody.AddForce (knockback + new Vector3 (0, 1, 0));
	}

	public void Kill() {
		currentHealth = 0;
	}

	protected void Jump(){
		rigidbody.velocity = new Vector3(rigidbody.velocity.x, jumpSpeed, rigidbody.velocity.z);
		//rigidbody.AddForce(0, jumpAcceleration * rigidbody.mass, 0);
	}

	protected void CheckIfGrounded() {
		RaycastHit hit;
		if (Physics.Raycast (transform.position, -Vector3.up, out hit, collider.bounds.extents.y-0.2f)) {
			if(hit.collider.gameObject.tag != "Gareth" || hit.collider.gameObject.tag != "Narissa"){
				isGrounded = true;
			}
		} else {
			isGrounded = false;
		}
	}
}
