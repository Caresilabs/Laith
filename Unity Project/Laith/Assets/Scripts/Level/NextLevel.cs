using UnityEngine;
using System.Collections;

/// <summary>
/// Next level.
/// Author: Simon Bothen
/// </summary>
/// 
public class NextLevel : MonoBehaviour {
	void OnTriggerEnter(Collider hit) {
		if (hit.tag == "Player") {
			int currentLevel =  int.Parse(Application.loadedLevelName.Substring(Application.loadedLevelName.Length - 2, 2 ));
			PhotonNetwork.LoadLevel ("Level0" + (currentLevel+1));
		}
	}
}
