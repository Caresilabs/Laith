using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
	
	private GameObject gareth;
	private GameObject narissa;

	private GameObject lastCheckpoint;
	public int? start;

	// Use this for initialization
	//void Awake () {
	void OnLevelWasLoaded(int level) {
		Vector3 startPos;
		if (!start.HasValue) {
			startPos = GameObject.Find ("Checkpoint 1") == null ? Vector3.up * 5 : GameObject.Find ("Checkpoint 1").transform.position;
		} else {
			startPos = GameObject.Find ("Checkpoint " + start) == null ? Vector3.up * 5 : GameObject.Find ("Checkpoint " + start).transform.position;//start.transform.position;
		}
		if (GameObject.Find ("Gareth") != null) {
			gareth = GameObject.Find ("Gareth");
			gareth.transform.position = startPos;
		}
		if (GameObject.Find ("Narissa") != null) {
			narissa = GameObject.Find ("Narissa");
			narissa.transform.position = startPos;
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
					player.transform.name = "Gareth";
					
					player.GetComponent<BasePlayerController> ().enabled = true;
					Camera c = player.transform.FindChild ("Camera").camera;
					c.enabled = true;
					
					player.transform.FindChild("Camera").gameObject.SetActive(true);
					player.transform.FindChild ("Camera").GetComponent<AudioListener> ().enabled = true;
					PhotonNetwork.Destroy(narissa);
					gareth = GameObject.Find ("Gareth");
				} else if(gareth != null){
					GameObject tempNarissa = Resources.Load ("Narissa") as GameObject;
					player = PhotonNetwork.Instantiate(tempNarissa.name, gareth.transform.position, Quaternion.identity, 0);
					player.transform.name = "Narissa";
					
					player.GetComponent<BasePlayerController> ().enabled = true;
					Camera c = player.transform.FindChild ("Camera").camera;
					c.enabled = true;
					
					player.transform.FindChild("Camera").gameObject.SetActive(true);
					player.transform.FindChild ("Camera").GetComponent<AudioListener> ().enabled = true;
					PhotonNetwork.Destroy(gareth);
					narissa = GameObject.Find ("Narissa");
					
				}
			}
		}
		
	}
	// Update is called once per frame
	void Update () {
		DeadState ();

		if (PhotonNetwork.inRoom)
			ChangeChar ();
	}
}
