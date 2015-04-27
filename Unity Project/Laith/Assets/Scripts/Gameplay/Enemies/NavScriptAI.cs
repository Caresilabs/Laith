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
		GameObject[] players = GameObject.FindGameObjectsWithTag ("Player");
		float distance = 1000;
		for (int i = 0; i < players.Length; ++i) {
			float testDistance = Vector3.Distance(players[i].transform.position, transform.position);
			if(testDistance < distance){
				target = players[i].transform;
			}
		}
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
		if (target != null) {
			agent.SetDestination (target.position);
		}
	}
}
