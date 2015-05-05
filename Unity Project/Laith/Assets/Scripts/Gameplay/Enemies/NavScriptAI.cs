using UnityEngine;
using System.Collections;

public class NavScriptAI : MonoBehaviour {

	//Summary
	//Class used to Fire Projectiles in interval
	//Author: Simon J
	//

	public Transform target;
	NavMeshAgent agent;
	// Use this for initialization
	void Start () {
		agent = GetComponent<NavMeshAgent> ();
	}
	private void FindPlayers (){
		bool found = false;
		float bestDistance = float.MaxValue;
		GameObject[] players = GameObject.FindGameObjectsWithTag ("Player");
		for (int i = 0; i < players.Length; ++i) {
			RaycastHit test;
			if(Physics.Raycast(transform.position, players[i].transform.position - transform.position, out test, 1000)){
				if(test.collider.tag == "Player"){
					float distance = Vector3.Distance (test.transform.position, transform.position);
					if(bestDistance > distance){
						bestDistance = distance;
						target = players[i].transform;
						found = true;
					}
				}
			}
		}
		if (found) {
			return;
		}
		target = null;
	}
	private void Bounderies(){
		if (transform.position.z > 0 ||
			transform.position.z < 0) {
			transform.position = new Vector3(transform.position.x, transform.position.y, 0);
		}
	}
	// Update is called once per frame
	void Update () {
		FindPlayers ();
		Bounderies ();
		if (target != null && agent.isOnOffMeshLink) {
			agent.SetDestination (target.position);
		}
	}
}
