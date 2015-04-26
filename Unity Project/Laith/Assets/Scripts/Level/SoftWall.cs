using UnityEngine;
using System.Collections;

public class SoftWall : MonoBehaviour {
	
	//Summary
	//Class used to destroy a soft object
	//Author: Simon J
	//

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void OnCollisionEnter(Collision Entity){
		if (Entity.gameObject.tag == "Player" && Entity.gameObject.name == "Gareth(Clone)") {
			if(Entity.gameObject.GetComponent<Gareth>().sprint == true){
				Destroy (this.gameObject);
			}
		}
	}
}
