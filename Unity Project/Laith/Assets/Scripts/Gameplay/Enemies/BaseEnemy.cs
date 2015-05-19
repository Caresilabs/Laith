using UnityEngine;
using System.Collections;


public class BaseEnemy :  Actor {

	// <summary>
	// Keeps track of the enemys health and destroys if dead
	// Author: Simon J
	// <summary>

	private GameObject healthBar;
	public Vector3 posOffSet;
	public BasePlayerController target;

	public virtual void Start(){
		if (!PhotonNetwork.isMasterClient)
			this.enabled = false;

		maxHealth = 100.0f;
		currentHealth = maxHealth;
		posOffSet = new Vector3 (0, 2, 0);

	}
	public void HealthBar(){
		if (healthBar != null) {
			healthBar.transform.position = transform.position + posOffSet;
			healthBar.transform.localScale = new Vector3(0.6f, currentHealth / maxHealth, 0.6f);
		} else {
			CheckForDmg();
		}
	}
	private void CheckForDmg(){
		if (currentHealth < maxHealth) {
			healthBar = PhotonNetwork.Instantiate (Resources.Load ("HealthBar").name, transform.position + posOffSet, Quaternion.Euler(new Vector3(0,0,90)), 0) as GameObject;
		}
	}
	public void CheckForDestroy(){
		if (currentHealth <= 0) {
			PhotonNetwork.Destroy (healthBar);
			GetComponent<PhotonView>().RPC ("DestroyEnemy", PhotonTargets.All);
		}
	}
	[RPC]
	public void DestroyEnemy(){
		Destroy (this.gameObject);
	}
	
	public override void Update() {
		if (!PhotonNetwork.isMasterClient)
			return;

		HealthBar ();
		CheckForDestroy ();
		base.Update ();
	}
	
}