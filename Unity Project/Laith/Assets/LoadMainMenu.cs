using UnityEngine;
using System.Collections;

public class LoadMainMenu : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Application.LoadLevel ("MainMenu");

		GameObject obj = GameObject.Find ("_SCRIPTS");
		obj.GetComponent<NetworkManager> ().enabled = true;
		obj.GetComponent<GameManager> ().enabled = true;
		obj.GetComponent<HealthBars> ().enabled = true;


	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
