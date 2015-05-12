﻿using UnityEngine;
using System.Collections;

public class SoftWall : Photon.MonoBehaviour {
	
	//Summary
	//Class used to destroy a soft object
	//Author: Simon J
	//
	void Start() {
		//PhotonView view = gameObject.AddComponent<PhotonView> ();
		//view.ObservedComponents.
		//view.isRuntimeInstantiated = true;
		//view.ObservedComponents.Add (this.gameObject.transform);
	}
	
	void OnCollisionEnter(Collision entity){
		//if (!photonView.is) return;

		Gareth gareth = entity.gameObject.GetComponent<Gareth> ();
		if(gareth == null)
			return;
		if(gareth.sprint){
			GetComponent<PhotonView>().RPC ("DestroyOnNetwork", PhotonTargets.All, entity.gameObject);	

			//PhotonNetwork.Destroy (this.gameObject);
		}	
	}

	[RPC]
	public void DestroyOnNetwork(GameObject ob) {
		//if( GetComponent<PhotonView>().instantiationId==0 ) {
			Destroy(ob);
		//}
		//else {
			//if(GetComponent<PhotonView>().isMine) {
				//PhotonNetwork.Destroy(ob);
			//}
		//}
	}


}
