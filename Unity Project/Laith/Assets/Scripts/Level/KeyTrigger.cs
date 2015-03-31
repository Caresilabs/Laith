using UnityEngine;
using System.Collections;

public class KeyTrigger : MonoBehaviour {

	public GameObject toDestroy;


	public bool hasKey;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

	}
	//Door remover
	void OnTriggerEnter(Collider hit) {
		if (hasKey == true) {	
			Destroy (gameObject);
			Destroy(toDestroy);
		}	
	}

}
