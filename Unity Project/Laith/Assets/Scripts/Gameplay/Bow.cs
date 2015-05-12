using UnityEngine;
using System.Collections;

public class Bow : MonoBehaviour {

	/// <summary>
	/// Script bound to the Bow prefab. Follows cursor, fires arrows.
	/// Author: Henrik P.
	/// </summary>
	
	public float arrowMaxSpeed = 30;
	public float arrowMinSpeed = 10;
	public float drawSpeed = 40;
	public float arrowPotentialSpeed = 0;
	private Narissa narissa;
	private Vector3 offset = new Vector3(0,0.5f,0);
	private float distanceOffset = 1;

	private Projectile arrow;
	
	public static Bow Create(Narissa narissa){
		GameObject newBow = PhotonNetwork.Instantiate ("Bow",  Vector3.zero, Quaternion.identity, 0) as GameObject;
		Bow bow = newBow.GetComponent<Bow> ();
		bow.transform.parent = narissa.transform;
		bow.transform.position = narissa.transform.position;
		bow.narissa = narissa;
		bow.enabled = true;
		
		return bow;
	}

	void Start(){
		narissa = transform.parent.gameObject.GetComponent<Narissa> ();
		
	}

	void Update(){
		FollowMouse ();
	}

	public void FollowMouse(){
		Vector3 bowDirection = narissa.mouseDirection;
		transform.LookAt(transform.position + bowDirection * 10);
		transform.Rotate(0,-90,0);
		transform.localPosition = offset + bowDirection * distanceOffset;
	}

	public void DrawBow(){
		if ((arrowPotentialSpeed < arrowMaxSpeed)) {
			arrowPotentialSpeed += drawSpeed * Time.deltaTime;
		}

		if (arrow == null) {
			arrow = Projectile.Create (
				"Arrow",
				transform.position + narissa.mouseDirection * 1,
				narissa.mouseDirection,
				arrowPotentialSpeed,
				0,
				narissa.gameObject,
				true
			);
			arrow.collider.enabled = false;
		} else {
			arrow.rigidbody.velocity = narissa.mouseDirection;
			arrow.transform.position = transform.position + narissa.mouseDirection * (1 + arrowPotentialSpeed/arrowMaxSpeed * -1.4f);
		}
	}

	public void Release(){
		if (arrowPotentialSpeed >= arrowMinSpeed) {
			arrow.collider.enabled = true;
			arrow.rigidbody.velocity = narissa.mouseDirection * arrowPotentialSpeed;
			arrow.damage = arrowPotentialSpeed;
			arrow = null;
		} else {
			Destroy(arrow.gameObject);
		}

		arrowPotentialSpeed = 0;
	}

}
