using UnityEngine;
using System.Collections;

public class DumbTurret : MonoBehaviour {

	//Summary
	//Class used to Fire Projectiles in interval
	//Author: Simon J
	//

	public bool interval;
	public float intervalTime;
	private float currentIntervalTimer;

	private bool delay;
	public float delayTime;
	private float currentDelayTimer;

	public float fireRate;
	private float currentFireTimer;

	public bool triggerable;
	public GameObject objectToTrigger;

	public GameObject projectile;
	public float projectileSpeed;
	public float projectileLifeTime;
	public float projectileDamage;

	// Use this for initialization
	void Start () {
		currentIntervalTimer = 0;
		currentFireTimer = 0;
		currentDelayTimer = 0;
	}
	private void ShootProjectile(){
		GameObject test = PhotonNetwork.Instantiate (projectile.name , transform.position, transform.rotation, 0) as GameObject;
		test.layer = 1;
		test.transform.parent = transform;
		test.transform.parent = GameObject.Find ("_EnemyProjectiles").transform;
		//Physics.IgnoreCollision (collider, test.collider);

		test.GetComponent<Projectile>().enabled = true;
		Vector2 test2 = new Vector2 (Mathf.Cos ((transform.eulerAngles.z + 90) * Mathf.PI / 180.0f), Mathf.Sin ((transform.eulerAngles.z + 90) * Mathf.PI / 180.0f));

		test.rigidbody.velocity = new Vector3(test2.x, test2.y, 0) * projectileSpeed;//transform.eulerAngles * projectileSpeed;
		test.rigidbody.useGravity = false;
		
		Projectile p = test.GetComponent<Projectile> ();
		p.damage = projectileDamage;
		p.wielder = null;
		p.maxLifeTime = projectileLifeTime;

	}
	private void FireRateTimer(){
		currentFireTimer += Time.deltaTime;
		if (currentFireTimer >= fireRate) {
			ShootProjectile();
			currentFireTimer = 0;
		}
	}
	private void IntervalTimer(){
		currentIntervalTimer += Time.deltaTime;
		if (currentIntervalTimer >= intervalTime) {
			delay = true;
			currentIntervalTimer = 0;
		}
	}
	private void DelayTimer(){
		currentDelayTimer += Time.deltaTime;
		if (currentDelayTimer >= delayTime) {
			delay = false;
			currentDelayTimer = 0;
			currentFireTimer = 0;
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (!PhotonNetwork.isMasterClient)
			return;

		if (GameObject.FindGameObjectWithTag("Player") != null) {
			if (!delay || !interval) {
				FireRateTimer ();
				if(interval){
					IntervalTimer ();
				}
			} else {
				DelayTimer ();
			}
		}
	}
}
