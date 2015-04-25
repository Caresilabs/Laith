using UnityEngine;
using System.Collections;

public class ActivateButton : MonoBehaviour {
	
	public GameObject toActivate;
	public GameObject toMove;
	public bool isActivated;

	public float Xval;
	public float Yval;
	public float Zval;
	//Summary
	//
	//Author: Tim L
	//
	
	
	// Use this for initialization
	void Start () {
		isActivated = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	//Key Enabler & remover
	void OnTriggerEnter(Collider hit) {
		if (hit.tag == "Narissa") {
			isActivated = true;

			toMove.rigidbody.position = new Vector3(Xval, Yval, Zval);
		}	
	}
	
}