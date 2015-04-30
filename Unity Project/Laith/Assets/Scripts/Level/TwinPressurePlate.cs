using UnityEngine;
using System.Collections;

public class TwinPressurePlate : MonoBehaviour {

	public Triggerable connectedTrigger1;
	public Triggerable connectedTrigger2;
	public TwinPressurePlate other;

	public bool pushed;
	bool pushing;
	float translateSpeed = 1f;
	float springDelay;

	float localHeight = 0;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (pushing) {
			springDelay = 10;
			if(localHeight > -0.3f){
				transform.Translate (0, -translateSpeed * Time.deltaTime, 0);
				localHeight -= translateSpeed * Time.deltaTime;
			} else {
				pushed = true;
			}
		} else if (springDelay <= 0) {
			if(localHeight < 0){
				transform.Translate (0, translateSpeed * Time.deltaTime, 0);
				localHeight += translateSpeed * Time.deltaTime;
			}
			pushed = false;
		}

		--springDelay;

		if (pushed && other.pushed) {
			connectedTrigger1.Trigger();
			connectedTrigger2.Trigger();
			Destroy (this);
		}
	}

	void OnCollisionStay(Collision collisionInfo){
		if (collisionInfo.gameObject.layer != 8)
			return;
		BasePlayerController bpc = collisionInfo.gameObject.GetComponent<BasePlayerController> ();
		if (bpc.transform.position.y > transform.position.y) {
			pushing = true;
		}
	}

	void OnCollisionExit(Collision collisionInfo){
		pushing = false;
	}

}
