using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour {

	/// <summary>
	/// Periodically spawns specified prefab.
	/// Author: Henrik P.
	/// </summary>

	public Vector3 spawnPosition;
	public float spawnInterval;
	public GameObject objectToSpawn;
	private float spawnTimer = 0;
	
	void Update () {
		if (spawnTimer <= 0) {
			Spawn();
			spawnTimer = spawnInterval;
		}
		spawnTimer -= Time.deltaTime;
	}

	void Spawn(){
		//GameObject boulder = PhotonNetwork.Instantiate (objectToSpawn.name,  spawnPosition, Quaternion.identity, 0) as GameObject;
		PhotonNetwork.Instantiate (objectToSpawn.name, spawnPosition, Quaternion.identity, 0);
	}
}
