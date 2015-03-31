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
	//void onCollissionDoor(UnityEngine.Collision hit){
	void OnTriggerEnter(Collider hit) {
		//if (hit.gameObject.DoorTrigger1 == "Baseplayer") {
		//if (hit.gameObject.tag == "Player");
			Destroy (gameObject);
			Destroy(toDestroy);
		//(-	

		
	}

}
