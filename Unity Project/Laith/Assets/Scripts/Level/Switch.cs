using UnityEngine;
using System.Collections;

public class Switch : Triggerable {

	public Triggerable connectedTrigger;

	void Start () {
		playerActivated = true;
	}

	void Update(){
		if (activated) {
			gameObject.name = "Active";
		} else {
			gameObject.name = "Inactive";
		}
	}

	public override void Activate (){
		connectedTrigger.Trigger ();
		activated = connectedTrigger.activated;
	}
	
	public override void Deactivate(){
		connectedTrigger.Trigger ();
		activated = connectedTrigger.activated;
	}

}
