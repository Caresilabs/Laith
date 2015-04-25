using UnityEngine;
using System.Collections;

public class ActivateButton : MonoBehaviour {
	
	public GameObject toActivate;
	public GameObject toMove;


	public float Xval;
	public float Yval;
	public float Zval;
	//Summary
	//ss
	//Author: Tim L
	//
	
	
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider hit) {
		if (hit.tag == "Narissa") {
			Debug.Log ("Asshat");


			toMove.transform.position = new Vector3(161, 20, 0);
		}	
	}
	
}