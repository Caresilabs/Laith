using UnityEngine;
using System.Collections;

/// <summary>
/// Network manager.
/// Author: Simon Bothen
/// </summary>
using System;


public class NetworkManager : MonoBehaviour {

	// Use this for initialization
	void Start()
	{
		PhotonNetwork.ConnectUsingSettings("0.1");

		narissa = Resources.Load ("Narissa") as GameObject;
		gareth = Resources.Load ("Gareth") as GameObject;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyUp(KeyCode.Escape) ){
			PhotonNetwork.LeaveRoom();
		}
	}

	private RoomInfo[] roomsList;
	
	void OnGUI()
	{
		if (!PhotonNetwork.connected) {
			GUILayout.Label (PhotonNetwork.connectionStateDetailed.ToString ());
		} else if (PhotonNetwork.room == null) {
			// Create Room
			if (GUI.Button (new Rect (100, 50, 250, 80), "Create New Room"))
				PhotonNetwork.CreateRoom ("#" + roomsList.Length, new RoomOptions () {isVisible = true, isOpen = true, maxPlayers = 2}, TypedLobby.Default);
			
			// Join Room
			if (roomsList != null && roomsList.Length != 0) {
				GUI.Label (new Rect (110, 170, 150, 60), "Open games");

				for (int i = 0; i < roomsList.Length; i++) {
					if (GUI.Button (new Rect (100, 200 + (110 * i), 250, 80), "Join Room " + roomsList [i].name))
						PhotonNetwork.JoinRoom (roomsList [i].name);
				}
			}
		} else {
			GUILayout.Label("Room: " + PhotonNetwork.room.name);
		}
	}
	
	void OnReceivedRoomListUpdate()
	{
		roomsList = PhotonNetwork.GetRoomList();
	}

	private GameObject narissa;
	private GameObject gareth; 

	void OnJoinedRoom()
	{
		// Spawn player
		GameObject player;

		if (PhotonNetwork.room.playerCount == 1) {
			player = PhotonNetwork.Instantiate(narissa.name, Vector3.up * 5, Quaternion.identity, 0);
		} else {
			player = PhotonNetwork.Instantiate(gareth.name, Vector3.up * 5, Quaternion.identity, 0);
		}


		player.GetComponent<BasePlayerController> ().enabled = true;
		Camera c = player.transform.FindChild ("Camera").camera;
		c.enabled = true;
	
		player.transform.FindChild("Camera").gameObject.SetActive(true);
		player.transform.FindChild ("Camera").GetComponent<AudioListener> ().enabled = true;
	}
}
