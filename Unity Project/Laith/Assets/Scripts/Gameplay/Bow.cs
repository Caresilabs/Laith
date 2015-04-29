using UnityEngine;
using System.Collections;

public class Bow : MonoBehaviour {

	/// <summary>
	/// Script bound to the Bow prefab.
	/// Author: Henrik P.
	/// </summary>
	
	public float arrowMaxSpeed = 30;
	public float arrowMinSpeed = 10;
	public float drawBackSpeed = 30;
	public float arrowPotentialSpeed = 0;
	private Narissa narissa;
	private Vector3 offset = new Vector3(0,0.5f,0);
	private float distanceOffset = 1;
	
	public static Bow Create(Narissa narissa){
		GameObject newBow = PhotonNetwork.Instantiate ("Bow",  Vector3.zero, Quaternion.identity, 0) as GameObject;
		Bow bow = newBow.GetComponent<Bow> ();
		bow.transform.parent = narissa.transform;
		bow.transform.position = narissa.transform.position;
		bow.narissa = narissa;
		bow.enabled = true;
		
		return bow;
	}

	void Update(){
		FollowMouse ();
	}

	public void FollowMouse(){
		Vector3 bowDirection = narissa.MouseDirection ();
		transform.LookAt(transform.position + bowDirection * 10);
		transform.Rotate(90,0,0);
		transform.localPosition = offset + bowDirection * distanceOffset;
	}

	public void DrawBow(){
		if ((arrowPotentialSpeed < arrowMaxSpeed)) {
			arrowPotentialSpeed += drawBackSpeed * Time.deltaTime;
		}
	}

	public void Release(){
		if (arrowPotentialSpeed >= arrowMinSpeed) {
			GameObject arrow = PhotonNetwork.Instantiate ("Arrow", transform.position, Quaternion.identity, 0) as GameObject;
			arrow.GetComponent<Projectile> ().enabled = true;
			
			arrow.rigidbody.velocity = narissa.MouseDirection () * arrowPotentialSpeed;
			
			Weapon a = arrow.GetComponent<Weapon> ();
			a.damage = (int)arrowPotentialSpeed;
			a.wielder = narissa as Actor;
		}

		arrowPotentialSpeed = 0;
	}

}
