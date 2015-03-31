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
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	private const string roomName = "#";
	private RoomInfo[] roomsList;
	
	void OnGUI()
	{
		if (!PhotonNetwork.connected)
		{
			GUILayout.Label(PhotonNetwork.connectionStateDetailed.ToString());
		}
		else if (PhotonNetwork.room == null)
		{
			// Create Room
			if (GUI.Button(new Rect(100, 100, 250, 100), "Start Server"))
				//PhotonNetwork.CreateRoom(roomName + Guid.NewGuid().ToString("N"), true, true, 2);
				PhotonNetwork.CreateRoom("", new RoomOptions() {isVisible = true, isOpen = true, maxPlayers = 2}, TypedLobby.Default);
			
			// Join Room
			if (roomsList != null)
			{
				for (int i = 0; i < roomsList.Length; i++)
				{
					if (GUI.Button(new Rect(100, 250 + (110 * i), 250, 100), "Join " + roomsList[i].name))
						PhotonNetwork.JoinRoom(roomsList[i].name);
				}
			}
		}
	}
	
	void OnReceivedRoomListUpdate()
	{
		roomsList = PhotonNetwork.GetRoomList();
	}

	public GameObject playerPrefab;
	
	void OnJoinedRoom()
	{
		// Spawn player
		GameObject player = PhotonNetwork.Instantiate(playerPrefab.name, Vector3.up * 5, Quaternion.identity, 0);

		player.GetComponent<BasePlayerController> ().enabled = true;
	}
}
