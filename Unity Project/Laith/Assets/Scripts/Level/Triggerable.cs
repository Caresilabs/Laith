using UnityEngine;
using System.Collections;

public abstract class Triggerable : MonoBehaviour {

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
