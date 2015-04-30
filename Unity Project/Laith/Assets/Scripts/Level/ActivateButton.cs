using UnityEngine;
using System.Collections;

public class ActivateButton : MonoBehaviour {
	
	public GameObject toKill;
	public GameObject toMove;


	public float Xval;
	public float Yval;
	public float Zval;
	//Summary
	//This scipt can trigger items, move objects and remove others.
	//Author: Tim L
	//
	
	
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider hit) {
		if (hit.tag == "Player") {
			Debug.Log ("Asshat");


			toMove.transform.position = new Vector3(Xval, Yval, Zval);
			Destroy(toKill);
			//161,20,0
		}	
	}
	
}