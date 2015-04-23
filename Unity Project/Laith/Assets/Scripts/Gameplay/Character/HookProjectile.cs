using UnityEngine;
using System.Collections;

public class HookProjectile : Photon.MonoBehaviour {

	public GameObject shooter;
	public LineRenderer line;
	public GameObject hookedObject;
	private Narissa n;
	private float maxLifeTime;
	private float lifeTime = 0;
	private bool hooked;

	void OnTriggerEnter(Collider other){
		if (!photonView.isMine)
			return;

		if (other.isTrigger) {
			return;
		} else if (other.gameObject.tag == "Hookable" || shooter == null) {
			AttachHook(other.gameObject);
		} else if (other.gameObject.tag == "Enemy"){

		} else {
			n.DestroyHook ();
		}

	}

	void AttachHook(GameObject hookedObject){
		this.hookedObject = hookedObject;

		n.hooked = true;
		n.joint.maxDistance = (n.transform.position - transform.position).magnitude;
		
		hooked = true;

		CharacterJoint joint = gameObject.AddComponent<CharacterJoint> ();
		joint.connectedBody = hookedObject.rigidbody;
	}

	void Start(){
		if (shooter == null)
			shooter = GameObject.Find ("Narissa(Clone)");

		n = shooter.GetComponent<Narissa> ();
		if (!photonView.isMine)
			return;

		maxLifeTime = n.hookRange/rigidbody.velocity.magnitude;
	}

	void Update(){
		line.SetPosition (0, shooter.transform.position);
		line.SetPosition (1, transform.position);
		
		if (!photonView.isMine)
			return;
		
		if (hooked) {
			return;
		}
		if (lifeTime >= maxLifeTime) {
			n.DestroyHook();
		}
		lifeTime += Time.deltaTime;
		transform.rotation = Quaternion.LookRotation (rigidbody.velocity);
		transform.Rotate (90, 0, 0);
	}
	
}
