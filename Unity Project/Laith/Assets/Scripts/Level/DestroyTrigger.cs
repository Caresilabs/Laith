using UnityEngine;
using System.Collections;

public class DestroyTrigger : MonoBehaviour {

	public GameObject toDestroy;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

	}
	//Door remover
	void OnTriggerEnter(Collider hit) {
		if (hit.tag == "Player" && hit.GetComponent<BasePlayerController>().hasKey == true) {

			Destroy (gameObject);
			Destroy(toDestroy);
		}	
	}



}
