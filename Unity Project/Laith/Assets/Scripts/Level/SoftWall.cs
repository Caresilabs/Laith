using UnityEngine;
using System.Collections;

public class SoftWall : MonoBehaviour {
	
	//Summary
	//Class used to destroy a soft object
	//Author: Simon J
	//
	
	void OnCollisionEnter(Collision entity){
		Gareth gareth = entity.gameObject.GetComponent<Gareth> ();
		if(gareth == null)
			return;
		if(gareth.sprint){
				Destroy (this.gameObject);
			}
	}
}
