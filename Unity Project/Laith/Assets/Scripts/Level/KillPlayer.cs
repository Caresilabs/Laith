using UnityEngine;
using System.Collections;

public class KillPlayer : MonoBehaviour {
	//Summary
	//Class used to kill of player characters, and can be used as a module. 
	//Good to use together with "Insta-gib-spikes and similar traps"
	//Author: Tim L
	//

	public GameObject instagibs;
	public float Xvalue;
	public float Yvalue;
	public float Zvalue;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider hit) {
		if (Layer.IsPlayer(hit.gameObject)) 
		{
			Actor a = hit.gameObject.GetComponent<Actor>();
			a.currentHealth = 0;


		}	
	}
}
