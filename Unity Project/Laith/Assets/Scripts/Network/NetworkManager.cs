using UnityEngine;
using System.Collections;

/// <summary>
/// Network manager.
/// Author: Simon Bothen
/// </summary>
using System;
using UnityEngine.UI;


public class NetworkManager : MonoBehaviour {

	private string levelInput = "1";
	private bool createAsNarissa;

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

			Destroy(GameObject.FindGameObjectWithTag("Narissa"));
			Destroy(GameObject.FindGameObjectWithTag("Gareth"));
			Destroy(GameObject.Find("_SCRIPTS"));

			Application.LoadLevel("MainMenu");
		}

		if (GameObject.Find ("ToggleNarrisa") != null)
			if (GameObject.Find ("ToggleNarrisa").GetComponent<Toggle> ().isOn) {
				createAsNarissa = true;	
			} else {
				createAsNarissa = false;	
			}
	}

	private RoomInfo[] roomsList;
	
	void OnGUI()
	{
		if (!PhotonNetwork.connected) {
			GUILayout.Label (PhotonNetwork.connectionStateDetailed.ToString ());
		} else if (PhotonNetwork.room == null) {
			// Create Room
			//if (GUI.Button (new Rect (100, 50, 250, 80), "Create New Room"))
			//	PhotonNetwork.CreateRoom ("#" + roomsList.Length, new RoomOptions () {isVisible = true, isOpen = true, maxPlayers = 2}, TypedLobby.Default);
			
			// Join Room
			if (roomsList != null && roomsList.Length != 0) {
				//GUI.Label (new Rect (110, 170, 150, 60), "Open games");

				for (int i = 0; i < roomsList.Length; i++) {
					if (GUI.Button (new Rect (100, 200 + (80 * i), 250, 50), "Join Room " + roomsList [i].name))
						PhotonNetwork.JoinRoom (roomsList [i].name);
				}
			}
		} else {
			GUILayout.Label("Room: " + PhotonNetwork.room.name);
		}
	}

	public void OnCreateRoomClick() {
		if (PhotonNetwork.room == null && PhotonNetwork.insideLobby) {
			// Create Room
			PhotonNetwork.CreateRoom ("#" + roomsList.Length, new RoomOptions () {isVisible = true, isOpen = true, maxPlayers = 2}, TypedLobby.Default);
		}
	}

	public void OnLevelInput(String text) {
		this.levelInput = GameObject.Find("LevelInput").transform.FindChild("Text").GetComponent<Text>().text;
		Debug.Log (levelInput);
	}
	
	void OnReceivedRoomListUpdate()
	{
		roomsList = PhotonNetwork.GetRoomList();
	}

	private GameObject narissa;
	private GameObject gareth; 


	void OnJoinedRoom()
	{

		Application.LoadLevel ("Level0" + levelInput);

		// Spawn player
		GameObject player;

		//Debug.Log(PhotonNetwork.room.playerCount);

		if (PhotonNetwork.room.playerCount == 1) {
			//Debug.LogError("haj");

			// Find the first Checkpoint, else just spawn at Zero
			Vector3 start = GameObject.Find("Checkpoint 1") == null ? (Vector3.up * 5) : GameObject.Find("Checkpoint 1").transform.position;

			if (createAsNarissa) {
				player = PhotonNetwork.Instantiate(narissa.name, start, Quaternion.identity, 0);
				//player.name = "Narissa";
			} else {
				player = PhotonNetwork.Instantiate(gareth.name, start, Quaternion.identity, 0);
				//player.name = "Gareth";
			}

		} else {
			//Debug.Log(GameObject.FindGameObjectWithTag("Gareth").transform + " a");
			if (GameObject.FindGameObjectWithTag("Gareth") == null) {
				//Vector3 start = GameObject.FindGameObjectWithTag("Narissa").transform.position + Vector3.up * 5;
				player = PhotonNetwork.Instantiate(gareth.name, Vector3.up * 5, Quaternion.identity, 0);
				//player.name = "Gareth";
			} else {
				//Vector3 start = GameObject.FindWithTag("Gareth").transform.position + Vector3.up * 5;
				player = PhotonNetwork.Instantiate(narissa.name, Vector3.up * 5, Quaternion.identity, 0);
				//player.name = "Narissa";
			}
		}
		
		player.GetComponent<BasePlayerController> ().enabled = true;
		Camera c = player.transform.FindChild ("Camera").camera;
		c.enabled = true;

		if (player.tag == "Narissa") {
			player.GetComponent<Swap> ().enabled = true;
		}
	
		player.transform.FindChild("Camera").gameObject.SetActive(true);
		player.transform.FindChild ("Camera").GetComponent<AudioListener> ().enabled = true;



		//Application.LoadLevel ("PlaygroundLevel");

	}



}
