using UnityEngine;
using System.Collections;

public class Narissa : BasePlayerController {

	public float hookLength = 100f;

	private SpringJoint hook;

	public void Start() {
		MaxJumps = 2;
	}

	public override void Update () {
		if (Input.GetKeyDown (KeyCode.Mouse0)) {
			Hook ();
		}

		if (hook != null) {
			Debug.DrawRay (hook.anchor, hook.connectedAnchor - hook.anchor, Color.red, hook.maxDistance);

			//var target = hook.connectedBody.transform.TransformPoint(hook.connectedBody.transform.position);
			var distance = Vector3.Distance(rigidbody.position, hook.connectedBody.position);

			if (distance < 2) {
				Destroy(hook);
				hook = null;
			}

		}

		base.Update ();
	}

	void Hook(){
		RaycastHit raycastHit;
		Vector3 mouseDirection = Input.mousePosition - Camera.main.WorldToScreenPoint(rigidbody.transform.position);
		mouseDirection.Normalize ();

		if(Physics.Raycast(rigidbody.transform.position, mouseDirection, out raycastHit, hookLength)){
			///if (raycastHit.rigidbody != null && raycastHit.rigidbody.tag == "Terrain") return;
			if (raycastHit.rigidbody == null) return;

			SpringJoint tempJoint;
			//rigidbody.transform.position = raycastHit.point;
			tempJoint = this.gameObject.AddComponent<SpringJoint>();
			tempJoint.connectedBody = raycastHit.rigidbody;
			tempJoint.maxDistance = 0;
			tempJoint.minDistance = 0f;
			tempJoint.spring = 1f;
			tempJoint.damper = 0f;


			hook = tempJoint;
		}
		//Debug.DrawRay (rigidbody.transform.position, mouseDirection * 100, Color.red, 10f);
			
	}
}
