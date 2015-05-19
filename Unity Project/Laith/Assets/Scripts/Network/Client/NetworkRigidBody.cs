using UnityEngine;
using System.Collections;

/// <summary>
/// Network Player.
/// Author: Simon Bothen
/// </summary>
using System;


public class NetworkRigidBody : Photon.MonoBehaviour {
	private float lastSynchronizationTime = 0f;
	private float syncDelay = 0f;
	private float syncTime = 0f;
	private Vector3 syncStartPosition = Vector3.zero;
	private Vector3 syncEndPosition = Vector3.zero;
	private Quaternion syncEndRotation = Quaternion.identity;

	public void Awake() {
		//if (photonView.isMine)
			//transform.FindChild("Camera").gameObject.SetActive(true);
	}

	// Update is called once per frame
	void Update () {
		if (!photonView.isMine)
		{
			SyncedMovement();
		}
	}

	private void SyncedMovement()
	{
		syncTime += Time.deltaTime;
		if ((syncStartPosition - syncEndPosition).magnitude > 3) {
			rigidbody.position = syncEndPosition;
		} else {
			rigidbody.position = Vector3.Lerp (syncStartPosition, syncEndPosition, syncTime / syncDelay);
		}
		rigidbody.rotation = Quaternion.Lerp(rigidbody.rotation, syncEndRotation, syncTime / syncDelay);
	}
	
	void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
	{
		if (stream.isWriting)
		{
			stream.SendNext(rigidbody.position);
			stream.SendNext(rigidbody.velocity);
			stream.SendNext(rigidbody.rotation);
		}
		else
		{
			Vector3 syncPosition = (Vector3)stream.ReceiveNext();
			Vector3 syncVelocity = (Vector3)stream.ReceiveNext();
			Quaternion syncRotation = (Quaternion)stream.ReceiveNext();
			
			syncTime = 0f;
			syncDelay = Time.time - lastSynchronizationTime;
			lastSynchronizationTime = Time.time;
			
			syncEndPosition = syncPosition + syncVelocity * syncDelay;
			syncStartPosition = rigidbody.position;

			syncEndRotation = syncRotation;
		}
	}

}