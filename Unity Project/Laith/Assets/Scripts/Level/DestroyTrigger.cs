using UnityEngine;
using System.Collections;

public class DestroyTrigger : MonoBehaviour {

	//Summary
	//Class used to trigger and open a certain door. Can be used in different locations.
	//Author: Tim L
	//

	public GameObject toDestroy;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

	}
	//Door remover
	void OnTriggerEnter(Collider hit) {
		if (Layer.IsPlayer(hit.gameObject) && hit.GetComponent<BasePlayerController>().HasKey == true) 
		{

			Destroy (gameObject);
			Destroy(toDestroy);
		}	
	}



}
