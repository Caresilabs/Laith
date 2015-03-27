using UnityEngine;
using System.Collections;

public class Narissa : BasePlayer {

	public float hookLength = 100f;

	public override void Update () {
		if (Input.GetKeyDown (KeyCode.Mouse0)) {
			Hook ();
		}

		base.Update ();
	}

	void Hook(){
		RaycastHit raycastHit;
		Vector3 mouseDirection = Input.mousePosition - Camera.main.WorldToScreenPoint(rigidbody.transform.position);
		if(Physics.Raycast(rigidbody.transform.position, mouseDirection, out raycastHit, hookLength)){
			SpringJoint tempJoint;
			rigidbody.transform.position = raycastHit.point;
			tempJoint = this.gameObject.AddComponent<SpringJoint>();
			tempJoint.connectedBody = raycastHit.rigidbody;
			tempJoint.maxDistance = 0f;
			tempJoint.minDistance = 0f;
			tempJoint.spring = 1f;
			tempJoint.damper = 0f;
		}
		Debug.DrawRay (rigidbody.transform.position, mouseDirection * 100, Color.red, 10f);
			
	}
}
