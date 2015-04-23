using UnityEngine;
using System.Collections;


public abstract class BaseEnemy :  Actor {

	private GameObject healthBar;
	public Vector3 posOffSet;

	public virtual void Start(){
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
			Destroy(this.gameObject);
		}
	}
	
	public virtual void Update() {
		HealthBar ();
		CheckForDestroy ();
	}
	
}