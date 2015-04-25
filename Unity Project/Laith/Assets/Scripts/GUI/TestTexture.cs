using UnityEngine;
using System.Collections;

public class TestTexture : MonoBehaviour {

	private Texture2D healthBar;
	private GameObject playerObject;
	private Actor player;
	// Use this for initialization
	void Start () {
		healthBar = Resources.Load ("Health_bar") as Texture2D;
		playerObject = GameObject.FindGameObjectWithTag ("Player");
		player = playerObject.GetComponent<Actor> ();
	}
	void OnGUI(){
		for (int i = 0; i < 2; ++i) {
			if(player != null)
				GUI.DrawTexture (new Rect (0, 100, (int)(healthBar.width * 0.5f), (int)(healthBar.height * 0.5f)), healthBar,ScaleMode.StretchToFill, false, 2.0f);
		}
	}
	private void FindPlayer(){
		if (player == null) {
			playerObject = GameObject.FindGameObjectWithTag ("Player");
			player = playerObject.GetComponent<Actor> ();
		}
	}
		
	// Update is called once per frame
	void Update () {
		FindPlayer ();
		
		if (player != null) {
			player.TakeDamage(1*Time.deltaTime, Vector3.zero);
		}
	}
}
