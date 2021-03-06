﻿using UnityEngine;
using System.Collections;

public class Switch : Triggerable {

	public Triggerable connectedTrigger;

	/// <summary>
	/// Toggles connected trigger when activated.
	/// Author: Henrik P.
	/// </summary>

	public override void Activate (){
		connectedTrigger.Trigger ();
		activated = connectedTrigger.activated;
	}
	
	public override void Deactivate(){
		connectedTrigger.Trigger ();
		activated = connectedTrigger.activated;
	}

}
