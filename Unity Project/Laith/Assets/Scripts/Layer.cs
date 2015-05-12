using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Layer : MonoBehaviour {

	public static int players = 8;
	public static GameObject[] FindGameObjectsWithLayer (int layer)  {
		GameObject[] goArray = (GameObject[])GameObject.FindObjectsOfType(typeof(GameObject));
		List<GameObject> goList = new List<GameObject>();
		for (int i = 0; i < goArray.Length; ++i) {
			if (goArray[i].layer == layer && goArray[i].transform.parent == null) {
				goList.Add(goArray[i]);
			}
		}

		if (goList.Count == 0) {
			return null;
		}
		return goList.ToArray();
	}
}
