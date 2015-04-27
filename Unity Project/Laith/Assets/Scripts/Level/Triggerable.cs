using UnityEngine;
using System.Collections;

public abstract class Triggerable : MonoBehaviour {

	/// <summary>
	/// Extend this class for anything with an on/off state.
	/// Author: Henrik P.
	/// </summary>

	public bool playerActivated;
	public bool activated;

	public virtual void Trigger (){
		if (activated) {
			Deactivate ();
			return;
		}
		Activate();
	}

	public virtual void Activate (){
		activated = true;
	}

	public virtual void Deactivate(){
		activated = false;
	}

}
