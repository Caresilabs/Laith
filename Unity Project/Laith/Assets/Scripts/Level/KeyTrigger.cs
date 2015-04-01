using UnityEngine;
using System.Collections;

public class KeyTrigger : MonoBehaviour {

	public GameObject toDestroy;

	//Summary
	//Class used to trigger set level keys which can be used to open doors. Can be re-used as a module.
	//Author: Tim L
	//


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

	}
	//Key Enabler & remover
	void OnTriggerEnter(Collider hit) {
		if (hit.tag == "Player" && hit.GetComponent<BasePlayerController>().HasKey == false) {
			hit.GetComponent<BasePlayerController>().HasKey = true;
			Destroy (gameObject);
			Destroy(toDestroy);
		}	
	}

}
