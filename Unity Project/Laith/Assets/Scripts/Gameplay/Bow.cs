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
			arrowPotentialSpeed += drawSpeed * Time.deltaTime;
		}
	}

	public void Release(){
		if (arrowPotentialSpeed >= arrowMinSpeed) {
			Projectile.Create (
				"Arrow",
				transform.position,
				narissa.MouseDirection () * arrowPotentialSpeed,
				arrowPotentialSpeed,
				0,
				narissa.gameObject,
				true
				);
		}

		arrowPotentialSpeed = 0;
	}

}
