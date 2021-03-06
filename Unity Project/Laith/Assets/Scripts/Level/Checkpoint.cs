﻿using UnityEngine;
using System.Collections;

public class Checkpoint : MonoBehaviour {

	//Summary
	//Class used to destroy a soft object
	//Author: Simon J
	//

	private bool entered;
	// Use this for initialization
	void Start () {
		entered = false;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void OnTriggerEnter(Collider entity){
		if (Layer.IsPlayer(entity.gameObject) && !entered) {
			entered = true;
			GameObject test = GameObject.Find("_SCRIPTS");
			test.GetComponent<GameManager>().SetLastCheckpoint(this.gameObject);
		}
	}
}
