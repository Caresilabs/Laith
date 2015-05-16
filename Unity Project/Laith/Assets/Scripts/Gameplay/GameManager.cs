using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
	/// <summary>
	/// Keeps count of Last Checkpoint and character deadStates
	/// Author: Simon J.
	/// </summary>
	
	private GameObject gareth;
	private GameObject narissa;

	private GameObject lastCheckpoint;
	private int? numberOfPlayers;
	public int? start;

	// Use this for initialization
	//void Awake () {
	void Start(){
		Physics.IgnoreLayerCollision (8, 9);
		Physics.IgnoreLayerCollision (10, 12);
	}

	void OnLevelWasLoaded(int level) {
		Vector3 startPos;

		if (!start.HasValue) {
			startPos = GameObject.Find ("Checkpoint 1") == null ? Vector3.up * 5 : GameObject.Find ("Checkpoint 1").transform.position;
		} else {
			startPos = GameObject.Find ("Checkpoint " + start) == null ? Vector3.up * 5 : GameObject.Find ("Checkpoint " + start).transform.position;//start.transform.position;
		}
		if (Layer.FindGameObjectsWithLayer(Layer.players) != null) {
			GameObject[] players = Layer.FindGameObjectsWithLayer(Layer.players);
			for(int i = 0; i < players.Length; ++i){
				if(players[i].GetComponent<Gareth>() != null){
					gareth = players[i];
				}
				if(players[i].GetComponent<Narissa>() != null){
					narissa = players[i];
				}
				players[i].transform.position = startPos;
			}
		}
	}

	public void SetLastCheckpoint(GameObject checkpoint){
		lastCheckpoint = checkpoint;
	}

	private void DeadState(){

		if (gareth != null && narissa != null) {
			if (gareth.GetComponent<BasePlayerController> ().dead == true &&
			    narissa.GetComponent<BasePlayerController> ().dead == true) {
				narissa.GetComponent<BasePlayerController>().Respawn(lastCheckpoint.transform.position);
				gareth.GetComponent<BasePlayerController>().Respawn(lastCheckpoint.transform.position);
			} else if (gareth.GetComponent<BasePlayerController> ().dead == true) {
				gareth.transform.position = narissa.transform.position;
			} else if (narissa.GetComponent<BasePlayerController> ().dead == true) {
				narissa.transform.position = gareth.transform.position;
			}
		} else if (gareth == null && narissa != null) {
			if(narissa.GetComponent<BasePlayerController> ().dead == true){
				narissa.GetComponent<BasePlayerController>().Respawn(lastCheckpoint.transform.position);
			}
		} else if (narissa == null && gareth != null) {
			if(gareth.GetComponent<BasePlayerController> ().dead == true){
				gareth.GetComponent<BasePlayerController>().Respawn(lastCheckpoint.transform.position);
			}
		}
	}
	//Funkar ej Multiplayer!!
	private void ChangeChar(){
		if(PhotonNetwork.room.playerCount == 1){
			if(Input.GetKeyDown(KeyCode.Tab)){
				GameObject player;
				if(narissa != null){
					GameObject tempGareth = Resources.Load ("Gareth") as GameObject;
					player = PhotonNetwork.Instantiate(tempGareth.name, narissa.transform.position, Quaternion.identity, 0);
					
					player.GetComponent<BasePlayerController> ().enabled = true;
					Camera c = player.transform.FindChild ("Camera").camera;
					c.enabled = true;
					
					player.transform.FindChild("Camera").gameObject.SetActive(true);
					player.transform.FindChild ("Camera").GetComponent<AudioListener> ().enabled = true;
					PhotonNetwork.Destroy(narissa);
					gareth = player;
				} else if(gareth != null){
					GameObject tempNarissa = Resources.Load ("Narissa") as GameObject;
					player = PhotonNetwork.Instantiate(tempNarissa.name, gareth.transform.position, Quaternion.identity, 0);
					
					player.GetComponent<BasePlayerController> ().enabled = true;
					Camera c = player.transform.FindChild ("Camera").camera;
					c.enabled = true;
					
					player.transform.FindChild("Camera").gameObject.SetActive(true);
					player.transform.FindChild ("Camera").GetComponent<AudioListener> ().enabled = true;
					PhotonNetwork.Destroy(gareth);
					narissa = player;
					
				}
			}
		}
		
	}
	private void FindNewPlayer(){
		if (!PhotonNetwork.inRoom) {
			return;
		}
		if (PhotonNetwork.room.playerCount != numberOfPlayers || !numberOfPlayers.HasValue) {
			if (Layer.FindGameObjectsWithLayer(Layer.players) != null) {
				GameObject[] players = Layer.FindGameObjectsWithLayer(Layer.players);
				for(int i = 0; i < players.Length; ++i){
					if(players[i].tag == "Gareth"){
						gareth = players[i];
					}
					if(players[i].tag == "Narissa"){
						narissa = players[i];
					}
				}
				numberOfPlayers = players.Length;
			}
		}
	}
	// Update is called once per frame
	void Update () {
		DeadState ();
		FindNewPlayer ();

		if (PhotonNetwork.inRoom)
			ChangeChar ();
	}
}
