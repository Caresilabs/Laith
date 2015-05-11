using UnityEngine;
using System.Collections;

public class DumbTurret : Triggerable {

	//Summary
	//Class used to Fire Projectiles in interval
	//Author: Simon J
	//

	public bool fireInVolleys;
	public int shotsPerVolley;
	private int remainingShotsInVolley;

	private bool waitForNextVolley;
	public float secondsBetweenVolleys;							//Time between volleys
	private float secondsToNextVolley;

	public float secondsBetweenShots;				//Time between shots
	private float secondsToNextShot;
	
	public GameObject projectile;
	public float projectileSpeed;
	public float projectileLifeTime;
	public float projectileDamage;

	// Use this for initialization
	void Start () {
		remainingShotsInVolley = 0;
		secondsToNextShot = 0;
		secondsToNextVolley = 0;
	}
	private void ShootProjectile(){
		Projectile p = Projectile.Create (
			projectile.name,
			transform.position,
			projectileSpeed * transform.up,
			projectileDamage,
			projectileLifeTime,
			gameObject,
			false
			);
		p.transform.parent = gameObject.transform;
		p.gameObject.name = "Trap Arrow";
		if (fireInVolleys) {
			TrackVolleyCount();
		}
	}
	private void FireRateTimer(){
		secondsToNextShot += Time.deltaTime;
		if (secondsToNextShot >= secondsBetweenShots) {
			ShootProjectile();
			secondsToNextShot = 0;
		}
	}
	private void TrackVolleyCount(){
		--remainingShotsInVolley;
		if(remainingShotsInVolley <= 0){
			waitForNextVolley = true;
			remainingShotsInVolley = shotsPerVolley;
		}
	}
	private void DelayTimer(){
		secondsToNextVolley += Time.deltaTime;
		if (secondsToNextVolley >= secondsBetweenVolleys) {
			waitForNextVolley = false;
			secondsToNextVolley = 0;
			secondsToNextShot = 0;
		}
	}
	
	void Update () {
		if (!PhotonNetwork.isMasterClient)
			return;

		if (!activated)
			return;

		if (GameObject.FindGameObjectWithTag("Player") != null) {
			if (!waitForNextVolley || !fireInVolleys) {
				FireRateTimer ();
			} else {
				DelayTimer ();
			}
		}
	}
}
