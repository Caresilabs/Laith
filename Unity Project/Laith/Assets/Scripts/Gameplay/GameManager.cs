using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
	
	public GameObject gareth;
	public GameObject narissa;
	public GameObject lastCheckpoint;
	// Use this for initialization
	void Start () {
	
	}
	public void SetLastCheckpoint(GameObject checkpoint){
		lastCheckpoint = checkpoint;
	}
	private void DeadState(){
		if (GameObject.Find ("Gareth(Clone)") != null) {
			gareth = GameObject.Find ("Gareth(Clone)");
		}
		if (GameObject.Find ("Narissa(Clone)") != null) {
			narissa = GameObject.Find ("Narissa(Clone)");
		}
		if (gareth != null && narissa != null) {
			if (gareth.GetComponent<BasePlayerController> ().dead == true &&
			    narissa.GetComponent<BasePlayerController> ().dead == true) {
				narissa.GetComponent<BasePlayerController>().respawn(lastCheckpoint.transform.position);
				gareth.GetComponent<BasePlayerController>().respawn(lastCheckpoint.transform.position);
			} else if (gareth.GetComponent<BasePlayerController> ().dead == true) {
				gareth.transform.position = narissa.transform.position;
			} else if (narissa.GetComponent<BasePlayerController> ().dead == true) {
				narissa.transform.position = gareth.transform.position;
			}
		} else if (gareth == null && narissa != null) {
			if(narissa.GetComponent<BasePlayerController> ().dead == true){
				narissa.GetComponent<BasePlayerController>().respawn(lastCheckpoint.transform.position);
			}
		} else if (narissa == null && gareth != null) {
			if(gareth.GetComponent<BasePlayerController> ().dead == true){
				gareth.GetComponent<BasePlayerController>().respawn(lastCheckpoint.transform.position);
			}
		}
	}
	// Update is called once per frame
	void Update () {
		DeadState ();
	}
}
