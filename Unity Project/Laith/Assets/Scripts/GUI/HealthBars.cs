using UnityEngine;
using System.Collections;

public class HealthBars : MonoBehaviour {
	
	/// <summary>
	/// Shows the player the current health status for both Characters.
	/// Author: Simon J.
	/// </summary>
	
	private Texture2D healthBar;
	private Texture2D redBar;
	public Vector2 healthbarPosition;
	public float healthbarsDistance;
	public Vector2 healthScale;
	public Vector2 offset;
	private GameObject[] playersObject;
	private Actor[] players;
	
	// Use this for initialization
	void Start () {
		FindNewPlayer ();
		healthBar = Resources.Load ("Health_bar") as Texture2D;
		redBar = Resources.Load ("Red") as Texture2D;
	}
	
	//Shows on the Screen for the Player
	void OnGUI(){
		if (players != null) {
			for (int i = 0; i < players.Length; ++i) {
				GUI.DrawTexture (new Rect (healthbarPosition.x, healthbarPosition.y + healthbarsDistance * i, (int)(healthBar.width * healthScale.x), (int)(healthBar.height * healthScale.y)), healthBar,ScaleMode.StretchToFill, false);
				float diff = players [i].currentHealth / players [i].maxHealth;
				Rect damage = new Rect((int)(offset.x * 0.5f * healthScale.x),
				                       (int)(offset.y * 0.5f * healthScale.y),
				                       (int)((healthBar.width - offset.x) * healthScale.x - ((healthBar.width - offset.x) * diff * healthScale.x)),
				                       (int)((healthBar.height - offset.y) * healthScale.y));
				GUI.DrawTexture(new Rect (healthbarPosition.x + damage.x + (int)((healthBar.width - offset.x) * healthScale.x * diff), healthbarPosition.y + healthbarsDistance * i + damage.y, damage.width, damage.height), redBar, ScaleMode.StretchToFill);
			}
		}
	}
	//Finds new Player who joins the Game
	private void FindNewPlayer(){
		if (PhotonNetwork.inRoom) {
			if (players == null || players.Length != PhotonNetwork.room.playerCount) {
				playersObject = Layer.FindGameObjectsWithLayer(Layer.players);
				//playersObject = GameObject.FindGameObjectsWithTag("Player");
				if(playersObject == null){
					return;
				}
				players = new Actor[playersObject.Length];
				for(int i = 0; i < playersObject.Length; ++i){
					Debug.Log(Layer.FindGameObjectsWithLayer(Layer.players).Length);
					players[i] = playersObject[i].GetComponent<Actor> ();
				}
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		FindNewPlayer ();
		
		//DAMAGE TESTER
		/*
		if (players != null) {
			for(int i = 0; i < players.Length; ++i){
				players[i].TakeDamage(1*Time.deltaTime, Vector3.zero);
			}
		}
		*/
	}
}
