using UnityEngine;
using System.Collections;
using System.Linq;


//Summary
//This scipt can trigger items, move objects and remove others.
//Author: Tim L
//

public class ActivateButton : Photon.MonoBehaviour {
	
	public GameObject toKill;
	public GameObject toMove;

	public float Xval;
	public float Yval;
	public float Zval;

	void OnTriggerEnter(Collider hit) {
		//if (Layer.FindGameObjectsWithLayer(Layer.players).AsQueryable().Any(x => x.layer == Layer.players && hit.gameObject == x)) {//hit.tag == "Player") {
		if (Layer.IsPlayer(hit.gameObject)) {
			photonView.RPC("Trigger", PhotonTargets.All);
		}	
	}

	[RPC]
	public void Trigger() {
		toMove.transform.position = new Vector3(Xval, Yval, Zval);
		Destroy(toKill);
	}
	
}