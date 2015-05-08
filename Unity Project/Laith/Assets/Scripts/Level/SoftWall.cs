using UnityEngine;
using System.Collections;

public class SoftWall : Photon.MonoBehaviour {
	
	//Summary
	//Class used to destroy a soft object
	//Author: Simon J
	//
	void Start() {
		PhotonView view = gameObject.AddComponent<PhotonView> ();
		//view.ObservedComponents.
		view.isRuntimeInstantiated = true;
		view.ObservedComponents.Add (this.gameObject.transform);
	}
	
	void OnCollisionEnter(Collision entity){
		if (!photonView.isMine) return;

		Gareth gareth = entity.gameObject.GetComponent<Gareth> ();
		if(gareth == null)
			return;
		if(gareth.sprint){
				PhotonNetwork.Destroy (this.gameObject);
			}
	}
}
